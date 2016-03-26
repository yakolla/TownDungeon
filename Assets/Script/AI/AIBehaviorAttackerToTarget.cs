using UnityEngine;
using System.Collections;

public class AIBehaviorAttackerToTarget : AIBehavior {

	protected Creature		m_creature;

	public AIBehaviorAttackerToTarget(Creature creature)
	{
		m_creature = creature;
	}

	public override void Start()
	{

	}

	public override AIBehaviorResultType Update()
	{
		if (m_creature.AIAgent.Attacker == null)
			return AIBehaviorResultType.FAIL;

		m_creature.AIAgent.Target = m_creature.AIAgent.Attacker.gameObject;
		return AIBehaviorResultType.SUCCESS;

	}
}
