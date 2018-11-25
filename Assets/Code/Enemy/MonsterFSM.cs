using UnityEngine;
using System.Collections;

public class MonsterFSM : MonoBehaviour {

	//
	public const int MS_Idle = 0;
	public const int MS_Move = 1;
	public const int MS_Attack = 2;
	public const int MS_Rotate = 3;
	public const int MS_Die = 4;
	
	public const int MS_Born = 4;




	//



	public MonsterState	LastRule;
	public MonsterState	CurRule;

	public void SetState(MonsterState rule)
	{
		LastRule = CurRule;

		if(CurRule != null) CurRule.Exit(mOwner);

		CurRule = rule;

		if(CurRule!= null) CurRule.Enter(mOwner);
	}

	public void SetCurState(MonsterState rule)
	{
		CurRule = rule;
	}
	//
	public void SetLastState(MonsterState rule)
	{
		LastRule = rule;
	}
	
	public void  RevertToPreviousState()
	{
		SetState(LastRule);
	}

	void Awake() {
		mOwner = gameObject.GetComponent<Monster>();
		
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(CurRule!= null) CurRule.Update(mOwner);
	}

	Monster mOwner = null;
}

public class MonsterState
{
	public virtual void Enter(Monster monster){}
	public virtual void Exit(Monster monster){}
	public virtual void Update(Monster monster){}
	//
}

public class Monster_Born : MonsterState
{
	static private Monster_Born instance = null;
	public static Monster_Born GetInstance()
	{
		if(instance == null) instance = new Monster_Born();
		return instance;
	}
	public override void Enter (Monster monster)
	{
		base.Enter (monster);
		
		monster.StartBorn();
	}
	//
	public override void Update (Monster monster)
	{
		base.Update (monster);
		
		monster.UpdateBorn();
		
	}
	//
	public override void Exit (Monster monster)
	{
		base.Exit (monster);
		
		monster.EndBorn();
		
	}
}

public class Monster_Idle : MonsterState
{
	static private Monster_Idle instance = null;
	public static Monster_Idle GetInstance()
	{
		if(instance == null) instance = new Monster_Idle();
		return instance;
	}
	public override void Enter (Monster monster)
	{
		base.Enter (monster);

		monster.StartIdle();
	}
	//
	public override void Update (Monster monster)
	{
		base.Update (monster);

		monster.UpdateIdle();
		
	}
	//
	public override void Exit (Monster monster)
	{
		base.Exit (monster);

		monster.EndIdle();
		
	}
}


public class Monster_MoveTo : MonsterState
{
	static private Monster_MoveTo instance = null;
	public static Monster_MoveTo GetInstance()
	{
		if(instance == null) instance = new Monster_MoveTo();
		return instance;
	}
	public override void Enter (Monster monster)
	{
		base.Enter (monster);
		//
		monster.StartMoveTo();
	}
	//
	public override void Update (Monster monster)
	{
		base.Update (monster);

		monster.UpdateMoveTo();
		
	}
	//
	public override void Exit (Monster monster)
	{
		base.Exit (monster);

		monster.EndMoveTo();
		
	}
}

public class Monster_RotateTo : MonsterState
{
	static private Monster_RotateTo instance = null;
	public static Monster_RotateTo GetInstance()
	{
		if(instance == null) instance = new Monster_RotateTo();
		return instance;
	}
	public override void Enter (Monster monster)
	{
		base.Enter (monster);

		monster.StartRotateTo();
	}
	//
	public override void Update (Monster monster)
	{
		base.Update (monster);

		monster.UpdateRotateTo();
	}
	//
	public override void Exit (Monster monster)
	{
		base.Exit (monster);

		monster.EndRotateTo();
	}
}

public class Monster_Attack : MonsterState
{
	static private Monster_Attack instance = null;
	public static Monster_Attack GetInstance()
	{
		if(instance == null) instance = new Monster_Attack();
		return instance;
	}
	public override void Enter (Monster monster)
	{
		base.Enter (monster);
		
		monster.StartAttack();
	}
	//
	public override void Update (Monster monster)
	{
		base.Update (monster);
		
		monster.UpdateAttack();
	}
	//
	public override void Exit (Monster monster)
	{
		base.Exit (monster);
		
		monster.EndAttack();
	}
}

public class Monster_Die : MonsterState
{
	static private Monster_Die instance = null;
	public static Monster_Die GetInstance()
	{
		if(instance == null) instance = new Monster_Die();
		return instance;
	}
	public override void Enter (Monster monster)
	{
		base.Enter (monster);

		monster.StartDie();
		
	}
	//
	public override void Update (Monster monster)
	{
		base.Update (monster);

		monster.UpdateDie();
		
	}
	//
	public override void Exit (Monster monster)
	{
		base.Exit (monster);
		
		monster.EndDie();
		
	}
}

