using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponInfoPanel : MonoBehaviour {

	CreatureStatsInfoPanel	m_creatureStatsInfoPanel;
	InventoryPanel	m_inventoryPanel;

    Creature[] m_heros = null;
    int m_index = 0;

	void Awake()
	{
		m_creatureStatsInfoPanel = transform.parent.GetComponentInChildren<CreatureStatsInfoPanel>();
		m_creatureStatsInfoPanel.Init();

		m_inventoryPanel = transform.parent.GetComponentInChildren<InventoryPanel>();
	}

	void OnEnable()
	{
        m_heros = GameObject.Find("Heros").GetComponentsInChildren<Creature>();
        if (m_heros.Length == 0)
        {
            m_creatureStatsInfoPanel.Clear();
            m_inventoryPanel.Clear();
            return;
        }

        m_index = 0;
        SetCreature(m_heros[m_index]);

    }

	public void OnClickButtonClose()
	{
		gameObject.SetActive(false);
	}

	void SetCreature(Creature creature)
	{
        m_creatureStatsInfoPanel.SetCreature(creature);
        m_inventoryPanel.SetCreature(creature);
    }

    public void OnClickNext()
    {
        if (m_heros.Length == 0)
            return;

        m_index = (m_index + 1) % m_heros.Length;
        SetCreature(m_heros[m_index]);
    }

    public void OnClickPrev()
    {
        if (m_heros.Length == 0)
            return;

        m_index = (m_index - 1) % m_heros.Length;
        SetCreature(m_heros[m_index]);
    }
}
