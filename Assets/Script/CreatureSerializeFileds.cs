using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class CreatureSerializeFileds {
    
	public int		RefCreatureID = 1;
    public StatsProp Stats;
    public ItemInventory ItemInventory = null;

    public void Init()
    {
        Stats.Init(RefDataMgr.Instance.RefCreatures[RefCreatureID].Stats);
        if (ItemInventory != null)
            ItemInventory.Init();
    }

}
