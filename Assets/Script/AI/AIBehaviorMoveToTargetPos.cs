using UnityEngine;
using System.Collections;

public class AIBehaviorMoveToTargetPos : AIBehavior {

	protected Creature		m_creature;
	bool	m_failOnHit	= false;

	public AIBehaviorMoveToTargetPos(Creature creature, bool m_failOnHit)
	{
		m_creature = creature;
	}

	public override void Start()
	{
		m_creature.AIPath.SearchPath(m_creature.AIAgent.TargetPos);		
	}
	
	public override AIBehaviorResultType Update()
	{
		if (m_failOnHit == true)
		{
			if (m_creature.DamagedTime > Time.time)
			{
				m_creature.AIPath.ClearPath();
				return AIBehaviorResultType.FAIL;
			}
		}

        if (m_creature.AIPath.TargetReached)
		{
			return AIBehaviorResultType.SUCCESS;
		}
		
		return AIBehaviorResultType.RUNNING;
	}
}
