using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool gameOn, gamePaused, canPause, EscLeft, Exit;
    public GameObject PauseMenuPanel, pauseText, spaceText, exitText, creditsText, exitFill, thanksText;
    private static Image exitBar;
    private float timer;
    public float holdExitFor;
    void Start()
    {
        gamePaused = false;
        gameOn = true;
        canPause = true;
        EscLeft = false;
        Exit = false;
        exitBar = exitFill.GetComponent<Image>(); 
        exitBar.fillAmount = 0;
    }
    void Update()
    {
       
        if (gameOn && Input.GetKeyDown(KeyCode.Escape) && canPause)
        {
            gamePaused = true;
            gameOn = false;
            ShowPaused();            
        }
        if (gamePaused)
        {
            PauseMenuActive();
        }

    }

    void ShowPaused()
    {
        StartCoroutine(PausedScreenOn());
    }
    void PauseMenuActive()
    {
        if (Input.GetKeyUp(KeyCode.Escape)&& !Exit)
        {
            EscLeft = true;
            timer = 0;
            exitBar.fillAmount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !Exit)
        {
            StartCoroutine(PausedScreenoff());
            gamePaused = false;
            gameOn = true;
        }
        if(EscLeft&& Input.GetKey(KeyCode.Escape)&& !Exit)
        {
            timer += Time.deltaTime;
            exitBar.fillAmount = timer / holdExitFor;
            if (timer > holdExitFor)
            {
                Exit = true;
                StartCoroutine(Exitscreen());
            }
            print(timer);
        }

    }
    IEnumerator PausedScreenOn()
    {
        PauseMenuPanel.SetActive(true);
        PauseMenuPanel.GetComponent<Animator>().SetTrigger("on");
        yield return new WaitForSeconds(1.5f);
        exitFill.SetActive(true);
        exitBar.fillAmount = 0;
        pauseText.SetActive(true);
        spaceText.SetActive(true);
        exitText.SetActive(true);
        creditsText.SetActive(true); 
    }
    IEnumerator Exitscreen()
    {
        thanksText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        ExitGame();
    }
    IEnumerator PausedScreenoff()
    {
        spaceText.GetComponent<Animator>().SetTrigger("off");
        yield return new WaitForSeconds(.5f);
        pauseText.SetActive(false);
        spaceText.SetActive(false);
        exitText.SetActive(false);
        creditsText.SetActive(false);
        exitFill.SetActive(false);
        yield return new WaitForSeconds(.5f);
        PauseMenuPanel.GetComponent<Animator>().SetTrigger("off");
        yield return new WaitForSeconds(1f);
        PauseMenuPanel.SetActive(false);
    }
   
    public void ExitGame()
    {
        Application.Quit();
    }
}
