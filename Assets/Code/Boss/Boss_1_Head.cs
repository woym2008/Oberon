using UnityEngine;
using System.Collections;

public class Boss_1_Head : MonoBehaviour {

	public enum Boss_1_State
	{
		Hide,Show,Back,Attack,Idle
	}
	public int HP = 70000;
	public float SleepTime = 5.0f;
	public Boss_1_State State = Boss_1_State.Hide;
	// Use this for initialization
	public GameObject Gun;
	public GameObject SecondGun;
	public bool EnableSecondGun = false;
	
	void Awake()
	{
		//GetComponent<Animator>().animation.Stop();
	}

	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		if(HP<0)
		{
			GetComponent<Animator>().SetBool("die",true);
		}
	}

	public void ActiveBoss()
	{
		Invoke("BossWake",SleepTime);
	}

	public void BossWake()
	{
		GetComponent<Animator>().GetComponent<Animation>().Play();
		
	}
	public void StartIdle()
	{
		State = Boss_1_State.Idle;
	}
	public void EndIdle()
	{

	}
	//
	public float AttackTotalTime = 6;

	public void StartAttack()
	{
		State = Boss_1_State.Attack;
		GetComponent<Animator>().SetBool("attack",true);
		
		//
		{
			BulletEmitter emitter = Gun.GetComponent<BulletEmitter>();

			if(emitter != null)
			{
				emitter.Shoot();
			}
		}
		//
		if(EnableSecondGun)
		{
			BulletEmitter emitter = SecondGun.GetComponent<BulletEmitter>();
			//
			if(emitter != null)
			{
				emitter.Shoot();
			}

		}
		//
		Invoke("EndAttack",AttackTotalTime);
	}
	public void EndAttack()
	{
		GetComponent<Animator>().SetBool("attack",false);
		//
		{
			BulletEmitter emitter = Gun.GetComponent<BulletEmitter>();
			//
			if(emitter != null)
			{
				emitter.HoldOn();
			}
		}
		if(EnableSecondGun)
		{
			BulletEmitter emitter = SecondGun.GetComponent<BulletEmitter>();
			//
			if(emitter != null)
			{
				emitter.HoldOn();
			}
			
		}
	}
	//
	public void StartShowHead()
	{
		State = Boss_1_State.Show;
	}
	public void EndShowHead()
	{
		StartAttack();
	}
	//
	public void StartBackHead()
	{
		State = Boss_1_State.Back;
	}
	public void EndBackHead()
	{
		
	}
	//
	public void StartHideHead()
	{
		State = Boss_1_State.Hide;
	}
	public void EndHideHead()
	{
		
	}
	//
	public void StartDie()
	{	
		GetComponent<CircleCollider2D>().enabled = false;
	}
	public void EndDie()
	{
		Destroy(this.gameObject);
	}
	///
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other != null && other.gameObject != null)
		{
			if(State == Boss_1_State.Attack || State == Boss_1_State.Idle)
			{
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
	}

	//
	public void Hurt()
	{
		GetComponent<Animator>().ResetTrigger("hurt");	
		GetComponent<Animator>().SetTrigger("hurt");	
	}
	public void EndHurt()
	{
		GetComponent<Animator>().ResetTrigger("hurt");	
	}
}
