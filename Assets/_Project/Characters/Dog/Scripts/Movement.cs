using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
	protected const float InputSqrThreshold = 0.1f;

	[Header("Settings")]
	[SerializeField]
	protected float horizontalVelocity = 3f;
	[SerializeField]
	protected float rotationSpeed = 0.1f;
	[SerializeField]
	protected bool disableMovement;
	[Header("References")]
	[SerializeField]
	protected Animator animator;
	[Header("Debug")]

	protected Vector3 moveVector;
	protected Vector2 input;
	protected float inputMagnitudeSqr;
	protected Vector3 desiredMoveDirection;
	protected float verticalVelocity;

	protected new Camera camera;
	protected CharacterController controller;

	protected virtual void Start()
	{
		camera = Camera.main;
		controller = GetComponent<CharacterController>();
	}

	protected virtual void Update()
	{
		HandleInput();
		MoveAndRotate();
		HandleGravity();

		if (!disableMovement)
			controller.Move(moveVector);

		UpdateAnimator();
	}

	protected virtual void MoveAndRotate()
	{
		inputMagnitudeSqr = input.sqrMagnitude;

		if (input.sqrMagnitude > InputSqrThreshold &&
			!disableMovement)
		{
			var forward = camera.transform.forward;
			var right = camera.transform.right;
			forward.y = 0f;
			right.y = 0f;
			forward.Normalize();
			right.Normalize();
			desiredMoveDirection = forward * input.y + right * input.x;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed);
			moveVector = desiredMoveDirection * Time.deltaTime * horizontalVelocity;
		}
		else
		{
			moveVector = Vector3.zero;
		}
	}

	protected virtual void HandleGravity()
	{
		if (controller.isGrounded)
			verticalVelocity -= 0;
		else
			verticalVelocity -= 0.2f;
		moveVector.y = verticalVelocity * 0.2f * Time.deltaTime;
	}

	protected virtual void UpdateAnimator()
	{
	}

	protected virtual void HandleInput()
	{
		input.x = Input.GetAxis("Horizontal");
		input.y = Input.GetAxis("Vertical");
	}
}
