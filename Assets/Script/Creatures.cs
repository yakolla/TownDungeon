using UnityEngine;
using System.Collections;

public class Creatures : MonoBehaviour {

	[SerializeField]
	GameObject	m_pref;
	[SerializeField]
	GameObject	m_canvasPref;

	[SerializeField]
	string[] m_prefabSkinNames;

	[SerializeField]
	string m_rootPath;

	
	// Use this for initialization
	

    public Creature Spawn(CreatureSerializeFileds fileds, Vector3 pos)
    {
        GameObject obj = Instantiate(m_pref) as GameObject;

        GameObject skinObj = Instantiate(Resources.Load(m_rootPath + m_prefabSkinNames[Random.Range(0, m_prefabSkinNames.Length)])) as GameObject;
        skinObj.transform.parent = obj.transform;
        skinObj.transform.name = "Skin";

        GameObject canvasObj = Instantiate(m_canvasPref) as GameObject;
        canvasObj.transform.SetParent(obj.transform);
        canvasObj.transform.name = "Canvas";

        obj.transform.SetParent(transform);
        obj.transform.position = pos;
        

        Creature creature = obj.GetComponent<Creature>();
        creature.CreatureSerializeFileds = fileds;

        return creature;
    }
			
}
