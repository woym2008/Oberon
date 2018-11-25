using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss_Worm : MonoBehaviour {

	const int inactive = 0;
	const int active = 1;
	const int complete = 2;	
	protected Queue<int>	ThinkQueue = new Queue<int>();
	protected int	ThinkState = inactive;
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
		case 1: Shoot();break;
		case 2: Attack();break;	
			
		}
		ThinkQueue.Enqueue(curType);
		ThinkState = active;
	}

	public void UpdateThink()
	{
		if(IsInactive())
		{
			PopThink();
		}
	}
	/// <summary>
	/// can shu
	/// </summary>
	/// pu
	public int HP = 8000;
	public GameObject ShellZone;
	protected float ShootCooldown = 1.2f;
	public Boss_WormSpawn MySpawn = null;
	public bool ShootRandomColor = false;
	//
	public GameObject[] ThreeColorBulletPool;

	//---------------------------------------------
	public void Move()
	{
		if(ShellZone != null)
		{
			CircleCollider2D cc = ShellZone.GetComponent<CircleCollider2D>();
			//ShellZone == Body ,so ShellZone.transform.parent == Boss, Boss's scale = 4;
			Vector2 target = Random.insideUnitCircle * (cc.radius - 0.16f) * ShellZone.transform.parent.transform.localScale.x + 
							 cc.offset + new Vector2(ShellZone.transform.position.x,
			                             ShellZone.transform.position.y);
			//
			Drive drive = GetComponent<Drive>();
			drive.MoveTo(new Vector3(target.x,target.y,0));
		}

	}
	public void Shoot()
	{
		Transform gun = transform.Find("Gun");
		if(gun != null)
		{
			BulletEmitter emitter = gun.GetComponent<BulletEmitter>();
			//
			if(emitter != null)
			{
				if(ShootRandomColor)
				{
					GameObject newBullet = ThreeColorBulletPool[Random.Range(0,ThreeColorBulletPool.Length)];
					if(newBullet != null) emitter.bulletPrefab = newBullet;
				}
				emitter.Shoot();
			}
			//
			GetComponent<SpriteRenderer>().color = new Color(0.32f,0.55f,1f,1f);
		//
		}
		Invoke("EndShoot",ShootCooldown);
	}
	public void EndShoot()
	{
		SetThinkInactive();
		GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void Attack()
	{

	}
	//---------------------------------------------
	void Awake() {

		ThinkQueue.Enqueue(0);
		
		ThinkQueue.Enqueue(1);

		ThinkState = active;

	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		UpdateThink();
		//
		if(HP<0)
		{
			Die();
		}

	}

	public void BornEnd()
	{
		GetComponent<Animator>().SetBool("borned",true);
		//
		transform.position += 0.16f * Vector3.up;
		//
		SetThinkInactive();
	}

	public void Die()
	{
		GetComponent<Animator>().SetBool("die",true);		
	}

	public void StartDie()
	{
		GetComponent<CircleCollider2D>().enabled = false;
	}


	public void EndDie()
	{
		MySpawn.ChildBeKilled();

		Destroy(this.gameObject);
	}
	///
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other != null && other.gameObject != null)
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
