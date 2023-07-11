using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int timeLimit;
    public MatchItem[] matchItems;
    public MatchItemUI itemUIPb;
    public Transform gridRoot;
    public GameState state;
    private List<MatchItem> m_matchItemsCoppy;
    private List<MatchItemUI> m_matchItemUIs;
    private List<MatchItemUI> m_answers;
    private float m_timeCounting;
    private int m_totatMatchItem;

    [SerializeField] private int m_totalMoving;
    public int m_rightMoving;
    private bool m_isAnswerChecking;

    public float TimeCounting { get => m_timeCounting; set => m_timeCounting = value; }
    public int TotatMatchItem { get => m_totatMatchItem; set => m_totatMatchItem = value; }
    public bool IsAnswerChecking { get => m_isAnswerChecking; set => m_isAnswerChecking = value; }
    public int TotalMoving { get => m_totalMoving; set => m_totalMoving = value; }

    public override void Awake()
    {
        MakeSingleton(false);
        m_matchItemsCoppy = new List<MatchItem>();
        m_matchItemUIs = new List<MatchItemUI>();
        m_answers = new List<MatchItemUI>();
        m_timeCounting = timeLimit;
        state = GameState.Starting;

    }

    public override void Start()
    {
        base.Start();
       
        if (AudioController.Ins)
        {
            AudioController.Ins.PlayBackgroundMusic();
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        state = GameState.Playing;

        GenerateMatchItems();


        if (GUIManager.Ins)
        {
            GUIManager.Ins.ShowGamePlay(true);
        }
    }

    private void Update()
    {
        if (state != GameState.Playing) return;
        m_timeCounting -= Time.deltaTime;
        if (m_timeCounting < 0 && state != GameState.TimeOut)
        {
            state = GameState.TimeOut;
            m_timeCounting = 0;
            if (GUIManager.Ins)
            {
                GUIManager.Ins.timeOutDialog.Show(true);

                if (AudioController.Ins)
                {
                    AudioController.Ins.PlaySound(AudioController.Ins.timeOut);
                }
                Debug.Log("time out");
            }

        }

        if (GUIManager.Ins)
        {
            GUIManager.Ins.UpdateTimeBar((float)m_timeCounting, (float)timeLimit);
        }
    }
    private void GenerateMatchItems()
    {
        if (matchItems == null || matchItems.Length <= 0 || itemUIPb == null || gridRoot == null) return;
        int totaItem = matchItems.Length;
        int divItem = totaItem % 2;
        m_totatMatchItem = totaItem - divItem;

        for (int i = 0; i < m_totatMatchItem; i++)
        {
            var matchItem = matchItems[i];
            if (matchItem != null)
                matchItem.Id = i;
        }

        m_matchItemsCoppy.AddRange(matchItems);// 1/2 so the trong game
        m_matchItemsCoppy.AddRange(matchItems);// add 2 lan

        ShuffMatchItems();
        ClearGrid();

        for (int i = 0; i < m_matchItemsCoppy.Count; i++)
        {
            var matItem = m_matchItemsCoppy[i];


            var matchItemUIClone = Instantiate(itemUIPb, Vector3.zero, Quaternion.identity);
            matchItemUIClone.transform.SetParent(gridRoot);
            matchItemUIClone.transform.localPosition = Vector3.zero;
            matchItemUIClone.transform.localScale = Vector3.one;
            matchItemUIClone.UpdateFirstState(matItem.icon);
            matchItemUIClone.Id = matItem.Id;
            m_matchItemUIs.Add(matchItemUIClone);

            if (matchItemUIClone.btnComp)
            {
                matchItemUIClone.btnComp.onClick.RemoveAllListeners();
                matchItemUIClone.btnComp.onClick.AddListener(() =>
                {
                    if (m_isAnswerChecking) return;
                    m_answers.Add(matchItemUIClone);
                    matchItemUIClone.OpenAnimTrigger();
                    if (m_answers.Count == 2)
                    {
                        TotalMoving++;
                        m_isAnswerChecking = true;
                        StartCoroutine(CheckAnswerCo());
                    }

                    matchItemUIClone.btnComp.enabled = false;
                });

            }

        }
    }

    private IEnumerator CheckAnswerCo()
    {
        bool isRight = m_answers[0] != null && m_answers[1] != null && m_answers[0].Id == m_answers[1].Id;
        yield return new WaitForSeconds(1f);

        if (m_answers != null && m_answers.Count == 2)
        {
            if (isRight)
            {
                m_rightMoving++;
                for (int i = 0; i < m_answers.Count; i++)
                {
                    var answer = m_answers[i];
                    if (answer)
                    {
                        answer.ExplodeAnimTrigger();


                        if (AudioController.Ins)
                        {
                            AudioController.Ins.PlaySound(AudioController.Ins.right);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_answers.Count; i++)
                {
                    var answer = m_answers[i];
                    if (answer)
                    {
                        answer.OpenAnimTrigger();


                        if (AudioController.Ins)
                        {
                            AudioController.Ins.PlaySound(AudioController.Ins.wrong);
                        }
                    }
                }
            }
        }
        m_answers.Clear();
        m_isAnswerChecking = false;
        if (m_rightMoving == m_totatMatchItem)
        {
            Pref.bestMove = m_totalMoving;
            if (GUIManager.Ins)
            {
                GUIManager.Ins.gameOverDialog.Show(true);
                Debug.Log("Game over");

                if (AudioController.Ins)
                {
                    AudioController.Ins.PlaySound(AudioController.Ins.gameover);
                }

            }
        }
    }

    private void ClearGrid()
    {
        if (gridRoot == null) return;
        for (int i = 0; i < gridRoot.childCount; i++)
        {
            var child = gridRoot.GetChild(i);
            if (child)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void ShuffMatchItems()
    {

        if (m_matchItemsCoppy == null || m_matchItemsCoppy.Count == 0) return;
        for (int i = 0; i < m_matchItemsCoppy.Count; i++)
        {
            var temp = m_matchItemsCoppy[i];
            if (temp != null)
            {
                int ranIdx = Random.Range(0, m_matchItemsCoppy.Count);
                m_matchItemsCoppy[i] = m_matchItemsCoppy[ranIdx];
                m_matchItemsCoppy[ranIdx] = temp;
            }
        }
    }


}
