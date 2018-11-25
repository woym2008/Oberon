using UnityEngine;
using System.Collections;

public class ShipSBullet : Bullet {

	BulletEmitter m_Emmiter = null;

	public int Power = 0;
	// Use this for initialization
	void Awake()
	{
		m_Emmiter = this.transform.GetComponent<BulletEmitter> ();
	}

	void Start () {
		
		safeDeleteTime = 3.0f;
	}
	
	void Update () {
		if(!CanFly) return ;
		transform.position = transform.position + (new Vector3(mV.x,mV.y,0) * Speed * Time.deltaTime);
		//
		safeDeleteTime -= Time.deltaTime ;
		if(safeDeleteTime <= 0)
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(CanFly == false) return;
		
		if(other != null && other.gameObject != null)
		{
			Monster pMonster = other.gameObject.GetComponent<Monster>();
			if(pMonster != null)
			{
				other.gameObject.SendMessage("Hurt",GetPower());

				if(m_Emmiter != null)
				{
					m_Emmiter.Shoot();
				}

				
				Destroy(this.gameObject);
			}
		}
	}
	//
	public int GetPower()
	{
		return ((Power == 0)? 1000: Power);
	}
}
