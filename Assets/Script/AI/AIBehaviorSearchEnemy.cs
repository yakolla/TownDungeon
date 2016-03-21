using UnityEngine;
using System.Collections;

public class AIBehaviorSearchEnemy : AIBehavior {

	protected Creature		m_creature;

	public AIBehaviorSearchEnemy(Creature creature)
	{
		m_creature = creature;
	}

	public override void Start()
	{

	}

	public override AIBehaviorResultType Update()
	{
		Creature target = null;
		if (m_creature.AIAgent.Target != null)
		{
			target = m_creature.AIAgent.Target.GetComponent<Creature>();
			if (target != null)
				if (m_creature.StatsProp.GetValue(StatsPropType.SIGHT) < Vector3.Distance(m_creature.transform.position, target.transform.position))
				    target = null;
		}

		if (target == null)
		{
            Creature[] targets = GameObject.Find("Mobs").GetComponentsInChildren<Creature>();
            if (targets.Length == 0)
                return AIBehaviorResultType.FAIL;

            target = targets[Random.Range(0, targets.Length)];
            /*
            Collider[] colls = Physics.OverlapSphere(m_creature.transform.position, Helper.MapArea.width, m_creature.LayerMaskForEnemy);
			if (colls.Length == 0)
				return AIBehaviorResultType.FAIL; 

			target = colls[Random.Range(0, colls.Length)].gameObject.GetComponent<Creature>();*/
		}

		if (target == null || target.IsDeath == true)
			return AIBehaviorResultType.FAIL;

		m_creature.AIAgent.Target = target.gameObject;

		return AIBehaviorResultType.SUCCESS;
	}
}
