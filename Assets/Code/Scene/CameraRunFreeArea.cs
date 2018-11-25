using UnityEngine;
using System.Collections;

public class CameraRunFreeArea : MonoBehaviour {

	float EnterHeight = 0.0f;

    public Transform UPPoint;
    public Transform DownPoint;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.gameObject != null)
		{
			TriangleShip pPCtrl = collision.gameObject.GetComponent<TriangleShip>();
			if(pPCtrl != null)
			{
				//EnterHeight = pPCtrl.gameObject.transform.position.y;

				PlayerManager.getInstance().CloseCameraFollow();
			}
		}
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.gameObject != null)
		{
			TriangleShip pPCtrl = collision.gameObject.GetComponent<TriangleShip>();
			if(pPCtrl != null)
			{
                //float curH = pPCtrl.gameObject.transform.position.y;

                //float delta = curH - EnterHeight;

                //PlayerManager.getInstance().GetPathMover().UpdateCameraY(delta);

                //EnterHeight = pPCtrl.gameObject.transform.position.y;

                PlayerManager.getInstance().SetCameraFollow(UPPoint.position, DownPoint.position);
            }
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject != null)
		{
			TriangleShip pPCtrl = collision.gameObject.GetComponent<TriangleShip>();
			if(pPCtrl != null)
			{
				//EnterHeight = 0.0f;

				PlayerManager.getInstance().SetCameraFollow(UPPoint.position, DownPoint.position);
				//PlayerManager.getInstance().GetPathMover().UpdateCameraY(0.0f);
			}
		}
	}
}
