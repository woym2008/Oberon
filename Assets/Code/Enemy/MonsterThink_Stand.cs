using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterThink_Stand : MonoBehaviour {

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
		mOwner.Idle(float.MaxValue - 1);
	}
	void AddAttack()
	{
		
	}
	void AddDie()
	{
		
	}
}
