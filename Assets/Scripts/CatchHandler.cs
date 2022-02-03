using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatchHandler : MonoBehaviour
{
    public int neededCatches = 7;
    public UIBar catchesBar;
    private int catches = 0;

    void Start() {
        catchesBar.maxValue = neededCatches;
        catchesBar.value = neededCatches;
    }

    public void RegisterCatch() {
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
