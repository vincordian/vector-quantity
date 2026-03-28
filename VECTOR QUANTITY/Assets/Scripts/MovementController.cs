using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{

    public float JumpHeight;
    public float WalkSpeed;
    public float Gravity;

    private CharacterController character_controller;

    private Vector3 final_velocity = Vector3.zero;
    private float y_velocity = 0.0f;

    private Vector2 walk_direction = Vector2.zero;

    private bool jumping = false;

    private void SolveYVelocityAndJump()
    {
        y_velocity += -Gravity * Time.deltaTime;

        if (character_controller.isGrounded)
        {
            if (jumping)
            {
                y_velocity += Mathf.Sqrt(JumpHeight * 2f * Gravity); // sqrt(jump*gravity*2)
                jumping = false;
            }

            if (y_velocity < 0.0f)
            {
                y_velocity = 0.0f;
            }
        }
    }

    private Vector3 CalculateWalk()
    {
        Vector3 walk_direction_vector3 = new Vector3(walk_direction.x, 0, walk_direction.y) * WalkSpeed;
        return walk_direction_vector3;
    }

    // Note that this function will actually calculate the rotation for the head of the player and the body of the player so it can visually work in multiplayer.
    //private Vector3 CalculateCameraMovement()

    void Awake()
    {
        character_controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        SolveYVelocityAndJump(); // Note that this function is VOID because the variable is a *private* variable rather than a variable declared in this scope hence the name solve
        Vector3 walk_speed_calculated = CalculateWalk();

        final_velocity = Vector3.zero; // Reset velocity because its not kept after the frames

        final_velocity.y = y_velocity;
        final_velocity += walk_speed_calculated;

        final_velocity *= Time.deltaTime; // anti p2w

        character_controller.Move(final_velocity);
    }


    public void OnWalk(InputAction.CallbackContext walk_input) => walk_direction = transform.TransformDirection(walk_input.ReadValue<Vector2>());
    public void OnJump(InputAction.CallbackContext jump_input) => jumping = (jump_input.performed);
}