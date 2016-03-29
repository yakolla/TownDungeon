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
        Collider[] colls = Physics.OverlapSphere(m_creature.transform.position, Helper.MapArea.width, 1<<Helper.LayerMaskItem);
        
        if (colls.Length == 0)
            return AIBehaviorResultType.FAIL;

		ItemBox itemBox = colls[Random.Range(0, colls.Length)].gameObject.GetComponent<ItemBox>();
		if (itemBox == null)
			return AIBehaviorResultType.FAIL;

		if (itemBox.PreOrder != null)
			return AIBehaviorResultType.FAIL;
        
		m_creature.AIAgent.Target = itemBox.gameObject;
		itemBox.PreOrder = m_creature.gameObject;
		itemBox.gameObject.layer = 0;
		return AIBehaviorResultType.SUCCESS;		
	}
}
