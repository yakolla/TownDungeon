using UnityEngine;
using System.Collections;

public class AIBehaviorSearchSurroundingItem : AIBehavior {

	protected Creature		m_creature;

	public AIBehaviorSearchSurroundingItem(Creature creature)
	{
		m_creature = creature;
	}

	public override void Start()
	{


	}

	public override AIBehaviorResultType Update()
	{
		Collider[] colls = Physics.OverlapSphere(m_creature.transform.position, float.PositiveInfinity, 1<<Helper.LayerMaskItem);
		for (int i = 0; i < colls.Length; ++i)
		{
			m_creature.AIAgent.Target = colls[i].gameObject;

			return AIBehaviorResultType.SUCCESS;
		}

		return AIBehaviorResultType.FAIL;
	}
}
