using UnityEngine;
using System.Collections;

public class Drive : MonoBehaviour {



	protected Vector3	startPos;
	protected Vector3 	targetPos;
	protected float 	moveto_Timer = 0;
	protected float    moveto_totalTime = 0;

	//
	protected Quaternion startRot;
	protected Quaternion targetRot;
	protected float rotateto_Timer = 0;
	protected float rotateto_totalTime = 0;
	//
	public float 	moveFallout = 1;
	public bool 	alwaysUp = false;

	public float 	moveSpeed = 1;
	public float 	rotateSpeed = 1;

	public bool 	Moving = false;
	public bool 	Rotating = false;

	public bool 	BreakDrive = true;


	public void MoveTo(Vector3 target)
	{
		ResetMoveTo();

		Moving = true;

		startPos = new Vector3(transform.position.x,transform.position.y,0);
		targetPos = new Vector3(target.x,target.y,0);
		float dis = Vector3.Magnitude(startPos - targetPos);
		float speed = moveSpeed;
		moveto_totalTime =  dis / (speed + 0.1f);
		
		if(alwaysUp == false)	transform.rotation = LookAtTarget(targetPos);
	
	}
	public void StopMove()
	{
		Moving = false;
	}
	public void ResetMoveTo()
	{
		moveto_Timer = 0;
		moveto_totalTime = 0;
		startPos = Vector3.zero;
		targetPos = Vector3.zero;
		moveFallout = 1;
		
	}
	public void UpdateMoveTo()
	{
		if(Moving == false) return;
		//
		Vector3 newPos = Vector3.Lerp(startPos,targetPos,Mathf.Min(moveto_Timer / moveto_totalTime,1));
		moveto_Timer += (Time.deltaTime * moveFallout);
		//
		transform.position = new Vector3(newPos.x,newPos.y,newPos.z);
		//
		if(moveto_Timer >= moveto_totalTime)
		{
			Moving = false;	

			this.SendMessage("SetThinkInactive",this.gameObject);
		}
	}
	

	
	public void LookAt(Vector3 target)
	{
		ResetRotateTo();
		
		Rotating = true;

		startRot = new Quaternion(transform.rotation.x,
		                          transform.rotation.y,
		                          transform.rotation.z,
		                          transform.rotation.w);
		targetRot = LookAtTarget(new Vector2(target.x,target.y));
		float a = Quaternion.Angle(startRot,targetRot);
		float speed = rotateSpeed;
		rotateto_totalTime = a/(speed + 0.1f);
		rotateto_Timer = 0;

		
	}

	public void StopLookAt()
	{
		Rotating = false;
	}

	public void ResetRotateTo()
	{
		rotateto_Timer = 0;
		rotateto_totalTime = 0;
		
	}
	public void UpdateRotateTo()
	{
		if(Rotating == false) return;
		//
		Quaternion newRot = Quaternion.Lerp(startRot,targetRot,Mathf.Min(rotateto_Timer / rotateto_totalTime,1));
		rotateto_Timer += Time.deltaTime;
		//
		transform.rotation = newRot;
		//
		if(rotateto_Timer >= rotateto_totalTime)
		{
			Rotating = false;

			this.SendMessage("SetThinkInactive",this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(BreakDrive)
		{
			UpdateMoveTo();
			
			UpdateRotateTo();
		}


	}

	public Quaternion LookAtTarget(Vector2 targetPos)
	{
		Vector2 x = targetPos - new Vector2(transform.position.x,transform.position.y);
		x.Normalize();
		
		return  Quaternion.FromToRotation(Vector3.up,x);
		
	}
}
