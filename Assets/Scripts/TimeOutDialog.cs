using UnityEngine.SceneManagement;
public class TimeOutDialog : Dialog
{
    public void BackToMenu()
    {
        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentSence();
        }


    }

    public void Replay()
    {
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
}
