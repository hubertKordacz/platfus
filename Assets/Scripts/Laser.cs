using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{


	public Vector3 rotationCenter = Vector3.zero;
	public float startingRotation = 30;
	public float startingRadius = 4;
	public float rotationSpeed = 1;
	[SerializeField]
	public AnimationCurve radiusSpeedCurve = AnimationCurve.EaseInOut(0, 10f, 1f, 15f);
	public Vector2 radiusLimits = new Vector2(2, 20);
    
	public float radiusDirection = -1;
	// Update is called once per frame


	private void Awake()
	{
		transform.position =new Vector3((Vector3.forward * startingRadius).x, this.transform.position.y, (Vector3.forward * startingRadius).z) ;
		transform.RotateAround(rotationCenter, Vector3.up, startingRotation );


	}

	void Update()
    {      
	
		transform.RotateAround(rotationCenter, Vector3.up, rotationSpeed * Time.deltaTime);


        Vector3 direction = rotationCenter - new Vector3(this.transform.position.x, 0, this.transform.position.z);
        float radius = direction.magnitude;


        if (radius <= radiusLimits.x)
        {
            radius = radiusLimits.x;
            radiusDirection *= -1f;
        }

        if (radius >= radiusLimits.y)
        {
            radius = radiusLimits.y;
            radiusDirection *= -1f;
        }


		transform.position += radiusDirection * direction.normalized * radiusSpeedCurve.Evaluate((radius - radiusLimits.x) / (radiusLimits.y - radiusLimits.x))* Time.deltaTime;;


    }
	private void OnTriggerEnter(Collider other)
	{

		PlayerController player = other.GetComponent<PlayerController>();
		if(player!=null)
		{

			//player.stun();
		}
	}
}
