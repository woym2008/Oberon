using UnityEngine;
using System.Collections;

public class LogicCamera : MonoBehaviour 
{
	bool CameraFollow = true;
    Vector2 LimitUp;
    Vector2 LimitDown;

    bool m_bDontUp = false;

	bool m_bDontDown = false;

	bool EnableSceneCenterFollow = false;

	TriangleShip FollowShip = null;
	
	public float FollowTime = 2.0f;
	float InvFollowTime = 1.0f;

	public float FollowSceneTime = 1.0f;
	float InvFollowSceneTime = 1.0f;

	GameObject SceneManagerObj = null;
	// Use this for initialization
	void Start ()
	{
		if(FollowTime <= 0.0f)
		{
			FollowTime = 0.1f;
		}

		InvFollowTime = 1.0f / FollowTime;

		if(FollowSceneTime <= 0.0f)
		{
			FollowSceneTime = 0.1f;
		}
		
		InvFollowSceneTime = 1.0f / FollowSceneTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(CameraFollow)
		{
			CameraFollowShipStep();
		}
   //     else
   //     {
			//CameraFollowCenterStep();
   //     }

		if(EnableSceneCenterFollow)
		{
			SceneCenterFollowStep();
		}
	}

	void CameraFollowShipStep()
	{
		if(FollowShip != null)
		{
            if(this.transform.position.y > LimitUp.y)
            {
                CameraFollowPointUpStep(LimitUp);
                return;
            }
            if (this.transform.position.y < LimitDown.y)
            {
                CameraFollowPointDownStep(LimitDown);
                return;
            }
            //float speed = CameraFlowSpeed*Time.deltaTime;

            float dis = FollowShip.gameObject.transform.position.y - this.transform.position.y;

			if(dis > 0.0f && m_bDontUp == true)
			{
				return;
			}

			if(dis < 0.0f && m_bDontDown == true)
			{
				return;
			}

			if(Mathf.Abs(dis) < 0.02f)
			{
				return;
			}

			float followSpeed = dis * InvFollowTime;

			float curspeed = followSpeed*Time.deltaTime;

			//if(this.transform.position.y > FollowShip.gameObject.transform.position.y)
			//{
			//	this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y-curspeed,this.transform.localPosition.z);
			//}
			//else if(this.transform.position.y < FollowShip.gameObject.transform.position.y)
			//{
			//	this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y+curspeed,this.transform.localPosition.z);
			//}

			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y+curspeed,this.transform.localPosition.z);
		}
	}

    void CameraFollowPointUpStep(Vector2 targetpos)
    {
        float dis = targetpos.y - this.transform.position.y;
        if(dis >= 0)
        {
            return;
        }

        float followSpeed = dis * InvFollowTime;

        float curspeed = followSpeed * Time.deltaTime;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + curspeed, this.transform.localPosition.z);
    }

    void CameraFollowPointDownStep(Vector2 targetpos)
    {
        float dis = targetpos.y - this.transform.position.y;
        if (dis <= 0)
        {
            return;
        }

        float followSpeed = dis * InvFollowTime;

        float curspeed = followSpeed * Time.deltaTime;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + curspeed, this.transform.localPosition.z);
    }

    void SceneCenterFollowStep()
	{
		if(SceneManagerObj != null)
		{
			float dis = SceneManagerObj.transform.position.y - this.transform.position.y;

			if(Mathf.Abs(dis) <= 0.05f)
			{
				this.transform.localPosition = new Vector3(0.0f,0.0f,this.transform.localPosition.z);
				EnableSceneCenterFollow = false;
				return;
			}
			
			float followSpeed = dis * InvFollowSceneTime;
			
			float curspeed = followSpeed*Time.deltaTime;
			
			this.transform.localPosition = new Vector3(this.transform.localPosition.x,this.transform.localPosition.y+curspeed,this.transform.localPosition.z);
		}
	}

	public void SetCameraFollow(Vector2 up, Vector2 down)
	{
		CameraFollow = true;
		EnableSceneCenterFollow = false;

        LimitUp = up;
        LimitDown = down;
    }

	public void CloseCameraFollow()
	{
		CameraFollow = false;
		EnableSceneCenterFollow = true;
	}

	public void LockUp()
	{
		m_bDontUp = true;
	}

	public void LockDown()
	{
		m_bDontDown = true;
	}

	public void RecoveryUp()
	{
		m_bDontUp = false;
	}

	public void RecoveryDown()
	{
		m_bDontDown = false;
	}

	public void SetShip(TriangleShip ship)
	{
		FollowShip = ship;
	}

	public void SetSceneMgrObj(GameObject value)
	{
		SceneManagerObj = value;
	}
}
