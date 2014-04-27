using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public enum State {
		Fading,
		Idle
	}


	private State titleState = State.Idle;
	private float timeStarted = 0f;

	private float initial = 0f;
	private float target = 1f;
	private float fadeTime = 0f;
	
	// Update is called once per frame
	void Update () {

		if ( titleState == State.Idle ) { 
			return; 

		}

		float alpha = GetAlpha();

		float t = ( Time.timeSinceLevelLoad - timeStarted ) / fadeTime;


		if ( t > 0.99 ) {

			SetAlpha( target );
			titleState = State.Idle;

		} else {

			alpha = Mathf.Lerp( initial, target, t );
			SetAlpha( alpha );

		}
	
	}

	public virtual float GetAlpha () {

		return this.renderer.material.color.a;

	}

	public virtual void SetAlpha ( float p_alpha ) {

		Color c = new Color( 1f, 1f, 1f, p_alpha );
		this.renderer.material.color = c;

	}

	public void FadeTo( float p_alpha, float p_time ) {

		timeStarted = Time.timeSinceLevelLoad;
		fadeTime = p_time;
		titleState = State.Fading;
		initial = GetAlpha();
		target = Mathf.Clamp01( p_alpha );

	}
}
