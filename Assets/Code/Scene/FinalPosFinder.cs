using UnityEngine;
using System.Collections;

public class FinalPosFinder : MonoBehaviour {

	ScenePathMover m_ScenePathMover= null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMover(ScenePathMover mover)
	{
		m_ScenePathMover = mover;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other != null && other.gameObject != null)
		{
			BossNest FArea = other.gameObject.GetComponent<BossNest>();
			if(FArea != null)
			{
				m_ScenePathMover.ArrivalFinalArea();
			}
		}
	}
}
