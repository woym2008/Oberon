using UnityEngine;
using System.Collections;

public class ScenePathMover : MonoBehaviour 
{
	SceneTailor m_SceneTailor = null;

	FinalPosFinder m_FinalFinder = null;

	public Vector2 m_CurMoverSpeed = new Vector2(0.02f,0.0f);

	Vector2 NormalMoveSpeed = new Vector2(0.02f,0.0f);

	bool m_bEnableMove = false;

	Vector3 StartPos = new Vector3(0.0f,0.0f,0.0f);

	bool m_ArrivalFinal = false;

	public GameObject MiddleSceneObj = null;

	Vector2 m_MiddleSceneSpeed = new Vector2(-0.0155f,0.0f);

	float CameraStandardHeight = 0.0f;

	float m_ShipUpSpeed = 3.0f;

	// Use this for initialization
	void Start () 
	{
		GameObject SceneObj = GameObject.Find("Scene");

		m_SceneTailor = SceneObj.GetComponent<SceneTailor>();

		m_FinalFinder = this.transform.Find("FinalFinder").gameObject.GetComponent<FinalPosFinder>();
		m_FinalFinder.SetMover (this);

		CameraStandardHeight = this.transform.position.y;

		//MiddleSceneObj = this.transform.FindChild ("MiddleScene").gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.R))
		{
			GameManager.getInstance().RestartGame();
		}

		if(m_bEnableMove == true && m_ArrivalFinal == false)
		{
			this.transform.position = new Vector3(
				this.transform.position.x + m_CurMoverSpeed.x,
				//this.transform.position.y + m_CurMoverSpeed.y,
				this.transform.position.y,
				this.transform.position.z
				);
			if(MiddleSceneObj != null)
			{
				MiddleSceneObj.transform.localPosition = new Vector3(
					MiddleSceneObj.transform.localPosition.x + m_MiddleSceneSpeed.x,
					MiddleSceneObj.transform.localPosition.y + m_MiddleSceneSpeed.y,
					MiddleSceneObj.transform.localPosition.z
					);
			}
		}
	}

	public void UpdateCameraY(float deltaY)
	{
		if(m_bEnableMove == true && m_ArrivalFinal == false)
		{
			this.transform.position = new Vector3(
				this.transform.position.x,
				CameraStandardHeight + deltaY,
				this.transform.position.z
				);
//			if(MiddleSceneObj != null)
//			{
//				MiddleSceneObj.transform.localPosition = new Vector3(
//					MiddleSceneObj.transform.localPosition.x + m_MiddleSceneSpeed.x,
//					MiddleSceneObj.transform.localPosition.y + m_MiddleSceneSpeed.y,
//					MiddleSceneObj.transform.localPosition.z
//					);
//			}
		}
	}

	public void LoadMiddleScene(int levelnum)
	{
		MiddleSceneObj = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefab/LevelMiddleScene/" + "MiddleScene_" + levelnum));

		if(MiddleSceneObj != null)
		{
			MiddleSceneObj.transform.parent = this.transform;
			MiddleSceneObj.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		}
	}

	public void ArrivalFinalArea()
	{
		m_ArrivalFinal = true;
	}

	public void EnableMover()
	{
		m_bEnableMove = true;

		//int numPathnode = m_SceneTailor.m_Path.nodes.Count;
		
		//Vector3[] paths = new Vector3[numPathnode];
		
		//for(int i = 0; i<numPathnode; ++i)
		//{
		//	paths[i] = m_SceneTailor.m_Path.nodes[i];
		//}

		//iTween.MoveTo(this.gameObject,iTween.Hash("path",paths,"speed",2.0f,"easetype",
		//                                    iTween.EaseType.linear,"oncomplete","AnimationEnd","oncompleteparams", this.gameObject));

	}

	public void StartMover()
	{
		//m_SceneTailor.CreatePathFromStart ();

		//if(m_SceneTailor.m_Path.nodes != null && m_SceneTailor.m_Path.nodes.Count > 0)
		//{
		//	this.transform.position = m_SceneTailor.m_Path.nodes[0];
		//}

		StartPos = m_SceneTailor.GetStartPos ();

		m_ArrivalFinal = false;
	}

	public void StopMover()
	{
		//iTween.Stop (this.gameObject);

		m_bEnableMove = false;
	}

	public void ContinueMover(Vector2 startpos)
	{
		m_bEnableMove = true;
		//if(startpos == null) 
		//{
		//	startpos = new Vector2(this.transform.position.x,this.transform.position.y);
		//}

		//m_SceneTailor.CreatePathFromCurrent (startpos);

		//EnableMover ();
	}

	public void FlyUpUpdate()
	{
		float upspeed =  m_ShipUpSpeed * Time.deltaTime;
		
		this.transform.localPosition = new Vector2 (this.transform.localPosition.x,this.transform.localPosition.y + upspeed);
	}
	
	public void FlyDownUpdate()
	{
		float upspeed =  m_ShipUpSpeed * Time.deltaTime;
		
		this.transform.localPosition = new Vector2 (this.transform.localPosition.x,this.transform.localPosition.y - upspeed);
	}

	public void SetCurMoverSpeed(float speed)
	{
		m_CurMoverSpeed.x = speed;
	}

	public void RecoveryMoverSpeed()
	{
		m_CurMoverSpeed = NormalMoveSpeed;
	}
}
