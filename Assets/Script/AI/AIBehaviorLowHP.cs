using UnityEngine;
using System.Collections;

public class AIBehaviorLowHP : AIBehavior {

	protected Creature		m_creature;

	public AIBehaviorLowHP(Creature creature)
	{
		m_creature = creature;
	}

	public override void Start()
	{

	}

	public override AIBehaviorResultType Update()
	{
		if (m_creature.HP/(float)m_creature.MaxHP < 0.2f)
			return AIBehaviorResultType.SUCCESS;

		return AIBehaviorResultType.FAIL;

	}
}
