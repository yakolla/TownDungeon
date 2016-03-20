using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public enum WeaponType
{
    Sword,
    Arrow,
    Meteo,
    IceBall,
    IceMeteo,
    Thunder,
    COUNT
}

public class RefCreature
{
    public int id;
    public StatsProp Stats;
    public ItemInventory ItemInventory;
    [JsonConverter(typeof(StringEnumConverter))]
    public WeaponType WeaponType;

}

public class RefItem
{
    public int id;
    public string iconName;
	public StatsProp Stats;
    public StatsProp LevelUpStats;
}


public class RefDataMgr {
	
	
	Dictionary<int, RefCreature>		m_refCreatures = new Dictionary<int, RefCreature>();
	Dictionary<int, RefItem>			m_refItems = new Dictionary<int, RefItem>();

	static RefDataMgr m_ins = null;
	static public RefDataMgr Instance
	{
		get {
			
			if (m_ins == null)
			{
				m_ins = new RefDataMgr();
				m_ins.Load();
			}
			
			return m_ins;
		}

	}
	
	void Load()
	{
		FileMgr.Deserialize(ref m_refCreatures, "RefData/RefCreatures");
		FileMgr.Deserialize(ref m_refItems, "RefData/RefItems");
	}

	public Dictionary<int, RefItem> RefItems
	{
		get {return m_refItems;}
	}

	public Dictionary<int, RefCreature> RefCreatures
	{
		get {return m_refCreatures;}
	}
}
