using UnityEngine;
using System.Collections;

public class ItemBoxs : MonoBehaviour {

	[SerializeField]
	GameObject	m_pref;

	[SerializeField]
	string[] m_prefabSkinNames;

	[SerializeField]
	string m_rootPath;

	// Use this for initialization
	void Start () {

	}	

	public void SpawnItemBox(Vector3 pos)
	{
		GameObject obj = Instantiate(m_pref) as GameObject;

		GameObject skinObj = Instantiate(Resources.Load(m_rootPath + m_prefabSkinNames[Random.Range(0, m_prefabSkinNames.Length)])) as GameObject;
		skinObj.transform.parent = obj.transform;
		skinObj.transform.name = "Skin";

		obj.transform.parent = transform;
		obj.transform.position = pos;
	}
}
