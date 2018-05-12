using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBelowY : MonoBehaviour {

	public float Yvalue =-100f;
	// Update is called once per frame
	void Update () {


		if (this.transform.position.y < Yvalue)
			Destroy(this.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Projector projector = this.GetComponent<Projector>();

		if(projector!=null)
		{
			Destroy(projector);

		}
	}
}
