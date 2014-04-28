using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	public GameObject enemyPrefab;
	private List<FishEater> eaters = new List<FishEater>();

	public System.Action<GameObject> onFishFound = null;
	private FishManager fm = null;


	private float enemySpeedLerpCeiling = 5f;


	public void SetFishManager ( FishManager p_fm ) {

		fm = p_fm;

	}

	public GameObject SpawnEnemy ( Vector3 p_position, Quaternion p_rotation ) {
		
		GameObject goEater = Instantiate( enemyPrefab, p_position, p_rotation ) as GameObject;
		FishEater f = goEater.GetComponent<FishEater>();

		//Start at base speed, then move to max speed based on enemy count
		f.SetSpeed( Mathf.Lerp( FishEater.MIN_SPEED, FishEater.MAX_SPEED, Mathf.Clamp01( eaters.Count / enemySpeedLerpCeiling ) ) );
		f.SetEnemyManager( this );
		f.SetFishManager( fm );
		eaters.Add( f );
		
		return goEater;
		
	}

	public void DidFindFish ( GameObject p_fish ) {

		if ( onFishFound != null ) {

			onFishFound( p_fish );

		}

	}
	
	public GameObject SpawnEnemy ( List<Fader> p_fishies ) {

		float x = 0;
		float z = 0;

		//Try to get a "fair" spawn position, else just spawn anywhere.
		int maxRetries = 3;

		for( int i = 0; i < maxRetries; i++ ) {

			x = Random.Range( LevelManager.BOUNDS_MIN.x, LevelManager.BOUNDS_MAX.x );
			bool bIsGood = true;
			foreach( Fader f in p_fishies ) {

				if ( Mathf.Abs( f.transform.position.x  - x ) < 1.5f ) {
					bIsGood = false;
					break;
				}
			}

			if ( bIsGood ) { break; }
		}

		for( int i = 0; i < maxRetries; i++ ) {
			
			z = Random.Range( LevelManager.BOUNDS_MIN.z, LevelManager.BOUNDS_MAX.z );
			bool bIsGood = true;
			foreach( Fader f in p_fishies ) {
				
				if ( Mathf.Abs( f.transform.position.z  - z ) < 1.5f ) {
					bIsGood = false;
					break;
				}
			}
			
			if ( bIsGood ) { break; }
		}

		Vector3 position = new Vector3( x, FishManager.INIT_FISH_POSITION.y, z );
		Quaternion rotation = Quaternion.Euler( 0f, Random.Range( 0f, 360f ), 0f );
		
		return SpawnEnemy( position, rotation );
		
	}
	
	public void ResetEnemies () {
		
		//there should be no fish in the list
		while( eaters.Count > 0 ) {
			FishEater eater = eaters[ 0 ];
			eaters.RemoveAt( 0 );
			eater.particleSystem.Stop();
			Destroy( eater.gameObject, 1.1f );
		}
	}
}
