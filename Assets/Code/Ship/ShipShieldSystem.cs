using UnityEngine;
using System.Collections;

public enum ShieldType
{
	nulltype = 0,
	Shield_1,
	Shield_2,
	Shield_3,
}

public class ShipShieldSystem : MonoBehaviour {

	bool m_bEnableSys = false;

	public ShieldType m_SelfType = ShieldType.nulltype;

	TriangleShip m_MainBody = null;

	public ShipShieldGun m_EmmiterBlueGun = null;
	public ShipShieldGun m_EmmiterYellowGun = null;
	public ShipShieldGun m_EmmiterGreenGun = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMainBody(TriangleShip ship)
	{
		m_MainBody = ship;

		m_EmmiterBlueGun.SetMainBody (ship);
		m_EmmiterYellowGun.SetMainBody (ship);
		m_EmmiterGreenGun.SetMainBody (ship);

	}

	void CloseShieldGuns()
	{
		if(m_EmmiterBlueGun != null)
		{
			m_EmmiterBlueGun.DisableGunSystem();
			//m_EmmiterBlueGun.gameObject.SetActive(false);
		}
		if(m_EmmiterYellowGun != null)
		{
			m_EmmiterYellowGun.DisableGunSystem();
			//m_EmmiterYellowGun.gameObject.SetActive(false);
		}
		if(m_EmmiterGreenGun != null)
		{
			m_EmmiterGreenGun.DisableGunSystem();
			//m_EmmiterGreenGun.gameObject.SetActive(false);
		}
	}

	public void EnableShieldSystem()
	{
		m_bEnableSys = true;

		CloseShieldGuns ();

		switch(m_MainBody.m_CurFrontType)
		{
			case ShipFrontType.FrontType_Shild1:
			{
				if(m_SelfType == ShieldType.Shield_1)
				{
					if(m_EmmiterBlueGun != null)
					{
						m_EmmiterBlueGun.EnableGunSystem();
						//m_EmmiterBlueGun.gameObject.SetActive(true);
					}
					if(m_EmmiterGreenGun != null)
					{
						m_EmmiterGreenGun.EnableGunSystem();
						//m_EmmiterGreenGun.gameObject.SetActive(true);
					}
					if(m_EmmiterYellowGun != null)
					{
						m_EmmiterYellowGun.EnableGunSystem();
						//m_EmmiterYellowGun.gameObject.SetActive(true);
					}
				}
			}
				break;
			case ShipFrontType.FrontType_Shild2:
			{
				if(m_SelfType == ShieldType.Shield_2)
				{
					if(m_EmmiterGreenGun != null)
					{
						m_EmmiterGreenGun.EnableGunSystem();
						//m_EmmiterYellowGun.gameObject.SetActive(true);
					}
					if(m_EmmiterBlueGun != null)
					{
						m_EmmiterBlueGun.EnableGunSystem();
						//m_EmmiterBlueGun.gameObject.SetActive(true);
						
					}
					if(m_EmmiterYellowGun != null)
					{
						m_EmmiterYellowGun.EnableGunSystem();
						//m_EmmiterYellowGun.gameObject.SetActive(true);
					}
				}
				
			}
				break;
			case ShipFrontType.FrontType_Shild3:
			{
				if(m_SelfType == ShieldType.Shield_3)
				{
					if(m_EmmiterBlueGun != null)
					{
						m_EmmiterBlueGun.EnableGunSystem();
						//m_EmmiterYellowGun.gameObject.SetActive(true);
					}
					if(m_EmmiterGreenGun != null)
					{
						m_EmmiterGreenGun.EnableGunSystem();
						//m_EmmiterGreenGun.gameObject.SetActive(true);				
					}
					if(m_EmmiterYellowGun != null)
					{
						m_EmmiterYellowGun.EnableGunSystem();
						//m_EmmiterYellowGun.gameObject.SetActive(true);
					}
				}
			}
				break;
			}
	}
	
	public void DisableShieldSystem()
	{
		m_bEnableSys = false;

		CloseShieldGuns ();
	}

	public void RecoveryWeapon()
	{
	}

	void OnTriggerEnter2D(Collider2D other)
	{		
		if(m_bEnableSys == false)
		{
			return;
		}
		if(other != null && other.gameObject != null)
		{
			Monster enemyMon = other.gameObject.GetComponent<Monster>();

			EnemyBullet enemybu = other.gameObject.GetComponent<EnemyBullet>();

			if(enemyMon != null)
			{}
			else if(enemybu != null)
			{
				switch(m_MainBody.m_CurFrontType)
				{
				case ShipFrontType.FrontType_Shild1:
				{
					if(enemybu.getBulletType() == EnemyBulletType.EnemyBullet_Blue)
					{
						EffectManager.GetInstance().CreateEffect(EffectType.Effect_Absorb,other.gameObject.transform.position,this.transform);
						m_MainBody.ShiledBulletAbsorb(EnemyBulletType.EnemyBullet_Blue,enemybu.EnemyBulletEnergy);
						other.gameObject.SendMessage("DestroySelf");
					}
					else if(m_MainBody.GetCurShipEnergy() > 0.0f)
					{
						EffectManager.GetInstance().CreateEffect(EffectType.Effect_Reduce,other.gameObject.transform.position,this.transform);
						m_MainBody.ShiledBulletReduce(EnemyBulletType.EnemyBullet_Blue,enemybu.EnemyBulletEnergy);
						other.gameObject.SendMessage("DestroySelf");
					}
				}
					break;
				case ShipFrontType.FrontType_Shild2:
				{
					if(enemybu.getBulletType() == EnemyBulletType.EnemyBullet_Green)
					{
						EffectManager.GetInstance().CreateEffect(EffectType.Effect_Absorb,other.gameObject.transform.position,this.transform);
						m_MainBody.ShiledBulletAbsorb(EnemyBulletType.EnemyBullet_Green,enemybu.EnemyBulletEnergy);
						other.gameObject.SendMessage("DestroySelf");
					}
					else if(m_MainBody.GetCurShipEnergy() > 0.0f)
					{
						EffectManager.GetInstance().CreateEffect(EffectType.Effect_Reduce,other.gameObject.transform.position,this.transform);
						m_MainBody.ShiledBulletReduce(EnemyBulletType.EnemyBullet_Green,enemybu.EnemyBulletEnergy);
						other.gameObject.SendMessage("DestroySelf");
					}
				}
					break;
				case ShipFrontType.FrontType_Shild3:
				{
					if(enemybu.getBulletType() == EnemyBulletType.EnemyBullet_Yellow)
					{
						EffectManager.GetInstance().CreateEffect(EffectType.Effect_Absorb,other.gameObject.transform.position,this.transform);
						m_MainBody.ShiledBulletAbsorb(EnemyBulletType.EnemyBullet_Yellow,enemybu.EnemyBulletEnergy);
						other.gameObject.SendMessage("DestroySelf");
					}
					else if(m_MainBody.GetCurShipEnergy() > 0.0f)
					{
						EffectManager.GetInstance().CreateEffect(EffectType.Effect_Reduce,other.gameObject.transform.position,this.transform);
						m_MainBody.ShiledBulletReduce(EnemyBulletType.EnemyBullet_Yellow,enemybu.EnemyBulletEnergy);
						other.gameObject.SendMessage("DestroySelf");
					}
				}
					break;
				default:
				{
					switch(m_SelfType)
					{
					case ShieldType.Shield_1:
					{
						if(enemybu.getBulletType() == EnemyBulletType.EnemyBullet_Blue)
						{
							EffectManager.GetInstance().CreateEffect(EffectType.Effect_Absorb,other.gameObject.transform.position,this.transform);
							m_MainBody.ShiledBulletAbsorb(EnemyBulletType.EnemyBullet_Blue,enemybu.EnemyBulletEnergy);
							other.gameObject.SendMessage("DestroySelf");
						}
						else if(m_MainBody.GetCurShipEnergy() > 0.0f)
						{
							EffectManager.GetInstance().CreateEffect(EffectType.Effect_Reduce,other.gameObject.transform.position,this.transform);
							m_MainBody.ShiledBulletReduce(EnemyBulletType.EnemyBullet_Blue,enemybu.EnemyBulletEnergy);
							other.gameObject.SendMessage("DestroySelf");
						}
					}
						break;
					case ShieldType.Shield_2:
					{
						if(enemybu.getBulletType() == EnemyBulletType.EnemyBullet_Green)
						{
							EffectManager.GetInstance().CreateEffect(EffectType.Effect_Absorb,other.gameObject.transform.position,this.transform);
							m_MainBody.ShiledBulletAbsorb(EnemyBulletType.EnemyBullet_Green,enemybu.EnemyBulletEnergy);
							other.gameObject.SendMessage("DestroySelf");
						}
						else if(m_MainBody.GetCurShipEnergy() > 0.0f)
						{
							EffectManager.GetInstance().CreateEffect(EffectType.Effect_Reduce,other.gameObject.transform.position,this.transform);
							m_MainBody.ShiledBulletReduce(EnemyBulletType.EnemyBullet_Blue,enemybu.EnemyBulletEnergy);
							other.gameObject.SendMessage("DestroySelf");
						}
					}
						break;
					case ShieldType.Shield_3:
					{
						if(enemybu.getBulletType() == EnemyBulletType.EnemyBullet_Yellow)
						{
							EffectManager.GetInstance().CreateEffect(EffectType.Effect_Absorb,other.gameObject.transform.position,this.transform);
							m_MainBody.ShiledBulletAbsorb(EnemyBulletType.EnemyBullet_Yellow,enemybu.EnemyBulletEnergy);
							other.gameObject.SendMessage("DestroySelf");
						}
						else if(m_MainBody.GetCurShipEnergy() > 0.0f)
						{
							EffectManager.GetInstance().CreateEffect(EffectType.Effect_Reduce,other.gameObject.transform.position,this.transform);
							m_MainBody.ShiledBulletReduce(EnemyBulletType.EnemyBullet_Blue,enemybu.EnemyBulletEnergy);
							other.gameObject.SendMessage("DestroySelf");
						}
					}
						break;
					}
				}
					break;
				}

			}
		}
	}
}
