using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdjacentAgent : MonoBehaviour {


	public float sphereRadius = 3.2f;
	private Collider[] collidersInRange;
	private bool goalIn = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

		//Gets objects within range of Adj Agent Sensor
		collidersInRange = Physics.OverlapSphere(transform.position, sphereRadius);
		goalIn = false;
		// Loop through colliders in range of the player
		for (int i = 0; i < collidersInRange.Length; i++) {
			if (collidersInRange[i].attachedRigidbody != null)  // Check if this is a rigid body
			{
				collidersInRange [i].attachedRigidbody.gameObject.GetComponent<Renderer> ().material.color = Color.red;
				if (collidersInRange [i].transform.tag == "goal")
					goalIn = true;
			}
		}
	}

	public Collider[] getNearbyNodes ()
	{
		return collidersInRange;
	}

	public bool goalInRange()
	{
		return goalIn;
	}
}
