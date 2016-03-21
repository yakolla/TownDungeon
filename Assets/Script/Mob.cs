using UnityEngine;
using System.Collections;

public class Mob : Creature {

	public override AIBehavior defaultAIBehavior()
	{
		AIBehaviorSequence aiBehaviorSequenceAttack 
            = new AIBehaviorSequence(
                new AIBehaviorTargetToAttacker(this), 
                new AIBehaviorAttackMoveToTarget(this));
			
		AIBehaviorSequence aiBehaviorSequenceWander 
            = new AIBehaviorSequence(
                new AIBehaviorSearchSurroundingArea(this, Helper.MapArea), 
                new AIBehaviorComposite(
                    new AIBehaviorTargetToAttacker(this), 
                    new AIBehaviorAttackMoveToTarget(this)));
			
		AIBehaviorSelector aiBehaviorSelector 
            = new AIBehaviorSelector(
                aiBehaviorSequenceAttack, 
                aiBehaviorSequenceWander);
			
		
		return aiBehaviorSelector;

	}
}
