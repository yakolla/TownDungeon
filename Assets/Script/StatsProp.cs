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

	public void Init(StatsProp baseProps)
	{
		m_baseProps = baseProps.m_props;
	}

	public float GetValue(StatsPropType type)
	{
		float baseValue = 0;
		float alphaValue = 0;
		if (true == m_baseProps.ContainsKey(type))
			baseValue = m_baseProps[type];
		if (true == m_props.ContainsKey(type))
			alphaValue = m_props[type];

		return baseValue+alphaValue;
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

}
