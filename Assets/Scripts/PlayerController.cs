using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public LayerMask hitLayer;
    public BoxCollider hitCollider;
    public float moveSpeed = 50;
    public float rotationSpeed = 500;
    public float minChargeForce = 50, maxChargeForce = 500;
    public float hitMultiplier = 2f;

    [SerializeField]
    Animator _animator;
    PlayerInput playerInput;
    Rigidbody _rigidbody;
    Collider _collider;
    float _chargeForceTimer;
    bool _isCharging;

    public Rigidbody Rigidbody { get { { return _rigidbody; } } }
    public Collider Collider { get { { return _collider; } } }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = transform.GetChild(0).GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        if (_isCharging)
        {
            if (_rigidbody.velocity.x < 5 && _rigidbody.velocity.z < 5)
            {
                _chargeForceTimer = 0;
                _isCharging = false;
            }
            return;
        }


        Move();
        Rotate();
    }

    private void Update()
    {
        Charge();
    }

    void Move()
    {
        var move = new Vector3(playerInput.GetAxis(PlayerInput.InputActions.Horizontal), 0, playerInput.GetAxis(PlayerInput.InputActions.Vertical));
        var lerp = 1 - _chargeForceTimer;
        _rigidbody.AddForce(move * moveSpeed * lerp);

        var magnitude = move.magnitude;
        _animator.SetFloat("Move", magnitude);
    }

    void Rotate()
    {
        var move = new Vector3(playerInput.GetAxis(PlayerInput.InputActions.Horizontal), 0, playerInput.GetAxis(PlayerInput.InputActions.Vertical));
        if (move == Vector3.zero) return;

        Quaternion wantedRotation = Quaternion.LookRotation(move);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, wantedRotation, rotationSpeed * Time.deltaTime);
    }

    void Charge()
    {
        if (_isCharging)
        {
            var hit = Physics.OverlapBox(hitCollider.transform.position, hitCollider.size * 0.5f, hitCollider.transform.rotation, hitLayer);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i] == _collider) continue;

                var force = _rigidbody.velocity * hitMultiplier;
                hit[i].transform.parent.GetComponent<PlayerController>().Push(force);

                Push(transform.forward * -5);
                _chargeForceTimer = 0;
                _isCharging = false;
            }
            return;
        }

        if (playerInput.GetButtonUp(PlayerInput.InputActions.Attack))
        {
            _isCharging = true;

            var lerp = _chargeForceTimer;
            var force = Mathf.Lerp(minChargeForce, maxChargeForce, lerp);
            _rigidbody.velocity = transform.forward * force;

            return;
        }

        if (playerInput.GetButton(PlayerInput.InputActions.Attack))
        {
            _chargeForceTimer += Time.deltaTime;
            _chargeForceTimer = Mathf.Clamp(_chargeForceTimer, 0, 1);
        }
    }

    public void Push(Vector3 force)
    {
        _rigidbody.isKinematic = true;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.isKinematic = false;

        _rigidbody.velocity = force;
    }
}
