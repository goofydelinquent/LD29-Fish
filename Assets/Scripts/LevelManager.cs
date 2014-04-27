using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Fader title;

	private readonly Vector3 INIT_FISH_POSITION = new Vector3( -0.437f, 0.15f, 1.14f );
	private readonly Quaternion INIT_FISH_ROTATION = Quaternion.Euler( 0f, 6.3f, 0 );

	public GameObject fishPrefab;
	List<GameObject> fishes = new List<GameObject>();

	// Use this for initialization
	void Awake () {

		ResetGame();

	}

	void ResetGame () {

		//there should be no fish in the list
		while( fishes.Count > 0 ) {
			GameObject fish = fishes[ 0 ];
			fishes.RemoveAt( 0 );
			Destroy( fish );
		}

		//Revert to original state
		title.SetAlpha( 0f );
		title.FadeTo( 1f, 2f );

		GameObject firstFish = Instantiate( fishPrefab, INIT_FISH_POSITION, INIT_FISH_ROTATION ) as GameObject;
		fishes.Add( firstFish );

		Fader f = firstFish.GetComponent<Fader>();
		if ( f != null ) {

			f.SetAlpha( 0f );
			f.FadeTo( 1f, 2f );

		}
	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetMouseButtonDown( 0 ) ) {

			title.FadeTo( 0f, 1f );

		}

	}
}
