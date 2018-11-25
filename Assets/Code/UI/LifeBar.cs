using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LifeBar : MonoBehaviour 
{
	public List<GameObject> curLifeSigns = new List<GameObject>();

	public GameObject LifeSignPrefab = null;

	// Use this for initialization
	void Start () {
		LifeSignPrefab = this.transform.Find ("LifeSign").gameObject;
		LifeSignPrefab.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddLife()
	{
		GameObject sign = null;
		
		sign = GameObject.Instantiate(LifeSignPrefab) as GameObject;

		sign.SetActive (true);

		sign.transform.parent = this.transform;

		sign.transform.localScale = new Vector3 (1.0f,1.0f,1.0f);

		float num = (float)(curLifeSigns.Count);

		curLifeSigns.Add (sign);

		Vector3 lopos = new Vector3(-50.0f*num,0.0f,0.0f);

		sign.transform.localPosition = lopos;		
	}

	public void LoseLife()
	{
		int num = curLifeSigns.Count;
		GameObject delSign = curLifeSigns[num-1];
		curLifeSigns.Remove(delSign);
		if(delSign != null)
		{
			Destroy(delSign);
		}
	}
}
