using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreatureStatsMiniInfoPanel : MonoBehaviour {


	Image m_imageIcon;
	Text m_textName;
	Creature m_creature;
	Dictionary<StatsPropType, Text>	m_statsTexts = new Dictionary<StatsPropType, Text>();
	List<RawImage>	m_equipItems = new List<RawImage>();
	void Awake()
	{
		m_textName = transform.Find("ImageNameBG/TextName").GetComponent<Text>();
		m_imageIcon = transform.Find("ImageIcon").GetComponent<Image>();

		string[] statsNames = System.Enum.GetNames(typeof(StatsPropType));
		for (int i = 0; i < statsNames.Length; ++i)
		{
			Transform trans = transform.Find("StatsScrollView/Contents/Text" + statsNames[i] + "/TextValue");
			if (trans == null)
				continue;

			m_statsTexts.Add((StatsPropType)i, trans.GetComponent<Text>());			
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
		m_textName.text = creature.CreatureName;

        int i = 0;
        for (i = 0; i < m_equipItems.Count; ++i)
		{
			m_equipItems[i].texture = null;
		}

        if (creature.ItemInventory != null)
        {
            i = 0;
            foreach (var entry in creature.ItemInventory.EquipItems)
            {
                m_equipItems[i].texture = entry.Value.Icon;
                ++i;
            }
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
