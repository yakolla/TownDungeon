using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FollowingCamera : MonoBehaviour {

	Vector3	m_startedPos;
	GameObject	m_target;

	void Update()
	{
		if (m_target != null)
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, m_target.transform.position, Time.deltaTime);

	}

	public void Watch(GameObject target)
	{
		m_target = target;
	}

}
