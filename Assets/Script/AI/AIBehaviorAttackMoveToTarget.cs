using UnityEngine;
using System.Collections;

public class AIBehaviorAttackMoveToTarget : AIBehavior {

	protected Creature		m_creature;
	AIBehaviorAttack		m_attack;
	AIBehaviorMoveToTargetPos m_move;

	bool moveStop = false;

	public AIBehaviorAttackMoveToTarget(Creature creature)
	{
		m_creature = creature;
		m_attack = new AIBehaviorAttack(creature);
		m_move = new AIBehaviorMoveToTargetPos(creature, true);
	}

	public override void Start()
	{
		m_attack.Start();
		m_move.Start();
		moveStop = false;
	}
	
	public override AIBehaviorResultType Update()
	{
        if (m_creature.DamagedTime > Time.time)
        {
            moveStop = true;
			m_creature.Animator.SetTrigger("Idle");
            m_creature.AIPath.ClearPath();
            return AIBehaviorResultType.RUNNING;
        }

        if (m_attack.Update() == AIBehaviorResultType.FAIL)
		{
            if (moveStop == true)
            {
                m_move.Start();
                moveStop = false;
            }

            return m_move.Update();
        }

		if (moveStop == false)
		{
			m_creature.Animator.SetTrigger("Idle");
			m_creature.AIPath.ClearPath();
			moveStop = true;
		}

		return AIBehaviorResultType.RUNNING;
	}
}
