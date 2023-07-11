using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverDialog : Dialog
{
    public Text totalMoveTxt;
    public Text bestMoveTxt;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        Time.timeScale = 0;

        if (totalMoveTxt && GameManager.Ins)
        {
            totalMoveTxt.text = GameManager.Ins.TotalMoving.ToString();
        }

        if (bestMoveTxt)
        {
            bestMoveTxt.text = Pref.bestMove.ToString();
        }


    }
    public void Continue()
    {
        Time.timeScale = 1;
        SceneManager.sceneLoaded += OnSceneLoadEvent;
        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentSence();
        }
    }

    private void OnSceneLoadEvent(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Ins)
        {
            GameManager.Ins.PlayGame();
        }
        SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;

        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentSence();
        }
    }

}
