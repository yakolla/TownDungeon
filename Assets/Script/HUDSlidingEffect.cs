using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUDSlidingEffect : MonoBehaviour{

    [SerializeField]
    bool m_open = false;
    [SerializeField]
    float m_speed = 1f;
    [SerializeField]
    Vector2 m_offset = Vector2.zero;
    Vector2 m_goal = Vector2.zero;
    Vector2 m_init = Vector2.zero;
    Vector2 m_size;

    RectTransform m_rtThis;

    void Awake()
    {
        m_rtThis = GetComponent<RectTransform>();
        m_size = m_rtThis.sizeDelta;
        m_init = m_rtThis.offsetMin;
        m_goal = m_init;
    }

    public void Sliding(bool open)
    {
        m_open = open;

        if (m_open == true)
            m_goal = m_init + m_offset;
        else
            m_goal = m_init;
    }

    public void OnClickQuick()
    {
        m_open = !m_open;

        Sliding(m_open);
    }
    
    void Update()
    {
        m_rtThis.offsetMin = Vector2.MoveTowards(m_rtThis.offsetMin, m_goal, Time.deltaTime* m_speed);
        m_rtThis.offsetMax = m_rtThis.offsetMin+m_size;
    }
}
