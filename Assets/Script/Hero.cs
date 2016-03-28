using UnityEngine;
using System.Collections;

public class Hero : Creature {

	public override AIBehavior defaultAIBehavior()
	{
		AIBehaviorSequence aiBehaviorSequencePickupItem 
            = new AIBehaviorSequence(
                   new AIBehaviorWait(1f),
                   new AIBehaviorSearchSurroundingItem(this), 
                   new AIBehaviorMoveToTargetPos(this, false), 
                   new AIBehaviorPickupItem(this));
		
		AIBehaviorSequence aiBehaviorSequenceEnemyAttack 
            = new AIBehaviorSequence(
                new AIBehaviorSearchEnemy(this), 
                new AIBehaviorAttackMoveToTarget(this));

        AIBehaviorSequence aiBehaviorSequenceWander
            = new AIBehaviorSequence(
                new AIBehaviorSearchSurroundingArea(this, Helper.MapArea),
                new AIBehaviorAttackMoveToTarget(this));

        AIBehaviorSequence aiBehaviorSequenceGotoInn
            = new AIBehaviorSequence(
                new AIBehaviorLowHP(this),
                new AIBehaviorSearchBuilding(this, "InnBuilding"),
                new AIBehaviorMoveToTargetPos(this, false),
                new AIBehaviorEnterBuilding(this));

        
        return new AIBehaviorSelector(                
                aiBehaviorSequencePickupItem,
                aiBehaviorSequenceGotoInn,
				aiBehaviorSequenceEnemyAttack,
                aiBehaviorSequenceWander);
		
	}
}
