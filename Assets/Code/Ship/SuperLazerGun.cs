using UnityEngine;
using System.Collections;

public class SuperLazerGun : BulletEmitter {

	TriangleShip m_MainBody = null;
	
	float m_ReloadTime = 0.5f;
	
	float m_CurReloadTime = 0.0f;
	
	bool m_bEnableSys = false;
	
	public void SetMainBody(TriangleShip ship)
	{
		m_MainBody = ship;
	}
	
	void Update () {
	}
	
	public override void Shoot()
	{
		base.Shoot();
		
		if(SType == ShootType.Shoot_SelfCtrl)
		{
			Bullet bullet = CreateBullet();
			
			bullet.V = this.transform.right;
			bullet.Speed = 2;
			
			bullet.Fly();
			
			if(m_MainBody != null)
			{
				bullet.transform.parent = this.gameObject.transform;
			}
			
		}
	}
}
