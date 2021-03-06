﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviorComposite : AIBehavior {

	List<AIBehavior> m_behaviors = new List<AIBehavior>();

    public AIBehaviorComposite(params AIBehavior[] behaviors)
    {
        foreach (var be in behaviors)
        {
            m_behaviors.Add(be);
        }
    }

    public override void Start ()
	{
		for (int i = 0; i < m_behaviors.Count; ++i)
			m_behaviors[i].Start();
	}

	public override AIBehaviorResultType Update()
	{

		AIBehaviorResultType resultType = AIBehaviorResultType.SUCCESS;

		for (int i = 0; i < m_behaviors.Count; ++i)
		{
			resultType = m_behaviors[i].Update();
		}

		return resultType;
	}
}
