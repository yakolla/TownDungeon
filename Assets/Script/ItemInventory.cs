using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Newtonsoft.Json.Converters;

public class ItemInventory {

    Creature m_creature = null;

    public void Init(Creature creature)
    {
        m_creature = creature;
        foreach (var entry in EquipItems)
        {
            entry.Value.Init();
            ApplyStatsToCreature(entry.Value);
        }

        foreach (var entry in Items)
        {
            entry.Value.Init();
        }
    }

	public Dictionary<int, Item> Items
	{
		get; set;
	}


	public Dictionary<int, Item> EquipItems
	{
		get; set;
	}

    void ApplyStatsToCreature(Item item)
    {
        m_creature.StatsProp.ApplyAlpha(item.Stats);
    }

	public void PutOn(Item item)
	{
        if (EquipItems.ContainsKey(item.RefItemID))
        {
            EquipItems[item.RefItemID].XP++;
            return;
        }

		if (Items.ContainsKey(item.RefItemID))
			Items[item.RefItemID].XP++;
		else
			Items.Add(item.RefItemID, item);
	}

    public bool Equip(Item item)
    {
        if (false == Items.ContainsKey(item.RefItemID))
            return false;

        if (EquipItems.Count + 1 == Helper.ItemCols)
            return false;

        if (EquipItems.ContainsKey(item.RefItemID) == true)
            return false;

        EquipItems.Add(item.RefItemID, item);
        Items.Remove(item.RefItemID);

        ApplyStatsToCreature(item);
        return true;

    }
}
