using UnityEngine;
using System.Collections;

public class SpriteFader : Fader {

	public override float GetAlpha () {

		SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
		float average = 0f;
		float factor = 1f / renderers.Length;
		for( int i = 0; i < renderers.Length; i++ ) {

			SpriteRenderer current = renderers[ i ];
			average += current.color.a * factor;

		}
		return average;
	}
	
	public override void SetAlpha ( float p_alpha ) {
		
		Color c = new Color( 1f, 1f, 1f, p_alpha );
		SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
		float average = 0f;
		float factor = 1f / renderers.Length;
		for( int i = 0; i < renderers.Length; i++ ) {
			
			SpriteRenderer current = renderers[ i ];
			current.color = c;
			
		}
	}
}
