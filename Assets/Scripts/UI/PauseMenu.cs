using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private bool gameOn, gamePaused, canPause, EscLeft, Exit;
    public GameObject PauseMenuPanel, pauseText, spaceText, exitText, exitFill, thanksText;
    private static Image exitBar;
    private float timer;
    public float holdExitFor;
    private bool transition;
    private MainMenu menu;
    void Start()
    {
        transition = false;
        gamePaused = false;
        gameOn = true;
        canPause = true;
        EscLeft = false;
        Exit = false;
        exitBar = exitFill.GetComponent<Image>(); 
        exitBar.fillAmount = 0;
        menu = GetComponent<MainMenu>();
    }
    void Update()
    {
        bool menuOn = menu.inMenu;
        if (gameOn && Input.GetKeyDown(KeyCode.Escape) && canPause && !menuOn)
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
        if (Input.GetKeyDown(KeyCode.Space) && !Exit && !transition)
        {
            StartCoroutine(PausedScreenoff());
            gamePaused = false;
            gameOn = true;
        }
        if(EscLeft&& Input.GetKey(KeyCode.Escape)&& !Exit && !transition)
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
        transition = true;
        PauseMenuPanel.SetActive(true);
        PauseMenuPanel.GetComponent<Animator>().SetTrigger("on");
        yield return new WaitForSeconds(1f);
        exitFill.SetActive(true);
        exitBar.fillAmount = 0;
        pauseText.SetActive(true);
        spaceText.SetActive(true);
        exitText.SetActive(true);
        transition = false;
    }
    IEnumerator Exitscreen()
    {
        transition = true;
        thanksText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("skip", 1);
        ExitGame();
    }
    IEnumerator PausedScreenoff()
    {
        canPause = false;
        transition = true;
        spaceText.GetComponent<Animator>().SetTrigger("off");
        yield return new WaitForSeconds(.1f);
        pauseText.SetActive(false);
        spaceText.SetActive(false);
        exitText.SetActive(false);
        exitFill.SetActive(false);
        yield return new WaitForSeconds(.1f);
        PauseMenuPanel.GetComponent<Animator>().SetTrigger("off");
        yield return new WaitForSeconds(1.5f);
        PauseMenuPanel.SetActive(false);
        transition = false;
        canPause = true;
    }
   
    public void ExitGame()
    {
        Application.Quit();
        
    }
}
