using UnityEngine;
using System.Collections;

public enum ShipFlyDirect
{
    Up      = 1,
    Down    = 2,
    Left    = 4,
    Right   = 8,
}

public class PlayerCtrl : MonoBehaviour {

	TriangleShip m_CtrlShip = null;

    int m_CurBlockFlyDirect = 0;

	bool m_bPressedRotKey = false;
	float m_PressedTime_RotKey = 0.0f;
	float BombEnableTime = 1.0f;

	bool m_bEnableBomb = false;

	float m_ShipUpSpeed = 3.0f;
    float m_ShipMoveSpeed = 3.0f;

	bool LockUp = false;
	bool LockDown = false;

	bool pressedFly = false;

	// Use this for initialization
	void Start () {
		m_CtrlShip = this.gameObject.transform.Find("TriangleShipObj").gameObject.GetComponent<TriangleShip>();
        //m_CtrlShip = this.GetComponent<TriangleShip> ();

        m_CurBlockFlyDirect = 0;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_CtrlShip == null)
		{
			return;
		}

		if(m_bPressedRotKey && m_bEnableBomb==false)
		{
			m_PressedTime_RotKey += Time.deltaTime;
			if(m_PressedTime_RotKey >= BombEnableTime)
			{
				m_bEnableBomb = true;
			}
		}

		//test
		if(Input.GetKeyDown(KeyCode.O))
		{
			m_CtrlShip.ShiledBulletAbsorb(EnemyBulletType.EnemyBullet_Blue,100.0f);
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		//testend

		PressFly ();

		if(m_bEnableBomb && m_CtrlShip.IsReadyUseBomb())
		{
			PlayerManager.getInstance().GetEnergyBar().ShowReleaseF();
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
			PressRot();
		}
		else if(Input.GetKeyUp(KeyCode.F))
		{
			ReleaseRot();
		}

	}

	void PressFly()
	{
		if(m_CtrlShip.GetShipState() == ShipState.ShipState_Alive)
		{
			//if(Input.GetKey(KeyCode.D) || pressedFly)
			//{
			//	FlyUp();
			//}
			//else
			//{
			//	FlyDown();
			//}

            if(Input.GetKey(KeyCode.W))
            {
                FlyUp();
            }
            else if(Input.GetKey(KeyCode.S))
            {
                FlyDown();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (CheckDirCanFly(ShipFlyDirect.Left))
                {
                    MoveBack();
                }                    
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if(CheckDirCanFly(ShipFlyDirect.Right))
                {
                    MoveForward();
                }                
            }
		}
	}

	public void PressFlyBtn()
	{
		pressedFly = true;
	}

	public void ReleaseFlyBtn()
	{
		pressedFly = false;
	}

	public void PressRot()
	{
		if(m_bPressedRotKey == false)
		{
			m_bPressedRotKey = true;
			
			m_PressedTime_RotKey = 0.0f;
		}
	}

	public void ReleaseRot()
	{
		m_bPressedRotKey = false;
		
		if(m_bEnableBomb && m_CtrlShip.IsReadyUseBomb())
		{
			m_CtrlShip.UseBomb();
			m_bEnableBomb = false;
		}
		else
		{
			m_CtrlShip.RotTrangle();
		}
	}

	public void FlyUp()
	{
		if(LockUp == true)
		{
			return;
		}

		float upspeed = m_ShipUpSpeed * Time.deltaTime;
		
		this.transform.localPosition = new Vector2 (this.transform.localPosition.x,this.transform.localPosition.y + upspeed);
	}

	public void FlyDown()
	{
		if(LockDown == true)
		{
			return;
		}

		float upspeed = m_ShipUpSpeed * Time.deltaTime;
		
		this.transform.localPosition = new Vector2 (this.transform.localPosition.x,this.transform.localPosition.y - upspeed);
	}

    public void MoveForward()
    {
        float speed = m_ShipMoveSpeed * Time.deltaTime;

        this.transform.localPosition = new Vector2(this.transform.localPosition.x + speed,this.transform.localPosition.y);
    }

    public void MoveBack()
    {
        float speed = m_ShipMoveSpeed * Time.deltaTime;

        this.transform.localPosition = new Vector2(this.transform.localPosition.x - speed, this.transform.localPosition.y);
    }

	public void LockUpCtrl()
	{
		LockUp = true;
	}

	public void LockDownCtrl()
	{
		LockDown = true;
	}

	public void UnLockUpCtrl()
	{
		LockUp = false;
	}
	
	public void UnLockDownCtrl()
	{
		LockDown = false;
	}
    //-----------------------------------------
    public void BlockFly(ShipFlyDirect dir)
    {
        m_CurBlockFlyDirect |= (int)dir;
    }

    public void ResumeFLy(ShipFlyDirect dir)
    {
        m_CurBlockFlyDirect &= ~((int)dir);
    }

    public bool CheckDirCanFly(ShipFlyDirect dir)
    {
        if((m_CurBlockFlyDirect & (int)dir) != 0)
        {
            return false;
        }

        return true;
    }
}
