using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishManager : MonoBehaviour {

	public GameObject fishPrefab;

	private List<Fader> fishies = new List<Fader>();

	public static Vector3 INIT_FISH_POSITION = new Vector3( -0.437f, 0.15f, 1.14f );
	public static Quaternion INIT_FISH_ROTATION = Quaternion.Euler( 0f, 6.3f, 0f );

	public System.Action<int> onFishKilled = null;

	public GameObject SpawnFish ( Vector3 p_position, Quaternion p_rotation ) {

		GameObject goFish = Instantiate( fishPrefab, p_position, p_rotation ) as GameObject;

		//Fader MUST exist, so we don't check for null. - Critical that it's there!
		Fader f = goFish.GetComponent<Fader>();
		f.SetAlpha( 0f );
		f.FadeTo( 1f, 2f );

		fishies.Add( f );

		return goFish;

	}

	public GameObject SpawnFish () {

		Vector3 position = new Vector3(
			Random.Range( LevelManager.BOUNDS_MIN.x, LevelManager.BOUNDS_MAX.x ),
			INIT_FISH_POSITION.y,
			Random.Range( LevelManager.BOUNDS_MIN.z, LevelManager.BOUNDS_MAX.z ) );
		Quaternion rotation = Quaternion.Euler( 0f, Random.Range( 0f, 360f ), 0f );

		return SpawnFish( position, rotation );

	}


	public bool KillFish ( GameObject p_fish ) {

		Fader f = p_fish.GetComponent<Fader>();
		if ( f == null ) { return false; }

		return KillFish( f );

	}


	public bool KillFish ( Fader p_fish ) {

		//if can remove fish from list, do cleanup
		bool result = fishies.Remove( p_fish );
		if ( result ) {

			p_fish.FadeTo( 0f, 1f );
			Destroy( p_fish.gameObject as Object, 1.1f );

			if ( onFishKilled != null ) {

				onFishKilled( fishies.Count );

			}

		}
		return result;

	}


	public List<Fader> GetList () {

		return fishies;

	}

	public void ResetFish () {

		//there should be no fish in the list
		while( fishies.Count > 0 ) {
			Fader fish = fishies[ 0 ];
			fish.FadeTo( 0f, 1f );
			fishies.RemoveAt( 0 );
			Destroy( fish.gameObject, 1.1f );
		}

		SpawnFish( INIT_FISH_POSITION, INIT_FISH_ROTATION );
	}
}
