using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{

	public float JumpHeight;
	public float WalkSpeed;
	public float Gravity;

	private CharacterController character_controller;
	private Camera player_camera;

	private Vector3 final_velocity = Vector3.zero;
	private float y_velocity = 0.0f;

	private Vector2 walk_direction = Vector2.zero;

	private bool jumping = false;

	void Awake()
	{
		character_controller = GetComponent<CharacterController>();
		player_camera = GetComponent<Camera>();
	}

	void Update()
	{
		y_velocity += -Gravity;

		if (character_controller.isGrounded)
		{
			if (jumping)
			{
				y_velocity += JumpHeight;
				jumping = false;
			}

			if (y_velocity < 0.0f)
			{
				y_velocity = 0.0f;
			}
		}


		jumping = false;

		Vector3 walk_direction_normalized = new Vector3(walk_direction.x, 0, walk_direction.y) * WalkSpeed;

        final_velocity = Vector3.zero;
		final_velocity.y = y_velocity;

		final_velocity += walk_direction_normalized; // walk

		final_velocity *= Time.deltaTime;

		character_controller.Move(final_velocity);
	}


    public void OnWalk(InputAction.CallbackContext walk_input) => walk_direction = transform.TransformDirection(walk_input.ReadValue<Vector2>());
	public void OnJump(InputAction.CallbackContext jump_input) => jumping = (!jumping && character_controller.isGrounded && jump_input.performed);
}