using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreatureStatsInfoPanel : MonoBehaviour {


	Image m_imageIcon;
	Text m_textCreatureName;
	Text m_textCreatureType;
	Text m_textCreatureDesc;
	Creature m_creature;
	Dictionary<StatsProp.Type, Text>	m_statsTexts = new Dictionary<StatsProp.Type, Text>();
	List<RawImage>	m_equipItems = new List<RawImage>();
	public void Init()
	{
		m_textCreatureName = transform.Find("ImageIcon/ImageCreatureDesc/TextName").GetComponent<Text>();
		m_textCreatureType = transform.Find("ImageIcon/ImageCreatureDesc/TextType").GetComponent<Text>();
		m_textCreatureDesc = transform.Find("ImageIcon/ImageCreatureDesc/TextDesc").GetComponent<Text>();
		m_imageIcon = transform.Find("ImageIcon").GetComponent<Image>();

		string[] statsNames = System.Enum.GetNames(typeof(StatsProp.Type));
		for (int i = 0; i < statsNames.Length; ++i)
		{
			Transform trans = transform.Find("ImageStats/Text" + statsNames[i] + "/TextValue");
			if (trans == null)
				continue;

			m_statsTexts.Add((StatsProp.Type)i, trans.GetComponent<Text>());			
		}

		for (int i = 0; true ; ++i)
		{
			Transform trans = transform.Find("EquipItems/ImageEquipItem" + i);
			if (trans == null)
				break;
			
			m_equipItems.Add(trans.GetComponent<RawImage>());			
		}
	}

	public void SetCreature(Creature creature)
	{

		m_creature = creature;
		m_imageIcon.sprite = Helper.Photo(creature.gameObject);
		m_textCreatureName.text = creature.CreatureName;

		for (int i = 0; i < m_equipItems.Count; ++i)
		{
			m_equipItems[i].texture = null;
		}

		for (int i = 0; i < Mathf.Min(creature.ItemInventory.EquipItems.Count, m_equipItems.Count); ++i)
		{
			m_equipItems[i].texture = creature.ItemInventory.EquipItems[i].Icon;
		}

	}

	void Update()
	{
		if (m_creature != null)
		{
			foreach (var a in m_statsTexts)
			{
				a.Value.text = ((int)(m_creature.StatsProp.GetValue(a.Key))).ToString();
			}
		}

	}
}
