using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishEater : MonoBehaviour {

	public const float MIN_SPEED = 0.5f;
	public const float MAX_SPEED = 1.4f;

	private EnemyManager em;
	private FishManager fm;

	float lastUpdated = 0;
	float seekThreshold = 0f;
	GameObject target = null;

	float movementSpeed = 1.45f;

	void Awake () {

		ResetSeek();

	}

	public void SetSpeed ( float p_speed ) {

		movementSpeed = p_speed;

	}

	void ResetSeek () {

		lastUpdated = Time.timeSinceLevelLoad;
		seekThreshold = Random.Range( 4f, 8f );

	}

	public void SetEnemyManager ( EnemyManager p_em ) {

		em = p_em;

	}

	public void SetFishManager ( FishManager p_fm ) {

		fm = p_fm;

	}

	void OnCollisionEnter ( Collision collision ) {

		if ( collision.gameObject.tag != "Fish" ) { return; }

		em.DidFindFish( collision.gameObject );

	}

	void FindTarget () {

		List<Fader> list = fm.GetList();
		//pick random target - ideally, we pick closest, but eeeh, let's not waste operations
		int index = Random.Range( 0, list.Count );
		target = list[ index ].gameObject;

	}


	void Update () {

		// Time to change targets
		if ( target == null || Time.timeSinceLevelLoad - lastUpdated > seekThreshold ) {
			ResetSeek();
			FindTarget();
			return;
		}

		//usual behavior - go to target!

		//Move to target
		Vector3 diff = ( target.transform.position - transform.position );
		transform.position = transform.position + ( diff.normalized * movementSpeed * Time.deltaTime );
	}
}
