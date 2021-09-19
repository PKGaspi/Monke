using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayableCharacter : MonoBehaviour
{
    public const int AERIAL_JUMPS = 1;

    private CharacterController controller;
    public Vector3 velocity;
    private Vector3 rawInputMovement = Vector3.zero;
    private float gravity = -15.0f;
    private float walk_speed = 3.0f;
    private float jump_speed = 6.0f;
    private int jumps_left = AERIAL_JUMPS;
    public bool isGrounded = true;




    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
    }

    // Update is called once per frame
    void Update()
    {
        // Change facing direction
        if (rawInputMovement != Vector3.zero) {
            gameObject.transform.forward = rawInputMovement;        
        }

        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = 0;
            jumps_left = AERIAL_JUMPS;
            isGrounded = true;
        } else {
            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            isGrounded = false;
        }
        
        velocity.x = walk_speed * rawInputMovement.x;
        velocity.z = walk_speed * rawInputMovement.z;
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value) {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    public void OnJump(InputAction.CallbackContext value) {
        if (value.started && jumps_left > 0) {
            velocity.y = jump_speed;
            if (!controller.isGrounded) {
                jumps_left--;
            }
            isGrounded = false;
        }
    }
}
