using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {
	public float LifeTime = 1.0f;
	// Use this for initialization
	void Start () 
	{
		Invoke ("Die", LifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable()
	{

	}

	void Die()
	{
		Destroy (this.gameObject);
	}
}
