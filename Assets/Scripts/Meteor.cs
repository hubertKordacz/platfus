﻿using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{


	public float explosionRadius = 10;
	public float explosionForce = 100;
	private bool isOnGround = false;
    public float massOnGrounded;

    public GameObject marker;

    public GameObject particleOnGrounded;
    public GameObject fallingAura;

    public AudioSource explodeSound;
    // Use this for initialization
    void Start()
    {
        explodeSound = GetComponent<AudioSource>();
    }

    void Update ()
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (marker && Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity,LayerMask.GetMask("Ground")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            marker.transform.position = hit.point + new Vector3(0,0.3f,0);
            marker.transform.rotation = hit.collider.transform.rotation;
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            
        }
    }

   
	private void OnCollisionEnter(Collision collision)
    {

		if (!isOnGround)
		{
			OnGroundCollision();
            explodeSound.Play();
            Destroy(marker);
            Destroy(fallingAura);

        }
	}

	private void OnGroundCollision()
	{
		isOnGround = true;
		Projector projector = this.GetComponent<Projector>();

		if (projector != null)
		{
			Destroy(projector);

		}
		this.GetComponent<Rigidbody>().useGravity = true;

        this.GetComponent<Rigidbody>().mass = massOnGrounded;
        Instantiate(particleOnGrounded,transform.position, transform.rotation);

        Collider[] overlaped= 	Physics.OverlapSphere(this.transform.position, explosionRadius);

		foreach (Collider colider  in overlaped)
		{
			Rigidbody rb = colider.GetComponent<Rigidbody>();
            
			if(rb!=null)
			{            
				rb.AddExplosionForce(this.explosionForce, this.transform.position, this.explosionRadius);
                
			}
			//if(rigidbody.gameObject.tag("ground"))
		}

	}
}
