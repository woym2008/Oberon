using UnityEngine;
using System.Collections;

public class MonsterDieFrame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other != null && other.gameObject != null)
		{
			Monster monst = other.gameObject.GetComponent<Monster>();

			Bullet bullet = other.gameObject.GetComponent<Bullet>();
			
			if(monst != null)
			{
				Destroy(monst.gameObject);
			}
			else if(bullet != null)
			{
				Destroy(bullet.gameObject);
			}
		}
	}
}
