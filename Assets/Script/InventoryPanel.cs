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

    [SerializeField]
    GameObject m_prefItemSlot = null;

    RectTransform m_rtPrefItemSlot = null;
    RectTransform m_rtPivotEquipItems = null;
    RectTransform m_rtPivotBagItems = null;
    CreatureStatsInfoPanel m_creatureStatsInfoPanel = null;

    public void Clear()
	{
		for (int i = 0; i < m_equipItems.Count; ++i)
		{
            if (m_equipItems[i] == null)
                continue;

			Destroy(m_equipItems[i].gameObject);
		}

		m_equipItems.Clear();

		for (int i = 0; i < m_bagItems.Count; ++i)
		{
            if (m_bagItems[i] == null)
                continue;

			Destroy(m_bagItems[i].gameObject);
		}

		m_bagItems.Clear();

        PreComputeItemStats(null, false);

    }

	public void SetCreature(Creature creature)
	{
        if (m_rtPrefItemSlot == null)
        {
            m_rtPrefItemSlot = m_prefItemSlot.GetComponent<RectTransform>();
            m_rtPivotEquipItems = transform.Find("ImageEquipItems/Pivot").GetComponent<RectTransform>();
            m_rtPivotBagItems = transform.Find("ImageBag/ItemScrollView/Contents/Pivot").GetComponent<RectTransform>();

            m_creatureStatsInfoPanel = transform.parent.GetComponentInChildren<CreatureStatsInfoPanel>();
        }

        Clear();

        m_creature = creature;
        pushItems(m_rtPivotEquipItems, m_creature.ItemInventory.EquipItems, m_equipItems, true);
        pushItems(m_rtPivotBagItems, m_creature.ItemInventory.Items, m_bagItems, false);
        
	}
   
    Vector3 getSlotPos(RectTransform rtPivot, int slot)
    {
        float startX = rtPivot.transform.localPosition.x;
        float startY = rtPivot.transform.localPosition.y;

        return new Vector3(startX + m_rtPrefItemSlot.rect.width * (slot % Helper.ItemCols), startY - rtPivot.rect.height * (slot / Helper.ItemCols), 0);
    }

	void pushItems(RectTransform pivot, Dictionary<int, Item> items, List<HUDItemSlot> container, bool isEquip)
	{
		
        int i = container.Count;
        foreach ( var entry in items)
		{
			HUDItemSlot itemSlotObj = (Instantiate(m_prefItemSlot) as GameObject).GetComponent<HUDItemSlot>();
			itemSlotObj.Init(this, entry.Value, isEquip);
			
			itemSlotObj.transform.SetParent(pivot.transform.parent);
			itemSlotObj.transform.localPosition = getSlotPos(pivot, i);
			itemSlotObj.transform.localScale = m_prefItemSlot.transform.localScale;

            container.Add(itemSlotObj);
            ++i;
		}
	}    

	public HUDItemSlot SelectedItemSlot
	{
		get {return m_selectedSlot;}
		set {  m_selectedSlot = value;  }
	}

    void MoveItem(List<HUDItemSlot> from, List<HUDItemSlot> to, RectTransform pivot, Item item)
    {
        int toIndex = to.Count;
        for (int i = 0; i < to.Count; ++i)
        {
            if (to[i] == null)
            {
                toIndex = i;
                break;
            }
        }

        for (int i = 0; i < from.Count; ++i)
        {
            if (from[i] != null && from[i].Item == item)
            {
                from[i].transform.SetParent(pivot.transform.parent);
                from[i].transform.localPosition = getSlotPos(pivot, toIndex);
                from[i].transform.localScale = m_prefItemSlot.transform.localScale;

                if (toIndex < to.Count)
                    to[toIndex] = from[i];
                else
                    to.Add(from[i]);

                from[i] = null;
                break;
            }
        }
    }

    public bool EquipItem(Item item)
    {
        if (false == m_creature.ItemInventory.Equip(item))
            return false;

        MoveItem(m_bagItems, m_equipItems, m_rtPivotEquipItems, item);        
        
        return true;
    }

    public bool UnEquipItem(Item item)
    {
        if (false == m_creature.ItemInventory.UnEquip(item))
            return false;

        MoveItem(m_equipItems, m_bagItems, m_rtPivotBagItems, item);
        return true;
    }

    public void PreComputeItemStats(Item item, bool equip)
    {
        m_creatureStatsInfoPanel.PreComputeItemStats(item, equip);
    }

    public void LevelUpItem(Item item)
    {
        m_creature.ItemInventory.UnApplyStatsToCreature(item);
        item.LevelUp();
        m_creature.ItemInventory.ApplyStatsToCreature(item);
    }

    
}
