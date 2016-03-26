using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Building : MonoBehaviour
{

    [SerializeField]
    GameObject m_hud;

    HashSet<Creature> m_creatures = new HashSet<Creature>();
    
    public GameObject HudGUI
    {
        get { return m_hud; }
    }

    void Start()
    {
        StartCoroutine(LoopChargeHP());

    }

    public void Enter(Creature creature)
    {
        m_creatures.Add(creature);
        creature.InBuilding = true;
    }

    IEnumerator LoopChargeHP()
    {
        List<Creature> m_remove = new List<Creature>();
        while(true)
        {
            foreach (var entry in m_creatures)
            {
                if (entry.HP == entry.MaxHP)
                {
                    m_remove.Add(entry);
                    continue;
                }

                entry.HP++;
            }

            foreach (var entry in m_remove)
            {
                entry.InBuilding = false;
                m_creatures.Remove(entry);
            }

            m_remove.Clear();
            yield return new WaitForSeconds(1f);
        }
        
    }
    
}
