using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class Table : MonoBehaviour
{


	public float hitForce = 10f;
	// Use this for initialization

	public Vector3 overlapSize = Vector3.one;
	public Vector3 overlapOffset= Vector3.zero;
    
	public float hitToSwipeRatio=0.7f;
	public List<TableSwipe> swipeAnims = new List<TableSwipe>();
	public List<GameObject> swipeMarkers = new List<GameObject>();

	[SerializeField]
    public AnimationCurve spownRate = AnimationCurve.Linear(0, 1.0f, 10.0f, 2.0f);

  
	  public TableSwipe hit;
	private float nextSpawnTS;
    private float spownedCount = 0;
	private bool inProgress = false;
	private void Awake()
    {
        nextSpawnTS = Time.time;
        this.CalculateNextSpownTS();
    }


    void Update()
    {

		if (Time.time >= this.nextSpawnTS && !inProgress)
        {
			this.inProgress = true;


            this.spownedCount++;

			if (Random.value < this.hitToSwipeRatio)
				DoSwipe();
			else

				StartCoroutine(HitWave());

         
        }
    }

    
	private  void DoSwipe()
	{

		StartCoroutine(Swipe(Random.Range(0, this.swipeAnims.Count)));
	}
   

	private IEnumerator Swipe(int index)
    {

		Debug.Log("swipe "+ index);

		GameObject marker = swipeMarkers[index];
		TableSwipe swipe = swipeAnims[index];


		marker.gameObject.SetActive(true);
		yield return new WaitForSeconds(swipe.time/3);
		marker.gameObject.SetActive(false);
		yield return new WaitForSeconds(swipe.time / 4);
		marker.gameObject.SetActive(true);
		yield return new WaitForSeconds(swipe.time / 5);
        marker.gameObject.SetActive(false);
		yield return new WaitForSeconds(swipe.time / 6);

		marker.gameObject.SetActive(true);
        yield return new WaitForSeconds(swipe.time / 7);
        marker.gameObject.SetActive(false);
        yield return new WaitForSeconds(swipe.time / 8);
		marker.gameObject.SetActive(true);
        yield return new WaitForSeconds(swipe.time / 9);
        marker.gameObject.SetActive(false);
        yield return new WaitForSeconds(swipe.time / 10);


		swipe.Swipe();
		marker.gameObject.SetActive(true);
		yield return new WaitForSeconds(swipe.time / 10);
        marker.gameObject.SetActive(false);
		yield return new WaitForSeconds(swipe.time / 10);
		marker.gameObject.SetActive(true);
        yield return new WaitForSeconds(swipe.time / 10);
        marker.gameObject.SetActive(false);
        yield return new WaitForSeconds(swipe.time / 10);
		marker.gameObject.SetActive(true);
        yield return new WaitForSeconds(swipe.time / 10);
        marker.gameObject.SetActive(false);
        yield return new WaitForSeconds(swipe.time / 10);
  
		this.CalculateNextSpownTS();
        
    }


    private void CalculateNextSpownTS()
    {
        this.nextSpawnTS = this.nextSpawnTS + this.spownRate.Evaluate(this.spownedCount);

		this.inProgress = false;
    }
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(this.transform.position + this.overlapOffset, this.overlapSize);
	}
 


    private IEnumerator HitWave()
    {      
		this.hit.Swipe();
		yield return new WaitForSeconds(this.hit.time);
		Hit(hitForce);  

		this.CalculateNextSpownTS();
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
				rb.AddForceAtPosition(Vector3.up * force, colider.transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f)));

            }
      
        }

	}
}
