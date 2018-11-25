using UnityEngine;
using System.Collections;

public class ChangeMoverSpeedArea : MonoBehaviour {

	public float ChangeSpeed = 1.0f;

	public bool RecoverySpeed = false;

	bool isEnable = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(isEnable == false)
		{
			return;
		}

		if(other != null && other.gameObject != null)
		{
			TriangleShip ship = other.gameObject.GetComponent<TriangleShip>();
			
			if(ship != null)
			{
				ScenePathMover mover = PlayerManager.getInstance().GetPathMover();
				if(mover!= null)
				{
					if(RecoverySpeed == false)
					{
						mover.SetCurMoverSpeed(ChangeSpeed);
					}
					else
					{
						mover.RecoveryMoverSpeed();
					}

					isEnable = false;
				}
			}
		}
	}
}
