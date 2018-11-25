using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameST
{
	GAMEST_BEGIN = 0,
	GAMEST_READY,
	GAMEST_RUNNING,
	GAMEST_READYOVER,
	GAMEST_OVER,
	GAMEST_PAUSE,
}

public class GameManager
{
	//---------------------------------------------------
	static GameManager m_Instance = null;
	
	public static GameManager getInstance()
	{
		if(m_Instance == null)
		{
			m_Instance = new GameManager();
		}
		
		return m_Instance;
	}
	//---------------------------------------------------
	const float cReadyWaitTime = 0.0f;
	float m_currentReadyTime = 0.0f;
	
	const float cGameReadyOverTime = 2.0f;
	float m_currentReadyOverTime = 0.0f;

	//---------------------------------------------------
	public float GetGameRunTime(){return m_GameRunTime;}
	float m_GameRunTime = 0.0f;

	float m_CurCountdownTime = 0.0f;

	//float MaxRandomShipNeedsEnergyTime = 20.0f;
	//float m_curNeedsEnergyTime = 0.0f;
	//---------------------------------------------------
	//public SoundController m_SECtrl = null;
	
	//public ShowBoard m_FinishObj = null;
	//---------------------------------------------------
	protected GameObject m_BGObject = null;
	//---------------------------------------------------
	GameST m_GameST;
	//---------------------------------------------------
	public bool m_bGodMode = false;
	//---------------------------------------------------
	public float GameScore = 0.0f;

	const int MaxKillCombo = 20;
	int KillCombo = 0;

	const float MaxKillComboTime = 0.5f;
	float KillComboTime = 0.0f;
	//---------------------------------------------------
	public GameST GST
	{
		get
		{
			return m_GameST;
		}
		
		set
		{
			switch(m_GameST)
			{
			case GameST.GAMEST_READY:
			{
				postGameReady();
			}
				break;
			case GameST.GAMEST_RUNNING:
			{
				postGameRunning();
			}
				break;
			case GameST.GAMEST_READYOVER:
			{
				postGameReadyOver();
			}
				break;
			case GameST.GAMEST_OVER:
			{
				postGameOver();
			}
				break;
			case GameST.GAMEST_PAUSE:
			{
				postGamePause();
			}
				break;
			}
			
			m_GameST = value;
			
			switch(m_GameST)
			{
			case GameST.GAMEST_READY:
			{
				preGameReady();
			}
				break;
			case GameST.GAMEST_RUNNING:
			{
				preGameRunning();
			}
				break;
			case GameST.GAMEST_READYOVER:
			{
				preGameReadyOver();
			}
				break;
			case GameST.GAMEST_OVER:
			{
				preGameOver();
			}
				break;
			case GameST.GAMEST_PAUSE:
			{
				preGamePause();
			}
				break;
			}
		}
	}

	//------------------------------
	public void Update()
	{
		switch(m_GameST)
		{
		case GameST.GAMEST_READY:
		{
			updateGameReady();
		}
			break;
		case GameST.GAMEST_RUNNING:
		{
			updateGameRunning();
		}
			break;
		case GameST.GAMEST_READYOVER:
		{
			updateGameReadyOver();
		}
			break;
		case GameST.GAMEST_OVER:
		{
			updateGameOver();
		}
			break;
		case GameST.GAMEST_PAUSE:
		{
			updateGamePause();
		}
			break;
		}
	}

	//------------------------------
	void preGameRunning()
	{
		//PlayerManager.getInstance ().GetPathMover ().EnableMover ();

		PlayerManager.getInstance ().GetPlayer ().SendMessage("ShipSail");

		//RandomShipNeedsEnergy();

	}

	void preGameReadyOver()
	{
		m_currentReadyOverTime = cGameReadyOverTime;

		GameScore = 0.0f;
	}
	
	void preGameOver()
	{
		PlayerManager.getInstance ().GetGameOverShow ().SetActive (true);
	}

	void preGamePause()
	{}
	
	void preGameReady()
	{
		MusicManager.GetInstance ().BGMCtrl.StopBGM ();
		MusicManager.GetInstance ().BGMCtrl.PlayBGM ("EnergyDrink");

		m_currentReadyTime = cReadyWaitTime;
		
		GameObject pObj = GameObject.Find ("SoundObject");
		if(pObj != null)
		{
		//	m_SECtrl = pObj.GetComponent<SoundController>();
		}
		else
		{
//			GameObject pNewObj = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefab/" + "SoundObject"));
//			if(pNewObj != null)
			{
		//		m_SECtrl = pNewObj.GetComponent<SoundController>();
			}
		}

		ClearGameScore ();
	}

	void updateGameRunning()
	{
		m_GameRunTime += Time.deltaTime;
		
		if(m_CurCountdownTime > 0.0f)
		{
			m_CurCountdownTime -= Time.deltaTime;
		}

		//m_curNeedsEnergyTime += Time.deltaTime;
		//if(m_curNeedsEnergyTime >= MaxRandomShipNeedsEnergyTime)
		//{
		//	RandomShipNeedsEnergy();
		//	m_curNeedsEnergyTime = 0.0f;
		//}

		//Combo
		if(KillCombo > 0)
		{
			KillComboTime -= Time.deltaTime;
			if(KillComboTime <= 0.0f)
			{
				KillCombo = 0;
			}
		}
	}

	void updateGameReadyOver()
	{
		if(m_currentReadyOverTime > 0.0f)
		{
			m_currentReadyOverTime -= Time.deltaTime;
		}
		else
		{
			GST = GameST.GAMEST_OVER;
		}
	}

	void updateGameOver()
	{}
	
	void updateGamePause()
	{}
	
	void updateGameReady()
	{
		if(m_currentReadyTime > 0.0f)
		{
			m_currentReadyTime -= Time.deltaTime;
		}
		else
		{
			GST = GameST.GAMEST_RUNNING;
		}
	}
	//------------------------------
	void postGameRunning()
	{}
	
	void postGameReadyOver()
	{
	}
	
	void postGameOver()
	{}
	
	void postGamePause()
	{}
	
	void postGameReady()
	{
		TriangleShip ship = PlayerManager.getInstance ().GetPlayer ().GetComponent<TriangleShip> ();
		if(ship != null)
		{
			ship.ShiledBulletReduce( EnemyBulletType.EnemyBullet_Blue,90.0f);
			ship.ShiledBulletAbsorb( EnemyBulletType.EnemyBullet_Blue,90.0f);
		}
	}

	public void GameOver()
	{
		GST = GameST.GAMEST_OVER;
	}

	public void StartGame()
	{
		GST = GameST.GAMEST_READY;
	}
	//------------------------------
	public void ClearShipEnergy()
	{
		if(PlayerManager.getInstance().GetPlayer() != null)
		{
			TriangleShip shipsc = PlayerManager.getInstance().GetPlayer().GetComponent<TriangleShip>();
			if(shipsc != null)
			{
				shipsc.ClearShipEnergy();
			}
		}
	}

	public void RandomShipNeedsEnergy()
	{
		if(PlayerManager.getInstance().GetPlayer() != null)
		{
			TriangleShip shipsc = PlayerManager.getInstance().GetPlayer().GetComponent<TriangleShip>();
			if(shipsc != null)
			{
				shipsc.RandomNeedsEnergyType();
			}
		}
	}

	public void RestartGame()
	{
		GST = GameST.GAMEST_READY;

        //Application.LoadLevel ("BattleField");
        GameLevelManager.getInstance().StartBegin();


		/*
		PlayerManager.getInstance ().GetPathMover ().StartMover ();

		PlayerManager.getInstance ().GetGameOverShow ().SetActive (false);

		TriangleShip ship = PlayerManager.getInstance ().GetPlayer ().GetComponent<TriangleShip> ();

		if(ship != null)
		{
			ship.gameObject.SetActive(true);
			ship.Restart();
		}
		*/
	}

	public void AddGameScore(float add)
	{
		add *= 10.0f;

		if(KillCombo > 0)
		{
			float comboscore = add + add * 0.1f * KillCombo * KillCombo;
			GameScore += comboscore;
		}
		else
		{
			GameScore += add;
		}

		if(KillCombo < MaxKillCombo)
		{
			KillCombo++;
		}
		else
		{
			KillCombo = MaxKillCombo;
		}

		KillComboTime = MaxKillComboTime;
	}

	public void ClearGameScore()
	{
		GameScore = 0.0f;

		KillCombo = 0;

		KillComboTime = 0.0f;
	}

	public float GetGameScore()
	{
		return GameScore;
	}
}
