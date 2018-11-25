using UnityEngine;
using System.Collections;

public enum EnemyBulletType
{
	EnemyBullet_Blue = 0,
	EnemyBullet_Green,
	EnemyBullet_Yellow,

}

public class EnemyBullet : Bullet {

	public EnemyBulletType m_BulletType;

	public float EnemyBulletEnergy = 20.0f;

	void Start()
	{
		safeDeleteTime = 3f;
	}

	public EnemyBulletType getBulletType()
	{
		return m_BulletType;
	}
	
	public void setBulletType(EnemyBulletType type)
	{
		m_BulletType = type;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(CanFly == false) return;
		
		if(other != null && other.gameObject != null)
		{
			TriangleShip pShip = other.gameObject.GetComponent<TriangleShip>();
			if(pShip != null)
			{
				other.gameObject.SendMessage("HurtByEnemy",1000.0f);
				
				Destroy(this.gameObject);
			}
		}
	}
}
