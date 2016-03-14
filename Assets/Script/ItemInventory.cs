using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class ItemInventory {

	[SerializeField]
	List<Item>	m_equipItems;

	[SerializeField]
	List<Item>	m_items;

	public void Init () {
		for (int i = 0; i < m_equipItems.Count; ++i)
		{
			m_equipItems[i].Init();
		}

		for (int i = 0; i < m_items.Count; ++i)
		{
			m_items[i].Init();
		}
	}

	public List<Item> Items
	{
		get {return m_items;}
	}

	public List<Item> EquipItems
	{
		get {return m_equipItems;}
	}
}
