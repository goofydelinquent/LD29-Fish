using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoodManager : MonoBehaviour {

	public GameObject foodPrefab;

	private List<Eatable> foodList = new List<Eatable>();

	private readonly Vector3 INIT_FOOD_POSITION = new Vector3( 2.8f, 0f, -1f );
	private readonly Quaternion INIT_FOOD_ROTATION = Quaternion.Euler( 45f, 45f, 45f );

	public System.Action onFoodEaten = null;

	public GameObject SpawnFood ( Vector3 p_position, Quaternion p_rotation ) {
		
		GameObject goFood = Instantiate( foodPrefab, p_position, p_rotation ) as GameObject;

		Eatable e = goFood.GetComponent<Eatable>();
		foodList.Add( e );
		e.SetFoodManager( this );

		return goFood;
		
	}


	public GameObject SpawnFood () {
		
		Vector3 position = new Vector3(
			Random.Range( LevelManager.BOUNDS_MIN.x, LevelManager.BOUNDS_MAX.x ),
			INIT_FOOD_POSITION.y,
			Random.Range( LevelManager.BOUNDS_MIN.z, LevelManager.BOUNDS_MAX.z ) );
		Quaternion rotation = Quaternion.Euler( 
		                                       Random.Range( 0f, 360f ), 
		                                       Random.Range( 0f, 360f ), 
		                                       Random.Range( 0f, 360f ) );
		
		return SpawnFood( position, rotation );
		
	}


	public bool EatFood ( GameObject p_eatable ) {

		Eatable e = p_eatable.GetComponent<Eatable>();
		if ( e == null ) { return false; }

		return EatFood( e );

	}

	public bool EatFood ( Eatable p_eatable ) {

		//if can remove from list, do cleanup
		bool result = foodList.Remove( p_eatable );
		if ( result ) {

			//TODO animate destruction
			Destroy( p_eatable.gameObject, 0.2f );

			if ( onFoodEaten != null ) {

				onFoodEaten();

			}

		}
		return result;

	}


	public void ResetFood () {

		while( foodList.Count > 0 ) {
			Eatable food = foodList[ 0 ];
			foodList.RemoveAt( 0 );
			Destroy ( food.gameObject );
		}

		SpawnFood( INIT_FOOD_POSITION, INIT_FOOD_ROTATION );

	}

}
