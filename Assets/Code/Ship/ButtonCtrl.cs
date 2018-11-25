using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonCtrl : MonoBehaviour {

	public ButtonCtrl_Left BtnLeft = null; 
	public ButtonCtrl_Right BtnRight = null;

	void Awake()
	{
		BtnLeft = this.gameObject.transform.Find ("LeftBtn").gameObject.GetComponent<ButtonCtrl_Left>();
		BtnRight = this.gameObject.transform.Find ("RightBtn").gameObject.GetComponent<ButtonCtrl_Right>();
	}

	// Use this for initialization
	void Start () {

	}

	public void SetMainBody(TriangleShip ship)
	{
		BtnLeft.SetMainBody (ship);
		BtnRight.SetMainBody (ship);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
