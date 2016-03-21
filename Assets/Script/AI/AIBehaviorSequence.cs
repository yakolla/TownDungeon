using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviorSequence : AIBehavior {

	List<AIBehavior> m_behaviors = new List<AIBehavior>();
	int m_runningIndex = 0;
    bool m_running = false;

    public AIBehaviorSequence(params AIBehavior[] behaviors)
    {
        foreach(var be in behaviors)
        {
            m_behaviors.Add(be);
        }
    }

    public override void Start ()
	{
		
	}

    

	public override AIBehaviorResultType Update()
	{
		if (m_behaviors.Count == 0)
			return AIBehaviorResultType.FAIL;

        
        for (int i = m_runningIndex; i < m_behaviors.Count; ++i)
        {
            if (m_running == true)
            {
                if (i != m_runningIndex)
                    m_behaviors[i].Start();
            }
            else
                m_behaviors[i].Start();


            AIBehaviorResultType resultType = m_behaviors[i].Update();
            if (AIBehaviorResultType.SUCCESS == resultType)
                continue;
            if (AIBehaviorResultType.FAIL == resultType)
            {
                m_runningIndex = 0;
                m_running = false;
                return AIBehaviorResultType.FAIL;
            }
            if (AIBehaviorResultType.RUNNING == resultType)
            {
                m_runningIndex = i;
                m_running = true;
                return AIBehaviorResultType.RUNNING;
            }
        }

        m_runningIndex = 0;
        m_running = false;
        return AIBehaviorResultType.SUCCESS;
	}
}
