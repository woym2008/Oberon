using UnityEngine;
using System.Collections;

public class ShipSuperLBullet : Bullet {

	public int Power = 5000;
	
	// Use this for initialization
	void Start () {
		safeDeleteTime = 3.0f;
	}
	
	void Update () {
		if(!CanFly) return ;
		Vector2 dir = new Vector2(1.0f,0.0f);
		dir.Normalize ();
		//transform.localPosition = transform.localPosition + (new Vector3(dir.x,0,0) * Speed * Time.deltaTime);
		//
		safeDeleteTime -= Time.deltaTime ;
		if(safeDeleteTime <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(CanFly == false) return;
		
		if(other != null && other.gameObject != null)
		{
			Monster pMonster = other.gameObject.GetComponent<Monster>();
			if(pMonster != null)
			{
				other.gameObject.SendMessage("Hurt",GetPower());
				
				//Destroy(this.gameObject);
			}
		}
	}

	public int GetPower()
	{
		return ((Power == 0)? 1: Power);
	}
}
