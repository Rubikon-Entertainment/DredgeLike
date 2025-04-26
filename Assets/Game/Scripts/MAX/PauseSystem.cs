using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu = null;

    bool isPaused;
    PauseSystem pauseSystem;

    public bool GetIsPaused() {return isPaused; }

    public void PauseGame() 
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    private void Awake() {
        pauseSystem = FindObjectOfType<PauseSystem>();
    }

    void Update() {
        if (pauseSystem.GetIsPaused()) { return; }
    }
}
