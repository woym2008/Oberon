using UnityEngine;
using System.Collections;

public class PlayerDieEdge : MonoBehaviour {

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
			TriangleShip ship = other.gameObject.GetComponent<TriangleShip>();
			
			if(ship != null)
			{
				if(ship.GetShipState() == ShipState.ShipState_Alive)
				{
					PlayerManager.getInstance().PlayerDie();
				}
			}
		}
	}
}
