using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class CreatureSerializeFileds {
    
	public int		RefCreatureID = 0;
    public StatsProp Stats = null;
    public ItemInventory ItemInventory = null;
    public Vector3 StartPos = Vector3.zero;

    public void Init(Creature creature)
    {
        Stats.Init(RefDataMgr.Instance.RefCreatures[RefCreatureID].Stats);
        if (ItemInventory != null)
            ItemInventory.Init(creature);
    }

}
