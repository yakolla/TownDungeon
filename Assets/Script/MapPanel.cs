using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler {

	Vector2	m_startedPos;
	Vector3	m_goalPos;
	GameObject m_followingTarget;

	CreatureStatsMiniInfoPanel	m_creatureStatsInfoPanel;
	GameObject				m_weaponInfoPanel;
	void Awake()
	{
		Application.runInBackground = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        m_creatureStatsInfoPanel = transform.parent.GetComponentInChildren<CreatureStatsMiniInfoPanel>();
		m_weaponInfoPanel = transform.parent.transform.Find("WeaponInfoPanel").gameObject;
		m_goalPos = Camera.main.transform.position;
		RefDataMgr.Instance.GetType();
	}

	void Start()
	{
		//Follow(GameObject.Find("Heros/Hero").transform.gameObject);
	}

	void Follow(GameObject target)
	{
		m_followingTarget = target;
		if (m_followingTarget != null)
		{
			if (m_followingTarget.layer == Helper.LayerMaskBuilding)
			{
				m_weaponInfoPanel.SetActive(true);
			}
			else
			{
				m_weaponInfoPanel.SetActive(false);
				Creature creature = m_followingTarget.GetComponent<Creature>();
				if (creature != null)
				{
					m_creatureStatsInfoPanel.SetCreature(creature);
				}
			}
		}
		else
		{
			m_goalPos = Camera.main.transform.position;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Follow(InputMgr.PickUpObject(eventData.position, (1<<Helper.LayerMaskHero)|(1<<Helper.LayerMaskMob)|(1<<Helper.LayerMaskBuilding)));
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		m_startedPos = eventData.position;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 dis = m_startedPos-eventData.position;
		dis.z = dis.y;
		dis.y = 0;
		m_goalPos = Camera.main.transform.position + dis.normalized * Mathf.Min(20f, dis.magnitude);
	}

	void Update()
	{
		if (m_followingTarget != null)
		{
			Vector2 aa = Camera.main.WorldToScreenPoint(m_followingTarget.transform.position);
			m_goalPos = Camera.main.ScreenToWorldPoint(aa);
			m_goalPos.y = Camera.main.transform.position.y;
		}
		Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, m_goalPos, Vector3.Distance(Camera.main.transform.position, m_goalPos)*Time.deltaTime*0.5f);
	}

}
