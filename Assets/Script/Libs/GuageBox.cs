using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuageBox : MonoBehaviour {

	[SerializeField]
	Text			m_text;
	[SerializeField]
	Image			m_guage;
	[SerializeField]
	Image			m_shadowGuage;
	[SerializeField]
	Image			m_fullGuage;
	[SerializeField]
	float			m_duration = 2f;
	float			m_finishTime;

	Vector3			m_raiseUpStartTextPos;
	Vector3			m_raiseUpGoalTextPos;
	[SerializeField]
	float			m_raiseUpHeight = 3f;

	void Awake()
	{
		m_raiseUpStartTextPos = m_text.rectTransform.localPosition;
	}

	// Update is called once per frame
	void Update() 
	{
		if (0 < m_duration && m_finishTime < Time.time)
		{
			gameObject.SetActive(false);
		}

		m_text.rectTransform.localPosition = Vector3.Lerp(m_text.rectTransform.localPosition, m_raiseUpGoalTextPos, Time.deltaTime);
		m_shadowGuage.fillAmount += (m_guage.fillAmount-m_shadowGuage.fillAmount)*(Time.deltaTime*2);


	}

	public void Amount(string msg, float fill)
	{
		gameObject.SetActive(true);

		m_text.text = msg;
		m_guage.fillAmount = fill;
		m_fullGuage.gameObject.SetActive(m_guage.fillAmount >= 1);

		m_finishTime = Time.time+m_duration;

		m_text.rectTransform.localPosition = m_raiseUpStartTextPos;
		m_raiseUpGoalTextPos = m_raiseUpStartTextPos;
		m_raiseUpGoalTextPos.y += m_raiseUpHeight;
	}
}
