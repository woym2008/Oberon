using UnityEngine;
using System.Collections;

public class SceneEdge : MonoBehaviour {

	public bool ForcesDeath = false;

	public bool StopBullet = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other != null && other.gameObject != null)
		{
			TriangleShip ship = other.gameObject.GetComponent<TriangleShip>();
			
			if(ship != null)
			{
				PlayerManager.getInstance().PlayerDie(ForcesDeath);
			}
			//
			if(StopBullet)
			{
				EnemyBullet bullet = other.gameObject.GetComponent<EnemyBullet>();

				if(bullet != null)
				{
					Destroy(bullet.gameObject);
				}
			}
		}
	}
}
