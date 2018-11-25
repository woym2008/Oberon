using UnityEngine;
using System.Collections;

public enum ShipGunType
{
	ShipGunType_MachineGun,
	ShipGunType_ExplosionGun,
	ShipGunType_Lazer,
}

public class ShipGunSystem : BulletEmitter 
{
	TriangleShip m_MainBody = null;

	public float m_ReloadTime = 0.2f;

	public float FastReloadTime = 0.05f;

	float m_CurReloadTime = 0.0f;

	bool m_bEnableSys = false;

	bool m_bEnableFastShoot = false;

	public ShipGunType m_CurGuntType = ShipGunType.ShipGunType_MachineGun;


	public void EnableFastShoot(){
		m_bEnableFastShoot = true;
	}

	public void DisableFastShoot(){
		m_bEnableFastShoot = false;
	}

	public void SetMainBody(TriangleShip ship)
	{
		m_MainBody = ship;
	}

	void Update () {

		if(m_bEnableSys == true)
		{
			if(m_bEnableFastShoot == true)
			{
				if(m_CurReloadTime >= 0.0f)
				{
					m_CurReloadTime -= Time.deltaTime;
				}
				else
				{
					Shoot();
					
					m_CurReloadTime = FastReloadTime;
				}
			}
			else
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
	}

	public override void Shoot()
	{
		base.Shoot();

		if(SType == ShootType.Shoot_SelfCtrl)
		{
			if(m_CurGuntType == ShipGunType.ShipGunType_MachineGun)
			{
				Bullet bullet = CreateBullet();
				
				bullet.V = this.transform.right;
				//bullet.Speed = 5;
				
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
			else if(m_CurGuntType == ShipGunType.ShipGunType_ExplosionGun)
			{
				Bullet bullet = CreateBullet();
				
				bullet.V = this.transform.right;
				//bullet.Speed = 5;
				
				bullet.transform.eulerAngles = this.transform.eulerAngles;
				
				bullet.Fly();
				
				if(m_MainBody != null)
				{
					bullet.transform.parent = m_MainBody.gameObject.transform.parent.parent;
				}
				
				if(MusicManager.GetInstance().SFXCtrl != null)
				{
					MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_Fire2);
				}
			}
			else if(m_CurGuntType == ShipGunType.ShipGunType_Lazer)
			{
				Bullet bullet = CreateBullet();
				
				bullet.V = this.transform.right;
				//bullet.Speed = 5;
				
				bullet.transform.eulerAngles = this.transform.eulerAngles;
				
				bullet.Fly();
				
				if(m_MainBody != null)
				{
					bullet.transform.parent = m_MainBody.gameObject.transform.parent.parent;
				}
				
				if(MusicManager.GetInstance().SFXCtrl != null)
				{
					MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_Fire3);
				}
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

	public void RecoveryWeapon()
	{}
}
