using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_1_Logic : MonoBehaviour {

	public Boss_1_Head Head;
	public Boss_Foot[]	Feet;
	protected int CurFeetNum = 0;
	public Boss_MonsterNest[] Nestes;
	public int StateLevel = 1;
	public GameObject Shell;
	public Boss_WormSpawn WormSpawn;
	public bool Invincible = true;	//无敌的
	public float MoveRadius = 4.5f;
	public int HP = 75000;
	protected Vector2 Poll;
	// Use this for initialization
	void Awake()
	{
		ThinkQueue.Enqueue(0);
	}

	void Start () {

		ThinkState = active;
	
		CurFeetNum = Feet.Length;
		//
		Poll = new Vector2(transform.position.x - 0.3f,transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {

		if(HP<=0)
		{
			Die();
		}
	
		UpdateThink();

	}
	//
	public void BreakOneFoot()
	{
		CurFeetNum--;
		//
		if(CurFeetNum <= 0 )
		{

			//激发第二形态
			ActiveSecondState();
		}
	}

	public void ActiveSecondState()
	{
		if(StateLevel == 2) return;

		StateLevel = 2;
		//开启怪物大门
		foreach(Boss_MonsterNest nest in Nestes)
		{
			if(nest != null) nest.Activation();
		}
		//随机移动
		SetThinkInactive();
		//小虫会发3色炮弹
		Boss_Worm[] worms = GetComponentsInChildren<Boss_Worm>();
		if(worms != null)
		{
			foreach(Boss_Worm worm in worms)
			{
				worm.ShootRandomColor = true;
			}
		}
		//Head
		if(Head != null)
		{
			Head.EnableSecondGun = true;
		}
		//去除Shell的无敌状态
		Invincible = false;

	}
	//
	///
	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject != null)
		{
			TriangleShip pPCtrl = other.gameObject.GetComponent<TriangleShip>();
			if(pPCtrl != null)
			{
				pPCtrl.HurtByEnemy();
			}
		}
		//
		if(other != null && other.gameObject != null)
		{

			if(Invincible) return;

			ShipMBullet Mbullet = other.gameObject.GetComponent<ShipMBullet>();
			ShipSBullet Sbullet = other.gameObject.GetComponent<ShipSBullet>();
			ShipLBullet Lbullet = other.gameObject.GetComponent<ShipLBullet>();
			
			
			if(Mbullet != null )
			{
				
				HP -= Mbullet.GetPower();
				//
				Hurt();
				//
				Destroy(Mbullet.gameObject);
			}
			else if(Sbullet != null)
			{
				
				HP -= Sbullet.GetPower();
				//
				Hurt();
				//
				Destroy(Sbullet.gameObject);
			}
			else if(Lbullet != null)
			{
				
				HP -= Lbullet.GetPower();
				//
				Hurt();
				//
				Destroy(Lbullet.gameObject);
			}
			
		}

	}

	//
	public void Hurt()
	{
		if(HP<= 0) return;

		GetComponent<Animator>().ResetTrigger("hurt");
		GetComponent<Animator>().SetTrigger("hurt");	

		if(MusicManager.GetInstance().SFXCtrl != null)
		{
			MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_BossHit);
		}

		GameManager.getInstance ().AddGameScore (10.0f);
	}
	public void EndHurt()
	{
		GetComponent<Animator>().ResetTrigger("hurt");	
		
		
	}

	public void Die()
	{
		GetComponent<Animator>().SetBool("die",true);
		//
		WormSpawn.GameOver_For_WormSpawn();
		Head.gameObject.SetActive(false);
		//
		foreach(Boss_MonsterNest nest in Nestes)
		{
			if(nest != null) nest.UnActivation();
		}
		
	}
	public void EndDie()
	{

		TriangleShip ship = PlayerManager.getInstance ().GetPlayer ().GetComponent<TriangleShip>();
		if(ship != null)
		{
			ship.ShipVictory();
		}

		//Destroy(this.gameObject);
	}

	/////////////////////////////////////////////////////
	/// 	
	const int inactive = 0;
	const int active = 1;
	const int complete = 2;	
	protected Queue<int>	ThinkQueue = new Queue<int>();
	protected int	ThinkState = inactive;
	protected bool CanThink = true;
	public void DisableThink()
	{
		CanThink = false;
		//
		Drive drive = GetComponent<Drive>();
		if(drive != null) drive.enabled = false;
	}
	///
	public void SetThinkInactive() 
	{ 
		ThinkState = inactive ;
	}
	public bool IsInactive()
	{
		return ThinkState == inactive;
	}
	
	public void PopThink()
	{
		int curType = ThinkQueue.Dequeue();
		
		
		switch(curType)
		{
			case 0: Move();break;			
		}
		ThinkQueue.Enqueue(curType);
		ThinkState = active;
	}
	
	public void UpdateThink()
	{
		if(CanThink == false) return;

		if(IsInactive())
		{
			PopThink();
		}
	}

	public void Move()
	{
		Vector2 target = Random.insideUnitCircle * MoveRadius + Poll ;
		//
		Drive drive = GetComponent<Drive>();
		if(drive != null) drive.MoveTo(new Vector3(target.x,target.y,0));
	}
}
