using UnityEngine;
using System.Collections;

public class AIBehaviorSearchSurroundingEnemy : AIBehavior {

	protected Creature		m_creature;

	public AIBehaviorSearchSurroundingEnemy(Creature creature)
	{
		m_creature = creature;
	}

	public override void Start()
	{


	}

	public override AIBehaviorResultType Update()
	{
		Collider[] colls = Physics.OverlapSphere(m_creature.transform.position, m_creature.StatsProp.GetValue(StatsPropType.SIGHT), m_creature.LayerMaskForEnemy);
		for (int i = 0; i < colls.Length; ++i)
		{
			Creature target = colls[i].gameObject.GetComponent<Creature>();
			if (target == null || target.IsDeath == true)
				continue;

			m_creature.AIAgent.Target = target.gameObject;

			return AIBehaviorResultType.SUCCESS;
		}

		return AIBehaviorResultType.FAIL;
	}
}
