using UnityEngine;
using System.Collections;

public class AIBehaviorMoveToTargetPos : AIBehavior {

	protected Creature		m_creature;
	bool	m_failOnHit	= false;

	public AIBehaviorMoveToTargetPos(Creature creature, bool failOnHit)
	{
		m_creature = creature;
		m_failOnHit = failOnHit;
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
				m_creature.Animator.SetTrigger("Idle");
				m_creature.AIPath.ClearPath();
				return AIBehaviorResultType.FAIL;
			}
		}

        if (m_creature.AIPath.TargetReached)
		{
			m_creature.Animator.SetTrigger("Idle");
			return AIBehaviorResultType.SUCCESS;
		}

		m_creature.Animator.SetTrigger("Run");
		return AIBehaviorResultType.RUNNING;
	}
}
