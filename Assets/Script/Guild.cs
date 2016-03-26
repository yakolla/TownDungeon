using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guild : MonoBehaviour
{

    CreatureSerializeFileds m_serializeFileds = new CreatureSerializeFileds();
    
    void Awake()
    {
        FileMgr.Deserialize(ref m_serializeFileds, "RefData/InsGuild");        
    }

    public StatsProp StatsProp
    {
        get { return m_serializeFileds.Stats; }
    }

}
