using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneTailor : MonoBehaviour {


	public iTweenPath m_Path = null;

	int DebugStartBGIndex = 0;	

	public List<GameObject> m_BGs = new List<GameObject>();

	public void SetDebugStartBGIndex(int i)
	{
		DebugStartBGIndex = Mathf.Min(m_BGs.Count - 1,i);

		CreatePath();
	}
	public void CreatePath()
	{
		int counter = 0;
		
		foreach(GameObject onebg in m_BGs)
		{
			if(onebg != null)
			{
				if(counter >= DebugStartBGIndex)
				{
					m_Path.nodes.Add(onebg.transform.position);
					m_Path.nodeCount++;
				}
				
				counter++;
				
			}
		}
	}

	public Vector3 GetStartPos()
	{
		if(m_BGs != null && m_BGs.Count > 0)
		{
			return m_BGs[0].transform.position;
		}

		return new Vector3 (0.0f, 0.0f, 0.0f);
	}

	public void CreatePathFromStart()
	{
		m_Path.nodes.Clear ();
		m_Path.nodeCount = 0;
		
		CreatePath();
	}

	public void CreatePathFromCurrent(Vector2 curpos)
	{
		//find nextmap
		m_Path.nodes.Clear ();
		m_Path.nodeCount = 0;

		foreach(GameObject onebg in m_BGs)
		{
			if(onebg != null)
			{
				//VerticalScene vScene = onebg.GetComponent<VerticalScene>();
				//if(vScene != null)
				//{
					//add vertical pos
				//}
				//else
				{
					if(curpos.x < onebg.transform.position.x)
					{
						m_Path.nodes.Add(onebg.transform.position);
						m_Path.nodeCount++;
					}
				}				
			}
		}
	}

	void Awake()
	{

	}

	// Use this for initialization
	void Start ()
	{
		//CreatePathFromStart ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
