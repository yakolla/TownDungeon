using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPanel : MonoBehaviour{

	HUDItemSlot m_selectedSlot;

	List<HUDItemSlot> m_equipItems = new List<HUDItemSlot>();

	public void Init()
	{
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
		for (int i = 0; i < Helper.ItemCols; ++i)
		{
			HUDItemSlot itemSlotObj = (Instantiate(prefItemSlot) as GameObject).GetComponent<HUDItemSlot>();
			itemSlotObj.Init(this);
			
			itemSlotObj.transform.parent = equipItems;
			itemSlotObj.transform.localPosition = new Vector3(startX+rtItemSlot.rect.width*i, startY, 0);
			itemSlotObj.transform.localScale = prefItemSlot.transform.localScale;
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
		for (int i = 0; i < 8; ++i)
		{
			HUDItemSlot itemSlotObj = (Instantiate(prefItemSlot) as GameObject).GetComponent<HUDItemSlot>();
			itemSlotObj.Init(this);
			
			itemSlotObj.transform.parent = equipItems;
			itemSlotObj.transform.localPosition = new Vector3(startX+rtItemSlot.rect.width*(i%Helper.ItemCols), startY-rtPivot.rect.height*(i/Helper.ItemCols), 0);
			itemSlotObj.transform.localScale = prefItemSlot.transform.localScale;
		}
		
	}

	public HUDItemSlot SelectedItemSlot
	{
		get {return m_selectedSlot;}
		set {m_selectedSlot = value;}
	}


}
