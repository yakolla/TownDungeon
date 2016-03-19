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
		if (target.HP == 0)
			return AIBehaviorResultType.FAIL;

		m_creature.transform.LookAt(target.transform, Vector3.up);

		target.OnFight(m_creature);

		m_creature.Animator.SetTrigger("Attack");
		float atkSpeed = m_creature.StatsProp.GetValue(StatsPropType.ATK_SPEED);
		m_creature.Animator.speed = atkSpeed;

		float delay = 1/atkSpeed;
		float aniLen = m_creature.AttackAniClip.length/atkSpeed;
		m_attackableTime = Time.time + delay + aniLen;


		return AIBehaviorResultType.RUNNING;
	}
}
