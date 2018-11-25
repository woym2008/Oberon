using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverBtn : MonoBehaviour {

	Button m_btn = null;

	// Use this for initialization
	void Start () {
		m_btn = this.gameObject.GetComponent<Button>();

		m_btn.onClick.AddListener (press);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void press()
	{
		GameManager.getInstance ().RestartGame ();
	}
}
