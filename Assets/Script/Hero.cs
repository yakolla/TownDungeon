using UnityEngine;
using System.Collections;

public class Hero : Creature {

	public override AIBehavior defaultAIBehavior()
	{
		AIBehaviorSequence aiBehaviorSequence = new AIBehaviorSequenceForDebug();
		//aiBehaviorSequence.Add(new AIBehaviorWait(1F));
		{
			AIBehaviorSequence aiBehaviorSequencePickupItem = new AIBehaviorSequenceForDebug();
				aiBehaviorSequencePickupItem.Add(new AIBehaviorSearchSurroundingItem(this));
					aiBehaviorSequencePickupItem.Add(new AIBehaviorMoveToTargetPos(this));
						aiBehaviorSequencePickupItem.Add(new AIBehaviorPickupItem(this));

			AIBehaviorSequence aiBehaviorSequenceEnemyAttack = new AIBehaviorSequenceForDebug();
				aiBehaviorSequenceEnemyAttack.Add(new AIBehaviorSearchEnemy(this));
					aiBehaviorSequenceEnemyAttack.Add(new AIBehaviorAttackMoveToTarget(this));

			AIBehaviorSelector aiBehaviorSelectorWander = new AIBehaviorSelectorForDebug();
			aiBehaviorSelectorWander.Add(aiBehaviorSequenceEnemyAttack);
			aiBehaviorSelectorWander.Add(aiBehaviorSequencePickupItem);


			aiBehaviorSequence.Add(aiBehaviorSelectorWander);
		}
		return aiBehaviorSequence;
		
	}
}
