using UnityEngine;
using System.Collections;

public class TouchRipple : MonoBehaviour {

	public GameObject ripplerPrefab;
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

			currentSystem = Instantiate( ripplerPrefab, worldPoint, Quaternion.identity ) as GameObject;
			currentSystem.particleSystem.Emit( 1 );

		} else {

			if ( ! currentSystem.particleSystem.IsAlive() ) {

				currentSystem.particleSystem.Play();

			}
			currentSystem.transform.position = Vector3.Lerp( currentSystem.transform.position, worldPoint, Mathf.Clamp01( 10 *  0.8f * Time.deltaTime ) );

		}
	}
}
