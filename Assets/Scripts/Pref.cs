using UnityEngine;

public static class Pref
{
    public static int bestMove
    {


        set
        {
            int oldMove = PlayerPrefs.GetInt(PrefKey.BestCore.ToString(), 0);
            if (oldMove > value || oldMove == 0)
            {
                PlayerPrefs.SetInt(PrefKey.BestCore.ToString(), value);
            }
        }
        get => PlayerPrefs.GetInt(PrefKey.BestCore.ToString(), 0);
    }

}
