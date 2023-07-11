using UnityEngine;
using UnityEngine.UI;

public class MatchItemUI : MonoBehaviour
{
    private int m_id;
    public Sprite bg;
    public Sprite BackBg;
    public Image itemBG;
    public Image itemIcon;
    public Button btnComp;
    private bool m_isOpened;
    private Animator m_anim;

    public int Id { get => m_id; set => m_id = value; }

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    //uppdate trang thai dau tien
    public void UpdateFirstState(Sprite icon)
    {
        if (itemBG) //kiem tra itemBG neu itemBG khac null thi gan itemBG = BackGg
            itemBG.sprite = BackBg;
        if (itemIcon)//Kiem tra itemIcon khac null
        {
            itemIcon.sprite = icon; //cap nhat lai icon
            itemIcon.gameObject.SetActive(false);//kiem tra neu icon khac null thi an icon di
        }
    }

    //
    public void ChangeState()
    {
        m_isOpened = !m_isOpened;//khi stage thay doi se dao nguoc bien m_isOpened
        if (itemBG) //kiem tra neu itemBg khac null 
            itemBG.sprite = m_isOpened ? bg : BackBg;//cap nhat bg

        if (itemIcon) //kiem tra itemIcon khac null thi se hien thi icon len
            itemIcon.gameObject.SetActive(m_isOpened);//
    }

    public void OpenAnimTrigger()//kich hoat trang thai Flip
    {
        if (m_anim)//kiem tra neu m_anim khac null xe xet tham so 
            m_anim.SetBool(AnimState.Flip.ToString(), true);
    }

    public void ExplodeAnimTrigger() //kich hoat trang thai Explode
    {
        if (m_anim)
            m_anim.SetBool(AnimState.Explode.ToString(), true);
    }
    public void BackToIdle() //kich hoat trang thai Idle(quay lai trang thai ban dau)
    {
        if (m_anim)
            m_anim.SetBool(AnimState.Flip.ToString(), false);

        if (btnComp)
            btnComp.enabled = !m_isOpened;//neu isopen la true thi se an btncomp. neu isopen la false thi se bat btnComp
    }

}
