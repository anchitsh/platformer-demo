using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startText, titleText, spaceText, MainMenuPanel, restartText, controls;
    private bool mainMenubool;
    public bool coinsCollected, inMenu;

    void Start()
    {
        
        MainMenuPanel.SetActive(true);
        startText.SetActive(false);
        titleText.SetActive(false);
        spaceText.SetActive(false);
        controls.SetActive(false);
        mainMenubool = false;
        inMenu = true;
        coinsCollected = false;
        restartText.SetActive(false);
        int skip = PlayerPrefs.GetInt("skip", 1);
        if(skip == 1)
        {
            PlayerPrefs.SetInt("skip", 0);
            StartCoroutine(StartScreen());
        }
        else
        {
            StartCoroutine(SkipStartScreen());
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && mainMenubool ){
            StartCoroutine(SpacePressed());
            mainMenubool = false;
        }
        if (coinsCollected)
        {
            restartText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.R) && coinsCollected)
        {
            StartCoroutine(RestartScreen());
        }
    }

    IEnumerator RestartScreen()
    {
        MainMenuPanel.GetComponent<Animator>().SetTrigger("on");
        yield return new WaitForSeconds(2f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        
    }

    IEnumerator StartScreen()
    {
        yield return new WaitForSeconds(1);
        startText.SetActive(true);
        yield return new WaitForSeconds(2);
        startText.SetActive(false);
        titleText.SetActive(true);
        spaceText.SetActive(true);
        controls.SetActive(true);

        mainMenubool = true;
    }
    IEnumerator SkipStartScreen()
    {
        yield return new WaitForSeconds(.5f);
        MainMenuPanel.GetComponent<Animator>().SetTrigger("off");
        yield return new WaitForSeconds(1.5f);
        inMenu = false;
    }
    IEnumerator SpacePressed()
    {
        yield return new WaitForSeconds(.1f);
        spaceText.GetComponent<Animator>().SetBool("off", true);
        yield return new WaitForSeconds(.5f);
        titleText.SetActive(false);
        spaceText.SetActive(false);
        controls.SetActive(false);
        yield return new WaitForSeconds(.5f);
        MainMenuPanel.GetComponent<Animator>().SetTrigger("off");
        yield return new WaitForSeconds(1.5f);
        inMenu = false;
    }
}
