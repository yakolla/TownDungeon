using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Newtonsoft.Json.Converters;

public class ItemInventory {

    public void Init()
    {
        foreach(var entry in EquipItems)
        {
            entry.Value.Init();
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

	public void PutOnBag(Item item)
	{
		if (Items.ContainsKey(item.RefItemID))
			Items[item.RefItemID].Stats.SetValue(StatsPropType.XP, Items[item.RefItemID].Stats.GetValue(StatsPropType.XP) + 1);
		else
			Items.Add(item.RefItemID, item);
	}
}
