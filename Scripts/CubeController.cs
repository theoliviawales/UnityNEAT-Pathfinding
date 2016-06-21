using UnityEngine;
using System.Collections;
using SharpNeat.Phenomes;
using System;

public class CubeController : UnitController {

	bool IsRunning;
	IBlackBox box;
	private GameObject goal, start;
	private Vector3 startPos, currentPos;
	private int wallHits = 0, gridDistance = 0;
	public float Speed = 5f, TurnSpeed = 1f, rangefinderDist = 10f;
	private float startTime, endTime = 0, totalRangeSensed, maxDistance, elapsedTime = 0, distanceFromPrev;
	private bool ingoal = false;
	public bool TEST = false;

	void Start()
	{
		goal = GameObject.Find ("Goal");
		start = GameObject.Find ("Start");

		startTime = Time.time;
		//startPos = transform.position;

		if (TEST) {
			if (goal.transform.position.y == -3.53f)
				startPos = new Vector3 (-8.48f, 0f, 0f);
			//startPos = new Vector3(-2.5f, -0.81f, 0f);
			if (goal.transform.position.y == 0f)
				startPos = new Vector3 (7.11f, -3.6f, 0f);
				//startPos = new Vector3(3.74f, 0.08f, 0f);
			if (goal.transform.position.y == -3.2f)
				startPos = new Vector3 (-3.15f, 1.5f, 0f);

			if (goal.transform.position.y == -2f)
				startPos = new Vector3 (5.9f, 3.6f, 0f);

			if (goal.transform.position.y == -3.71f)
				startPos = new Vector3 (-5.49f, -2.77f, 0f);

			if (goal.transform.position.y == 2.7f)
				startPos = new Vector3 (-6f, -3f, 0f);


			//transform.position = startPos;
		} 

		else 
		{
			//transform.position = start.transform.position;
			startPos = start.transform.position;
		}

		currentPos = startPos;
	}
	void FixedUpdate() 
	{
		Speed = 0.1f;

		Vector3 toVector = goal.transform.position - transform.position;
		float currentAngle = transform.eulerAngles.z;
		transform.Translate (toVector * Speed);
		//transform.position = new Vector3 (transform.position.x + Speed * -Mathf.Sin (Mathf.Deg2Rad * currentAngle), 
		//	transform.position.y + Speed * Mathf.Cos (Mathf.Deg2Rad * currentAngle), 0);
//		elapsedTime += Time.deltaTime;
//		if (elapsedTime > 1) {
//			distanceFromPrev += Vector3.Distance (transform.position, currentPos);
//			currentPos = transform.position;
//			elapsedTime = 0;
//		}
//		if (IsRunning) 
//		{
////			Collider[] adjNodes = transform.GetComponent<AdjacentAgent>	().getNearbyNodes ();
////			maxDistance = float.MaxValue;
////			for (int i = 0; i < adjNodes.Length; i++) {
////				if (adjNodes [i].transform.tag != "Cube") {
////					float distance = Vector3.Distance (adjNodes [i].transform.position, goal.transform.position);
////					if (distance < maxDistance) {
////						maxDistance = distance;
////					}
////				}
////			}
//			float[] rangeFinders = transform.GetComponent<WallSense> ().getSensors ();
//			Vector3 toVector = goal.transform.position - transform.position;
//			float angleToTarget = Vector3.Angle(transform.up, toVector);
//
//			ISignalArray inputArr = box.InputSignalArray;
//			//inputArr[0] = Vector3.Distance(transform.position, goal.transform.position); //Distance from goal
//			inputArr [0] = angleToTarget;
//			inputArr[1] = Convert.ToInt32(transform.GetComponent<AdjacentAgent>().goalInRange());
//			inputArr [2] = rangeFinders [0];
//			inputArr [3] = rangeFinders [1];
//			inputArr [4] = rangeFinders [2];
//			box.Activate ();
//
//			//Debug.Log ("In range: " + Convert.ToInt32 (transform.GetComponent<AdjacentAgent> ().goalInRange ()));
//
//			ISignalArray outputArr = box.OutputSignalArray;
//
//			checkGoal ();
//			if (!ingoal) {
////				if (transform.GetComponent<AdjacentAgent> ().goalInRange ()) {
////					transform.LookAt (goal.transform);
////					float currentAngle = transform.eulerAngles.z;
////					transform.position = new Vector3 (transform.position.x + Speed * -Mathf.Sin (Mathf.Deg2Rad * currentAngle), 
////						transform.position.y + Speed * Mathf.Cos (Mathf.Deg2Rad * currentAngle), 0);
////					
////				} else {
////					
//					var steer = (float)outputArr[0] - 1;
//					//var gas = (float)outputArr[1] * 2 - 1;
//
//					var moveDist = Speed * Time.deltaTime;
//				var turnAngle = steer * TurnSpeed * Time.deltaTime;
//					
//				//Debug.Log ("Steer: " + steer);
//					transform.Rotate(new Vector3(0, 0, turnAngle));
//					transform.Translate(Vector3.up * moveDist);
//				//}
//			}
//
//			if (ingoal) {
//				if (endTime == 0)
//					endTime = Time.time;
//			}
//				
//			totalRangeSensed += rangeFinders [0] + rangeFinders [1] + rangeFinders [2];
//		}
	}

	public override void Stop()
	{
		this.IsRunning = false;
	}

	public override void Activate(IBlackBox box)
	{
		this.box = box;
		this.IsRunning = true;
	}

	void checkGoal()
	{
		Collider[] nearbyNodes = Physics.OverlapSphere (transform.position, 1f);

		for (int i = 0; i < nearbyNodes.Length; i++) {
			if (nearbyNodes [i].tag == "goal") {
				ingoal = true;
				break;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		//Debug.Log ("Hit wall");
		if (collision.transform.tag == "Wall")
			wallHits++;
		
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform.tag == "Grid") {
			gridDistance = collision.transform.GetComponent<Distance> ().getDistance ();
		}
	}

	public override float GetFitness()
	{
//		RaycastHit2D[] hits;
//		LayerMask lm = 1 << 9;
//		hits = Physics2D.RaycastAll (transform.position, goal.transform.position, 1000f, lm); 
//
		Debug.Log ("Distance: " + distanceFromPrev);
		//Debug.Log ("Start: " + startTime + " End: " + Time.time);
		if (ingoal)
			return 200 + (20 - (endTime - startTime)) - 3 * wallHits;
		float fit = 20 - Vector3.Distance (transform.position, goal.transform.position) - 2 * wallHits; //+ totalRangeSensed / 1000 + Vector3.Distance (startPos, transform.position);
		//Debug.Log("Dist: " + Vector3.Distance(transform.position, goal.transform.position));
		if (fit < 0 || maxDistance > 100)
			return 0;
			
		return fit;
	}
}
