using UnityEngine;
using System.Collections;

public class DumbFollow : MonoBehaviour {

	private float speed = 20f;
	private float maxSpeed = 50f;
	private float squareThreshold = 0.75f;
	private Vector3 targetPosition;

	void Awake () {

		targetPosition = transform.position;

	}

	// Update is called once per frame
	void FixedUpdate () {

		// if input detected, try to go to world position
		if ( Input.GetMouseButton( 0 ) ) {

			Vector3 position = Input.mousePosition;
			position.x = Mathf.Clamp( position.x, 0f, Screen.width );
			position.y = Mathf.Clamp( position.y, 0f, Screen.height );

			Vector3 worldPoint = Camera.main.ScreenToWorldPoint( position );
			worldPoint.y = transform.position.y;

			//Check target point if within line of sight? Nah, maybe next time. :P
			targetPosition = worldPoint;

		}


		//Velocity decay
		if ( rigidbody.velocity.sqrMagnitude > 0f  ) {
			rigidbody.velocity = rigidbody.velocity * 0.9f;
		}

		if ( rigidbody.velocity.sqrMagnitude < 0.1f ) {
			rigidbody.velocity = Vector3.zero;
		}

		Vector3 diff = ( targetPosition - transform.position );
		if ( diff.sqrMagnitude > squareThreshold ) {

			Vector3 normal = diff.normalized;

			Quaternion look = Quaternion.LookRotation( normal );
			transform.rotation = Quaternion.Lerp( transform.rotation, look, Mathf.Clamp01( 1.2f * Time.fixedDeltaTime ) );


			if ( Vector3.Dot( this.transform.forward, normal ) > 0.6f ) {
				rigidbody.AddForce( normal * speed );

				if ( rigidbody.velocity.magnitude > maxSpeed ) {
					rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
				}

			} else {
				//rigidbody.velocity = rigidbody.velocity * 0.9f;
			}

		} 
	}
}
