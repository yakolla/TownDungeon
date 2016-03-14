using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class StatsProp {
	public enum Type
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
	public struct Prop
	{
		public Type type;
		public float value;
	}

	[SerializeField]
	List<Prop>	m_propsForInspector;

	Dictionary<Type, float>	m_props = new Dictionary<Type, float>();

	public void Init()
	{
		for(int i = 0; i < m_propsForInspector.Count; ++i)
		{
			m_props.Add(m_propsForInspector[i].type, m_propsForInspector[i].value);
		}
	}

	public float GetValue(Type type)
	{
		if (false == m_props.ContainsKey(type))
			return 0f;

		return m_props[type];
	}

	public void SetValue(Type type, float value)
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
		get { return (int)GetValue(Type.HP);}
		set 
		{
			SetValue(Type.HP, Mathf.Clamp(value, 0f, GetValue(Type.MAX_HP)));
		}
	}

	public int MaxXP
	{
		get {return Level*Helper.XPBlock;}
	}

	public int Level
	{
		get {return (int)(1+GetValue(Type.XP)/Helper.XPBlock);}
	}


}
