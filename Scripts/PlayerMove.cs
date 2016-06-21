using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    // Initialize player speed, rotational speed and variables to print a player's location and heading to the screen
    public float playerSpeed = 0.1f;
    public float playerRotationSpeed = 2f;
	public Text posText, headingText;

	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Gets the current heading of the player
        float currentAngle = transform.eulerAngles.z;

		//While W is pressed, the X and Y coordinates of the player are incremented with trig
		//such that they will be moved forward in their current heading
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x + playerSpeed * -Mathf.Sin(Mathf.Deg2Rad * currentAngle), 
                transform.position.y + playerSpeed * Mathf.Cos(Mathf.Deg2Rad * currentAngle), 0);  // Set the new position vector using trig
        }

		//While A is pressed, the player rotates to the left with a specified speed
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, playerRotationSpeed);  // Rotate around the center of the player at the specified speed counterclockwise
        }

		//While D is pressed, rotate to the right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -playerRotationSpeed);  // Rotate around the center of the player at the specified speed clockwise
        }

		//Same as the "w" key code but with opposite directions
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x - playerSpeed * -Mathf.Sin(Mathf.Deg2Rad * currentAngle),
                transform.position.y - playerSpeed * Mathf.Cos(Mathf.Deg2Rad * currentAngle), 0);
        }

		//Debug.Log ("Player heading: " + currentAngle);

        // Unity will raise errors without this null check
		if (posText != null)
			posText.text = "World Position: (" + (int)transform.position.x + ", " +
				(int)transform.position.y + ")";  // print player's current location
		if (headingText != null)
			headingText.text = "Heading: " + (int)currentAngle;  // print player's current heading
    }
}
