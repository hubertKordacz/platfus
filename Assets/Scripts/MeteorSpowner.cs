using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpowner : MonoBehaviour {



	public Rigidbody meteorPrefab;

	[SerializeField]
	public AnimationCurve spownRate = AnimationCurve.Linear(0,1.0f,10.0f,2.0f);

	private float nextSpawnTS;
	private float spownedCount = 0;
	// Update is called once per frame
	public float speed = 10f;
	private void Awake()
	{
		nextSpawnTS = Time.time;
		this.CalculateNextSpownTS();
	}


	void Update () {

		if(Time.time >= this.nextSpawnTS)
		{
			this.Spawn(this.meteorPrefab);
			this.CalculateNextSpownTS();
		}
	}

	private void Spawn(Rigidbody orignal)
	{
		GameObject instance= 	Instantiate(orignal.gameObject, this.transform.position, this.transform.rotation);
		instance.transform.position += this.transform.up * this.transform.localScale.y/2f * Random.Range(-1f,1f) ;
		instance.transform.position += this.transform.right * this.transform.localScale.x/2f * Random.Range(-1f,1f) ;
		instance.GetComponent<Rigidbody>().velocity = instance.transform.forward * this.speed;
		this.spownedCount++;
	}

	private void CalculateNextSpownTS()
    {
		this.nextSpawnTS =this.nextSpawnTS+ this.spownRate.Evaluate(this.spownedCount);
    }
}
