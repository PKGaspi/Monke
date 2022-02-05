using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{

    public AudioSource music;

    public void Start() {
        music.Play();
    }
    
    public void OnGameStart(InputAction.CallbackContext value) {
        if (value.started) {
            SceneManager.LoadScene("Level1");
        }
    }
}
