using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{


	public float explosionRadius = 10;
	public float explosionForce = 100;
	private bool isOnGround = false;
    // Use this for initialization
    void Start()
    {

    }

   
	private void OnCollisionEnter(Collision collision)
    {

		if (!isOnGround)
		{
			OnGroundCollision();

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

		Collider[] overlaped= 	Physics.OverlapSphere(this.transform.position, explosionRadius);

		foreach (Collider colider  in overlaped)
		{
			Rigidbody rb = colider.GetComponent<Rigidbody>();
            
			if(rb!=null)
			{            
				rb.AddExplosionForce(this.explosionForce, this.transform.position, this.explosionRadius);
                Debug.Log(rb.gameObject);
			}
			//if(rigidbody.gameObject.tag("ground"))
		}

	}
}
