using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private Button m_button;

    private void Awake()
    {
        m_button = GetComponent<Button>();
    }

    private void Start()
    {
        if (m_button == null) return;
        m_button.onClick.AddListener(() => PlaySound());
    }

    private void PlaySound()
    {
        if (AudioController.Ins == null) return;
        AudioController.Ins.PlaySound(AudioController.Ins.button);
    }
}
