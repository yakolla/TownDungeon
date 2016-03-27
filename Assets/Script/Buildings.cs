using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buildings : Creatures
{
   // Use this for initialization
    void Start () {

        Dictionary<int, CreatureSerializeFileds> creatures = new Dictionary<int, CreatureSerializeFileds>();
        FileMgr.Deserialize(ref creatures, "RefData/InsBuildings");

        foreach (var entry in creatures)
        {
            Creature creature = Spawn(entry.Value, RefDataMgr.Instance.RefCreatures[entry.Value.RefCreatureID].SkinName, entry.Value.StartPos, Vector3.one);
            creature.gameObject.name = RefDataMgr.Instance.RefCreatures[entry.Value.RefCreatureID].SkinName;
        }

    }
   
}
