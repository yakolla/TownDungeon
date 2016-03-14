using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponInfoPanel : MonoBehaviour {

	CreatureStatsInfoPanel	m_creatureStatsInfoPanel;
	InventoryPanel	m_inventoryPanel;

	Creature	m_activeCreature;

	void Awake()
	{
		m_creatureStatsInfoPanel = transform.parent.GetComponentInChildren<CreatureStatsInfoPanel>();
		m_creatureStatsInfoPanel.Init();

		m_inventoryPanel = transform.parent.GetComponentInChildren<InventoryPanel>();
		m_inventoryPanel.Init();
	}

	void OnEnable()
	{
		m_activeCreature = GameObject.Find("Heros").GetComponentInChildren<Hero>();
		m_creatureStatsInfoPanel.SetCreature(m_activeCreature);
	}

	public void OnClickButtonClose()
	{
		gameObject.SetActive(false);
	}

	public Creature ActiveCreature
	{
		get {return m_activeCreature;}
	}
}
