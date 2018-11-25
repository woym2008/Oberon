using UnityEngine;
using System.Collections;

public enum BombType
{
	BombType_1 = 0,
	BombType_2,
	BombType_PlayerBomb,
}

public class BombManager : MonoBehaviour 
{
	private static BombManager mSingleton;
	public static BombManager GetInstance(){return mSingleton;}

	public GameObject[]		BombPrefabs;

	void Awake()
	{
		mSingleton = this;
		//
		foreach(GameObject b in BombPrefabs)
		{
			b.SetActive(false);
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject CreateBomb(BombType MType,Vector2 Pos)
	{
		GameObject bomb = null;
		
		bomb = GameObject.Instantiate(BombPrefabs[(int)MType]) as GameObject;
		
		bomb.transform.position = Pos;
		
		bomb.SetActive(true);
		
		return bomb;
	}
}
