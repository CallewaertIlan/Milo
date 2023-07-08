using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettingsManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool isOpen;
    private bool isPaused;

    void Start()
    {
        isOpen = false;
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;
            canvas.enabled = isOpen;

            if (isOpen)
            {
                isPaused = true;
                PauseGame();
            }
            else
            {
                isPaused = false;
                ResumeGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}