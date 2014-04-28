using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public Fader title;
	public FishManager fishManager;
	public FoodManager foodManager;
	public EnemyManager enemyManager;

	public GUIText scoreText;
	public GUIText highScoreText;

	private int score = 0;
	private int bestScore = 0;

	public static readonly Vector3 BOUNDS_MIN = new Vector3( -7.42f, 0.1f, 4.4f );
	public static readonly Vector3 BOUNDS_MAX = new Vector3( 7.42f, 0.1f, -4.4f );
	
	// Use this for initialization
	void Awake () {

		ResetGame();
		foodManager.onFoodEaten = OnFoodEaten;
		fishManager.onFishKilled = OnFishKilled;
		enemyManager.onFishFound = OnFishFoundByEnemy;

		enemyManager.SetFishManager( fishManager );

	}

	void ResetGame () {

		//Revert to original state
		title.SetAlpha( 0f );
		title.FadeTo( 1f, 2f );

		fishManager.ResetFish();
		foodManager.ResetFood();
		enemyManager.ResetEnemies();

		score = 0;
		SetScore( score );

	}

	void OnFoodEaten () {

		foodManager.SpawnFood();
		fishManager.SpawnFish();

		AddScore();

	}

	void OnFishFoundByEnemy ( GameObject p_fish ) {

		fishManager.KillFish( p_fish );

	}

	private void AddScore () {

		score += 1;
		SetScore( score );

		if ( score % 5 == 0 ) {

			enemyManager.SpawnEnemy( fishManager.GetList() );

		}
	}

	private void SetScore ( int p_score ) {

		scoreText.text = p_score.ToString();
		if ( bestScore < p_score ) {

			bestScore = p_score;
			highScoreText.text = p_score.ToString();

		}

	}

	void OnFishKilled ( int p_count ) {

		if ( p_count == 0 ) {

			ResetGame();

		}

	}
	
	// Update is called once per frame
	void Update () {

		if ( Input.GetMouseButtonDown( 0 ) ) {

			title.FadeTo( 0f, 1f );

		}

	}
}
