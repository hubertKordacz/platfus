using UnityEngine;
using System.Collections;

public class Disc : MonoBehaviour
{
	public Vector3 rotationCenter = Vector3.zero;
    public float rotationSpeed = 12;

    // Update is called once per frame
	void Update()
    {
		transform.RotateAround(rotationCenter, Vector3.up, rotationSpeed * Time.deltaTime);
        
    }
}
