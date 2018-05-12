using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50;
    public float rotationSpeed = 500;

    public float minChargeForce = 50, maxChargeForce = 500;

    Rigidbody _rigidbody;
    Collider _collider;
    float _chargeForceTimer;
    bool _isCharging;

    public Rigidbody Rigidbody { get { { return _rigidbody; } } }
    public Collider Collider { get { { return _collider; } } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        if (_isCharging)
        {
            if (_rigidbody.velocity == Vector3.zero)
                _isCharging = false;
            return;
        }

        Move();
        Rotate();
        Charge();
    }

    void Move()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        var lerp = 1 - _chargeForceTimer;
        _rigidbody.AddForce(move * moveSpeed * lerp);
    }

    void Rotate()
    {
        if (_rigidbody.velocity == Vector3.zero) return;

        Quaternion wantedRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, wantedRotation, rotationSpeed * Time.deltaTime);
    }

    void Charge()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isCharging = true;

            var lerp = _chargeForceTimer;
            var force = Mathf.Lerp(minChargeForce, maxChargeForce, lerp);
            _rigidbody.velocity = transform.forward * force;

            _chargeForceTimer = 0;
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _chargeForceTimer += Time.deltaTime;
            _chargeForceTimer = Mathf.Clamp(_chargeForceTimer, 0, 1);
        }
    }
}
