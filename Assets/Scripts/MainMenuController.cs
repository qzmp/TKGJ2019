using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainMenuController : MonoBehaviour
{
    public GameObject InstructionsGameObject;
    public GameObject CreditsGameObject;
    public GameObject AboutGameObject;
    public GameObject IntroGameObject;
    public GameObject MenuPanel;
    public Text IntroText;
    public Text MenuTitleText;
    public AudioSource AudioSource;
    public List<AudioClip> MenuAudioClips = new List<AudioClip>();
    private bool _start;

    private Color blackAlphaOne = new Color(0f,0f,0f,1f);
    
    private Transform monster_trans;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource.clip = MenuAudioClips[Random.Range(0, MenuAudioClips.Count - 1)];
        AudioSource.Play();
        StartCoroutine(IntroAnim());
    }

    // Update is called once per frame
    private IEnumerator IntroAnim()
    {
        while (IntroText.color != blackAlphaOne)
        {
            IntroText.color = Color.LerpUnclamped(IntroText.color, blackAlphaOne, Time.time/10f);
            yield return null;
        }
        while (IntroText.transform.position != MenuTitleText.transform.position)
        {
            IntroText.transform.position = Vector3.LerpUnclamped(IntroText.transform.position, MenuTitleText.transform.position, Time.time/40f);
            IntroText.fontSize = (int)Mathf.LerpUnclamped((float)IntroText.fontSize, (float)MenuTitleText.fontSize, Time.time/40f);
            yield return null;
        }
        MenuTitleText.gameObject.SetActive(true);
        IntroGameObject.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Instructions()
    {
        CreditsGameObject.SetActive(false);
        AboutGameObject.SetActive(false);
        InstructionsGameObject.SetActive(true);   
    }
    
    public void About()
    {
        CreditsGameObject.SetActive(false);
        InstructionsGameObject.SetActive(false);
        AboutGameObject.SetActive(true);
    }

    public void Credits()
    {
        InstructionsGameObject.SetActive(false);
        AboutGameObject.SetActive(false);
        CreditsGameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
