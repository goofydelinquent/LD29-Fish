using UnityEngine;
using System.Collections;

public class TouchRipple : MonoBehaviour {

	bool bWasButtonDown = false;
	public GameObject particleSystem;
	private GameObject currentSystem;

	// Update is called once per frame
	void Update () {

		// If left mouse button is not held down
		if ( ! Input.GetMouseButton( 0 ) ) {

			if ( currentSystem != null ) {
				currentSystem.particleSystem.Stop();
				Destroy( currentSystem, currentSystem.particleSystem.startLifetime );
				currentSystem = null;
			}
			return;
		}


		Vector3 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		worldPoint.y = transform.position.y;

		if ( currentSystem == null ) {

			currentSystem = Instantiate( particleSystem, worldPoint, Quaternion.identity ) as GameObject;
			currentSystem.particleSystem.Emit( 1 );

		} else {

			currentSystem.transform.position = Vector3.Lerp( currentSystem.transform.position, worldPoint, Mathf.Clamp01( 10 *  0.8f * Time.deltaTime ) );

		}
	}
}
