using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonCtrl_Left : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	TriangleShip m_CtrlShip = null;

	PlayerCtrl mCtrl = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void SetMainBody(TriangleShip ship)
	{
		m_CtrlShip = ship;
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		if(mCtrl != null)
		{
			if(m_CtrlShip.GetShipState() == ShipState.ShipState_Alive)
			{
				mCtrl.PressFlyBtn();
			}
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
			if(m_CtrlShip.GetShipState() == ShipState.ShipState_Alive)
			{
				mCtrl.ReleaseFlyBtn();
			}
		}	
		else
		{
			mCtrl = PlayerManager.getInstance().GetPlayerCtrl();
		}
	}
}
