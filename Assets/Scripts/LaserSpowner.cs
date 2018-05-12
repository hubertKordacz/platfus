using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpowner : MonoBehaviour {



	public List<GameObject> laserPrefabList = new List<GameObject>();

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

		if(Time.time >= this.nextSpawnTS && laserPrefabList.Count!=0)
		{
			this.Spawn(laserPrefabList[0]);
			laserPrefabList.RemoveAt(0);
			this.CalculateNextSpownTS();
		}
	}

	private void Spawn(GameObject orignal)
	{
		GameObject instance= 	Instantiate(orignal.gameObject, this.transform.position, this.transform.rotation);
		this.spownedCount++;

	}

	private void CalculateNextSpownTS()
    {
		this.nextSpawnTS =this.nextSpawnTS+ this.spownRate.Evaluate(this.spownedCount);
    }
}
