using UnityEngine;
using System.Collections;

public class Boss_WormSpawn : MonoBehaviour {
	
	public GameObject[]	Spawns;
	public GameObject WormSimple = null;
	public int MaxAliveNum = 4;
	public int CurAliveWormNum = 0;
	public float SpawnGapMin = 2;
	public float SpawnGapMax = 4;
	// Use this for initialization
	void Start () {
		
		if(WormSimple != null)
		{
			WormSimple.SetActive(false);
		}
		//
		ActiveSpawn();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ActiveSpawn()
	{
		if(CurAliveWormNum < MaxAliveNum)
		{
			Invoke("Spawn",Random.Range(SpawnGapMin,SpawnGapMax));
		}
	}

	public void GameOver_For_WormSpawn()
	{
		CancelInvoke("Invoke");
		//
		Boss_Worm[] Worms = GetComponentsInChildren<Boss_Worm>();
		//
		foreach(Boss_Worm w in Worms)
		{
			w.gameObject.SetActive(false);
		}
	}
	
	public void Spawn()
	{
		int index = Random.Range(1,Spawns.Length);
		CreateWorm(Spawns[index].transform.position);
	}
	
	public void CreateWorm(Vector3 Pos)
	{
		GameObject monster = GameObject.Instantiate(WormSimple) as GameObject;


		monster.transform.parent = transform;		
		monster.transform.position = Pos;
		monster.transform.localScale = Vector3.one;
		
				
		monster.SetActive(true);
		//
		CurAliveWormNum++;
		
		ActiveSpawn();
		
	}
	
	public void ChildBeKilled()
	{
		CurAliveWormNum--;
		//
		ActiveSpawn();
	}
}
