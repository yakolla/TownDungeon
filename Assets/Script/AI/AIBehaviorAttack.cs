using UnityEngine;
using System.Collections;

public class AIBehaviorAttack : AIBehavior {

	Creature		m_creature;

	float			m_attackableTime;

	public AIBehaviorAttack(Creature creature)
	{
		m_creature = creature;
	}


	public override void Start()
	{

	}

	public override AIBehaviorResultType Update()
	{

		if (Time.time < m_attackableTime)
			return AIBehaviorResultType.RUNNING;

		if (m_creature.AIAgent.Target == null)
			return AIBehaviorResultType.FAIL;

		if (Vector3.Distance(m_creature.transform.position, m_creature.AIAgent.TargetPos) > m_creature.StatsProp.GetValue(StatsPropType.ATK_RANGE))
			return AIBehaviorResultType.FAIL;

		Creature target = m_creature.AIAgent.Target.GetComponent<Creature>();
		if (target == null)
			return AIBehaviorResultType.FAIL;
		else if (target.IsDeath == true)
			return AIBehaviorResultType.SUCCESS;

        m_attackableTime = m_creature.OnFight(target);

		return AIBehaviorResultType.RUNNING;
	}
}
