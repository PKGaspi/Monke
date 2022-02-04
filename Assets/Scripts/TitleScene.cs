using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    
    public void OnGameStart(InputAction.CallbackContext value) {
        if (value.started) {
            SceneManager.LoadScene("Level1");
        }
    }
}
