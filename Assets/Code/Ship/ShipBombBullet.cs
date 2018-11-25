using UnityEngine;
using System.Collections;

public class ShipBombBullet : Bullet {

	// Use this for initialization
	void Start () {
		
	}
	
	void Update () {
		if(!CanFly) return ;
		Vector2 dir = new Vector2(1.0f,0.0f);
		dir.Normalize ();
		transform.localPosition = transform.localPosition + (new Vector3(dir.x,0,0) * Speed * Time.deltaTime);

		if(transform.localPosition.x >= 0.0f)
		{
			BombManager.GetInstance().CreateBomb(BombType.BombType_PlayerBomb,new Vector2(this.transform.position.x,this.transform.position.y));
			if(MusicManager.GetInstance().SFXCtrl != null)
			{
				MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_BigBomb);
			}
			Destroy(this.gameObject);
		}
		//
	}
}
