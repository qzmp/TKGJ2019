﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject instructions;
    public GameObject credits;
    public GameObject intro;

    private Image img;
    private bool start;

    private Transform monster_trans;

    // Start is called before the first frame update
    void Start()
    {
        intro.SetActive(true);
        img = intro.GetComponent<Image>();
        start = true;

    }

    // Update is called once per frame
    void Update()
    {


        if (start)
        {
            img.color = Color.Lerp(img.color, Color.black, Time.time / 20);
            if (img.color == Color.black)
            {
                start = false;
                intro.SetActive(false);
            }
        }

    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Instructions()
    {
        credits.SetActive(false);
        instructions.SetActive(true);
    }

    public void Credits()
    {
        instructions.SetActive(false);
        credits.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
