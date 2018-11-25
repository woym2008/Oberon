using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour 
{
	public bool IsHurtPlayer = false;

	public bool IsHurtEnemy = false;

	public bool IsHurtPlayerBullet = false;

	public bool IsHurtEnemyBullet = false;

	public float LiftTime = 1.0f;

	public float HurtPlayerValue = 100.0f;

	public float HurtEnemyValue = 100.0f;

	public float BombTime = 0.1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		LiftTime -= Time.deltaTime;

		if(LiftTime <=0.0f)
		{
			Destroy(this.gameObject);
		}

		if(GetComponent<CircleCollider2D>() != null && GetComponent<CircleCollider2D>().enabled == true)
		{
			BombTime -= Time.deltaTime;
			if(BombTime <= 0.0f)
			{
				GetComponent<CircleCollider2D>().enabled = false;	
			}
		}
	}

	void OnEnable()
	{

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other != null && other.gameObject != null) 
		{
			if (IsHurtPlayer) 
			{
				TriangleShip ship = other.gameObject.GetComponent<TriangleShip>();
			
				if(ship != null)
				{
					ship.HurtByEnemy(HurtPlayerValue);
				}
			}

			if (IsHurtEnemy) 
			{
				Monster mon = other.gameObject.GetComponent<Monster>();
				
				if(mon != null)
				{
					mon.Hurt(HurtEnemyValue);
				}
			}

			if (IsHurtPlayerBullet) 
			{
				ShipMBullet bu = other.gameObject.GetComponent<ShipMBullet>();
				
				if(bu != null)
				{
					bu.DestroySelf();
				}
			}

			if (IsHurtEnemyBullet) 
			{
				EnemyBullet bu = other.gameObject.GetComponent<EnemyBullet>();
				
				if(bu != null)
				{
					bu.DestroySelf();
				}
			}
		}

	}
}
