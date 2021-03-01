using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startText, titleText, spaceText, MainMenuPanel;
    private bool mainMenubool;
    
    void Start()
    {
        MainMenuPanel.SetActive(true);
        startText.SetActive(false);
        titleText.SetActive(false);
        spaceText.SetActive(false);
        StartCoroutine(StartScreen());
        mainMenubool = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && mainMenubool ){
            StartCoroutine(SpacePressed());
            mainMenubool = false;
        }
    }
    IEnumerator StartScreen()
    {
        yield return new WaitForSeconds(1);
        startText.SetActive(true);
        yield return new WaitForSeconds(2);
        startText.SetActive(false);
        titleText.SetActive(true);
        spaceText.SetActive(true);
        mainMenubool = true;
    }
    IEnumerator SpacePressed()
    {
        spaceText.GetComponent<Animator>().SetBool("off", true);
        yield return new WaitForSeconds(1);
        titleText.SetActive(false);
        spaceText.SetActive(false);
        yield return new WaitForSeconds(1);
        MainMenuPanel.GetComponent<Animator>().SetBool("off", true);
    }
}
