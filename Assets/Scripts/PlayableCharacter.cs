using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayableCharacter : MonoBehaviour
{
    public const int MAX_N_JUMPS = 2;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 rawInputMovement = Vector3.zero;
    private float gravity = -15.0f;
    private float walk_speed = 3.0f;
    private float jump_speed = 6.0f;
    public int n_jumps = MAX_N_JUMPS;




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

        // Apply gravity
        if (controller.isGrounded && velocity.y < 0) {
            velocity.y = 0;
            n_jumps = MAX_N_JUMPS;
        } else {
            velocity.y += gravity * Time.deltaTime;
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
        if (value.started && n_jumps > 0) {
            velocity.y = jump_speed;
            n_jumps--;
        }
    }
}
