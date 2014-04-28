using UnityEngine;
using System.Collections;

public class Eatable : MonoBehaviour {

	private FoodManager fm;

	private bool bIsEaten = false;

	public void SetFoodManager ( FoodManager p_fm ) {

		fm = p_fm;

	}

	void OnCollisionEnter ( Collision collision ) {

		if ( bIsEaten || fm == null ) { return; }

		if ( collision.gameObject.tag != "Fish" ) { return; }

		fm.EatFood( this );
		bIsEaten = true;

	}

}
