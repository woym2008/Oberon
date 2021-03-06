//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.33440
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class MonsterThink_Hurter : MonoBehaviour 
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

		ThinkQueue.Enqueue(curType);
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
	void Update () 
	{
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
		Vector3 target = mOwner.transform.position + Random.onUnitSphere * 2;
		mOwner.MoveTo(new Vector3(target.x,target.y,mOwner.transform.position.z),2);
	}
	
	void AddIdle()
	{
		mOwner.Idle(2f);
	}
	void AddAttack()
	{
		mOwner.Attack(1.0f);
	}
	void AddDie()
	{
		
	}
}