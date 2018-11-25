using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ThanksBoard : MonoBehaviour , IPointerDownHandler
{
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.anyKeyDown == true) 
		{
			GameManager.getInstance ().RestartGame ();
		}
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		GameManager.getInstance ().RestartGame ();
	}
}
