using UnityEngine;
using System.Collections;

public class FillMap : MonoBehaviour {

	int[] distances;
	public Rigidbody node;
	// Use this for initialization
	void Start () {
		
		Vector3 startPos = transform.position;
		for (int i = 0; i < 43; i++) {
			for (int j = 0; j < 21; j++) {
				Rigidbody newNode = (Rigidbody)Instantiate (node, startPos + new Vector3 (.5f * i, .5f * j, startPos.z + 1), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
