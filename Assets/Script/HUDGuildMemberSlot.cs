using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUDGuildMemberSlot : MonoBehaviour {


	Image m_imageIcon;
	Text m_textCreatureName;
	Text m_textCreatureType;
	Creature m_creature;
	Dictionary<StatsPropType, Text>	m_statsTexts = new Dictionary<StatsPropType, Text>();

    public void Init(Creature creature)
	{
		m_textCreatureName = transform.Find("ImageIcon/TextName").GetComponent<Text>();
		m_textCreatureType = transform.Find("ImageIcon/TextType").GetComponent<Text>();
		m_imageIcon = transform.Find("ImageIcon").GetComponent<Image>();

		string[] statsNames = System.Enum.GetNames(typeof(StatsPropType));
		for (int i = 0; i < statsNames.Length; ++i)
		{
			Transform trans = transform.Find("ImageIcon/ScrollRect/Contents/Stats/Text" + statsNames[i]);
			if (trans == null)
				continue;

			m_statsTexts.Add((StatsPropType)i, trans.GetComponent<Text>());

        }

        SetCreature(creature);

    }    

	void SetCreature(Creature creature)
	{
		m_creature = creature;
		m_imageIcon.sprite = Helper.Photo(creature.gameObject);
		m_textCreatureName.text = creature.CreatureName;
        m_textCreatureType.text = RefDataMgr.Instance.RefCreatures[creature.RefCreatureID].WeaponType.ToString();

    }

	void Update()
	{
		if (m_creature != null)
		{
			foreach (var entry in m_statsTexts)
			{
				entry.Value.text = ((int)(m_creature.StatsProp.GetValue(entry.Key))).ToString();
			}
		}

	}
   
}
