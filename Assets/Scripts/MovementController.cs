using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour {

	public Transform groundCheck;

	public float maxSpeed = 10f;
	public float moveForce = 3f;
	public float jumpForce = 5f;

	private bool bIsGrounded = false;
	private bool bShouldJump = false;
	private bool bIsFacingRight = true;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		bIsGrounded = Physics2D.Linecast( transform.position, groundCheck.position, 1 << LayerMask.NameToLayer( "Ground" ) );

		if ( Input.GetButtonDown( "Jump" ) && bIsGrounded ) {
			bShouldJump = true;
		}
	}

	void FixedUpdate () {

		float h = Input.GetAxis( "Horizontal" );

		//TODO set animator parameter for speed

		//Add force if moving
		if ( h * rigidbody2D.velocity.x < maxSpeed ) {
			
			rigidbody2D.AddForce( Vector2.right * h * moveForce );
			
		}

		if ( bIsGrounded && Mathf.Abs( h ) < 0.5f ) {

			rigidbody2D.velocity = new Vector2( Mathf.Lerp( rigidbody2D.velocity.x, 0f, 0.5f ), rigidbody2D.velocity.y );

		}

		// cap speed on horizontal axis
		if ( Mathf.Abs( rigidbody2D.velocity.x ) > maxSpeed ) {

			rigidbody2D.velocity = new Vector2( Mathf.Sign( rigidbody2D.velocity.x ) * maxSpeed, rigidbody2D.velocity.y );
		}

		// Flip sprite as needed
		if ( ( h > 0 && !bIsFacingRight ) || ( h < 0 && bIsFacingRight ) ) {

			Flip();

		}

		//
		if ( bShouldJump ) {

			//TODO trigger jump animation
			rigidbody2D.AddForce( new Vector2( 0f, jumpForce ) );
			bShouldJump = false;

		}
	}

	void Flip () {
	}
}