﻿using UnityEngine;
using System.Collections;

public class AIBehaviorPickupItem : AIBehavior {

	Creature		m_creature;

	float			m_attackableTime;

	public AIBehaviorPickupItem(Creature creature)
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

		if (Vector3.Distance(m_creature.transform.position, m_creature.AIAgent.TargetPos) > m_creature.StatsProp.GetValue(StatsProp.Type.ATK_RANGE))
			return AIBehaviorResultType.FAIL;

		ItemBox target = m_creature.AIAgent.Target.GetComponent<ItemBox>();
		if (target == null)
			return AIBehaviorResultType.FAIL;

		m_creature.transform.LookAt(target.transform, Vector3.up);

		m_creature.OnPickUpItem(target);

		m_creature.Animator.SetTrigger("PickUp");
		float aniLen = m_creature.AttackAniClip.length;
		m_attackableTime = Time.time + aniLen;

		return AIBehaviorResultType.RUNNING;
	}
}
