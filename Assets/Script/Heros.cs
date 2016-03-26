using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Heros : Creatures
{

    // Use this for initialization
    void Start () {

        
        StartCoroutine(LoopSpawn());
        
    }

    IEnumerator LoopSpawn()
    {
        Dictionary<int, CreatureSerializeFileds> heros = new Dictionary<int, CreatureSerializeFileds>();
        FileMgr.Deserialize(ref heros, "RefData/InsHeros");

        foreach (var entry in heros)
        {
            Spawn(entry.Value, Vector3.zero, Vector3.one);
            yield return new WaitForSeconds(5f);
        }

    }
}
