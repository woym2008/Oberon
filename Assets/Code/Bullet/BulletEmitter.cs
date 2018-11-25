using UnityEngine;
using System.Collections;

public class BulletEmitter : MonoBehaviour {

	Vector2 TR = new Vector2(0.707f,0.707f);
	Vector2 TL = new Vector2(-0.707f,0.707f);
	Vector2 BR = new Vector2(0.707f,-0.707f);
	Vector2 BL = new Vector2(-0.707f,-0.707f);


	Vector2[]	EightDir = new Vector2[8];
	// Use this for initialization
	public GameObject bulletPrefab;
	public ShootType SType;
	public Vector2	BaseDir = new Vector2 (1.0f,0.0f);

	public enum ShootType
	{
		Shoot_Straight,
		Shoot_8_Dir,
		Shoot_3_Dir,
		Shoot_Player,
		Shoot_LoopAround,
		Shoot_SelfCtrl,
		Shoot_Random,

	}

	void Awake()
	{
		if(bulletPrefab != null)
			bulletPrefab.gameObject.SetActive(false);
		//
		EightDir[0] = Vector2.up;
		EightDir[1] = -Vector2.up;
		EightDir[2] = Vector2.right;
		EightDir[3] = -Vector2.right;
		EightDir[4] = TR;
		EightDir[5] = TL;
		EightDir[6] = BR;
		EightDir[7] = BL;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if(Input.GetKeyDown(KeyCode.A))
		//{
		//	Shoot();
		//}
	}

	public void HoldOn()
	{
		if(SType == ShootType.Shoot_LoopAround)
		{
			ShootLoopAround	sla = GetComponent<ShootLoopAround>();
			if(sla != null)
			{
				sla.HoldOn();
			}
		}
	}

	public virtual void Shoot()
	{

		if(SType == ShootType.Shoot_Straight)
		{
			Bullet bullet = CreateBullet();
			
			bullet.V = BaseDir;

			if(bullet.Speed == 0 )bullet.Speed = 7;
			
			bullet.Fly();
		}
		else if(SType == ShootType.Shoot_8_Dir)
		{
			foreach(Vector2 dir in EightDir)
			{
				Bullet bullet = CreateBullet();
				
				bullet.V = dir;
				if(bullet.Speed == 0 )bullet.Speed = 7;
				
				bullet.Fly();
			}
		}
		else if(SType == ShootType.Shoot_3_Dir)
		{
			Vector2 dir = this.gameObject.transform.right;
			Quaternion qt = Quaternion.AngleAxis(30.0f,new Vector3(0.0f,0.0f,1.0f));
			Quaternion qt2 = Quaternion.AngleAxis(-30.0f,new Vector3(0.0f,0.0f,1.0f));
			Vector2 dir2 = qt * dir;
			Vector2 dir3 = qt2 * dir;

			Bullet bullet = CreateBullet();
				
			bullet.V = dir;
			if(bullet.Speed == 0 )bullet.Speed = 5;
			bullet.gameObject.transform.up = -dir;
				
			bullet.Fly();

			Bullet bullet2 = CreateBullet();
			
			bullet2.V = dir2;
			if(bullet.Speed == 0 )bullet2.Speed = 5;
			bullet2.gameObject.transform.up = -dir2;
			
			bullet2.Fly();

			Bullet bullet3 = CreateBullet();
			
			bullet3.V = dir3;
			if(bullet.Speed == 0 )bullet3.Speed = 5;
			bullet3.gameObject.transform.up = -dir3;
			
			bullet3.Fly();
		}
		else if(SType == ShootType.Shoot_Random)
		{
			Bullet bullet = CreateBullet();

			Vector2 rndDir = new Vector2( Random.Range(-1.0f,1.0f),Random.Range(-1.0f,1.0f));
			rndDir.Normalize();

			bullet.V = rndDir;
			if(bullet.Speed == 0 )bullet.Speed = 7;
			
			bullet.Fly();
		}
		else if(SType == ShootType.Shoot_Player)
		{
			GameObject go = PlayerManager.getInstance().GetPlayer();
			if(go != null)
			{
				Vector3 tarV3 = go.transform.position;
				
				Bullet bullet = CreateBullet();
				
				bullet.SetTarget(new Vector2(go.transform.position.x,go.transform.position.y));
				if(bullet.Speed == 0 )bullet.Speed = 7;
				
				bullet.Fly();				
			}
		}
		else if(SType == ShootType.Shoot_LoopAround)
		{
			ShootLoopAround	sla = GetComponent<ShootLoopAround>();
			if(sla != null)
			{
				sla.Shoot();
			}
		}
	}
	//
	public Bullet CreateBullet()
	{
		GameObject go = GameObject.Instantiate(bulletPrefab) as GameObject;
		go.transform.position = new Vector3(gameObject.transform.position.x,
		                                    gameObject.transform.position.y,
		                                    gameObject.transform.position.z);
		go.SetActive(true);
		Bullet bullet = go.GetComponent<Bullet>();
		return bullet;
	}
}
