using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
public enum StatsPropType
{
	HP,
	MAX_HP,
	STR,
	DEX,
	MAG,
	DEF,
	SIGHT,
	ATK_RANGE,
	ATK_SPEED,
	XP,
	DEATH_XP,
	GOLD,
	DEATH_GOLD,
    LEVEL,
	COUNT
}

public class StatsProp {

    [JsonProperty(PropertyName = "Props")]
    Dictionary<StatsPropType, float>	m_props = new Dictionary<StatsPropType, float>();
	Dictionary<StatsPropType, float>	m_baseProps = null;
    Dictionary<StatsPropType, float>    m_alphaProps = new Dictionary<StatsPropType, float>();

    public void Init(StatsProp baseProps)
	{
		m_baseProps = baseProps.m_props;
	}

	public float GetValue(StatsPropType type)
	{
		float baseValue = 0;
		float value = 0;
        float alphaValue = 0;
        if (true == m_baseProps.ContainsKey(type))
			baseValue = m_baseProps[type];
		if (true == m_props.ContainsKey(type))
			value = m_props[type];
        if (true == m_alphaProps.ContainsKey(type))
            alphaValue = m_alphaProps[type];

        return baseValue+value+ alphaValue;
	}

	public void SetValue(StatsPropType type, float value)
	{
		if (false == m_props.ContainsKey(type))
		{
			m_props.Add(type, value);
			return;
		}
		
		m_props[type] = value;
	}

    public void OffsetAlphaValue(StatsPropType type, float value)
    {
        if (false == m_alphaProps.ContainsKey(type))
        {
            m_alphaProps.Add(type, value);
            return;
        }

        m_alphaProps[type] += value;
    }

    public HashSet<StatsPropType> HasStatsPropTypes
    {
        get
        {
            HashSet<StatsPropType> set = new HashSet<StatsPropType>();
            if (m_baseProps != null)
            {
                foreach(var entry in m_baseProps)
                {
                    set.Add(entry.Key);
                }
            }

            foreach (var entry in m_props)
            {
                set.Add(entry.Key);
            }

            return set;
        }
    }

    public void ApplyAlpha(StatsProp props)
    {
        foreach (var type in props.HasStatsPropTypes)
        {
            OffsetAlphaValue(type, props.GetValue(type));
        }
    }

    public void UnApplyAlpha(StatsProp props)
    {
        foreach (var type in props.HasStatsPropTypes)
        {
            OffsetAlphaValue(type, -props.GetValue(type));
        }
    }
}
