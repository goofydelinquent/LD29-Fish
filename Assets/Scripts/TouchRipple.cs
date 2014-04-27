using UnityEngine;
using System.Collections;

public class TouchRipple : MonoBehaviour {

	bool bWasButtonDown = false;
	bool bDidEmit = false;
	float particleSystemY = 0.1f;
	public GameObject particleSystem;

	private GameObject currentSystem;

	// Update is called once per frame
	void Update () {

		// If left mouse button is not held down
		if ( ! Input.GetMouseButton( 0 ) ) {

			currentSystem.particleSystem.Stop();
			Destroy( currentSystem, 3f );
			currentSystem = null;
			return;
		}


		Vector3 worldPoint = Camera.main.ScreenToWorldPoint( Input.mousePosition );
		worldPoint.y = particleSystemY;

		if ( currentSystem == null ) {

			currentSystem = Instantiate( particleSystem, worldPoint, Quaternion.identity ) as GameObject;

		} else {

			currentSystem.transform.position = Vector3.Lerp( currentSystem.transform.position, worldPoint, Mathf.Clamp01( 10 *  0.8f * Time.deltaTime ) );

		}
	}
}
