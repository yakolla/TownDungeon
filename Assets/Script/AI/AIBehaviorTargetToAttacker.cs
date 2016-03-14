using UnityEngine;
using System.Collections;

public class AIBehaviorTargetToAttacker : AIBehavior {

	protected Creature		m_creature;

	public AIBehaviorTargetToAttacker(Creature creature)
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
