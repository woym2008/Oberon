using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterThink_ShotForward : MonoBehaviour 
{
	Monster	mOwner;
	
	const int inactive = 0;
	const int active = 1;
	const int complete = 2;
	
	public Queue<int>	ThinkQueue = new Queue<int>();
	
	int	ThinkState = inactive;	
	
	public void PopThink()
	{
		int curType = ThinkQueue.Dequeue();
		
		switch(curType)
		{
		case MonsterFSM.MS_Idle: AddIdle();break;
		case MonsterFSM.MS_Move: AddMoveTo();break;
		case MonsterFSM.MS_Attack: AddAttack();break;
		case MonsterFSM.MS_Die: AddDie();break;	
		}
		//踢出这个一开始的move
		if(curType != MonsterFSM.MS_Move)
		{
			ThinkQueue.Enqueue(curType);			
		}
		
		ThinkState = active;
	}

	public void ParseThinkType()
	{
		
	}

	void Awake() 
	{
		mOwner  = GetComponent<Monster>();
		
		ThinkQueue.Enqueue(MonsterFSM.MS_Move);
		ThinkQueue.Enqueue(MonsterFSM.MS_Attack);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(IsInactive())
		{
			PopThink();
		}
	}

	
	public void SetThinkInactive() 
	{ 
		ThinkState = inactive ;
	}
	public void SetThinkComplete() 
	{ 
		ThinkState = complete ;
	}
	
	public bool IsComplete()
	{
		return ThinkState == complete;
	}
	public bool IsInactive()
	{
		return ThinkState == inactive;
	}

	void AddMoveTo()
	{
		// fly to Left forever
		Vector2 curpos = this.transform.position;
		Vector2 targetPos = new Vector2(curpos.x-0.5f,curpos.y);
		
		mOwner.MoveTo(targetPos,2f);
	}

	void AddIdle()
	{
		mOwner.Idle(1f);
	}
	void AddAttack()
	{
		RandomShot ();

		ThinkQueue.Enqueue(MonsterFSM.MS_Move);
		ThinkQueue.Enqueue(MonsterFSM.MS_Attack);
	}
	void AddDie()
	{
		
	}

	void RandomShot()
	{
		mOwner.Attack (0.0f);
		
		if(MusicManager.GetInstance().SFXCtrl != null)
		{
			MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_EnemyFire_1);
		}
	}
}
