using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScore : MonoBehaviour {

	public Text m_ValueText;

	float curScore = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(curScore != GameManager.getInstance().GetGameScore())
		{
			curScore = GameManager.getInstance().GetGameScore();

			m_ValueText.text = "" + curScore;
		}
	
	}
}
