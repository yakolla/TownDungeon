using UnityEngine;
using System.Collections;

public class Mob : Creature {

	public override AIBehavior defaultAIBehavior()
	{
		AIBehaviorSequence aiBehaviorSequence = new AIBehaviorSequence();
		//aiBehaviorSequence.Add(new AIBehaviorWait(1F));
		{
			AIBehaviorSequence aiBehaviorSequenceAttack = new AIBehaviorSequence();
			{
				aiBehaviorSequenceAttack.Add(new AIBehaviorTargetToAttacker(this));
					aiBehaviorSequenceAttack.Add(new AIBehaviorAttackMoveToTarget(this));
			}
			AIBehaviorSequence aiBehaviorSequenceWander = new AIBehaviorSequence();
			{
				aiBehaviorSequenceWander.Add(new AIBehaviorSearchSurroundingArea(this, Helper.MapArea));
				{
					AIBehaviorComposite	aiBehaviorComposite = new AIBehaviorComposite();
					{
						aiBehaviorComposite.Add(new AIBehaviorTargetToAttacker(this));
						aiBehaviorComposite.Add(new AIBehaviorAttackMoveToTarget(this));
						aiBehaviorSequenceWander.Add(aiBehaviorComposite);
					}
				}
			}

			AIBehaviorSelector aiBehaviorSelector = new AIBehaviorSelector();
			aiBehaviorSelector.Add(aiBehaviorSequenceAttack);
			aiBehaviorSelector.Add(aiBehaviorSequenceWander);

			aiBehaviorSequence.Add(aiBehaviorSelector);
		}

		return aiBehaviorSequence;

	}
}
