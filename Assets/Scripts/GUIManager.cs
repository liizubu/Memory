using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{

    public GameObject mainMenu;
    public GameObject gamePlay;
    public Image timeBar;
    public PauseDialog pauseDialog;
    public TimeOutDialog timeOutDialog;
    public GameOverDialog gameOverDialog;



    public override void Awake()
    {


        MakeSingleton(false);
    }

    public void ShowGamePlay(bool isShow)
    {
        if (gamePlay)
        {
            gamePlay.SetActive(isShow);
        }
        if (mainMenu)
        {
            mainMenu.SetActive(!isShow);
        }
    }

    public void UpdateTimeBar(float curTime, float totalTime)
    {
        float rate = curTime / totalTime;
        if (timeBar)
        {
            timeBar.fillAmount = rate;
        }
    }

}
