using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mobs : Creatures
{

	[SerializeField]
	float	m_spawnTime = 10f;

	[SerializeField]
	Transform	m_area;
	Rect		m_rtArea;
	// Use this for initialization
	void Start () {

        m_rtArea = new Rect((m_area.position.x - m_area.localScale.x / 2f), (m_area.position.z - m_area.localScale.z / 2f), m_area.localScale.x / 2f, m_area.localScale.z / 2f);
        StartCoroutine(LoopSpawn());
		
	}	

	IEnumerator LoopSpawn()
	{
        List<int> keyList = new List<int>(RefDataMgr.Instance.RefCreatures.Keys);

        while (true)
		{

            RefCreature randRefCreature = RefDataMgr.Instance.RefCreatures[keyList[Random.RandomRange(1, keyList.Count)]];
            CreatureSerializeFileds fileds = new CreatureSerializeFileds();
            fileds.RefCreatureID = randRefCreature.id;
            fileds.ItemInventory = randRefCreature.ItemInventory;
            fileds.Stats = new StatsProp();
            Spawn(fileds, new Vector3(Random.Range(m_rtArea.xMin, m_rtArea.width), 0, Random.Range(m_rtArea.yMin, m_rtArea.height)));
            yield return new WaitForSeconds(m_spawnTime);
		}

	}
}
