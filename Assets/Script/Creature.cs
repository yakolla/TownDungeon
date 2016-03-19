using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class Creature : MonoBehaviour {

	[SerializeField]
	string 				m_creatureName = null;
    

    AIAgent				m_aiAgent = null;
	AIPath				m_aiPath = null;

	[SerializeField]
	AnimationClip		m_attackAniClip = null;


	int				m_layerMaskForEnemy;
	Animator		m_animator;

	SpeechBox		m_speechBox;
	GuageBox		m_hpBox;
	GuageBox		m_xpBox;
	int				m_aniEffectCount = 0;

	public void Start () {

        CreatureSerializeFileds.Init();

		m_aiPath = GetComponent<AIPath>();
		m_animator = GetComponentInChildren<Animator>();
		m_speechBox = transform.Find("Canvas/SpeechPanel").GetComponent<SpeechBox>();
		m_hpBox = transform.Find("Canvas/HPPanel").GetComponent<GuageBox>();
		m_xpBox = transform.Find("Canvas/XPPanel").GetComponent<GuageBox>();

        if (gameObject.layer == LayerMask.NameToLayer("Hero"))
            m_layerMaskForEnemy = 1 << LayerMask.NameToLayer("Mob");
        else if (gameObject.layer == LayerMask.NameToLayer("Mob"))
            m_layerMaskForEnemy = 1 << LayerMask.NameToLayer("Hero");
        
        m_aiAgent = new AIAgent();
        m_aiAgent.Init(this, defaultAIBehavior());

    }

	bool isAiUpdate()
	{
		return IsDeath == false && m_aniEffectCount == 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (isAiUpdate())
			m_aiAgent.Update();

	}

	public bool IsDeath
	{
		get { return CreatureSerializeFileds.Stats.HP <= 0;}
	}

	IEnumerator LoopCheckDeathDone(Creature attacker)
	{
		AIPath.enabled = false;
		GetComponent<Pathfinding.RVO.RVOController>().enabled = false;

		// 경험치 주자.
		yield return new WaitForSeconds(2f);

		Helper.ItemBoxs.SpawnItemBox(transform.position);

		if (attacker != null)
		{
			attacker.GiveXP((int)StatsProp.GetValue(StatsPropType.DEATH_XP));
            if (ItemInventory != null && attacker.ItemInventory != null)
            {
                foreach (var entry in ItemInventory.Items)
                    attacker.ItemInventory.PutOnBag(entry.Value);
            }
		}
        
		// 가라앉기
		yield return new WaitForSeconds(2f);
		Vector3 pos = transform.position;
		pos.y = -3f;
		while(pos.y < transform.position.y)
		{
			transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime);
			yield return null;
		}
        

        GameObject.Destroy(gameObject);
	}

	IEnumerator LoopLevelUp()
	{

		++m_aniEffectCount;

		if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			m_animator.SetTrigger("LevelUp");
			
			while(m_animator.GetCurrentAnimatorStateInfo(0).IsName("LevelUpDone") == false)
			{
				yield return null;
			}
		}

		if (gameObject != null)
		{
			m_animator.SetTrigger("Idle");
			--m_aniEffectCount;
		}

	}

	public void OnFight(Creature attacker)
	{
        int dmg = (int)attacker.StatsProp.GetValue(StatsPropType.STR);
		AIAgent.Attacker = attacker;
		AIAgent.AiBehaviorRestart = true;

		StatsProp.HP -= dmg;
		m_hpBox.Amount("-"+dmg, StatsProp.HP/StatsProp.GetValue(StatsPropType.MAX_HP));

		if (IsDeath)
		{
			m_animator.SetBool("Death", true);
			StartCoroutine(LoopCheckDeathDone(attacker));
		}
	}

	public void OnPickUpItem(ItemBox itemBox)
	{
		Destroy(itemBox.gameObject);
	}

	public void GiveXP(int xp)
	{
		int lv = StatsProp.Level;
		StatsProp.SetValue(StatsPropType.XP, StatsProp.GetValue(StatsPropType.XP) + xp );

		// levelup
		if (lv < StatsProp.Level)
		{
			m_xpBox.Amount("Lv UP "+StatsProp.Level, 0f);
			
			StartCoroutine(LoopLevelUp());
		}
		else
		{
			int lvBaseXP = (int)StatsProp.GetValue(StatsPropType.XP)-(StatsProp.Level-1)*Helper.XPBlock;
			m_xpBox.Amount("+"+xp, lvBaseXP/(float)Helper.XPBlock);
		}

	}

	public StatsProp StatsProp
	{
		get { return CreatureSerializeFileds.Stats;}
	}



	public Animator	Animator
	{
		get {return m_animator;}
	}

	public int	LayerMaskForEnemy
	{
		get {return m_layerMaskForEnemy;}
	}

	public SpeechBox SpeechBox
	{
		get {return m_speechBox;}
	}

	public AIAgent AIAgent
	{
		get {return m_aiAgent;}
	}

	public AIPath	AIPath
	{
		get {return m_aiPath;}
	}

	public AnimationClip AttackAniClip
	{
		get {return m_attackAniClip;}
	}

	public string CreatureName
	{
		get {return m_creatureName;}
	}

	public int RefCreatureID
	{
		get { return CreatureSerializeFileds.RefCreatureID;  }
	}

	public ItemInventory ItemInventory
	{
		get { return CreatureSerializeFileds.ItemInventory; }
	}

    public CreatureSerializeFileds CreatureSerializeFileds
    {
        get; set;
    }


    public abstract AIBehavior defaultAIBehavior();

}
