using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUDFamilyTreeNode : MonoBehaviour
{

    RectTransform m_rtThis = null;


    Vector2 m_size = Vector2.zero;

    public int paddingCount = 0;

    void Awake()
    {
        m_rtThis = GetComponent<RectTransform>();
        m_size = m_rtThis.sizeDelta;
    }
    // Update is called once per frame
    void Update()
    {
        m_rtThis.offsetMin = new Vector2(((m_size.x + PaddingWidth) * SiblingOrder) - ((m_size.x + PaddingWidth) * SiblingCount) * 0.5f, -m_size.y * 2);
        m_rtThis.offsetMax = m_rtThis.offsetMin + m_size;
    }

    public int SiblingOrder
    {
        get;
        set;
    }

    public int SiblingCount
    {
        get;
        set;
    }

    public float PaddingWidth
    {
        get;
        set;
    }
}