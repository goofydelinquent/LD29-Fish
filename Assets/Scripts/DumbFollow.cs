using UnityEngine;
using System.Collections;

public class DumbFollow : MonoBehaviour {

	private float speed = 15f;
	private float maxSpeed = 50f;
	private float squareThreshold = 0.75f;
	private Vector3 targetPosition;

	private float timeLastInterest;
	private float timeUntilBored;
	private bool bIsBored = false;

	void Awake () {

		targetPosition = transform.position;
		ResetBoredTimer();

	}

	void ResetBoredTimer () {

		bIsBored = false;
		timeUntilBored = Random.Range( 4f, 8f );
		timeLastInterest = Time.timeSinceLevelLoad;

	}

	void SetRandomTarget () {

		targetPosition = new Vector3 ( 
				Random.Range( LevelManager.BOUNDS_MIN.x, LevelManager.BOUNDS_MAX.x ),
				transform.position.y,
				Random.Range( LevelManager.BOUNDS_MIN.z, LevelManager.BOUNDS_MAX.z )
			);

	}

	void Update () {

		if ( bIsBored ) {

			SetRandomTarget();
			ResetBoredTimer();
			return;

		}

		if ( Time.timeSinceLevelLoad - timeLastInterest > timeUntilBored ) {

			bIsBored = true;

		}

	}

	// Update is called once per frame
	void FixedUpdate () {

		// if input detected, set new target position
		if ( Input.GetMouseButton( 0 ) ) {

			Vector3 position = Input.mousePosition;
			position.x = Mathf.Clamp( position.x, 0f, Screen.width );
			position.y = Mathf.Clamp( position.y, 0f, Screen.height );

			Vector3 worldPoint = Camera.main.ScreenToWorldPoint( position );
			worldPoint.y = transform.position.y;

			//Check target point if within line of sight? Nah, maybe next time. :P
			targetPosition = worldPoint;

			ResetBoredTimer();

		}


		//Velocity decay
		if ( rigidbody.velocity.sqrMagnitude > 0f  ) {
			rigidbody.velocity = rigidbody.velocity * 0.9f;
		}

		if ( rigidbody.velocity.sqrMagnitude < 0.05f ) {
			rigidbody.velocity = Vector3.zero;
		}

		//Move to target
		Vector3 diff = ( targetPosition - transform.position );
		if ( diff.sqrMagnitude > squareThreshold ) {

			Vector3 normal = diff.normalized;

			Quaternion look = Quaternion.LookRotation( normal );
			transform.rotation = Quaternion.Lerp( transform.rotation, look, Mathf.Clamp01( 1.2f * Time.fixedDeltaTime ) );

			if ( Vector3.Dot( transform.forward, normal ) > 0.7f ) {
				rigidbody.AddForce( normal * speed );

				if ( rigidbody.velocity.magnitude > maxSpeed ) {
					rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
				}

			}
		} 
	}
}
