using UnityEngine;
using System.Collections;

public class DynamicBlock : MonoBehaviour {

	public bool ForcesDeath = false;

	public GameObject StartPosObj = null;
	public GameObject EndPosObj = null;

	Vector3 StartPos;
	Vector3 EndPos;

	public bool Loop = false;

	public float Speed = 0.5f;

	bool StopWork = false;

	Vector3 curDir = new Vector3(1.0f,-1.0f,0.0f);

	bool curState = true; //true forward; false back

	// Use this for initialization
	void Start () 
	{
		StartPosObj = this.transform.Find ("StartObj").gameObject;
		EndPosObj = this.transform.Find ("EndObj").gameObject;

		StartPos = StartPosObj.transform.position;
		EndPos = EndPosObj.transform.position;

		curState = true;

		curDir = EndPosObj.transform.position - StartPos;

		curDir.Normalize ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(StopWork)
		{
			return;
		}

		if(curState)
		{
			this.transform.position += curDir*Speed;

			if(Loop)
			{
				float dis = Vector3.Distance(this.transform.position,EndPos);
				if(dis< 0.3f)
				{
					curState = !curState;
				}
			}
			else
			{
				float dis = Vector3.Distance(this.transform.position,EndPos);
				if(dis< 0.3f)
				{
					StopWork = true;
				}
			}
		}
		else
		{
			this.transform.position -= curDir*Speed;

			float dis = Vector3.Distance(this.transform.position,StartPos);
			if(dis< 0.3f)
			{
				curState = !curState;
			}
		}
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
		}
	}
}
