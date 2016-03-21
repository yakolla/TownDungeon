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
    bool m_death = true;

	public void Start () {

        CreatureSerializeFileds.Init(this);
        HP = (int)StatsProp.GetValue(StatsPropType.MAX_HP);
        

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

        m_death = false;
        m_aiPath.speed = StatsProp.GetValue(StatsPropType.MOVE_SPEED);
        GetComponent<Pathfinding.RVO.RVOController>().maxSpeed = m_aiPath.speed;
    }

	bool canAiUpdate()
	{
		return IsDeath == false && m_aniEffectCount == 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (canAiUpdate())
			m_aiAgent.Update();

	}

	public bool IsDeath
	{
		get { return m_death; }
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
                    attacker.ItemInventory.PutOn(entry.Value);
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

    public void OnDamage(Creature attacker, int dmg)
    {
        AIAgent.Attacker = attacker;
        AIAgent.AiBehaviorRestart = true;

        HP -= dmg;
        m_hpBox.Amount("-" + dmg, HP / StatsProp.GetValue(StatsPropType.MAX_HP));

        if (HP <= 0 && IsDeath == false)
        {
            m_death = true;
            m_animator.SetBool("Death", true);
            StartCoroutine(LoopCheckDeathDone(attacker));
        }
    }

    IEnumerator LoopAttackHit(float hitTime, Creature target, int dmg)
    {
        yield return new WaitForSeconds(hitTime);

        if (target != null)
            target.OnDamage(this, dmg);
    }

	public float OnFight(Creature target)
	{
        transform.LookAt(target.transform, Vector3.up);

        Animator.SetTrigger("Attack");
        float atkSpeed = StatsProp.GetValue(StatsPropType.ATK_SPEED);
        Animator.speed = atkSpeed;

        float delay = 1 / atkSpeed;
        float aniLen = AttackAniClip.length / atkSpeed;
        float nextToAttackTime = Time.time + delay + aniLen;        

        
        int dmg = (int)StatsProp.GetValue(StatsPropType.STR);

        WeaponType weaponType = RefDataMgr.Instance.RefCreatures[RefCreatureID].WeaponType;
        switch (weaponType)
        {
            case WeaponType.Sword:
                StartCoroutine(LoopAttackHit((delay + aniLen) * 0.5f, target, dmg));
                break;
            case WeaponType.Arrow:
            case WeaponType.IceBall:
                {
                    GameObject obj = Instantiate(Resources.Load("Pref/Weapons/"+ weaponType)) as GameObject;
                    Arrow weapon = obj.GetComponent<Arrow>();
                    weapon.Init(transform.position, target.transform.position, 5, 0.5f, () => {
                        StartCoroutine(LoopAttackHit(0f, target, dmg));
                    });
                }
                break;
            
            case WeaponType.Meteo:
            case WeaponType.IceMeteo:
                {
                    GameObject obj = Instantiate(Resources.Load("Pref/Weapons/"+ weaponType)) as GameObject;
                    Meteo weapon = obj.GetComponent<Meteo>();

                    weapon.Init(new Vector3(transform.position.x, 20f, transform.position.z), target.transform.position, 8f, () => {
                        StartCoroutine(LoopAttackHit(0f, target, dmg));
                    });
                }
                break;
            case WeaponType.Thunder:
                {
                    GameObject obj = Instantiate(Resources.Load("Pref/Weapons/" + weaponType)) as GameObject;
                    Thunder weapon = obj.GetComponent<Thunder>();

                    weapon.Init(new Vector3(target.transform.position.x, 20f, target.transform.position.z), new Vector3(target.transform.position.x, 0f, target.transform.position.z), 28f, () => {
                        StartCoroutine(LoopAttackHit(0f, target, dmg));
                    });
                }
                break;
        }
        
		return nextToAttackTime;

    }

	public void OnPickUpItem(ItemBox itemBox)
	{
		Destroy(itemBox.gameObject);
	}

    

	public void GiveXP(int xp)
	{
		int oldLv = Level;
		XP += xp;
        
        while(true)
        {
            int xpToLevelup = XPToNextLevel;
            if (XP < xpToLevelup)
                break;

            XP = XP - xpToLevelup;
            Level++;

        }

        // levelup
        if (oldLv < Level)
		{
			m_xpBox.Amount("Lv UP "+Level, 0f);
			
			StartCoroutine(LoopLevelUp());
		}
		else
		{
			m_xpBox.Amount("+"+xp, XP / (float)XPToNextLevel);
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

    public int XPToNextLevel
    {
        get { return (Level + 1) * Helper.XPBlock; }
    }

    public int HP
    {
        get { return (int)StatsProp.GetValue(StatsPropType.HP); }
        set { StatsProp.SetValue(StatsPropType.HP, Mathf.Clamp(value, 0f, StatsProp.GetValue(StatsPropType.MAX_HP))); }
    }

    public int Level
    {
        get { return (int)StatsProp.GetValue(StatsPropType.LEVEL); }
        set { StatsProp.SetValue(StatsPropType.LEVEL, value); }
    }

    public int XP
    {
        get { return (int)StatsProp.GetValue(StatsPropType.XP); }
        set { StatsProp.SetValue(StatsPropType.XP, value); }
    }

    public abstract AIBehavior defaultAIBehavior();

}
