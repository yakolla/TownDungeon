using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GuildMemberDialogPanel : MonoBehaviour{

    [SerializeField]
    GameObject m_contents = null;
    [SerializeField]
    GameObject m_prefSlot = null;

    RectTransform m_rtContents = null;
    RectTransform m_rtSlot = null;
    Vector2 m_sizeSlot = Vector2.zero;
    Vector2 m_sizeContents = Vector2.zero;
    public void Awake()
    {
        m_rtContents = m_contents.GetComponent<RectTransform>();
        m_rtSlot = m_prefSlot.GetComponent<RectTransform>();

        m_sizeContents = m_rtContents.sizeDelta;
        m_sizeSlot = m_rtSlot.sizeDelta;
    }

    public void OnClickOpen()
    {
        gameObject.SetActive(true);

        HUDGuildMemberSlot[] slots = m_contents.GetComponentsInChildren<HUDGuildMemberSlot>();
        foreach( var slot in slots)
        {
            GameObject.Destroy(slot.gameObject);
        }
        
        Creature[] creatures = GameObject.Find("Heros").GetComponentsInChildren<Creature>();

        Vector2 scaleContents = new Vector2(m_sizeContents.x, Mathf.Max(4, creatures.Length) * m_sizeSlot.y);
        m_rtContents.offsetMin = new Vector2(m_rtContents.offsetMin.x, (m_sizeContents.y- scaleContents.y) /2);
        m_rtContents.offsetMax = m_rtContents.offsetMin+ scaleContents;

        for (int i = 0; i < creatures.Length; ++i)
        {
            GameObject obj = Instantiate(m_prefSlot) as GameObject;
            obj.transform.SetParent(m_contents.transform);

            HUDGuildMemberSlot slot = obj.GetComponent<HUDGuildMemberSlot>();
            slot.Init(creatures[i]);
            
            RectTransform rt = obj.GetComponent<RectTransform>();
            
            rt.offsetMin = new Vector2(-m_sizeSlot.x/2, m_rtContents.sizeDelta.y/2 - (i+1)*m_sizeSlot.y);
            rt.offsetMax = rt.offsetMin + m_sizeSlot;
            
        }

    }
    public void OnClickClose()
    {
        gameObject.SetActive(false);
    }
}
