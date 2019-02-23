using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    private bool gameState;


    // Start is called before the first frame update
    void Start()
    {
        gameState = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameState)
                PauseAction();
        }

    }


    // GAME OVER
    //-----------
    public void GameOver()
    {
        gameState = false;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        gameState = true;
        gameOverPanel.SetActive(false);
    }
    //------------

    // PAUSE
    //---------------
    private void PauseAction()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
    }
    //---------------
}
