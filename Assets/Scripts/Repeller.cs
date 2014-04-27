using UnityEngine;
using System.Collections;

public class Repeller : MonoBehaviour {

	private float distanceAffected = 2.5f;
	float force = 2f;

	// Update is called once per frame
	void FixedUpdate () {

		// if input detected, set new target position
		if ( ! Input.GetMouseButton( 0 ) ) { return; }


		//TODO optimize - called in multiple components
		Vector3 position = Input.mousePosition;
		position.x = Mathf.Clamp( position.x, 0f, Screen.width );
		position.y = Mathf.Clamp( position.y, 0f, Screen.height );
		
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint( position );
		worldPoint.y = transform.position.y;
		//----------------------------------------


		//Check distance
		Vector3 diff = worldPoint - transform.position;
		float distance = diff.magnitude;

		if ( distance > distanceAffected ) { return; }

		Vector3 opposite = diff.normalized * -1f;
		float forceStrength = 1f - ( distance / distanceAffected );
		rigidbody.AddForce( opposite * force * forceStrength );
	}
}
