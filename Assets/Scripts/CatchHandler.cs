using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatchHandler : MonoBehaviour
{
    public int neededCatches = 7;
    public UIBar catchesBar;
    public AudioSource catchSound;
    private int catches = 0;

    void Start() {
        catchesBar.maxValue = neededCatches;
        catchesBar.value = neededCatches;
    }

    public void RegisterCatch() {
        catchSound.Play();
        catches++;
        catchesBar.value = neededCatches - catches;
        if (catches >= neededCatches) {
            Win();
        }
    }

    private void Win() {
        SceneManager.LoadScene("Victory");
    }
}
