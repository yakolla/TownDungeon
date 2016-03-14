using UnityEngine;
using System.Collections;

public class Mobs : MonoBehaviour {

	[SerializeField]
	GameObject	m_mobPref;
	[SerializeField]
	GameObject	m_canvasPref;

	[SerializeField]
	string[] m_prefabSkinNames;

	[SerializeField]
	string m_rootPath;

	[SerializeField]
	float	m_spawnTime = 10f;

	[SerializeField]
	Transform	m_area;
	Rect		m_rtArea;
	// Use this for initialization
	void Start () {
		Application.runInBackground = true;
		StartCoroutine(LoopSpawn());
		m_rtArea = new Rect((m_area.position.x-m_area.localScale.x/2), (m_area.position.z-m_area.localScale.z/2), m_area.localScale.x/2, m_area.localScale.z/2);
	}	

	IEnumerator LoopSpawn()
	{
		while(true)
		{
			GameObject obj = Instantiate(m_mobPref) as GameObject;

			GameObject skinObj = Instantiate(Resources.Load(m_rootPath + m_prefabSkinNames[Random.Range(0, m_prefabSkinNames.Length)])) as GameObject;
			skinObj.transform.parent = obj.transform;
			skinObj.transform.name = "Skin";

			GameObject canvasObj = Instantiate(m_canvasPref) as GameObject;
			canvasObj.transform.parent = obj.transform;
			canvasObj.transform.name = "Canvas";

			obj.transform.parent = transform;
			obj.transform.position = new Vector3(Random.Range(m_rtArea.xMin, m_rtArea.width), 0, Random.Range(m_rtArea.yMin, m_rtArea.height));
			yield return new WaitForSeconds(m_spawnTime);
		}

	}
}
