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
	Dictionary<StatsPropType, Text>	m_statsTexts = new Dictionary<StatsPropType, Text>();

	public void Init()
	{
		m_textCreatureName = transform.Find("ImageIcon/ImageCreatureDesc/TextName").GetComponent<Text>();
		m_textCreatureType = transform.Find("ImageIcon/ImageCreatureDesc/TextType").GetComponent<Text>();
		m_textCreatureDesc = transform.Find("ImageIcon/ImageCreatureDesc/TextDesc").GetComponent<Text>();
		m_imageIcon = transform.Find("ImageIcon").GetComponent<Image>();

		string[] statsNames = System.Enum.GetNames(typeof(StatsPropType));
		for (int i = 0; i < statsNames.Length; ++i)
		{
			Transform trans = transform.Find("ImageStats/Text" + statsNames[i] + "/TextValue");
			if (trans == null)
				continue;

			m_statsTexts.Add((StatsPropType)i, trans.GetComponent<Text>());			
		}


	}

	public void SetCreature(Creature creature)
	{

		m_creature = creature;
		m_imageIcon.sprite = Helper.Photo(creature.gameObject);
		m_textCreatureName.text = creature.CreatureName;
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
