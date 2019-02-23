using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject instructions;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Instructions()
    {
        credits.SetActive(false);
        instructions.SetActive(!instructions.activeSelf);
    }

    public void Credits()
    {
        instructions.SetActive(false);
        credits.SetActive(!credits.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
