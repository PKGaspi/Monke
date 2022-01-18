using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ThirdPersonMovement : Character {
    public const int AERIAL_JUMPS = 1;

    public Transform cam;
    public CinemachineInputProvider camInput;
    public PlayerInput playerInput;
    public GadgetHandler gadgetHandler;

    private Vector3 gadgetDir = Vector3.zero;

    private float aerialJumpSpeed = 8.0f;
    private bool canUseGadget = false;
    private InputActionReference defaultXYAxis;
    private int aerialJumpsLeft = AERIAL_JUMPS;

    // Start is called before the first frame update
    void Start() {
        defaultXYAxis = camInput.XYAxis;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (controller.isGrounded) {
            aerialJumpsLeft = AERIAL_JUMPS;
        }

        Move(walkSpeed);
    }

    public void OnMovement(InputAction.CallbackContext value) {

        Vector2 inputMovementDir = value.ReadValue<Vector2>();
        moveDir = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * new Vector3(inputMovementDir.x, 0f, inputMovementDir.y);

        // Face towards movement dir.
        if (gadgetDir == Vector3.zero && moveDir != Vector3.zero) {
            transform.forward = moveDir; 
        }
    }

    public void OnJump(InputAction.CallbackContext value) {
        if (value.started && aerialJumpsLeft > 0 && gadgetDir == Vector3.zero) {
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

    public void OnGadget(InputAction.CallbackContext value) {
        if (!canUseGadget) {
            return;
        }

        // Gadget usage is canceled.
        if (value.canceled) {
            gadgetDir = Vector3.zero;
        }

        // Get gadget dir and cancel movement.
        //moveDir = Vector3.zero;
        Vector2 inputGadgetDir = value.ReadValue<Vector2>();
        gadgetDir = Quaternion.Euler(0f, cam.eulerAngles.y, 0f) * new Vector3(inputGadgetDir.x, 0f, inputGadgetDir.y);

        gadgetHandler.Use(inputGadgetDir.magnitude);

        // Use gadget
        if (gadgetDir != Vector3.zero) {
            transform.forward = gadgetDir;
        }
    }

    public void OnGadgetCameraToggle(InputAction.CallbackContext value) {
        if (value.performed) {
            return;
        }
        // Hold for gadget, release for camera.
        if (value.started) {
            // Set Gadget and disable camera.
            canUseGadget = true;
            playerInput.actions["Look"].Disable();
            camInput.XYAxis = null;
        }
        else if (value.canceled) {
            // Set Camera and disable camera.
            gadgetDir = Vector3.zero;
            canUseGadget = false;
            gadgetHandler.Use(0);
            camInput.XYAxis = defaultXYAxis;
            playerInput.actions["Look"].Enable();
        }
    }

}
