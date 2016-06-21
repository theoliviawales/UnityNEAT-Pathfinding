using UnityEngine;
using System.Collections;

public class Astar : MonoBehaviour {

	public Transform player;
	private Transform selectedNode;
	private Collider[] nearbyNodes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		nearbyNodes = player.GetComponent<AdjacentAgent> ().getNearbyNodes ();
		selectedNode = player.GetComponent<RaycastMouse> ().getSelectedNode ();
		float[] heuristics = new float[nearbyNodes.Length], nodeCosts, totalCosts;

		if (selectedNode != null) {
			for (int i = 0; i < nearbyNodes.Length; i++) {
						
			}
		}
	}
}
