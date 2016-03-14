using UnityEngine;
using System.Collections;

public enum AIBehaviorResultType
{
	FAIL,
	SUCCESS,
	RUNNING,
}

public abstract class AIBehavior {

	public abstract void Start();
	public abstract AIBehaviorResultType Update();
}
