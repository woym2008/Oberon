using UnityEngine;
using System.Collections;

public class BossNest : MonoBehaviour {

	public GameObject BossSimple;
	public float DelayTime = 2.0f;
	protected bool bossActived = false;
	// Use this for initialization
	void Awake()
	{
		if(BossSimple != null) BossSimple.SetActive(false);
	}

	void Start () {
	
		if(BossSimple != null) BossSimple.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//
	public void ActiveNest()
	{


		if(bossActived == false)
		{
			Invoke("ShowBoss",DelayTime);
		}
	}
	//
	public void ShowBoss()
	{
		MusicManager.GetInstance ().BGMCtrl.StopBGM ();
		MusicManager.GetInstance ().BGMCtrl.PlayBGM ("Boss1");

		BossSimple.SetActive(true);
	}
}
