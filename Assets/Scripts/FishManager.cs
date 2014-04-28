using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishManager : MonoBehaviour {

	public GameObject fishPrefab;
	public GameObject foodPrefab;

	private GameObject currentFood;

	private List<Fader> fishes = new List<Fader>();

	private readonly Vector3 INIT_FISH_POSITION = new Vector3( -0.437f, 0.15f, 1.14f );
	private readonly Quaternion INIT_FISH_ROTATION = Quaternion.Euler( 0f, 6.3f, 0f );

	private readonly Vector3 INIT_FOOD_POSITION = new Vector3( 2.8f, 0f, -1f );
	private readonly Quaternion INIT_FOOD_ROTATION = Quaternion.Euler( 45f, 45f, 45f );


	public readonly Vector3 BOUNDS_MIN = new Vector3( -7.42f, 0.1f, 4.4f );
	public readonly Vector3 BOUNDS_MAX = new Vector3( 7.42f, 0.1f, -4.4f );

	
	public GameObject SpawnFish ( Vector3 p_position, Quaternion p_rotation ) {

		GameObject goFish = Instantiate( fishPrefab, p_position, p_rotation ) as GameObject;

		//Fader MUST exist, so we don't check for null. - Critical that it's there!
		Fader f = goFish.GetComponent<Fader>();
		f.SetAlpha( 0f );
		f.FadeTo( 1f, 2f );

		fishes.Add( f );

		return goFish;
	}

	public GameObject SpawnFish () {

		Vector3 position = new Vector3(
			Random.Range( BOUNDS_MIN.x, BOUNDS_MAX.x ),
			INIT_FISH_POSITION.y,
			Random.Range( BOUNDS_MIN.z, BOUNDS_MAX.z ) );
		Quaternion rotation = Quaternion.Euler( 0f, Random.Range( 0f, 360f ), 0f );

		return SpawnFish( position, rotation );
	}


	public bool KillFish ( GameObject p_fish ) {

		Fader f = p_fish.GetComponent<Fader>();
		if ( f == null ) { return false; }

		return KillFish( f );

	}


	public bool KillFish( Fader p_fish ) {

		//if can remove fish from list, do cleanup
		bool result = fishes.Remove( p_fish );
		if ( result ) {

			p_fish.FadeTo( 0f, 1f );
			Destroy( p_fish.gameObject as Object, 1.1f );

		}
		return result;
	}

	public GameObject SpawnFood ( Vector3 p_position, Quaternion p_rotation ) {

		if ( currentFood != null ) { 

			DespawnCurrentFood();

		}

		GameObject goFood = Instantiate( foodPrefab, p_position, p_rotation ) as GameObject;
		currentFood = goFood;
		return goFood;

	}

	public GameObject SpawnFood () {

		Vector3 position = new Vector3(
			Random.Range( BOUNDS_MIN.x, BOUNDS_MAX.x ),
			INIT_FOOD_POSITION.y,
			Random.Range( BOUNDS_MIN.z, BOUNDS_MAX.z ) );
		Quaternion rotation = Quaternion.Euler( 
		    Random.Range( 360f, 360f ), 
		    Random.Range( 360f, 360f ), 
			Random.Range( 360f, 360f ) );
		
		return SpawnFood( position, rotation );

	}

	//Put in separate function in case we want to do something fancy later
	public void DespawnCurrentFood () {

		Destroy ( currentFood );
		currentFood = null;
	}

	public void ResetFish() {

		//there should be no fish in the list
		while( fishes.Count > 0 ) {
			Fader fish = fishes[ 0 ];
			fish.FadeTo( 0f, 1f );
			fishes.RemoveAt( 0 );
			Destroy( fish.gameObject, 1.1f );
		}

		SpawnFish( INIT_FISH_POSITION, INIT_FISH_ROTATION );
		SpawnFood( INIT_FOOD_POSITION, INIT_FOOD_ROTATION );
	}}
