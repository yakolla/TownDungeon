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

	public List<Item> Items
	{
		get {return m_items;}
	}

	public List<Item> EquipItems
	{
		get {return m_equipItems;}
	}
}
