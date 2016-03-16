using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


public class RefBaseData
{
	public int 				id;
}

[System.Serializable]
public class RefCreature : RefBaseData
{
	public Dictionary<StatsPropType, float>	stats;
}

[System.Serializable]
public class RefItem : RefBaseData
{
	//[JsonConverter(typeof(StringEnumConverter))]
	//public ItemData.Option  type;
	public Dictionary<StatsPropType, float>	stats;
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
		Deserialize(ref m_refCreatures, "RefCreatures");
		Deserialize(ref m_refItems, "RefItems");
	}
	
	void DeserializeArray<T>(Dictionary<int, T> records, string fileName) where T : RefBaseData
	{ 
		TextAsset textDocument =  Resources.Load("RefData/" + fileName) as TextAsset;
		
		T[] datas = JsonConvert.DeserializeObject<T[]>(textDocument.text);				
		foreach(T data in datas)
		{
			records[data.id] = data;
		}
		
	}
	
	void Deserialize<T>(ref T records, string fileName)
	{ 
		TextAsset textDocument =  Resources.Load("RefData/" + fileName) as TextAsset;
		
		records = JsonConvert.DeserializeObject<T>(textDocument.text);			
		
		
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
