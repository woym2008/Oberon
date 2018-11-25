using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterThink_BombLauncher : MonoBehaviour {

	Monster	mOwner;
	
	const int inactive = 0;
	const int active = 1;
	const int complete = 2;
	
	public MonsterType SpawnMonsterID = MonsterType.MonsterType_4;
	
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

		
		ThinkState = active;
	}
	
	public void ParseThinkType()
	{
		
	}
	
	void Awake() 
	{
		mOwner  = GetComponent<Monster>();
		
		//ThinkQueue.Enqueue(MonsterFSM.MS_Attack);
		//ThinkQueue.Enqueue(MonsterFSM.MS_Attack);
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
		mOwner.Idle(3.0f);

		ThinkQueue.Enqueue(MonsterFSM.MS_Attack);
		ThinkQueue.Enqueue(MonsterFSM.MS_Attack);
		ThinkQueue.Enqueue(MonsterFSM.MS_Attack);
		ThinkQueue.Enqueue(MonsterFSM.MS_Attack);
		ThinkQueue.Enqueue(MonsterFSM.MS_Attack);

		ThinkQueue.Enqueue(MonsterFSM.MS_Idle);
	}
	void AddAttack()
	{
		mOwner.Attack (0.1f);

		//Throw Bomb

		if(this.transform.localScale.y < 0.0f)
		{
			MonsterManager.GetInstance().CreateMonster(SpawnMonsterID,new Vector2(this.transform.position.x,this.transform.position.y),new Vector2(0.0f,1.0f),true);
		}
		else
		{
			MonsterManager.GetInstance().CreateMonster(SpawnMonsterID,new Vector2(this.transform.position.x,this.transform.position.y),new Vector2(0.0f,1.0f));
		}


	}
	void AddDie()
	{
		
	}
}
