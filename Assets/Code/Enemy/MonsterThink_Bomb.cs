using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterThink_Bomb : MonoBehaviour 
{

	Monster	mOwner;
	
	public bool ReverseMove = false;
	
	const int inactive = 0;
	const int active = 1;
	const int complete = 2;
	
	public float FallingSpeed = 1.0f;
	public float MoveSpeed = 50.0f;

	bool AlreadyFall = false;
	
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
		
		ThinkState = active;
	}
	
	public void ParseThinkType()
	{
		
	}
	
	void Awake() 
	{
		mOwner  = GetComponent<Monster>();

		ThinkQueue.Enqueue(MonsterFSM.MS_Idle);
		
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
		Vector2 curpos = this.transform.position;
		if (this.transform.localScale.y < 0.0f) 
		{
			Vector2 targetPos = new Vector2(Random.Range(curpos.x-0.6f,curpos.x+0.6f),Random.Range(curpos.y-2.5f,curpos.y-1.0f));
			
			mOwner.MoveTo(targetPos,MoveSpeed);
		}
		else
		{
			Vector2 targetPos = new Vector2(Random.Range(curpos.x-0.6f,curpos.x+0.6f),Random.Range(curpos.y+1.0f,curpos.y+2.5f));
			
			mOwner.MoveTo(targetPos,MoveSpeed);
		}

	}
	
	void AddIdle()
	{
		mOwner.Idle (0.5f);
	}
	void AddAttack()
	{
		Vector2 curpos = this.transform.position;
		Vector2 targetPos = new Vector2(curpos.x,curpos.y-9999.0f);

		mOwner.MoveTo(targetPos,FallingSpeed);

		AlreadyFall = true;
	}
	void AddDie()
	{
		
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject != null)
		{
			if(AlreadyFall == false)
			{
				SceneEdge edge = collision.gameObject.GetComponent<SceneEdge>();
				if(edge != null)
				{
					SetThinkInactive();
				}
			}
		}
	}
}
