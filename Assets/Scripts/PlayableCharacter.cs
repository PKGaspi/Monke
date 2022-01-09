using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayableCharacter : MonoBehaviour
{
    public const int AERIAL_JUMPS = 1;

    public CharacterController controller;
    public Vector3 velocity;
    private Vector3 rawInputMovement = Vector3.zero;
    private float gravity = -15.0f;
    private float walkSpeed = 3.0f;
    private float jumpSpeed = 6.0f;
    private int jumpsLeft = AERIAL_JUMPS;
    public bool isGrounded = true;




    // Start is called before the first frame update
    void Start()
    {
        //controller = gameObject.GetComponent(typeof(CharacterController)) as CharacterController;
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
            jumpsLeft = AERIAL_JUMPS;
            isGrounded = true;
        } else {
            // Apply gravity
            velocity.y += gravity * Time.deltaTime;
            isGrounded = false;
        }
        
        velocity.x = walkSpeed * rawInputMovement.x;
        velocity.z = walkSpeed * rawInputMovement.z;
        controller.Move(velocity * Time.deltaTime);
    }

    public void OnMovement(InputAction.CallbackContext value) {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    public void OnJump(InputAction.CallbackContext value) {
        if (value.started && jumpsLeft > 0) {
            velocity.y = jumpSpeed;
            if (!controller.isGrounded) {
                jumpsLeft--;
            }
            isGrounded = false;
        }
    }
}
