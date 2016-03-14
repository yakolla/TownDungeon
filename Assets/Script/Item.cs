using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Item {

	[SerializeField]
	Texture2D	m_icon;

	[SerializeField]
	StatsProp	m_property;

	public void Init () {

		m_property.Init();
	}

	public Texture2D Icon
	{
		get {return m_icon;}
	}
}
