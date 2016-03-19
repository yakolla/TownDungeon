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

    public int Level
    {
        get { return (int)Stats.GetValue(StatsPropType.LEVEL); }
        set { Stats.SetValue(StatsPropType.LEVEL, value); }
    }

    public int XPToNextLevel
    {
        get { return (Level + 1) * Helper.XPBlock; }
    }

    public int XP
    {
        get { return (int)Stats.GetValue(StatsPropType.XP); }
        set { Stats.SetValue(StatsPropType.XP, value); }
    }

    public bool LevelUp()
    {
        if (XP < XPToNextLevel)
            return false;

        XP = XP-XPToNextLevel;
        Level++;

        return true;
    }

}
