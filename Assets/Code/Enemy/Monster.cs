using UnityEngine;
using System.Collections;




public class Monster: MonoBehaviour
{
	public bool AlwaysUp = false;
	public float Speed = 0;

	bool m_bAlreadyDie = false;

	public float MonsterScore = 10.0f;

	public bool IsEntity = true;

	public bool ReverseMove = false;

	//add by WWH[2015-12-14 1:27]
	public void Born(float idleTime)
	{

	}
	public  void StartBorn()
	{
		
	}
	public  void EndBorn()
	{
		//仅仅是播放动画~
		Animator animator = GetComponent<Animator>();
		
		if(animator != null)
		{
			animator.SetBool("idle",true);
		}
	}
	public  void UpdateBorn()
	{

	}


	public float idleTimer = 0;
	public float idleTimeBound = 0;

	public void Idle(float idleTime)
	{

		//
		Animator animator = GetComponent<Animator>();
		
		if(animator != null)
		{
			animator.SetBool("attack",false);
		}
		//
		mFSM.SetState(Monster_Idle.GetInstance());

		idleTimeBound = idleTime;
	}
	public virtual void StartIdle()
	{

	}
	public virtual void EndIdle()
	{
		idleTimer = 0;
		idleTimeBound = 0;
	}
	public virtual void UpdateIdle()
	{
		idleTimer += Time.deltaTime;

		if(idleTimer >= idleTimeBound)
		{
			this.SendMessage("SetThinkInactive",this.gameObject);
		}
	}
	public Vector3	startPos;
	public Vector3 	targetPos;
	public float 	moveto_Timer = 0;
	public float    moveto_totalTime = 0;
	public float 	moveFallout = 1;
	bool m_bCurveMove = false;
	public virtual void MoveTo_Curve(Vector3 target,float speed)
	{
		if(mFSM.CurRule == Monster_Die.GetInstance()) return;
		
		mFSM.SetState(Monster_MoveTo.GetInstance());

		if(ReverseMove == true)
		{
			target.x = -target.x;
		}
		
		//targetPos = new Vector3(2.5f,1.5f,0);
		startPos = new Vector3(transform.position.x,transform.position.y,0);
		targetPos = new Vector3(target.x,target.y,0);
		float dis = Vector3.Magnitude(startPos - targetPos);
		moveto_totalTime =  dis / (speed + 0.1f);

		m_bCurveMove = true;
		
		if(AlwaysUp == false)transform.rotation = LookAtTarget(targetPos);
		//
		Animator animator = GetComponent<Animator>();
		
		if(animator != null)
		{
			animator.SetBool("attack",false);
		}
		//
	}

	public virtual void MoveTo(Vector3 target,float speed)
	{
		if(mFSM.CurRule == Monster_Die.GetInstance()) return;

		mFSM.SetState(Monster_MoveTo.GetInstance());

		if(ReverseMove == true)
		{
			target.x = -target.x;
		}

		//targetPos = new Vector3(2.5f,1.5f,0);
		startPos = new Vector3(transform.position.x,transform.position.y,0);
		targetPos = new Vector3(target.x,target.y,0);
		float dis = Vector3.Magnitude(startPos - targetPos);
		moveto_totalTime =  dis / (speed + 0.1f);

		if(AlwaysUp == false)transform.rotation = LookAtTarget(targetPos);
		//
		Animator animator = GetComponent<Animator>();
		
		if(animator != null)
		{
			animator.SetBool("attack",false);
		}
		//
	}
	public virtual void StartMoveTo()
	{

	}
	public virtual void EndMoveTo()
	{
		moveto_Timer = 0;
		moveto_totalTime = 0;
		startPos = Vector3.zero;
		targetPos = Vector3.zero;
		moveFallout = 1;

		m_bCurveMove = false;
	}
	public virtual void UpdateMoveTo()
	{
		if(m_bCurveMove == true)
		{
			float newPos_X = Mathf.Lerp(startPos.x,targetPos.x,Mathf.Min(moveto_Timer / moveto_totalTime,1));
			float newPos_Y = startPos.y + (Mathf.Sin(newPos_X*2.0f) * 0.1f * 4.0f);
			moveto_Timer += (Time.deltaTime * moveFallout);

			transform.position = new Vector3(newPos_X,newPos_Y,0.0f);

			if(moveto_Timer >= moveto_totalTime)
			{
				this.SendMessage("SetThinkInactive",this.gameObject);			
			}
		}
		else
		{
			Vector3 newPos = Vector3.Lerp(startPos,targetPos,Mathf.Min(moveto_Timer / moveto_totalTime,1));
			moveto_Timer += (Time.deltaTime * moveFallout);
			//
			transform.position = new Vector3(newPos.x,newPos.y,newPos.z);
			//
			if(moveto_Timer >= moveto_totalTime)
			{
				this.SendMessage("SetThinkInactive",this.gameObject);			
			}
		}
	}

	public Quaternion startRot;
	public Quaternion targetRot;
	public float rotateto_Timer = 0;
	public float rotateto_totalTime = 0;
	
	public virtual void LookAt(Vector3 target,float speed)
	{
		if(mFSM.CurRule == Monster_Die.GetInstance()) return;
		
		startRot = new Quaternion(transform.rotation.x,
		                          transform.rotation.y,
		                          transform.rotation.z,
		                          transform.rotation.w);
		targetRot = LookAtTarget(new Vector2(target.x,target.y));
		float a = Quaternion.Angle(startRot,targetRot);
		rotateto_totalTime = a/(speed + 0.1f);
		rotateto_Timer = 0;
		//
		Animator animator = GetComponent<Animator>();
		
		if(animator != null)
		{
			animator.SetBool("attack",false);
		}
		//

		mFSM.SetState(Monster_RotateTo.GetInstance());

	}
	public virtual void StartRotateTo()
	{		


	}
	public virtual void EndRotateTo()
	{
		rotateto_Timer = 0;
		rotateto_totalTime = 0;
		
	}
	public virtual void UpdateRotateTo()
	{
		Quaternion newRot = Quaternion.Lerp(startRot,targetRot,Mathf.Min(rotateto_Timer / rotateto_totalTime,1));
		rotateto_Timer += Time.deltaTime;
		//
		transform.rotation = newRot;
		//
		if(rotateto_Timer >= rotateto_totalTime)
		{
			this.SendMessage("SetThinkInactive",this.gameObject);
		}
	}
	//
	public float AttackCoolTimer = 0;
	public float AttackCoolTimeBound = 0;

	public virtual void Attack(float coolTime,bool useAnimationEvent = false)
	{
		//
		BulletEmitter emitter = GetComponent<BulletEmitter>();
		//
		if(emitter != null)
		{

			//
			if(!useAnimationEvent) emitter.Shoot();
			//
			mFSM.SetState(Monster_Attack.GetInstance());

			//
			AttackCoolTimeBound = coolTime;
			//
			Animator animator = GetComponent<Animator>();
			
			if(animator != null)
			{
				animator.SetBool("attack",true);
			}
		}



	}
	public virtual void StartAttack()
	{

	}
	public virtual void UpdateAttack()
	{
		AttackCoolTimer += Time.deltaTime;
		
		if(AttackCoolTimer >= AttackCoolTimeBound)
		{
			this.SendMessage("SetThinkInactive",this.gameObject);
		}
	}
	public virtual void EndAttack()
	{
		AttackCoolTimer = 0;
		AttackCoolTimeBound = 0;
	}
	//
	public MonsterFSM	mFSM;
	/////////////////////////////////////////////////////////////////////////////////////
	public float HP = 100.0f;
	public void Born()
	{

	}

	public void Hurt(float damage = 100.0f)
	{
		HP -= damage;

		this.SendMessage("MonsterHurt",SendMessageOptions.DontRequireReceiver);
	}

	public void Die()
	{
		m_bAlreadyDie = true;

		mFSM.SetState(Monster_Die.GetInstance());	

		//GameManager.getInstance().m_SECtrl.PlaySound(SoundType.Sound_KillMonster);

		Animator animator = GetComponent<Animator>();
		
		if(animator != null)
		{
			animator.SetBool("die",true);
		}
		//
		if(GetComponent<BoxCollider2D>() != null)
		{
			GetComponent<BoxCollider2D>().enabled = false;
		}

		GameManager.getInstance ().AddGameScore (MonsterScore);
	}

	public void StartDie()
	{
		if(MusicManager.GetInstance().SFXCtrl != null)
		{
			Debug.Log(this.gameObject.name);
			MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_KillMonster);

		}
		//安全删除需要的，3秒后删除
		Destroy(this.gameObject,3);	
	}

	public void UpdateDie()
	{

	}

	public void EndDie()
	{
	}

	public void DestoryMe()
	{
		Destroy(this.gameObject);	
	}
	
	/////////////////////////////////////////////////////////////////////////////////////
	void Awake() 
	{
		mFSM = GetComponent<MonsterFSM>();
	}
	
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {

		if(HP <= 0 && m_bAlreadyDie == false)
		{
			//SendMessage("SetThinkComplete",this.gameObject);
			//
			Die();
		}

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(IsEntity)
		{
			if(collision.gameObject != null)
			{
				TriangleShip pPCtrl = collision.gameObject.GetComponent<TriangleShip>();
				if(pPCtrl != null)
				{
					pPCtrl.HurtByEnemy();

					Hurt(10000.0f);
				}
			}
			else
			if(collision.GetComponent<Rigidbody>() != null && collision.GetComponent<Rigidbody>().gameObject != null)
			{
				TriangleShip pPCtrl = collision.GetComponent<Rigidbody>().gameObject.GetComponent<TriangleShip>();
				if(pPCtrl != null)
				{
					pPCtrl.HurtByEnemy();

					Hurt(10000.0f);
				}
			}
		}
	}
	/*
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject != null && 
		   collision.gameObject.tag == "Player")
		{
			TriangleShip pPCtrl = collision.gameObject.GetComponent<TriangleShip>();
			pPCtrl.HurtByEnemy();
		}

		if(collision.rigidbody != null && collision.rigidbody.gameObject != null &&
		   collision.rigidbody.gameObject.tag == "Player")
		{
			TriangleShip pPCtrl = collision.rigidbody.gameObject.GetComponent<TriangleShip>();
			pPCtrl.HurtByEnemy();
		}
	}
	*/
	public Quaternion LookAtTarget(Vector2 targetPos)
	{
		Vector2 x = targetPos - new Vector2(transform.position.x,transform.position.y);
		x.Normalize();

		return  Quaternion.FromToRotation(Vector3.up,x);
		
	}

	public void MoveToTargetPos(Vector2 targetPos,float Speed)
	{
		LookAtTarget(targetPos);

		Emit();
	}

	public void Emit()
	{
			
	}

	public void SetEntity(bool isEnt)
	{
		IsEntity = isEnt;
	}
}