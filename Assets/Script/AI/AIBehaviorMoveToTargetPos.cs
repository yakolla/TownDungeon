using UnityEngine;
using System.Collections;

public class AIBehaviorMoveToTargetPos : AIBehavior {

	protected Creature		m_creature;

	public AIBehaviorMoveToTargetPos(Creature creature)
	{
		m_creature = creature;
	}

	public override void Start()
	{
		m_creature.AIPath.SearchPath(m_creature.AIAgent.TargetPos);		
	}
	
	public override AIBehaviorResultType Update()
	{
		if (m_creature.AIPath.TargetReached)
		{
			return AIBehaviorResultType.SUCCESS;
		}
		
		return AIBehaviorResultType.RUNNING;
	}
}
