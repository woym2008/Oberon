using UnityEngine;
using System.Collections;

public class CameraStopDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject != null)
		{
			CameraEdge pCE = collision.gameObject.GetComponent<CameraEdge>();
			if(pCE != null)
			{
				PlayerManager.getInstance().GetGameCamera().LockDown();
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject != null)
		{
			CameraEdge pCE = collision.gameObject.GetComponent<CameraEdge>();
			if(pCE != null)
			{
				PlayerManager.getInstance().GetGameCamera().RecoveryDown();
			}
		}
	}
}
