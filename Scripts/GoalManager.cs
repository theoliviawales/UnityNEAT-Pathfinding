using UnityEngine;
using System.Collections;

public class GoalManager : MonoBehaviour {

	GameObject goal;
	// Use this for initialization
	void Start () {
		goal = GameObject.Find ("Goal");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			goal.transform.position = new Vector3 (2.59f, -3.53f, 0f);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			goal.transform.position = new Vector3 (-1.23f, 0f, 0f);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			goal.transform.position = new Vector3 (6.55f, -3.2f, 0f);
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			goal.transform.position = new Vector3 (-4.82f, -2f, 0f);
		}
		if (Input.GetKeyDown (KeyCode.T)) {
			goal.transform.position = new Vector3 (8f, 2.7f, 0f);
		}
	}
}
