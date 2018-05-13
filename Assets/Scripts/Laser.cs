using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{


	public Vector3 rotationCenter = Vector3.zero;
	public float startingRotation = 30f;
	public float startingRadius = 4f;
	public float rotationSpeed = 1f;
	public float targetY = 5f;

	public float explosionForce = 10f;
	public float explosionRadius =1f;

	[SerializeField]
    public AnimationCurve dropCurve = AnimationCurve.EaseInOut(0, 0.2f, 1f, 2f);
	[SerializeField]
	public AnimationCurve radiusSpeedCurve = AnimationCurve.EaseInOut(0, 10f, 1f, 15f);
	public Vector2 radiusLimits = new Vector2(2, 20);

	private bool isDropping = true;
	public float radiusDirection = -1;
    // Update is called once per frame

    public GameObject sparks;
    public GameObject particleOnGrounded;


	private void Awake()
	{
		transform.position =new Vector3((Vector3.forward * startingRadius).x, this.transform.position.y, (Vector3.forward * startingRadius).z) ;
		transform.RotateAround(rotationCenter, Vector3.up, startingRotation );


	}

	void Update()
    {

		if (isDropping)
		{

            if (this.transform.position.y - targetY > 0.1)
			{
                
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-this.dropCurve.Evaluate(Mathf.Abs(this.transform.position.y - targetY))*Time.deltaTime, this.transform.position.z);
   
	        }
			else
			{
				isDropping = false;
				this.transform.position = new Vector3(this.transform.position.x, targetY + 3, this.transform.position.z);

                // is Grounded ;)

                sparks.SetActive(true);

			}
            return;
		}

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
			player.GetComponent<Rigidbody>().AddExplosionForce(this.explosionForce, new Vector3(this.transform.position.x, other.transform.position.y, this.transform.position.z), this.explosionRadius);
			player.Stun();
		}
	}
}
