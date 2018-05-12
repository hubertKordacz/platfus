using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterT : MonoBehaviour {

	public float time = 10f;
	// Update is called once per frame

	private float destryTS;
	private void Awake()
	{
		this.destryTS = Time.time + time;
	}

	void Update () {


		if (Time.time> this.destryTS)
			Destroy(this.gameObject);
	}


}
