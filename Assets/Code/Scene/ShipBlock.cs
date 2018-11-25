using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBlock : MonoBehaviour {
    public ShipFlyDirect m_Direct;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        TriangleShip ship = collision.gameObject.GetComponent<TriangleShip>();
        if(ship != null && collision.gameObject.transform.parent != null)
        {
            PlayerCtrl ctrl = collision.gameObject.transform.parent.GetComponent<PlayerCtrl>();
            ctrl.BlockFly(m_Direct);
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        TriangleShip ship = collision.gameObject.GetComponent<TriangleShip>();
        if (ship != null && collision.gameObject.transform.parent != null)
        {
            PlayerCtrl ctrl = collision.gameObject.transform.parent.GetComponent<PlayerCtrl>();
            ctrl.ResumeFLy(m_Direct);
        }
	}
}
