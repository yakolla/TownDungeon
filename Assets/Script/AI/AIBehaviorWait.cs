using UnityEngine;
using System.Collections;

public class AIBehaviorWait : AIBehavior {

	float	m_time;
	float	m_finishTime;
	AIBehavior 		m_behaviorOnSuccess;

	public AIBehaviorWait(float time)
	{
		m_time = time;
	}

	public override void Start()
	{
		m_finishTime = m_time + Time.time;
	}

	public override AIBehaviorResultType Update()
	{
		if (m_finishTime < Time.time)
			return AIBehaviorResultType.SUCCESS;

		return AIBehaviorResultType.RUNNING;
	}
}
