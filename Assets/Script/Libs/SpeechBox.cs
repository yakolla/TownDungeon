using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBox : MonoBehaviour {

	[SerializeField]
	Text			m_text;
	[SerializeField]
	float			m_duration = 2f;
	float			m_finishTime;

	RectTransform	m_rectTran;

	// Update is called once per frame
	void Update () 
	{
		if (m_finishTime < Time.time)
		{
			gameObject.SetActive(false);
		}	
	}

	public void Speech(string msg)
	{
		if (m_rectTran == null)
			m_rectTran = GetComponent<RectTransform>();

		m_rectTran.sizeDelta = new Vector2( msg.Length, m_rectTran.sizeDelta.y);		

		gameObject.SetActive(true);
		m_text.text = msg;
		m_finishTime = Time.time+m_duration;
	}
}
