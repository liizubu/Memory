using UnityEngine;

[System.Serializable]
public class MatchItem
{
    public Sprite icon;
    private int m_id;

    public int Id { get => m_id; set => m_id = value; }
}

public enum AnimState
{
    Flip,
    Explode,
    Idle
}

public enum GameState
{
    Starting,
    Playing,
    TimeOut,
    GameOver
}

public enum PrefKey
{
    BestCore
}

