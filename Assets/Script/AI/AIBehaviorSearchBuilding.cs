using UnityEngine;
using System.Collections;

public class AIBehaviorSearchBuilding : AIBehavior {

	protected Creature		m_creature;
    string m_buildingName;

	public AIBehaviorSearchBuilding(Creature creature, string buildingName)
	{
		m_creature = creature;
        m_buildingName = buildingName;
	}

	public override void Start()
	{
        GameObject buildingObj = GameObject.Find("Buildings/" + m_buildingName) as GameObject;
        m_creature.AIAgent.Target = buildingObj;

	}

	public override AIBehaviorResultType Update()
	{
        return AIBehaviorResultType.SUCCESS;		
	}
}
