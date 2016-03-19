using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Item {


	[SerializeField]
	Texture2D	m_icon = null;

    public void Init()
    {
        RefItem refItem = RefDataMgr.Instance.RefItems[RefItemID];
        Stats.Init(refItem.Stats);
        RefItemID = refItem.id;
        m_icon = Resources.Load<Texture2D>("Sprites/"+refItem.iconName);
    }

	public Texture2D Icon
	{
		get {return m_icon;}
	}

	public StatsProp Stats
	{
		get; set;
	}

	public int RefItemID
	{
		get; set;
	}
}
