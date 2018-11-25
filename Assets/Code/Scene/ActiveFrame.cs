using UnityEngine;
using System.Collections;

public class ActiveFrame : MonoBehaviour {

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
			MonstNest nest = other.gameObject.GetComponent<MonstNest>();

			if(nest != null && nest.IsActiveByEnableArea() == false)
			{
				nest.m_EnableSpawnMonster = true;
			}
			//
			BossNest bossnest = other.gameObject.GetComponent<BossNest>();
			
			if(bossnest != null)
			{
				bossnest.ActiveNest();
			}


		}
	}
}
