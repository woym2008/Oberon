using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterThink_Turn : MonoBehaviour {

	Monster	mOwner;

	public bool ReverseMove = false;
	
	const int inactive = 0;
	const int active = 1;
	const int complete = 2;

	public float turnDis = 3.2f;

	public bool FlyToUp = true;

	public Vector2 flyupDir = new Vector2 (-0.8f,1.0f);

	public Vector2 flydownDir = new Vector2 (-0.8f,-1.0f);
	
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
		if(ReverseMove == false)
		{
			// fly to Left forever
			Vector2 curpos = this.transform.position;
			Vector2 targetPos = new Vector2(curpos.x-turnDis,curpos.y);
			
			mOwner.MoveTo(targetPos,2f);
		}
		else
		{
			// fly to Left forever
			Vector2 curpos = this.transform.position;
			Vector2 targetPos = new Vector2(curpos.x+turnDis*0.25f,curpos.y);
			
			mOwner.MoveTo(targetPos,2f);
		}
	}
	
	void AddIdle()
	{
		mOwner.Idle(1f);
	}
	void AddAttack()
	{
		GameObject player = PlayerManager.getInstance ().GetPlayer ();

		if(player == null) return;
		
		Vector2 curpos = player.transform.position;

		FlyToUp = curpos.y > mOwner.transform.position.y ? true:false;

		if(ReverseMove)
		{
			if(FlyToUp)
			{
				flyupDir.x = -flyupDir.x;
				Vector2 targetPos = new Vector2(this.transform.position.x,this.transform.position.y) + flyupDir * 100.0f;
				
				mOwner.MoveTo(targetPos,2f);
			}
			else
			{
				flydownDir.x = -flydownDir.x;
				Vector2 targetPos = new Vector2(this.transform.position.x,this.transform.position.y) + flydownDir * 100.0f;
				
				mOwner.MoveTo(targetPos,2f);
			}
		}
		else
		{
			if(FlyToUp)
			{
				Vector2 targetPos = new Vector2(this.transform.position.x,this.transform.position.y) + flyupDir * 100.0f;

				mOwner.MoveTo(targetPos,2f);
			}
			else
			{
				Vector2 targetPos = new Vector2(this.transform.position.x,this.transform.position.y) + flydownDir * 100.0f;
				
				mOwner.MoveTo(targetPos,2f);
			}
		}
	}
	void AddDie()
	{
		
	}
}
