using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
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
        bool titleShowed = false;
        bool titleMoved = false;
        
        IntroText.DOColor(blackAlphaOne, 3f).onComplete += () => titleShowed = true;
        yield return new WaitUntil(()=>titleShowed);
        DOTween.To(SetFontSize,(float)IntroText.fontSize, (float)MenuTitleText.fontSize, 3f);
        IntroText.transform.DOMove(MenuTitleText.transform.position,3f).onComplete += () => titleMoved = true;
        yield return new WaitUntil(()=>titleMoved);
        MenuTitleText.gameObject.SetActive(true);
        IntroGameObject.SetActive(false);
        MenuPanel.SetActive(true);
    }

    public void SetFontSize(float value)
    {
        IntroText.fontSize = (int)value;
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
