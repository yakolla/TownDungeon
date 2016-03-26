using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResourceMniInfoPanel : MonoBehaviour {


    [SerializeField]
    Text m_goldText;	

    void Update()
    {
        m_goldText.text = NumberString.ToSI(Helper.Guild.StatsProp.GetValue(StatsPropType.GOLD)) + "G";
    }
    
}
