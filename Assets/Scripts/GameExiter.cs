using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameExiter : MonoBehaviour
{
    public float holdTime = 1f;
    private float progress = 0f;
    private bool exiting = false;

    void Update() {
        if (!exiting) {
            progress = 0;
            return;
        }
        progress += Time.deltaTime;
        if (progress >= holdTime) {
            Application.Quit();
        }
    }
    public void OnGameExit(InputAction.CallbackContext value) {
        if (value.started) {
            exiting = true;
        }
        if (value.canceled) {
            exiting = false;
        }
    }

}
