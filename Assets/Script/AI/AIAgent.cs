using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIAgent {


	Creature 		m_creature;
	AIBehavior		m_aiBehavior = null;

	Vector3			m_targetPos;
	GameObject		m_target;
	Creature		m_attacker;
	bool			m_aiBehaviorRestart = true;

	public void Init (Creature creature, AIBehavior aiBehavior) {

		m_creature = creature;

		m_aiBehavior = aiBehavior;

	}
	
	// Update is called once per frame
	public void Update () {
		

        switch (m_aiBehavior.Update())
		{
		case AIBehaviorResultType.SUCCESS:
			break;
		case AIBehaviorResultType.FAIL:
			break;
		case AIBehaviorResultType.RUNNING:
			break;
		}
	}

	public Vector3 TargetPos
	{
		set {
			m_target = null;
			m_targetPos = value;
		}
		get {
			if (m_target != null)
				return m_targetPos = m_target.transform.position;
			return m_targetPos;
		}
	}

	public GameObject Target
	{
		set {
			m_target = value;
			if (m_target != null)
				m_targetPos = m_target.transform.position;
		}
		get {return m_target;}
	}

	public Creature Attacker
	{
		set {m_attacker = value;}
		get {return m_attacker;}
	}

	public bool AiBehaviorRestart
	{
		set {m_aiBehaviorRestart = value;}
	}
}
