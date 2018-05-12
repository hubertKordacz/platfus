using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50;
    public float rotationSpeed = 500;

    public float minChargeForce = 50, maxChargeForce = 500;

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
        _collider = GetComponentInChildren<Collider>();
    }

    private void FixedUpdate()
    {
        if (_isCharging)
        {
            if (_rigidbody.velocity.x < 5 && _rigidbody.velocity.z < 5)
                _isCharging = false;
            return;
        }

        Move();
        Rotate();
        Charge();
    }

    void Move()
    {
        var move = new Vector3(playerInput.GetAxis(PlayerInput.InputActions.Horizontal), 0, playerInput.GetAxis(PlayerInput.InputActions.Vertical));
        var lerp = 1 - _chargeForceTimer;
        _rigidbody.AddForce(move * moveSpeed * lerp);
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
        if (playerInput.GetButtonUp(PlayerInput.InputActions.Attack))
        {
            _isCharging = true;

            var lerp = _chargeForceTimer;
            var force = Mathf.Lerp(minChargeForce, maxChargeForce, lerp);
            _rigidbody.velocity = transform.forward * force;

            _chargeForceTimer = 0;
            return;
        }

        if (playerInput.GetButton(PlayerInput.InputActions.Attack))
        {
            _chargeForceTimer += Time.deltaTime;
            _chargeForceTimer = Mathf.Clamp(_chargeForceTimer, 0, 1);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
