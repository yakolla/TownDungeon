using UnityEngine;
using System.Collections;

public class Mob : Creature {

	public override AIBehavior defaultAIBehavior()
	{
		AIBehaviorSequence aiBehaviorSequenceAttack 
            = new AIBehaviorSequence(
                new AIBehaviorAttackerToTarget(this), 
                new AIBehaviorAttackMoveToTarget(this));

        AIBehaviorSequence aiBehaviorSequenceWander
            = new AIBehaviorSequence(
                new AIBehaviorWait(1F),
                new AIBehaviorSearchBuilding(this, "Building"), 
                new AIBehaviorMoveToTargetPos(this, true));
			
		return new AIBehaviorSelector(
                aiBehaviorSequenceAttack,
                aiBehaviorSequenceWander); ;

	}
}
