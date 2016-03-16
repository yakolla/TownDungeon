using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPanel : MonoBehaviour{

	HUDItemSlot m_selectedSlot;
	Creature	m_creature;
	List<HUDItemSlot> m_equipItems = new List<HUDItemSlot>();
	List<HUDItemSlot> m_bagItems = new List<HUDItemSlot>();

	void Clear()
	{
		for (int i = 0; i < m_equipItems.Count; ++i)
		{
			Destroy(m_equipItems[i].gameObject);
		}

		m_equipItems.Clear();

		for (int i = 0; i < m_bagItems.Count; ++i)
		{
			Destroy(m_bagItems[i].gameObject);
		}

		m_bagItems.Clear();
	}

	public void SetCreature(Creature creature)
	{
		m_creature = creature;
		initEquipItems();
		initBagItems();
	}

	void initEquipItems()
	{
		Transform equipItems = transform.Find("ImageEquipItems");

		GameObject prefItemSlot = Resources.Load("Pref/HUD/HudItemSlot") as GameObject;
		RectTransform rtItemSlot = prefItemSlot.GetComponent<RectTransform>();
		RectTransform rtPivot = equipItems.Find("Pivot").GetComponent<RectTransform>();
		float startX = rtPivot.transform.localPosition.x;
		float startY = rtPivot.transform.localPosition.y;
		for (int i = 0; i < m_creature.ItemInventory.EquipItems.Count; ++i)
		{
			HUDItemSlot itemSlotObj = (Instantiate(prefItemSlot) as GameObject).GetComponent<HUDItemSlot>();
			itemSlotObj.Init(this, m_creature.ItemInventory.EquipItems[i]);
			
			itemSlotObj.transform.parent = equipItems;
			itemSlotObj.transform.localPosition = new Vector3(startX+rtItemSlot.rect.width*i, startY, 0);
			itemSlotObj.transform.localScale = prefItemSlot.transform.localScale;

			m_equipItems.Add(itemSlotObj);
		}


	}

	void initBagItems()
	{
		Transform equipItems = transform.Find("ImageBag/ItemScrollView/Contents");
		
		GameObject prefItemSlot = Resources.Load("Pref/HUD/HudItemSlot") as GameObject;
		RectTransform rtItemSlot = prefItemSlot.GetComponent<RectTransform>();
		RectTransform rtPivot = equipItems.Find("Pivot").GetComponent<RectTransform>();
		float startX = rtPivot.transform.localPosition.x;
		float startY = rtPivot.transform.localPosition.y;
		for (int i = 0; i < m_creature.ItemInventory.Items.Count; ++i)
		{
			HUDItemSlot itemSlotObj = (Instantiate(prefItemSlot) as GameObject).GetComponent<HUDItemSlot>();
			itemSlotObj.Init(this, m_creature.ItemInventory.Items[i]);
			
			itemSlotObj.transform.parent = equipItems;
			itemSlotObj.transform.localPosition = new Vector3(startX+rtItemSlot.rect.width*(i%Helper.ItemCols), startY-rtPivot.rect.height*(i/Helper.ItemCols), 0);
			itemSlotObj.transform.localScale = prefItemSlot.transform.localScale;

			m_bagItems.Add(itemSlotObj);
		}
		
	}

	public HUDItemSlot SelectedItemSlot
	{
		get {return m_selectedSlot;}
		set {m_selectedSlot = value;}
	}


}
