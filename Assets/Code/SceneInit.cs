using UnityEngine;
using System.Collections;

public class SceneInit : MonoBehaviour {
	// Use this for initialization

	float BG_Width = 12.8f;
	float BG_Height = 7.2f;

	public int LevelNumber = 1;

    private void Awake()
    {
        
    }

    void Start () 
	{
		Object loadMonMgr = Resources.Load ("Prefab/MonsterManager/" + "MonsterManager_" + LevelNumber);
		if(loadMonMgr != null)
		{
			GameObject MonsterMgr = (GameObject)UnityEngine.Object.Instantiate(loadMonMgr);
		}

		GameObject BombMgr = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefab/" + "BombManager"));
		GameObject EffectMgr = (GameObject)UnityEngine.Object.Instantiate(Resources.Load("Prefab/" + "EffectManager"));


		GameObject SceneObj = GameObject.Find("Scene");
		
		SceneTailor st = SceneObj.GetComponent<SceneTailor>();
		
		float Px = this.transform.position.x;
		
		float num = Px / BG_Width;
		
		int index = Mathf.CeilToInt(num);
		
		//st.SetDebugStartBGIndex(index);

		//
		PlayerManager.getInstance ().CreatePlayer (this.transform.position, LevelNumber);
		//


		
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameManager.getInstance ().Update ();
	}
}
