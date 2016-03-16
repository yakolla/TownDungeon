﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	COUNT
}

[System.Serializable]
public class StatsProp {


	[System.Serializable]
	public struct Prop
	{
		public StatsPropType type;
		public float value;
	}

	Dictionary<StatsPropType, float>	m_props = new Dictionary<StatsPropType, float>();
	Dictionary<StatsPropType, float>	m_baseProps = null;

	public void Init(Dictionary<StatsPropType, float> baseProps)
	{
		m_baseProps = baseProps;
		HP = (int)m_baseProps[StatsPropType.MAX_HP];
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

	public int HP
	{
		get { return (int)GetValue(StatsPropType.HP);}
		set 
		{
			SetValue(StatsPropType.HP, Mathf.Clamp(value, 0f, GetValue(StatsPropType.MAX_HP)));
		}
	}

	public int MaxXP
	{
		get {return Level*Helper.XPBlock;}
	}

	public int Level
	{
		get {return (int)(1+GetValue(StatsPropType.XP)/Helper.XPBlock);}
	}


}
