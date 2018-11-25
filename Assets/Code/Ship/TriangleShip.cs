using UnityEngine;
using System.Collections;

public enum ShipFrontType
{
	FrontType_Gun1 = 0,
	FrontType_Shild1,
	FrontType_Gun2,
	FrontType_Shild2,
	FrontType_Gun3,
	FrontType_Shild3,
	NumFrontType,
}

public enum ShipState
{
	ShipState_Ready = 0,
	ShipState_Begin,
	ShipState_Alive,
	ShipState_Die,
	ShipState_Victory,
}

public class TriangleShip : MonoBehaviour {

	public ShipFrontType m_CurFrontType = ShipFrontType.FrontType_Gun1;

	GameObject m_ShipTexObj = null;
	SpriteRenderer m_ShipTexObj_0 = null;
	SpriteRenderer m_ShipTexObj_60 = null;
	SpriteRenderer m_ShipTexObj_120 = null;
	SpriteRenderer m_ShipTexObj_180 = null;
	SpriteRenderer m_ShipTexObj_240 = null;
	SpriteRenderer m_ShipTexObj_300 = null;

	GameObject m_Guns = null;

	public ShipGunSystem m_Gun1 = null;
	public ShipGunSystem m_Gun2 = null;
	public ShipGunSystem m_Gun3 = null;

	public BombGun m_BombGun = null;
	public SuperLazerGun m_SLGun = null;

	public ShipShieldSystem m_Shild1 = null;
	public ShipShieldSystem m_Shild2 = null;
	public ShipShieldSystem m_Shild3 = null;

	Sprite ShipTex_Degree_0 = null;
	Sprite ShipTex_Degree_60 = null;
	Sprite ShipTex_Degree_120 = null;
	Sprite ShipTex_Degree_180 = null;
	Sprite ShipTex_Degree_240 = null;
	Sprite ShipTex_Degree_300 = null;

	public GameObject StartPosObject = null;

	public GameObject CompletePosObject = null;

	public GameObject BornPosObject = null;

	Animator m_CoreAnim = null;

	Animator m_ShipRing = null;

	ShipState m_ShipState = ShipState.ShipState_Begin;
	//----------------------------------------

	float m_ShipUpSpeed = 3.0f;

	float m_ShipMaxHP = 10.0f;
	float m_ShipHp = 10.0f;

	float m_DieTime = 2.0f;
	//----------------------------------------
	bool m_bRotingShip = false;

	Vector3 m_TargetRotEulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);

	float m_RotSpeed = 720.0f;
	
	float m_ShipSpriteAlphaSpeed = 12.0f;
	//----------------------------------------
	float m_BackStepLength = 0.2f;

	float m_RecoveryStepLength = 0.005f;
	//----------------------------------------
	float MaxShipEnergy = 90.0f;
	float OneSecShipEnergy = 30.0f;

	float m_CurShipEnergy = 0.0f;
	public float GetCurShipEnergy(){
		return m_CurShipEnergy;
	}

	bool m_bReadyBomb = false;
	public bool IsReadyUseBomb(){return m_bReadyBomb;}

	bool m_bUsingBomb = false;
	//---------------
	bool m_bEnableRotaryGun = false;

	float m_RotaryGunDegree = 0.0f;

	float MaxRotaryGunDegree = 1800.0f;

	float m_RotaryGunSpeed = 720.0f;
	//----------------------------------------
	EnemyBulletType m_CurNeedsEnergyType = EnemyBulletType.EnemyBullet_Blue;
	//----------------------------------------
	const int MaxShipLifeTimes = 3;
	int m_ShipLifeTimes = MaxShipLifeTimes;
	public int GetCurShipLifeTimes()
	{
		return m_ShipLifeTimes;
	}
	//----------------------------------------
	Vector2 m_ShipDefaultPos = new Vector2(0.0f,0.0f);

	bool m_IsUp = false;

	/// <summary>
	/// WWH Add
	/// </summary>
	public float GodTime = 2.5f;
	public bool  IsGod = false;

	void Awake()
	{
		m_CoreAnim = this.transform.Find ("energy-Core").gameObject.GetComponent<Animator>();
		
		m_ShipRing = this.transform.Find ("ShipRing").gameObject.GetComponent<Animator>();

		m_ShipTexObj = this.transform.Find ("TriangleShipTex").gameObject;
		m_ShipTexObj_0 = m_ShipTexObj.transform.Find ("TriangleShipTex_0").gameObject.GetComponent<SpriteRenderer>();
		m_ShipTexObj_60 = m_ShipTexObj.transform.Find ("TriangleShipTex_60").gameObject.GetComponent<SpriteRenderer>();
		m_ShipTexObj_120 = m_ShipTexObj.transform.Find ("TriangleShipTex_120").gameObject.GetComponent<SpriteRenderer>();
		m_ShipTexObj_180 = m_ShipTexObj.transform.Find ("TriangleShipTex_180").gameObject.GetComponent<SpriteRenderer>();
		m_ShipTexObj_240 = m_ShipTexObj.transform.Find ("TriangleShipTex_240").gameObject.GetComponent<SpriteRenderer>();
		m_ShipTexObj_300 = m_ShipTexObj.transform.Find ("TriangleShipTex_300").gameObject.GetComponent<SpriteRenderer>();

		m_Guns = this.transform.Find ("Guns").gameObject;
	}

	void Start () 
	{

		m_ShipRing.gameObject.SetActive (false);
		//------
		//ShipTex_Degree_0 = Resources.Load<Sprite> ("Texture/green-back");
		//ShipTex_Degree_60 = Resources.Load<Sprite> ("Texture/blue-front");
		//ShipTex_Degree_120 = Resources.Load<Sprite> ("Texture/yellow-back");
		//ShipTex_Degree_180 = Resources.Load<Sprite> ("Texture/green-front");
		//ShipTex_Degree_240 = Resources.Load<Sprite> ("Texture/blue-back");
		//ShipTex_Degree_300 = Resources.Load<Sprite> ("Texture/yellow-front");

		SetDefaultRot ();

		if(m_Gun1 != null)
		{
			m_Gun1.SetMainBody(this);
		}

		if(m_Gun2 != null)
		{
			m_Gun2.SetMainBody(this);
		}

		if(m_Gun3 != null)
		{
			m_Gun3.SetMainBody(this);
		}

		if(m_BombGun != null)
		{
			m_BombGun.SetMainBody(this);
		}

		if(m_SLGun != null)
		{
			m_SLGun.SetMainBody(this);
		}

		if(m_Shild1 != null)
		{
			m_Shild1.SetMainBody(this);
		}

		if(m_Shild2 != null)
		{
			m_Shild2.SetMainBody(this);
		}

		if(m_Shild3 != null)
		{
			m_Shild3.SetMainBody(this);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch(m_ShipState)
		{
		case ShipState.ShipState_Begin:
		{
			ShipStaate_BeginUpdate();
		}
			break;
		case ShipState.ShipState_Ready:
		{
			ShipStaate_ReadyUpdate();
		}
			break;
		case ShipState.ShipState_Alive:
		{
			ShipStaate_AliveUpdate();
		}
			break;
		case ShipState.ShipState_Die:
		{
			ShipStaate_DieUpdate();
		}
			break;
		case ShipState.ShipState_Victory:
		{
			ShipStaate_VictoryUpdate();
		}
			break;
		}
	}

	public ShipState GetShipState()
	{
		return m_ShipState;
	}

	void SetShipState(ShipState st)
	{
		switch(m_ShipState)
		{
		case ShipState.ShipState_Begin:
		{
			ShipStaate_BeginEnd();
		}
			break;
		case ShipState.ShipState_Ready:
		{
			ShipStaate_ReadyEnd();
		}
			break;
		case ShipState.ShipState_Alive:
		{
			ShipStaate_AliveEnd();
		}
			break;
		case ShipState.ShipState_Die:
		{
			ShipStaate_DieEnd();
		}
			break;
		case ShipState.ShipState_Victory:
		{
			ShipStaate_VictoryEnd();
		}
			break;
		}

		m_ShipState = st;

		switch(m_ShipState)
		{
		case ShipState.ShipState_Begin:
		{
			ShipStaate_BeginStart();
		}
			break;
		case ShipState.ShipState_Ready:
		{
			ShipStaate_ReadyStart();
		}
			break;
		case ShipState.ShipState_Alive:
		{
			ShipStaate_AliveStart();
		}
			break;
		case ShipState.ShipState_Die:
		{
			ShipStaate_DieStart();
		}
			break;
		case ShipState.ShipState_Victory:
		{
			ShipStaate_VictoryStart();
		}
			break;
		}
	}

	public void FlyUp()
	{
		float upspeed = m_ShipUpSpeed * Time.deltaTime;

		this.transform.localPosition = new Vector2 (this.transform.localPosition.x,this.transform.localPosition.y + upspeed);
	}

	public void FlyUpStart()
	{
		m_IsUp = true;
	}

	public void FlyUpOver()
	{
		m_IsUp = false;
	}

	public void FlyDown()
	{
		float upspeed = m_ShipUpSpeed * Time.deltaTime;
		
		this.transform.localPosition = new Vector2 (this.transform.localPosition.x,this.transform.localPosition.y - upspeed);
	}

	void SetDefaultRot()
	{
		m_CurFrontType = ShipFrontType.FrontType_Gun1;

		//
		m_Guns.transform.eulerAngles = new Vector3(0.0f,0.0f,0.0f);
		//

		m_ShipTexObj.transform.eulerAngles = new Vector3(0.0f,0.0f,0.0f);

		CloseAllShipTex ();

		//m_ShipTexObj_0.gameObject.SetActive (true);
		m_ShipTexObj_0.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
		//SpriteRenderer sp = m_ShipTexObj.GetComponent<SpriteRenderer>();
		//sp.sprite = ShipTex_Degree_0;
	}

	void closeAllShipCoreAnim()
	{
		m_CoreAnim.SetBool("blue",false);
		m_CoreAnim.SetBool("green",false);
		m_CoreAnim.SetBool("yellow",false);
		//m_CoreAnim.SetBool("boost",false);
		//m_CoreAnim.SetBool("Jump",false);
	}

	public void RotTrangle()
	{
		m_bRotingShip = true;

		CloseAllWeapon ();
		CloseAllShield ();

		switch(m_CurFrontType)
		{
		case ShipFrontType.FrontType_Gun1:
		{
			m_TargetRotEulerAngles = new Vector3(0.0f,0.0f,300.0f);

			closeAllShipCoreAnim();
			m_CoreAnim.SetBool("blue",true);
		}
			break;
		case ShipFrontType.FrontType_Shild1:
		{
			m_TargetRotEulerAngles = new Vector3(0.0f,0.0f,240.0f);
		}
			break;
		case ShipFrontType.FrontType_Gun2:
		{
			m_TargetRotEulerAngles = new Vector3(0.0f,0.0f,180.0f);

			closeAllShipCoreAnim();
			m_CoreAnim.SetBool("green",true);
		}
			break;
		case ShipFrontType.FrontType_Shild2:
		{
			m_TargetRotEulerAngles = new Vector3(0.0f,0.0f,120.0f);
		}
			break;
		case ShipFrontType.FrontType_Gun3:
		{
			m_TargetRotEulerAngles = new Vector3(0.0f,0.0f,60.0f);

			closeAllShipCoreAnim();
			m_CoreAnim.SetBool("yellow",true);
		}
			break;
		case ShipFrontType.FrontType_Shild3:
		{
			m_TargetRotEulerAngles = new Vector3(0.0f,0.0f,0.0f);
		}
			break;
		}
	}

	public void RotTrangleOver()
	{
		m_bRotingShip = false;

		int curfronttypeindex = (int)m_CurFrontType;
		m_CurFrontType = (ShipFrontType)(curfronttypeindex + 1);

		if(m_CurFrontType == ShipFrontType.NumFrontType)
		{
			m_CurFrontType = ShipFrontType.FrontType_Gun1;
		}

		CloseAllShipTex ();

		EnableAllShield ();

		switch(m_CurFrontType)
		{
		case ShipFrontType.FrontType_Gun1:
		{
			m_Gun1.EnableGunSystem();

			//
			m_Guns.transform.eulerAngles = new Vector3(0.0f,0.0f,0.0f);
				//
			m_ShipTexObj_0.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			//SpriteRenderer sp = m_ShipTexObj.GetComponent<SpriteRenderer>();
			//sp.sprite = ShipTex_Degree_0;
		}
			break;
		case ShipFrontType.FrontType_Shild1:
		{
			//m_Gun1.EnableGunSystem();
			//m_Gun2.EnableGunSystem();
			//m_Shild1.EnableShieldSystem();
			//
			m_Guns.transform.eulerAngles = new Vector3(0.0f,0.0f,-60.0f);
			//
			m_ShipTexObj_60.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			//SpriteRenderer sp = m_ShipTexObj.GetComponent<SpriteRenderer>();
			//sp.sprite = ShipTex_Degree_60;
		}
			break;
		case ShipFrontType.FrontType_Gun2:
		{
			m_Gun2.EnableGunSystem();
			//
			m_Guns.transform.eulerAngles = new Vector3(0.0f,0.0f,-120.0f);
			//
			m_ShipTexObj_120.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			//SpriteRenderer sp = m_ShipTexObj.GetComponent<SpriteRenderer>();
			//sp.sprite = ShipTex_Degree_120;
		}
			break;
		case ShipFrontType.FrontType_Shild2:
		{
			//m_Gun3.EnableGunSystem();
			//m_Gun2.EnableGunSystem();
			//m_Shild2.EnableShieldSystem();
			//
			m_Guns.transform.eulerAngles = new Vector3(0.0f,0.0f,-180.0f);
			//
			m_ShipTexObj_180.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			//SpriteRenderer sp = m_ShipTexObj.GetComponent<SpriteRenderer>();
			//sp.sprite = ShipTex_Degree_180;
		}
			break;
		case ShipFrontType.FrontType_Gun3:
		{
			m_Gun3.EnableGunSystem();
			//
			m_Guns.transform.eulerAngles = new Vector3(0.0f,0.0f,-240.0f);
			//
			m_ShipTexObj_240.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			//SpriteRenderer sp = m_ShipTexObj.GetComponent<SpriteRenderer>();
			//sp.sprite = ShipTex_Degree_240;
		}
			break;
		case ShipFrontType.FrontType_Shild3:
		{
			//m_Gun1.EnableGunSystem();
			//m_Gun3.EnableGunSystem();
			//m_Shild3.EnableShieldSystem();
			//
			m_Guns.transform.eulerAngles = new Vector3(0.0f,0.0f,-300.0f);
			//
			m_ShipTexObj_300.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			//SpriteRenderer sp = m_ShipTexObj.GetComponent<SpriteRenderer>();
			//sp.sprite = ShipTex_Degree_300;
		}
			break;
		}

		//rot 60 dgree

	}

	void CloseAllShipTex()
	{
		m_ShipTexObj_0.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,0.0f);
		m_ShipTexObj_60.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,0.0f);
		m_ShipTexObj_120.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,0.0f);
		m_ShipTexObj_180.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,0.0f);
		m_ShipTexObj_240.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,0.0f);
		m_ShipTexObj_300.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,0.0f);

		//m_ShipTexObj_0.gameObject.SetActive (false);
		//m_ShipTexObj_60.gameObject.SetActive (false);
		//m_ShipTexObj_120.gameObject.SetActive (false);
		//m_ShipTexObj_180.gameObject.SetActive (false);
		//m_ShipTexObj_240.gameObject.SetActive (false);
		//m_ShipTexObj_300.gameObject.SetActive (false);

	}

	void EnableAllWeapon()
	{
		m_Gun1.EnableGunSystem();
		m_Gun2.EnableGunSystem();
		m_Gun3.EnableGunSystem();
	}

	void EnableAllShield()
	{
		m_Shild1.EnableShieldSystem();
		m_Shild2.EnableShieldSystem();
		m_Shild3.EnableShieldSystem();
	}

	void CloseAllWeapon()
	{
		m_Gun1.DisableGunSystem();
		m_Gun2.DisableGunSystem();
		m_Gun3.DisableGunSystem();
	}

	void CloseAllShield()
	{
		m_Shild1.DisableShieldSystem();
		m_Shild2.DisableShieldSystem();
		m_Shild3.DisableShieldSystem();
	}

	//Use This When Not Hit By Enemy Bullet
	void BakcToDefaultPos()
	{
		//
	}

	public void FireBullet()
	{

	}

	public void HurtByEnemy(float hurthp = 100.0f)
	{
		if(m_ShipState != ShipState.ShipState_Alive)
		{
			return;
		}

		if(m_bUsingBomb)
		{
			return;
		}

		if(IsGod)
		{
			return;
		}

		m_ShipHp -= hurthp;
	}

	public void ShipSail()
	{
		SetShipState (ShipState.ShipState_Ready);
	}

	public void ShipFighting()
	{
		SetShipState (ShipState.ShipState_Alive);

		//m_Gun1.EnableGunSystem();
		RecoveryCurWeapon ();

		EnableAllShield ();
	}

	public void ShipDie()
	{
		SetShipState (ShipState.ShipState_Die);
	}

	public void ShipVictory()
	{
		SetShipState (ShipState.ShipState_Victory);
	}
	//-----------------------------------------------------
	void ShipStaate_BeginEnd()
	{
	}

	void ShipStaate_ReadyEnd()
	{}

	void ShipStaate_AliveEnd()
	{}

	void ShipStaate_DieEnd()
	{}
	//---------
	void ShipStaate_VictoryStart()
	{
		CloseAllWeapon ();
		CloseAllShield ();
        
		MusicManager.GetInstance ().BGMCtrl.StopBGM ();
		MusicManager.GetInstance ().BGMCtrl.PlayBGM ("StatgeEnd");

		ShipOutToStage ();
	}
	void ShipStaate_VictoryUpdate()
	{}
	void ShipStaate_VictoryEnd()
	{}
	//---------
	void ShipStaate_BeginStart()
	{

	}

	void ShipStaate_ReadyStart()
	{
		m_ShipHp = m_ShipMaxHP;

		if(PlayerManager.getInstance ().GetShipGroup () != null)
		{
			this.gameObject.transform.parent = PlayerManager.getInstance ().GetShipGroup ().transform;
		}

		ShipComeToStage ();
	}

	void ShipStaate_AliveStart()
	{

	}

	void ShipStaate_DieStart()
	{
		Animator amin = m_ShipTexObj.GetComponent<Animator> ();

		if(amin != null)
		{
			amin.SetBool("Die",true);
		}

		InvisibleShip ();

		EffectManager.GetInstance().CreateEffect(EffectType.Effect_ShipDie,this.gameObject.transform.position,this.transform.parent);

		CloseAllWeapon ();
		CloseAllShield ();

		MusicManager.GetInstance ().SFXCtrl.PlaySound (SoundType.Sound_HeroDie);

		this.gameObject.transform.parent = null;

		PlayerManager.getInstance ().GetPathMover ().StopMover ();

		//PlayerManager.getInstance ().GetPathMover ().ContinueMover ();
	}
	//---------
	void ShipStaate_BeginUpdate()
	{
		SetShipState (ShipState.ShipState_Ready);
	}

	void ShipStaate_ReadyUpdate()
	{}
	
	void ShipStaate_AliveUpdate()
	{
		if(m_IsUp)
		{
		//	FlyUp();
		}
		else
		{
		//	FlyDown();
		}
		
		if(m_ShipHp <= 0.0f)
		{
			ShipDie();
		}

		if(m_bRotingShip == true)
		{
			float deltaAngle = m_RotSpeed * Time.deltaTime;
			float rotangle = m_ShipTexObj.transform.eulerAngles.z - deltaAngle;

			switch(m_CurFrontType)
			{
			case ShipFrontType.FrontType_Gun1:
			{
				if(m_ShipTexObj_0 != null)
				{
					float newAlpha = m_ShipTexObj_0.color.a - m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_0.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,newAlpha);
				}
				
				if(m_ShipTexObj_60 != null)
				{
					float newAlpha = m_ShipTexObj_60.color.a + m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_60.color = new Color(m_ShipTexObj_60.color.r,m_ShipTexObj_60.color.g,m_ShipTexObj_60.color.b,newAlpha);
				}
			}
				break;
			case ShipFrontType.FrontType_Shild1:
			{
				if(m_ShipTexObj_60 != null)
				{
					float newAlpha = m_ShipTexObj_60.color.a - m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_60.color = new Color(m_ShipTexObj_60.color.r,m_ShipTexObj_60.color.g,m_ShipTexObj_60.color.b,newAlpha);
				}
				
				if(m_ShipTexObj_120 != null)
				{
					float newAlpha = m_ShipTexObj_120.color.a + m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_120.color = new Color(m_ShipTexObj_120.color.r,m_ShipTexObj_120.color.g,m_ShipTexObj_120.color.b,newAlpha);
				}
			}
				break;
			case ShipFrontType.FrontType_Gun2:
			{
				if(m_ShipTexObj_120 != null)
				{
					float newAlpha = m_ShipTexObj_120.color.a - m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_120.color = new Color(m_ShipTexObj_120.color.r,m_ShipTexObj_120.color.g,m_ShipTexObj_120.color.b,newAlpha);
				}
				
				if(m_ShipTexObj_180 != null)
				{
					float newAlpha = m_ShipTexObj_180.color.a + m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_180.color = new Color(m_ShipTexObj_180.color.r,m_ShipTexObj_180.color.g,m_ShipTexObj_180.color.b,newAlpha);
				}
			}
				break;
			case ShipFrontType.FrontType_Shild2:
			{
				if(m_ShipTexObj_180 != null)
				{
					float newAlpha = m_ShipTexObj_180.color.a - m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_180.color = new Color(m_ShipTexObj_180.color.r,m_ShipTexObj_180.color.g,m_ShipTexObj_180.color.b,newAlpha);
				}
				
				if(m_ShipTexObj_240 != null)
				{
					float newAlpha = m_ShipTexObj_240.color.a + m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_240.color = new Color(m_ShipTexObj_240.color.r,m_ShipTexObj_240.color.g,m_ShipTexObj_240.color.b,newAlpha);
				}
			}
				break;
			case ShipFrontType.FrontType_Gun3:
			{
				if(m_ShipTexObj_240 != null)
				{
					float newAlpha = m_ShipTexObj_240.color.a - m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_240.color = new Color(m_ShipTexObj_240.color.r,m_ShipTexObj_240.color.g,m_ShipTexObj_240.color.b,newAlpha);
				}
				
				if(m_ShipTexObj_300 != null)
				{
					float newAlpha = m_ShipTexObj_300.color.a + m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_300.color = new Color(m_ShipTexObj_300.color.r,m_ShipTexObj_300.color.g,m_ShipTexObj_300.color.b,newAlpha);
				}
			}
				break;
			case ShipFrontType.FrontType_Shild3:
			{
				if(m_ShipTexObj_300 != null)
				{
					float newAlpha = m_ShipTexObj_300.color.a - m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_300.color = new Color(m_ShipTexObj_300.color.r,m_ShipTexObj_300.color.g,m_ShipTexObj_300.color.b,newAlpha);
				}
				
				if(m_ShipTexObj_0 != null)
				{
					float newAlpha = m_ShipTexObj_0.color.a + m_ShipSpriteAlphaSpeed*Time.deltaTime;
					m_ShipTexObj_0.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,newAlpha);
				}
			}
				break;
			}


			if(m_TargetRotEulerAngles.z == 300.0f && m_ShipTexObj.transform.eulerAngles.z == 0.0f)
			{
				m_ShipTexObj.transform.eulerAngles = new Vector3(m_ShipTexObj.transform.eulerAngles.x, m_ShipTexObj.transform.eulerAngles.y,rotangle);
			}
			else if(rotangle <= m_TargetRotEulerAngles.z)
			{
				if(m_TargetRotEulerAngles.z == 360.0f)
				{
					m_ShipTexObj.transform.eulerAngles = new Vector3(m_ShipTexObj.transform.eulerAngles.x, m_ShipTexObj.transform.eulerAngles.y,0.0f);
				}
				else
				{
					m_ShipTexObj.transform.eulerAngles = m_TargetRotEulerAngles;
				}

				RotTrangleOver();
			}
			else
			{
				m_ShipTexObj.transform.eulerAngles = new Vector3(m_ShipTexObj.transform.eulerAngles.x, m_ShipTexObj.transform.eulerAngles.y,rotangle);
			}
		}

		if(m_bUsingBomb)
		{
			if(m_bEnableRotaryGun)
			{
				float deltadegree = m_RotaryGunSpeed * Time.deltaTime;
				m_RotaryGunDegree += deltadegree;

				this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x,
				                                         this.transform.eulerAngles.y,
				                                         this.transform.eulerAngles.z + deltadegree);

				if(m_RotaryGunDegree >= MaxRotaryGunDegree )
				{
					CloseRotaryGun();
				}
			}
		}

		//check recovery back
		if(this.transform.localPosition != StartPosObject.transform.localPosition)
		{
			RecoveryShipPos ();
		}
	}
	
	void ShipStaate_DieUpdate()
	{
		m_DieTime -= Time.deltaTime;

		if(m_DieTime <= 0.0f)
		{
			m_ShipLifeTimes--;

			PlayerManager.getInstance().GetLifeBar().LoseLife();

			if(m_ShipLifeTimes>0)
			{
				SetShipState (ShipState.ShipState_Ready);
			}
			else
			{
				GameManager.getInstance().GameOver();
				
				this.gameObject.SetActive(false);
				//Destroy(this.gameObject);
			}
		}
	}

	//-----------------------------------------------------------------
	public void RandomNeedsEnergyType()
	{
		int randType = Random.Range (0, 3);
		m_CurNeedsEnergyType = (EnemyBulletType)randType;

		if(PlayerManager.getInstance().GetEnergyBar() != null)
		{
			PlayerManager.getInstance().GetEnergyBar().ChangeBulletType(m_CurNeedsEnergyType);
		}
	}

	public void ClearShipEnergy()
	{
		if(PlayerManager.getInstance().GetEnergyBar() != null)
		{
			PlayerManager.getInstance().GetEnergyBar().ClearEnergy();
		}
	}

	public EnemyBulletType GetCurNeedsEnergyType()
	{
		return m_CurNeedsEnergyType;
	}

	public void ShiledBulletAbsorb(EnemyBulletType type , float energyValue = 10.0f)
	{
		energyValue = energyValue * 0.5f;
		//1 block
		//if(m_bReadyBomb == false)
		if(m_CurShipEnergy < MaxShipEnergy)
		{
			AbsorbEnergy(energyValue);
		}

		//2 Back step
		//ShipBackStep (type);
	}

	public void ShiledBulletReduce(EnemyBulletType type , float energyValue = 10.0f)
	{
		//1 block
		//if(m_bReadyBomb == true)
		//{
		//	m_bReadyBomb = false;
		//
		//	m_CoreAnim.SetBool("boost",false);
		//
		//	if(PlayerManager.getInstance().GetEnergyBar() != null)
		//	{
		//		PlayerManager.getInstance().GetEnergyBar().EnergyNotFull();
		//	}
		//}

		ReduceEnergy(energyValue);

		if(m_bReadyBomb == true && m_CurShipEnergy < OneSecShipEnergy)
		{
			m_bReadyBomb = false;

			m_CoreAnim.SetBool("boost",false);

			if(PlayerManager.getInstance().GetEnergyBar() != null)
			{
				PlayerManager.getInstance().GetEnergyBar().EnergyNotFull();
			}
		}
		
		//2 Back step
		ShipBackStep (type);
	}

	void AbsorbEnergy(float energyValue = 5.0f)
	{
		if(m_bUsingBomb == true)
		{
			return;
		}

		if(m_CurShipEnergy >= MaxShipEnergy)
		{
			m_CurShipEnergy = MaxShipEnergy;
		}
		else
		{
			m_CurShipEnergy += energyValue;
			if(m_CurShipEnergy >= MaxShipEnergy)
			{
				m_CurShipEnergy = MaxShipEnergy;
			}

			if(PlayerManager.getInstance().GetEnergyBar() != null)
			{
				float amount = m_CurShipEnergy / MaxShipEnergy;
				PlayerManager.getInstance().GetEnergyBar().SetEnergy(amount);
				
				if(MusicManager.GetInstance().SFXCtrl != null)
				{
					MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_ShipAbsorb);
				}
			}
		}

		if(m_bReadyBomb == false && m_CurShipEnergy >= OneSecShipEnergy)
		{
			m_bReadyBomb = true;

			if(m_CoreAnim != null)
			{
				m_CoreAnim.SetBool("boost",true);
			}
			
			if(PlayerManager.getInstance().GetEnergyBar() != null)
			{
				PlayerManager.getInstance().GetEnergyBar().EnergyEnough();
			}
		}




	}

	void ReduceEnergy(float energyValue)
	{
		m_CurShipEnergy -= energyValue;

		if(m_CurShipEnergy < 0.0f)
		{
			m_CurShipEnergy = 0.0f;
			if(PlayerManager.getInstance().GetEnergyBar() != null)
			{
				float amount = m_CurShipEnergy / MaxShipEnergy;
				PlayerManager.getInstance().GetEnergyBar().SetEnergy(0.0f);
			}
		}
		else
		{
			if(PlayerManager.getInstance().GetEnergyBar() != null)
			{
				float amount = m_CurShipEnergy / MaxShipEnergy;
				PlayerManager.getInstance().GetEnergyBar().SetEnergy(amount);

				if(MusicManager.GetInstance().SFXCtrl != null)
				{
					MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_ShipReduce);
				}
			}
		}


	}

	public void UseBomb()
	{
		if(m_bReadyBomb == false)
		{
			return;
		}

		m_CurShipEnergy -= OneSecShipEnergy;
		if(m_CurShipEnergy < 0.0f)
		{
			m_CurShipEnergy = 0.0f;
		}

		if(m_CurShipEnergy < OneSecShipEnergy)
		{
			m_bReadyBomb = false;
			
			m_CoreAnim.SetBool("boost",false);

			PlayerManager.getInstance().GetEnergyBar().EnergyNotFull();
		}

		m_bUsingBomb = true;

		//m_CurShipEnergy = 0.0f;
		//PlayerManager.getInstance().GetEnergyBar().ClearEnergy();
		float amount = m_CurShipEnergy / MaxShipEnergy;
		PlayerManager.getInstance().GetEnergyBar().SetEnergy(amount);
		PlayerManager.getInstance().GetEnergyBar().HideReleaseF ();
		if(m_CurShipEnergy >= OneSecShipEnergy)
		{
			PlayerManager.getInstance().GetEnergyBar().EnergyEnough();
		}


		switch(m_CurFrontType)
		{
		case ShipFrontType.FrontType_Gun1:
		{
			UseRotaryGun();
		}
			break;
		case ShipFrontType.FrontType_Shild1:
		{
			UseRotaryGun();
		}
			break;
		case ShipFrontType.FrontType_Gun2:
		{
			UseHugeBomb();
		}
			break;
		case ShipFrontType.FrontType_Shild2:
		{
			UseHugeBomb();
		}
			break;
		case ShipFrontType.FrontType_Gun3:
		{
			UseHugeLazer();
		}
			break;
		case ShipFrontType.FrontType_Shild3:
		{
			UseHugeLazer();
		}
			break;
		}
	}

	void UseHugeBomb()
	{
		//fire sp bullet
		m_BombGun.Shoot ();

		m_bUsingBomb = false;
		RecoveryCurWeapon ();
	}

	void UseRotaryGun()
	{
		m_bEnableRotaryGun = true;

		m_RotaryGunDegree = 0.0f;

		EnableAllWeapon ();

		m_Gun1.EnableFastShoot ();
		m_Gun2.EnableFastShoot ();
		m_Gun3.EnableFastShoot ();
	}

	void CloseRotaryGun()
	{
		m_Gun1.DisableFastShoot ();
		m_Gun2.DisableFastShoot ();
		m_Gun3.DisableFastShoot ();

		CloseAllWeapon ();

		this.gameObject.transform.eulerAngles = new Vector3 (0.0f,0.0f,0.0f);

		m_bEnableRotaryGun = false;

		m_bUsingBomb = false;
		RecoveryCurWeapon ();
	}

	void RecoveryCurWeapon()
	{
		switch(m_CurFrontType)
		{
		case ShipFrontType.FrontType_Gun1:
		{
			//m_Gun1.gameObject.SetActive(true);
			m_Gun1.EnableGunSystem();
		}
			break;
		case ShipFrontType.FrontType_Shild1:
		{
			//m_Shild1.gameObject.SetActive(true);
			m_Shild1.EnableShieldSystem();;
		}
			break;
		case ShipFrontType.FrontType_Gun2:
		{
			//m_Gun2.gameObject.SetActive(true);
			m_Gun2.EnableGunSystem();
		}
			break;
		case ShipFrontType.FrontType_Shild2:
		{
			//m_Shild2.gameObject.SetActive(true);
			m_Shild2.EnableShieldSystem();
		}
			break;
		case ShipFrontType.FrontType_Gun3:
		{
			//m_Gun3.gameObject.SetActive(true);
			m_Gun3.EnableGunSystem();
		}
			break;
		case ShipFrontType.FrontType_Shild3:
		{
			//m_Shild3.gameObject.SetActive(true);
			m_Shild3.EnableShieldSystem();
		}
			break;
		}
	}

	void UseHugeLazer()
	{
		m_SLGun.Shoot ();
		
		m_bUsingBomb = false;
		RecoveryCurWeapon ();
	}

	void UseFloatingGun()
	{
		m_bUsingBomb = false;
		RecoveryCurWeapon ();
	}

	//-------------------------
	public void ShipBackStep(EnemyBulletType type)
	{
		Vector2 dir_right = new Vector2(1.0f,0.0f);
		Vector2 dir_rightUp = new Vector2(0.577f,1.0f);
		Vector2 dir_leftUp = new Vector2(-0.577f,1.0f);
		Vector2 dir_left = new Vector2(-1.0f,0.0f);
		Vector2 dir_leftDown = new Vector2(-0.577f,-1.0f);
		Vector2 dir_rightDown = new Vector2(0.577f,-1.0f);

		dir_rightUp.Normalize();
		dir_leftUp.Normalize();
		dir_leftDown.Normalize();
		dir_leftDown.Normalize();

		Vector2 step = new Vector2 (0.0f, 0.0f);

		switch(m_CurFrontType)
		{
		case ShipFrontType.FrontType_Gun1:
			{
			if(type == EnemyBulletType.EnemyBullet_Blue)
			{
				step = m_BackStepLength * dir_leftDown;
			}
			else if(type == EnemyBulletType.EnemyBullet_Green)
			{
				step = m_BackStepLength * dir_right;
			}
			else if(type == EnemyBulletType.EnemyBullet_Yellow)
			{
				step = m_BackStepLength * dir_leftUp;
			}
			}
			break;
		case ShipFrontType.FrontType_Shild1:
		{
			if(type == EnemyBulletType.EnemyBullet_Blue)
			{
				step = m_BackStepLength * dir_left;
			}
			else if(type == EnemyBulletType.EnemyBullet_Green)
			{
				step = m_BackStepLength * dir_rightDown;
			}
			else if(type == EnemyBulletType.EnemyBullet_Yellow)
			{
				step = m_BackStepLength * dir_rightUp;
			}
		}
			break;
		case ShipFrontType.FrontType_Gun2:
		{
			{
				if(type == EnemyBulletType.EnemyBullet_Blue)
				{
					step = m_BackStepLength * dir_leftUp;
				}
				else if(type == EnemyBulletType.EnemyBullet_Green)
				{
					step = m_BackStepLength * dir_leftDown;
				}
				else if(type == EnemyBulletType.EnemyBullet_Yellow)
				{
					step = m_BackStepLength * dir_right;
				}
			}
		}
			break;
		case ShipFrontType.FrontType_Shild2:
		{
			{
				if(type == EnemyBulletType.EnemyBullet_Blue)
				{
					step = m_BackStepLength * dir_rightUp;
				}
				else if(type == EnemyBulletType.EnemyBullet_Green)
				{
					step = m_BackStepLength * dir_left;
				}
				else if(type == EnemyBulletType.EnemyBullet_Yellow)
				{
					step = m_BackStepLength * dir_rightDown;
				}
			}
		}
			break;
		case ShipFrontType.FrontType_Gun3:
		{
			{
				if(type == EnemyBulletType.EnemyBullet_Blue)
				{
					step = m_BackStepLength * dir_right;
				}
				else if(type == EnemyBulletType.EnemyBullet_Green)
				{
					step = m_BackStepLength * dir_leftDown;
				}
				else if(type == EnemyBulletType.EnemyBullet_Yellow)
				{
					step = m_BackStepLength * dir_leftDown;
				}
			}
		}
			break;
		case ShipFrontType.FrontType_Shild3:
		{
			{
				if(type == EnemyBulletType.EnemyBullet_Blue)
				{
					step = m_BackStepLength * dir_rightDown;
				}
				else if(type == EnemyBulletType.EnemyBullet_Green)
				{
					step = m_BackStepLength * dir_rightUp;
				}
				else if(type == EnemyBulletType.EnemyBullet_Yellow)
				{
					step = m_BackStepLength * dir_left;
				}
			}
		}
			break;
		}

		this.transform.localPosition = new Vector3 (this.transform.localPosition.x + step.x, 
		                                            this.transform.localPosition.y + step.y,
		                                            this.transform.localPosition.z);
		//this.transform.localPosition = new Vector3 (this.transform.localPosition.x - m_BackStepLength, 
		//                                           this.transform.localPosition.y,
		//                                           this.transform.localPosition.z);
	}

	public void RecoveryShipPos()
	{
		Vector3 dir = StartPosObject.transform.localPosition - this.transform.localPosition;
		dir.Normalize ();

		Vector3 recoveryStep = m_RecoveryStepLength * dir;
		this.transform.localPosition = new Vector3 (this.transform.localPosition.x + recoveryStep.x, 
		                                            this.transform.localPosition.y + recoveryStep.y,
		                                            this.transform.localPosition.z);
	}

	public void ShipComeToStage()
	{
		float bornx = BornPosObject.transform.position.x;
		float borny = PlayerManager.getInstance ().GetGameCamera ().gameObject.transform.position.y;
		this.transform.position = new Vector3(bornx,borny,BornPosObject.transform.position.z);

		Vector3 vStartpos = new Vector3 (StartPosObject.transform.position.x,borny,StartPosObject.transform.position.z);

		iTween.MoveTo(this.gameObject,iTween.Hash("position",vStartpos,"speed",2.0f,"easetype",iTween.EaseType.easeInSine,
		                                          "oncomplete","ShipComeToStageOver"));

		if(MusicManager.GetInstance().SFXCtrl != null)
		{
			MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_ShipRush);
		}
		//
		MakeMeGod();

		VisibleShip ();
	}

	public void ShipComeToStageOver()
	{
		//PlayerManager.getInstance ().GetPathMover ().ContinueMover(new Vector2(StartPosObject.transform.position.x,StartPosObject.transform.position.y));

		PlayerManager.getInstance ().GetPathMover ().EnableMover ();

		ShipFighting ();
		//

		Invoke("MakeMeUngod",GodTime);
	}

	public void ShipOutToStage()
	{
		iTween.MoveTo(this.gameObject,iTween.Hash("position",StartPosObject.transform.position,"speed",0.5f,"easetype",iTween.EaseType.easeInSine,
		                                          "oncomplete","ShipOutToStageOver1"));


	}

	public void ShipOutToStageOver1()
	{
		iTween.MoveTo(this.gameObject,iTween.Hash("position",CompletePosObject.transform.position,"speed",6.0f,"easetype",iTween.EaseType.easeInSine,
		                                          "oncomplete","ShipOutToStageOver2"));
		
		if(MusicManager.GetInstance().SFXCtrl != null)
		{
			MusicManager.GetInstance().SFXCtrl.PlaySound(SoundType.Sound_ShipRush);
		}
	}

	public void ShipOutToStageOver2()
	{
		PlayerManager.getInstance ().GetThanksBoard ().gameObject.SetActive (true);
	}

	public void Restart()
	{
		m_ShipLifeTimes = MaxShipLifeTimes;

		SetShipState (ShipState.ShipState_Begin);
	}
	//-------------------------
	public void MakeMeUngod()
	{
		m_ShipRing.gameObject.SetActive (false);
		IsGod = false;
	}
	public void MakeMeGod()
	{
		if(m_ShipRing != null)
		{
			m_ShipRing.gameObject.SetActive (true);
			IsGod = true;
		}
	}

	public void VisibleShip()
	{
		switch(m_CurFrontType)
		{
			case ShipFrontType.FrontType_Gun1:
			{
				m_ShipTexObj_0.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			}
				break;
			case ShipFrontType.FrontType_Shild1:
			{
				m_ShipTexObj_60.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			}
				break;
			case ShipFrontType.FrontType_Gun2:
			{
				m_ShipTexObj_120.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			}
				break;
			case ShipFrontType.FrontType_Shild2:
			{
				m_ShipTexObj_180.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			}
				break;
			case ShipFrontType.FrontType_Gun3:
			{
				m_ShipTexObj_240.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			}
				break;
			case ShipFrontType.FrontType_Shild3:
			{
				m_ShipTexObj_300.color = new Color(m_ShipTexObj_0.color.r,m_ShipTexObj_0.color.g,m_ShipTexObj_0.color.b,1.0f);
			}
				break;
		}

		m_CoreAnim.gameObject.SetActive (true);
	}

	public void InvisibleShip()
	{
		m_ShipTexObj_0.color = new Color (m_ShipTexObj_0.color.r, m_ShipTexObj_0.color.g, m_ShipTexObj_0.color.b, 0.0f);
		m_ShipTexObj_60.color = new Color (m_ShipTexObj_60.color.r, m_ShipTexObj_60.color.g, m_ShipTexObj_60.color.b, 0.0f);
		m_ShipTexObj_120.color = new Color (m_ShipTexObj_120.color.r, m_ShipTexObj_120.color.g, m_ShipTexObj_120.color.b, 0.0f);
		m_ShipTexObj_180.color = new Color (m_ShipTexObj_180.color.r, m_ShipTexObj_180.color.g, m_ShipTexObj_180.color.b, 0.0f);
		m_ShipTexObj_240.color = new Color (m_ShipTexObj_240.color.r, m_ShipTexObj_240.color.g, m_ShipTexObj_240.color.b, 0.0f);
		m_ShipTexObj_300.color = new Color (m_ShipTexObj_300.color.r, m_ShipTexObj_300.color.g, m_ShipTexObj_300.color.b, 0.0f);

		m_CoreAnim.gameObject.SetActive (false);
	}
}