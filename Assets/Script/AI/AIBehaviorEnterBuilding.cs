using UnityEngine;
using System.Collections;

public class AIBehaviorEnterBuilding : AIBehavior {

	protected Creature		m_creature;

	public AIBehaviorEnterBuilding(Creature creature)
	{
		m_creature = creature;
	}

	public override void Start()
	{
        
    }

	public override AIBehaviorResultType Update()
	{
        if (m_creature.AIAgent.Target == null)
            return AIBehaviorResultType.FAIL;

        Building building = m_creature.AIAgent.Target.GetComponent<Building>();
        if (building == null)
            return AIBehaviorResultType.FAIL;

        building.Enter(m_creature);

        return AIBehaviorResultType.SUCCESS;		
	}
}
