using UnityEngine;
using System.Collections;

public class Creatures : MonoBehaviour {

	[SerializeField]
	GameObject	m_pref;
	[SerializeField]
	GameObject	m_canvasPref;    

	[SerializeField]
	string m_rootPath;

	
	// Use this for initialization
	

    public Creature Spawn(CreatureSerializeFileds fileds, string prefabSkinName, Vector3 pos, Vector3 scale)
    {
        GameObject obj = Instantiate(m_pref) as GameObject;

        GameObject skinObj = Instantiate(Resources.Load(m_rootPath + prefabSkinName)) as GameObject;
        skinObj.transform.parent = obj.transform;
        skinObj.transform.name = "Skin";

        GameObject canvasObj = Instantiate(m_canvasPref) as GameObject;
        canvasObj.transform.SetParent(obj.transform);
        canvasObj.transform.name = "Canvas";

        obj.transform.SetParent(transform);
        obj.transform.position = pos;
        obj.transform.localScale = scale;

        Creature creature = obj.GetComponent<Creature>();
        creature.CreatureSerializeFileds = fileds;

        return creature;
    }
			
}
