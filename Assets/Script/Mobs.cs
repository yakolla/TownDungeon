using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mobs : Creatures
{

    [SerializeField]
    float m_spawnTime = 10f;
    [SerializeField]
    float m_maxAliveMobs = 3;

    [SerializeField]
    Transform m_area;
    Rect m_rtArea;
    // Use this for initialization
    void Start()
    {

        m_rtArea = new Rect((m_area.position.x - m_area.localScale.x / 2f), (m_area.position.z - m_area.localScale.z / 2f), m_area.position.x+m_area.localScale.x, m_area.position.z+m_area.localScale.z);
        StartCoroutine(LoopSpawn());

    }

    IEnumerator LoopSpawn()
    {
        List<int> keyList = RefDataMgr.Instance.RefMobs;
        
        while (true)
        {
            Creature[] aliveMobs = GetComponentsInChildren<Creature>();
            if (aliveMobs.Length < m_maxAliveMobs)
            {
                bool boss = Random.RandomRange(0, 100) < 50 ? true : false;
                Vector3 scale = Vector3.one;
                if (boss == true)
                    scale *= 1.8f;

                RefCreature randRefCreature = RefDataMgr.Instance.RefCreatures[Random.RandomRange(keyList[0], keyList[1]+1)];
                CreatureSerializeFileds fileds = new CreatureSerializeFileds();
                fileds.RefCreatureID = randRefCreature.id;
                fileds.ItemInventory = randRefCreature.ItemInventory;
                fileds.Stats = new StatsProp();
                Creature mob = Spawn(fileds, randRefCreature.SkinName, new Vector3(Random.Range(m_rtArea.xMin, m_rtArea.width), 0, Random.Range(m_rtArea.yMin, m_rtArea.height)), scale);
                StartCoroutine(LoopAppearEffect(mob));

            }

            yield return new WaitForSeconds(m_spawnTime);
        }

    }

    IEnumerator LoopAppearEffect(Creature mob)
    {
        int backupLayer = mob.gameObject.layer;
        mob.gameObject.layer = 0;
        Vector3 start = mob.transform.position;
        start.y = 0;
        Vector3 end = start;
        end.y = 0;
        float elapse = 0f;
        while (true)
        {
            mob.transform.position = TrajectoryParabola.Update(start, end, 3f, elapse+=Time.deltaTime);
            if (mob.transform.position.y <= 0)
                break;
            yield return null;
        }

        mob.transform.position = start;
        mob.gameObject.layer = backupLayer;

    }
}