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
		initEquipItems(transform.Find("ImageEquipItems"), m_creature.ItemInventory.EquipItems, m_equipItems);
        initEquipItems(transform.Find("ImageBag/ItemScrollView/Contents"), m_creature.ItemInventory.Items, m_bagItems);
        
	}

	void initEquipItems(Transform equipItems, Dictionary<int, Item> items, List<HUDItemSlot> container)
	{
		GameObject prefItemSlot = Resources.Load("Pref/HUD/HudItemSlot") as GameObject;
		RectTransform rtItemSlot = prefItemSlot.GetComponent<RectTransform>();
		RectTransform rtPivot = equipItems.Find("Pivot").GetComponent<RectTransform>();
		float startX = rtPivot.transform.localPosition.x;
		float startY = rtPivot.transform.localPosition.y;

        int i = 0;
        foreach ( var entry in items)
		{
			HUDItemSlot itemSlotObj = (Instantiate(prefItemSlot) as GameObject).GetComponent<HUDItemSlot>();
			itemSlotObj.Init(this, entry.Value);
			
			itemSlotObj.transform.SetParent(equipItems);
			itemSlotObj.transform.localPosition = new Vector3(startX + rtItemSlot.rect.width * (i % Helper.ItemCols), startY - rtPivot.rect.height * (i / Helper.ItemCols), 0);
			itemSlotObj.transform.localScale = prefItemSlot.transform.localScale;

            container.Add(itemSlotObj);
            ++i;
		}


	}    

	public HUDItemSlot SelectedItemSlot
	{
		get {return m_selectedSlot;}
		set {m_selectedSlot = value;}
	}


}
