using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour
{

    [SerializeField]
    GameObject m_hud;

    public GameObject HudGUI
    {
        get { return m_hud; }
    }
}
