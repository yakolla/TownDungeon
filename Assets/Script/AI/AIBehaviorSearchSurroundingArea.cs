using UnityEngine;
using System.Collections;

public class AIBehaviorSearchSurroundingArea : AIBehavior {

	protected Creature		m_creature;
	protected Rect			m_area;

	public AIBehaviorSearchSurroundingArea(Creature creature, Rect area)
	{
		m_creature = creature;
		m_area = area;
	}

	public override void Start()
	{

		float sight = m_creature.StatsProp.GetValue(StatsPropType.SIGHT);
		float x = Random.Range(-sight, sight)+m_creature.transform.position.x;
		float z = Random.Range(-sight, sight)+m_creature.transform.position.z;
		m_creature.AIAgent.TargetPos = new Vector3(Mathf.Clamp(x, m_area.xMin, m_area.width), 0, Mathf.Clamp(z, m_area.yMin, m_area.height));

	}

	public override AIBehaviorResultType Update()
	{
		return AIBehaviorResultType.SUCCESS;
	}
}
