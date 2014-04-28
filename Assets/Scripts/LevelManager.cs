using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Fader title;
	public FishManager fishManager;

	// Use this for initialization
	void Awake () {

		ResetGame();

	}

	void ResetGame () {

		//Revert to original state
		title.SetAlpha( 0f );
		title.FadeTo( 1f, 2f );

		fishManager.ResetFish();

	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetMouseButtonDown( 0 ) ) {

			title.FadeTo( 0f, 1f );

		}

	}
}
