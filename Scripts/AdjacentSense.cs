using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdjacentSense : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Gets objects within range of Adj Agent Sensor
		Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, 3.2f);

        // Loop through colliders in range of the player
		for (int i = 0; i < collidersInRange.Length; i++) {
			if (collidersInRange[i].attachedRigidbody != null)  // Check if this is a rigid body
			{
				collidersInRange [i].gameObject.GetComponent<Renderer> ().material.color = Color.cyan;
			}
		}

	}
}
