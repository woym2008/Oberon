using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterThink_Falling : MonoBehaviour 
{
	Monster	mOwner;

	public bool ReverseMove = false;
	
	const int inactive = 0;
	const int active = 1;
	const int complete = 2;

	public float FallingSpeed = 0.4f;

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
		if(ReverseMove == false)
		{
			// fly to Left forever
			Vector2 curpos = this.transform.position;
			Vector2 targetPos = new Vector2(curpos.x,curpos.y-9999.0f);
			
			mOwner.MoveTo(targetPos,FallingSpeed);
		}
		else
		{
			// fly to Right forever
			Vector2 curpos = this.transform.position;
			Vector2 targetPos = new Vector2(curpos.x,curpos.y+9999.0f);
			
			mOwner.MoveTo(targetPos,FallingSpeed);
		}
	}

	void AddIdle()
	{
		mOwner.Idle(1f);
	}
	void AddAttack()
	{

	}
	void AddDie()
	{
		
	}
}
