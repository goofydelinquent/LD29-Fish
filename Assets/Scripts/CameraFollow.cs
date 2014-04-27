using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Vector2 margin;
	public Vector2 smoothing;

	Transform player;

	void Awake () {

		//Find player
		player = GameObject.FindGameObjectWithTag( "Player" ).transform;

	}

	void FixedUpdate () {

		//Get camera's current position
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		if ( Mathf.Abs( targetX - player.position.x ) > margin.x ) {

			targetX = Mathf.Lerp( targetX, player.position.x, smoothing.x * Time.fixedDeltaTime );

		}

		if ( Mathf.Abs( targetY - player.position.y ) > margin.y ) {

			targetY = Mathf.Lerp( targetY, player.position.y, smoothing.y * Time.fixedDeltaTime );

		}

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3( targetX, targetY, transform.position.z );

	}
}
