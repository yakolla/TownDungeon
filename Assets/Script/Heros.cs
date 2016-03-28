using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Heros : Creatures
{
    [SerializeField]
    Transform m_area;
    Rect m_rtArea;
    // Use this for initialization
    void Start () {

        m_rtArea = new Rect((m_area.position.x - m_area.localScale.x / 2f), (m_area.position.z - m_area.localScale.z / 2f), m_area.position.x + m_area.localScale.x, m_area.position.z + m_area.localScale.z);

        StartCoroutine(LoopSpawn());
        
    }

    IEnumerator LoopSpawn()
    {
        Dictionary<int, CreatureSerializeFileds> heros = new Dictionary<int, CreatureSerializeFileds>();
        FileMgr.Deserialize(ref heros, "RefData/InsHeros");

        foreach (var entry in heros)
        {
            Spawn(entry.Value, RefDataMgr.Instance.RefCreatures[entry.Value.RefCreatureID].SkinName, new Vector3(Random.Range(m_rtArea.xMin, m_rtArea.width), 0, Random.Range(m_rtArea.yMin, m_rtArea.height)), Vector3.one);
            yield return new WaitForSeconds(5f);
        }

    }
}
