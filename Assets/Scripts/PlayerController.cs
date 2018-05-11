using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotationSpeed;

    Rigidbody _rigidbody;
    Collider _collider;

    public Rigidbody Rigidbody { get { { return _rigidbody; } } }
    public Collider Collider { get { { return _collider; } } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Move(move);

        if (_rigidbody.velocity.x != 0 && _rigidbody.velocity.y != 0)
        {
            //transform.eulerAngles = 
            Debug.Log(_rigidbody.velocity);
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }

    void Move(Vector3 force)
    {
        _rigidbody.AddForce(force * moveSpeed);


    }
}
