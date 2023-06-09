﻿using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	public Transform renderPart;
	public float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.

	public GameObject egg;
	public bool IsGrounded()
	{
		return m_Grounded;
	}
	const float k_CeilingRadius = .02f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;

	public Rigidbody2D rigidBody()
	{
		return m_Rigidbody2D;
	}
	private bool m_FacingRight = false;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[SerializeField] public float footstepTimeMin;
	[SerializeField] public float footstepTimeMax;
	[SerializeField] public float footstepTime;
	public float footstepTimer;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	public UnityEvent OnStepEvent;

	//public StudioEventEmitter footStepEmitter;
	//public StudioParameterTrigger footStepParamChanger;
	public float footStepMakeSoundMinDistance = 0.3f;
	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	Animator animator;

	private void Awake()
	{

		animator = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		k_GroundedRadius = m_GroundCheck.GetComponent<CircleCollider2D>().radius * m_GroundCheck.lossyScale.x;

		footstepTime = Random.Range(footstepTimeMin, footstepTimeMax);
		localY = renderPart.transform.localPosition.y;
	}

	public void changeParamByName(string paramName,float paramValue)
	{
		//footStepEmitter.SetParameter(paramName, paramValue);
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
                {

					OnLandEvent.Invoke();
					//footStepEmitter.Play();
				}
			}
		}

		if (m_Rigidbody2D.velocity.magnitude>footStepMakeSoundMinDistance && m_Grounded && !GetComponent<PlayerMovement>().isDead)
		{
			if (footstepTimer > footstepTime)
			{
				OnStepEvent.Invoke();
				//footStepEmitter.Play();
				footstepTimer = 0;
				footstepTime = Random.Range(footstepTimeMin, footstepTimeMax);
			}

			footstepTimer += Time.deltaTime;
		}
	}

	private bool wasMoving = false;
	public float moveScale = 0.5f;
	public float moveTransform = 0.1f;
	public float moveScaleTime = 0.3f;
	private float localY = 0;
	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		//if (!crouch)
		//{
		//	// If the character has a ceiling preventing them from standing up, keep them crouching
		//	if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
		//	{
		//		crouch = true;
		//	}
		//}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if (!wasMoving && math.abs(m_Rigidbody2D.velocity.x)>0.3f)
			{
				wasMoving = true;
				renderPart. transform.DOScaleY(moveScale, moveScaleTime).SetLoops(-1, LoopType.Yoyo);
				renderPart. transform.DOLocalMoveY(localY+moveTransform, moveScaleTime).SetLoops(-1, LoopType.Yoyo);
			}

			if (wasMoving && math.abs(m_Rigidbody2D.velocity.x) < 0.1f)
			{
				wasMoving = false;
				renderPart.transform.DOKill();
				renderPart.transform.localScale = Vector3.one;
				renderPart.transform.localPosition = new Vector3(0, localY,0);
			}

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump && GameManager.Instance. eggUsedCount<GameManager.Instance.maxEggCount)
		{
			sfxManager.Instance.play(sfxManager.Instance.layEgg);
			GameManager.Instance.useEgg();
			//AudioManager.Instance.playJump();
			animator.SetBool("jump", true);
			// Add a vertical force to the player.
			//m_Grounded = false;
			//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			transform.position = transform.position + Vector3.up * 1.6f;
			//m_Rigidbody2D.MovePosition(transform.position+Vector3.up*2);
			Instantiate(egg, transform.position- Vector3.up * 1.3f, quaternion.identity);
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}