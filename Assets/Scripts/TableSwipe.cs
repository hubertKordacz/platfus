using UnityEngine;
using System.Collections;

public class TableSwipe : MonoBehaviour
{
	// Use this for initialization

	public float time =  1f;
	private Vector3 startPosition = Vector3.zero;
	public Vector3 targetPosition = Vector3.zero;
	[SerializeField]
	public AnimationCurve positionOverTime = AnimationCurve.Linear(0,1f, 1f,1f);

	private Quaternion startRotation = Quaternion.identity;
	public Quaternion targetRotation = Quaternion.identity;
	[SerializeField]
	public AnimationCurve rotationOverTime = AnimationCurve.Linear(0, 1f, 1f, 1f);
    
	private float startTime;
	private bool isPlaying;

    void Start()
    {
		this.startPosition = this.transform.position;
        this.startRotation = this.transform.rotation;
    }

    public void Swipe()
	{
		if (isPlaying)
			return;


		Debug.Log("swipe");
		this.isPlaying = true;
		this.transform.position=this.startPosition;
		this.transform.rotation=this.startRotation;
		this.startTime = Time.time;
	}

	// Update is called once per frame
	private void FixedUpdate()
	{

		if (!isPlaying)
			return;
		float t = (Time.time - this.startTime) / this.time;

        if(t>1f)
		{
			isPlaying = false;
			return;
				
		}
		this.transform.position = Vector3.Lerp(this.startPosition, this.targetPosition, t * this.positionOverTime.Evaluate(t));
		this.transform.rotation = Quaternion.Lerp(this.startRotation, this.targetRotation, t*this.rotationOverTime.Evaluate(t)  );
	}
 
}
