using UnityEngine;
using System.Collections;

public class ShootLoopAround : MonoBehaviour {

	public enum LoopType
	{
		Once,Pingpang,Normal
	}

	public float ShootGap = 0.33f;
	public float ShootLoopTotalTime = 3f;
	protected float ShootTimer = 0f;
	protected int ShootedCounter = 0;
	public bool Clockwise = false;
	protected int Rot = 0;	//
	public float ShootBeginAngel = 0;
	public float ShootTotalAngel = 180;
	//
	public LoopType ShootLoopType = LoopType.Once;
	public int 		LoopTotalNum = 1;
	protected int   LoopCounter = 0;
	//
	protected int MaxShootNum = 0;
	protected float AngelGap = 0;
	//
	//因为是逆时针，用到圆的参数方程
	//x=a+r*cos@    y=b+r*sin@   (a,b) 为圆心坐标，r为圆半径，@为参数
	//
	public bool OnShooting = false;
	public void Shoot()
	{
		if(OnShooting == false)
		{
			Reset();
			//
			Rot = (Clockwise == true? 1: -1);
			ShootTimer = ShootGap;
			MaxShootNum = (int)(ShootLoopTotalTime / ShootGap);
			AngelGap =  (int)(ShootTotalAngel / MaxShootNum);
			OnShooting = true;
		}
	}

	public void HoldOn()
	{
		OnShooting = false;
	}

	public void Reset()
	{
		ShootTimer = 0;
		ShootedCounter = 0;
		MaxShootNum = 0;
		LoopCounter = 0;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(OnShooting == true)
		{
			ShootTimer -= Time.deltaTime;

			if(ShootTimer<= 0)
			{
				Bullet bullet = CreateBullet();

				Vector2 target = new Vector2(/*transform.position.x + */Mathf.Cos( 0 + Mathf.Deg2Rad * (ShootBeginAngel + Rot * AngelGap * ShootedCounter)),
				                             /*transform.position.y + */Mathf.Sin( 0 + Mathf.Deg2Rad * (ShootBeginAngel + Rot * AngelGap * ShootedCounter)));


				if(bullet != null)
				{
					bullet.V = target;

					if(bullet.Speed == 0)
					{
						bullet.Speed = 7;
					}
					
					bullet.Fly();	
				}

				ShootTimer = ShootGap;
				ShootedCounter++;
				//
				if(ShootedCounter > MaxShootNum)
				{
					FinishOnceLoop();
				}
			}
		}
	}
	//
	void FinishOnceLoop()
	{
		OnShooting = false;
		LoopCounter++;

		if(LoopCounter < LoopTotalNum)
		{
			if(ShootLoopType == LoopType.Once)
			{
			}
			else if(ShootLoopType == LoopType.Normal)
			{
				Shoot();
			}
			else if(ShootLoopType == LoopType.Pingpang)
			{
				//
				Clockwise = !Clockwise;
				ShootBeginAngel = ShootBeginAngel + ShootTotalAngel * (Clockwise == true? 1: -1);
				ShootTotalAngel = 180;
				//
				Shoot();
			}
			//
		}
	}
	//
	Bullet CreateBullet()
	{
		BulletEmitter be = GetComponent<BulletEmitter>();
		if(be != null)
		{
			return be.CreateBullet();
		}
		//
		return null;
	}
}
