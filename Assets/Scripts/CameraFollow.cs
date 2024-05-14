using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject player;      //Public variable to store a reference to the player game object
	public float FollowSpeed = 2f;

	private bool startFollow = false;
	private Vector3 offset;         //Private variable to store the offset distance between the player and camera


	// LateUpdate is called after Update each frame
	void LateUpdate () 
	{
		if (startFollow) {
			// Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
			transform.position = Vector3.Slerp(transform.position, player.transform.position + offset, FollowSpeed * Time.deltaTime);
		}
	}

	public void StartFollow(){
		startFollow = true;
		offset = transform.position - player.transform.position;
	}
}
	