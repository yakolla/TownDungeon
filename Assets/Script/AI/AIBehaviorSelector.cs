using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviorSelector : AIBehavior {

	List<AIBehavior> m_behaviors = new List<AIBehavior>();
	int m_cur = 0;
	int m_next = 0;

	public void Add(AIBehavior behavior)
	{
		m_behaviors.Add(behavior);
	}

	public override void Start ()
	{
		m_behaviors[m_cur].Start();
	}

	public override AIBehaviorResultType Update()
	{
		if (m_cur != m_next)
		{
			m_cur = m_next;
			Start ();
		}

		AIBehavior behavior = m_behaviors[m_cur];
		AIBehaviorResultType resultType = behavior.Update();
		if (AIBehaviorResultType.SUCCESS == resultType)
		{
			return resultType;
		}

		if (resultType == AIBehaviorResultType.FAIL)
			m_next = (m_cur+1)%m_behaviors.Count;

		return resultType;
	}
}
