using UnityEngine;
using System.Collections;

public class ShipShieldGun : BulletEmitter
{
	public float m_ReloadTime = 0.2f;

	float m_CurReloadTime = 0.0f;

	bool m_bEnableSys = false;

	TriangleShip m_MainBody = null;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		
		if(m_bEnableSys == true)
		{
			if(m_CurReloadTime >= 0.0f)
			{
				m_CurReloadTime -= Time.deltaTime;
			}
			else
			{
				Shoot();
				
				m_CurReloadTime = m_ReloadTime;
			}	
		}
	}

	public void SetMainBody(TriangleShip body)
	{
		m_MainBody = body;
	}

	public override void Shoot()
	{
		base.Shoot();
		
		if(SType == ShootType.Shoot_SelfCtrl)
		{
			Bullet bullet = CreateBullet();
			
			bullet.V = this.transform.right;
			bullet.Speed = 5;
			
			bullet.transform.eulerAngles = this.transform.eulerAngles;
			
			bullet.Fly();

			if(m_MainBody != null)
			{
				bullet.transform.parent = m_MainBody.gameObject.transform.parent.parent;
			}

			if(MusicManager.GetInstance().SFXCtrl != null)
			{
				MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_Fire);
			}
		}
	}
	
	public void EnableGunSystem()
	{
		m_bEnableSys = true;
	}
	
	public void DisableGunSystem()
	{
		m_bEnableSys = false;
	}
}
