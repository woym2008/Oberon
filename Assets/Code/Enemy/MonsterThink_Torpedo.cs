using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterThink_Torpedo : MonoBehaviour {

	Monster	mOwner;
	
	const int inactive = 0;
	const int active = 1;
	const int complete = 2;
	
	public Queue<int>	ThinkQueue = new Queue<int>();
	
	int	ThinkState = inactive;	

	bool hasAttack = false;

	float m_FindDis = 8.0f;
	
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

		ThinkQueue.Enqueue(MonsterFSM.MS_Idle);

		ThinkQueue.Enqueue(MonsterFSM.MS_Move);

		ThinkQueue.Enqueue(MonsterFSM.MS_Die);
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

		/*
		GameObject player = PlayerManager.getInstance ().GetPlayer ();

		if(player != null)
		{
			float dis = Vector2.Distance (player.transform.position, this.gameObject.transform.position);
			
			if(dis < m_FindDis && hasAttack == false)
			{
				PopThink();
				
				hasAttack = true;
			}
		}
		*/

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
		GameObject player = PlayerManager.getInstance ().GetPlayer ();

		if(player != null)
		{
			Vector2 curpos = player.transform.position;
			Vector2 targetPos = new Vector2(curpos.x + 1.2f,curpos.y);
			mOwner.MoveTo(targetPos,5f);
			
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
		BombManager.GetInstance ().CreateBomb (BombType.BombType_1, this.transform.position);

		if(MusicManager.GetInstance().SFXCtrl != null)
		{
			MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_EnemyBomb);
		}

		Destroy (this.gameObject);
	}
}
