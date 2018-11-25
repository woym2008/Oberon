using UnityEngine;
using System.Collections;

public enum SceneBoundaryType
{
	SceneBoundary_Up = 0,
	SceneBoundary_Down,
}

public class SceneBoundary : MonoBehaviour {

	public bool ForcesDeath = false;
	
	public SceneBoundaryType EdgeType = SceneBoundaryType.SceneBoundary_Up;
	
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
				PlayerCtrl pCtrl = PlayerManager.getInstance().GetPlayerCtrl();
				if(EdgeType == SceneBoundaryType.SceneBoundary_Up)
				{
					pCtrl.LockUpCtrl();
				}
				else if(EdgeType == SceneBoundaryType.SceneBoundary_Down)
				{
					pCtrl.LockDownCtrl();
				}
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other != null && other.gameObject != null)
		{
			TriangleShip ship = other.gameObject.GetComponent<TriangleShip>();
			
			if(ship != null)
			{
				PlayerCtrl pCtrl = PlayerManager.getInstance().GetPlayerCtrl();
				if(EdgeType == SceneBoundaryType.SceneBoundary_Up)
				{
					pCtrl.UnLockUpCtrl();
				}
				else if(EdgeType == SceneBoundaryType.SceneBoundary_Down)
				{
					pCtrl.UnLockDownCtrl();
				}
			}
		}
	}
}
