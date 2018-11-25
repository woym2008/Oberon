using UnityEngine;
using System.Collections;

public class PlayerManager
{
	static PlayerManager m_Instance = null;
	
	public static PlayerManager getInstance()
	{
		if(m_Instance == null)
		{
			m_Instance = new PlayerManager();
		}
		
		return m_Instance;
	}

	GameObject m_Player = null;

	ScenePathMover m_SceneMover = null;

	GameObject m_ShipGroup = null;
	
	EnergyBar m_EnergyBar = null;

	LifeBar m_LifeBar = null;

	GameObject m_GameOverShow = null;

	ThanksBoard m_ThanksBoard = null;

	ButtonCtrl m_BtnCtrl = null;

	LogicCamera GameCamera = null;

	PlayerCtrl m_PlayerCtrl = null;

	public bool CtrlCameraHeight = false;

	public GameObject CreatePlayer(Vector2 StartPos, int LevelNum)
	{
		GameObject SceneManager = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefab/" + "SceneManager"));

		SceneManager.transform.position = new Vector3 (StartPos.x, StartPos.y, SceneManager.transform.position.z);

		m_SceneMover = SceneManager.GetComponent<ScenePathMover> ();

		m_SceneMover.LoadMiddleScene (LevelNum);

		m_ShipGroup = SceneManager.transform.Find("ShipGroup").gameObject;

		m_PlayerCtrl = m_ShipGroup.GetComponent<PlayerCtrl>();

		m_Player = SceneManager.transform.Find("ShipGroup/TriangleShipObj").gameObject;

		TriangleShip ship = m_Player.GetComponent<TriangleShip> ();

		ship.StartPosObject = SceneManager.transform.Find("ShipGroup/StartPos").gameObject;

		ship.StartPosObject.SetActive (false);

		ship.BornPosObject = SceneManager.transform.Find("ShipGroup/BornPos").gameObject;
		
		ship.BornPosObject.SetActive (false);

		ship.CompletePosObject = SceneManager.transform.Find("CompletePos").gameObject;
		
		ship.CompletePosObject.SetActive (false);

		GameCamera = SceneManager.transform.Find("MainCamera").gameObject.GetComponent<LogicCamera>();
		GameCamera.SetShip (ship);
		GameCamera.SetSceneMgrObj (SceneManager);

		m_EnergyBar = SceneManager.transform.Find("Canvas/EnergyBar").gameObject.GetComponent<EnergyBar>();

		m_LifeBar = SceneManager.transform.Find("Canvas/LifeBar").gameObject.GetComponent<LifeBar>();

		for (int i=0; i<ship.GetCurShipLifeTimes(); ++i)
		{
			m_LifeBar.AddLife();
		}

		m_ThanksBoard = SceneManager.transform.Find("Canvas/ThanksPlayer").gameObject.GetComponent<ThanksBoard>();
		m_ThanksBoard.gameObject.SetActive (false);

		m_BtnCtrl = SceneManager.transform.Find("Canvas/BtnCtrl").gameObject.GetComponent<ButtonCtrl>();
		m_BtnCtrl.SetMainBody (ship);

#if UNITY_STANDALONE
		m_BtnCtrl.gameObject.SetActive(false);
#else

#endif

		MusicManager.GetInstance().SFXCtrl = SceneManager.transform.Find("SFXCtrl").gameObject.GetComponent<SoundController>();

		MusicManager.GetInstance().BGMCtrl = SceneManager.transform.Find("BGMCtrl").gameObject.GetComponent<BGMController>();

		m_GameOverShow = SceneManager.transform.Find ("Canvas/GameOver").gameObject;
		m_GameOverShow.SetActive (false);

		return m_Player;
	}

	public void PlayerDie(bool MustDie = false)
	{
		if(m_Player != null)
		{
			TriangleShip ship = m_Player.GetComponent<TriangleShip> ();
			if(ship != null && ship.GetShipState() == ShipState.ShipState_Alive )
			{
				if(MustDie == true)
				{
					ship.ShipDie();
				}
				else
				{
					if(ship.IsGod == false)
					{
						ship.ShipDie();
					}
				}
			}
		}
	}

	public void PlayerReborn()
	{
		if(m_Player != null)
		{
			m_Player.SendMessage("ShipReborn");
		}
	}

	public void PlayerBattle()
	{
	}

	public GameObject GetPlayer()
	{
		return m_Player;
	}

	public ScenePathMover GetPathMover()
	{
		return m_SceneMover;
	}

	public GameObject GetShipGroup()
	{
		return m_ShipGroup;
	}

	public EnergyBar GetEnergyBar()
	{
		return m_EnergyBar;
	}

	public LifeBar GetLifeBar()
	{
		return m_LifeBar;
	}

	public GameObject GetGameOverShow()
	{
		return m_GameOverShow;
	}

	public ThanksBoard GetThanksBoard()
	{
		return m_ThanksBoard;
	}

	public LogicCamera GetGameCamera()
	{
		return GameCamera;
	}

	public void SetCameraFollow(Vector2 up, Vector2 down)
	{
		if(GameCamera != null)
		{
			GameCamera.SetCameraFollow(up, down);
		}
	}

	public void CloseCameraFollow()
	{
		if(GameCamera != null)
		{
			GameCamera.CloseCameraFollow();
		}
	}

	public PlayerCtrl GetPlayerCtrl()
	{
		return m_PlayerCtrl;
	}
}
