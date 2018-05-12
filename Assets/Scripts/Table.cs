using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour
{


	public float hitForce = 10f;
	// Use this for initialization

	public Vector3 overlapSize = Vector3.one;
	public Vector3 overlapOffset= Vector3.zero;
	void Start()
	{

	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(this.transform.position + this.overlapOffset, this.overlapSize);
	}

	// Update is called once per frame
	void Update()
	{
		if (Random.value < 0.02)
		{
			StartCoroutine(HitWave());
		}
	}
    


    private IEnumerator HitWave()
    {
		Hit(hitForce);
        yield return new WaitForSeconds(0.2f);
		Hit(hitForce/2);
		yield return new WaitForSeconds(0.1f);
        Hit(hitForce / 3);

		yield return new WaitForSeconds(0.05f);
        Hit(hitForce / 5);
      
    }
	public void Hit(float force)
	{
		Collider[] overlaped = Physics.OverlapBox(this.transform.position + this.overlapOffset, this.overlapSize * 0.5f);


		Debug.Log(overlaped.Length);
		foreach (Collider colider in overlaped)
        {
            Rigidbody rb = colider.GetComponent<Rigidbody>();

            if (rb != null)
            {
				rb.AddForceAtPosition(Vector3.up * force, colider.transform.position + new Vector3(Random.Range(-0.1f,0.1f),0,Random.Range(-0.1f, 0.1f)));

            }
      
        }

	}
}
