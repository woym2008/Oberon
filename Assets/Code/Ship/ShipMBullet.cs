using UnityEngine;
using System.Collections;

public class ShipMBullet : Bullet {

	public int Power = 0;
	// Use this for initialization
	void Start () {
	
		safeDeleteTime = 2.0f;
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
