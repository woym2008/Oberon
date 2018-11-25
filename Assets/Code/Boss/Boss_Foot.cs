using UnityEngine;
using System.Collections;

public class Boss_Foot : MonoBehaviour {

	public Boss_1_Logic Boss = null;
	public int HP = 5000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(HP<0)
		{
			Die();
		}
	}
	//
	public void Idle()
	{
		GetComponent<Animator>().ResetTrigger("hurt");		
	}
	public void Recovery()
	{
		GetComponent<SpriteRenderer>().color = Color.white;
		
	}
	//
	public void Die()
	{
		GetComponent<BoxCollider2D>().enabled = false;
		GetComponent<Animator>().SetBool("die",true);
	}
	public void EndDie()
	{
		if(Boss != null)
		{
			Boss.BreakOneFoot();
		}
		Destroy(this.gameObject);
	}

	public void Hurt()
	{
		GetComponent<Animator>().SetTrigger("hurt");	

		if(MusicManager.GetInstance().SFXCtrl != null)
		{
			MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_BossHit);
		}

		GameManager.getInstance ().AddGameScore (10.0f);
	}
	//
	void OnTriggerEnter2D(Collider2D collision)
	{
		if(HP<0) return;

		if(collision.gameObject != null)
		{
			TriangleShip pPCtrl = collision.gameObject.GetComponent<TriangleShip>();
			if(pPCtrl != null)
			{
				pPCtrl.HurtByEnemy(1000);
			}
		}
		//
		if(collision.gameObject != null)
		{
			ShipMBullet Mbullet = collision.gameObject.GetComponent<ShipMBullet>();
			ShipSBullet Sbullet = collision.gameObject.GetComponent<ShipSBullet>();
			ShipLBullet Lbullet = collision.gameObject.GetComponent<ShipLBullet>();
			
			
			if(Mbullet != null )
			{
				
				HP -= Mbullet.GetPower();
				//
				Hurt();
				//
				Destroy(Mbullet.gameObject);
			}
			else if(Sbullet != null)
			{
				
				HP -= Sbullet.GetPower();
				//
				Hurt();
				//
				Destroy(Sbullet.gameObject);
			}
			else if(Lbullet != null)
			{
				
				HP -= Lbullet.GetPower();
				//
				Hurt();
				//
				Destroy(Lbullet.gameObject);
			}

		}
	}
}
