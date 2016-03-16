using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Item {

	[SerializeField]
	int			m_refItemID;

	[SerializeField]
	Texture2D	m_icon;

	StatsProp	m_statsProp = new StatsProp();

	public Item (int refItemID) {

		m_refItemID = refItemID;
		m_statsProp.Init(RefDataMgr.Instance.RefItems[m_refItemID].stats);
	}

	public Texture2D Icon
	{
		get {return m_icon;}
	}

	public StatsProp StatsProp
	{
		get {return m_statsProp;}
	}
}
