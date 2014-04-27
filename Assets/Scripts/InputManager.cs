using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	public CharacterController playerController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 movement = Vector2.zero;

		if ( Input.GetButtonDown( "left" ) ) {
			movement.x -= 1f;
		}

		if ( Input.GetButtonDown( "right" ) ) {
			movement.x += 1f;
		}

	}
}
