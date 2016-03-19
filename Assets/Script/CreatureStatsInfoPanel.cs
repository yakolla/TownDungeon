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
    Dictionary<StatsPropType, Text> m_statsOffsetTexts = new Dictionary<StatsPropType, Text>();

    public void Init()
	{
		m_textCreatureName = transform.Find("ImageIcon/ImageCreatureDesc/TextName/Text").GetComponent<Text>();
		m_textCreatureType = transform.Find("ImageIcon/ImageCreatureDesc/TextType/Text").GetComponent<Text>();
		m_textCreatureDesc = transform.Find("ImageIcon/ImageCreatureDesc/TextDesc/Text").GetComponent<Text>();
		m_imageIcon = transform.Find("ImageIcon").GetComponent<Image>();

		string[] statsNames = System.Enum.GetNames(typeof(StatsPropType));
		for (int i = 0; i < statsNames.Length; ++i)
		{
			Transform trans = transform.Find("ImageStats/Text" + statsNames[i] + "/TextValue");
			if (trans == null)
				continue;

			m_statsTexts.Add((StatsPropType)i, trans.GetComponent<Text>());
            m_statsOffsetTexts.Add((StatsPropType)i, trans.Find("TextOffsetValue").GetComponent<Text>());

        }
	}

    public void Clear()
    {
        m_textCreatureName.text = "";
        m_textCreatureType.text = "";
        m_textCreatureDesc.text = "";
        m_imageIcon.sprite = null;
        foreach (var entry in m_statsTexts)
        {
            entry.Value.text = "";
        }
    }

	public void SetCreature(Creature creature)
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

    public void PreComputeItemStats(Item item, bool equip)
    {
        string flag = equip == true ? "+" : "-";
        foreach (var entry in m_statsOffsetTexts)
        {
            if (item == null)
            {
                entry.Value.text = "";
            }
            else
            {
                int value = (int)item.Stats.GetValue(entry.Key);
                if (value == 0)
                    entry.Value.text = "";
                else
                    entry.Value.text = flag + value;
            }
        }
    }
}
