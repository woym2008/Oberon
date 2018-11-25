using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterThink_Egg : MonoBehaviour {

	Monster	mOwner;
	
	const int inactive = 0;
	const int active = 1;
	const int complete = 2;

	public float SpawnTime = 5.0f;

	bool AlreadyDie = false;
	
	public Queue<int>	ThinkQueue = new Queue<int>();
	
	int	ThinkState = inactive;	
	
	public void PopThink()
	{
		int curType = ThinkQueue.Dequeue();
		
		switch(curType)
		{
		case MonsterFSM.MS_Idle: AddIdle();break;
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
		
		ThinkQueue.Enqueue(MonsterFSM.MS_Idle);
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(IsInactive())
		{
			PopThink();
		}

		SpawnTime -= Time.deltaTime;
		if(SpawnTime <= 0.0f && AlreadyDie == false)
		{
			AlreadyDie = true;

			ThinkQueue.Enqueue(MonsterFSM.MS_Die);	

			this.gameObject.SendMessage("Hurt",90000);

			GameObject playerObj = PlayerManager.getInstance ().GetPlayer ();
			if(playerObj != null)
			{
				MonsterManager.GetInstance().CreateMonster(MonsterType.MonsterType_4,new Vector2(this.transform.position.x,this.transform.position.y),new Vector2(playerObj.transform.position.x,playerObj.transform.position.y));
			}
		}
	}
	///
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

	}
	
	void AddIdle()
	{
		mOwner.Idle(999.0f);
	}
	void AddAttack()
	{
		mOwner.Attack (0.5f);
	}
	void AddDie()
	{

	}

	void MonsterHurt()
	{
		if(mOwner.mFSM.CurRule == Monster_Idle.GetInstance())
		{
			ThinkQueue.Enqueue(MonsterFSM.MS_Attack);

			SetThinkInactive ();

			ThinkQueue.Enqueue(MonsterFSM.MS_Idle);	
		}

	}
}
