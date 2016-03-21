using UnityEngine;
using System.Collections;

public class Hero : Creature {

	public override AIBehavior defaultAIBehavior()
	{
		AIBehaviorSequence aiBehaviorSequencePickupItem 
            = new AIBehaviorSequence(
                   new AIBehaviorSearchSurroundingItem(this), 
                   new AIBehaviorMoveToTargetPos(this), 
                   new AIBehaviorPickupItem(this));
		
		AIBehaviorSequence aiBehaviorSequenceEnemyAttack 
            = new AIBehaviorSequence(
                new AIBehaviorSearchEnemy(this), 
                new AIBehaviorAttackMoveToTarget(this));		
        
		return new AIBehaviorSelector(
                aiBehaviorSequenceEnemyAttack,
                aiBehaviorSequencePickupItem);
		
	}
}
