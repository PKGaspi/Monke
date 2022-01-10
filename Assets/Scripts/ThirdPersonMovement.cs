using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public const int AERIAL_JUMPS = 1;

    public CharacterController controller;
    public Transform cam;

    public Vector3 velocity;
    private Vector3 movementDir = Vector3.zero;
    private float gravity = -15.0f;
    private float walkSpeed = 3.0f;
    private float jumpSpeed = 6.0f;
    private float aerialJumpSpeed = 8.0f;
    public int aerialJumpsLeft = AERIAL_JUMPS;




    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.isGrounded) {
            aerialJumpsLeft = AERIAL_JUMPS;
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        velocity.x = walkSpeed * movementDir.x;
        velocity.z = walkSpeed * movementDir.z;

        // Apply movement.
        controller.Move(velocity * Time.deltaTime);

        // Update values after movement.
        velocity = controller.velocity;
    }

    public void OnMovement(InputAction.CallbackContext value) {
        Vector2 inputMovementDir = value.ReadValue<Vector2>();
        movementDir = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * new Vector3(inputMovementDir.x, 0f, inputMovementDir.y);

        // Face towards movement dir.
        if (movementDir != Vector3.zero) {
            transform.forward = movementDir; 
        }
    }

    public void OnJump(InputAction.CallbackContext value) {
        if (value.started && aerialJumpsLeft > 0) {
            if (!controller.isGrounded) {
                // Remove aerial jump.
                aerialJumpsLeft--;
                velocity.y = aerialJumpSpeed;
            }
            else {
                velocity.y = jumpSpeed;
            }
        }
    }
}
