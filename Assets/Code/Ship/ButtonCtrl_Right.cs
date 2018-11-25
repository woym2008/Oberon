using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCtrl_Right : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	TriangleShip m_CtrlShip = null;

	PlayerCtrl mCtrl = null;

	bool m_bPressedRotKey = false;
	float m_PressedTime_RotKey = 0.0f;
	float BombEnableTime = 1.0f;
	
	bool m_bEnableBomb = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetMainBody(TriangleShip ship)
	{
		m_CtrlShip = ship;
	}
	
	public void OnPointerDown (PointerEventData eventData)
	{
		if(mCtrl != null)
		{
			mCtrl.PressRot();
		}
		else
		{
			mCtrl = PlayerManager.getInstance().GetPlayerCtrl();
		}

	}
	
	public void OnPointerUp (PointerEventData eventData)
	{
		if(mCtrl != null)
		{
			mCtrl.ReleaseRot();
		}
		else
		{
			mCtrl = PlayerManager.getInstance().GetPlayerCtrl();
		}
	}
}
