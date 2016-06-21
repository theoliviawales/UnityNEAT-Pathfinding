using UnityEngine;
using System.Collections;

public class RaycastMouse : MonoBehaviour {

	private Transform selectedNode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 nodePos;

		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit)) 
			{
				if (hit.transform.tag == "node")
				{
					hit.transform.gameObject.GetComponent<Renderer> ().material.color = Color.green;
					selectedNode = hit.transform;
				}
			}
		}
	}

	public Transform getSelectedNode()
	{
		return selectedNode;
	}
}