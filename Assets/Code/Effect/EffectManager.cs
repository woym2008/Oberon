using UnityEngine;
using System.Collections;

public enum EffectType
{
	Effect_Absorb = 0,
	Effect_Reduce,
	Effect_ShipDie,
}

public class EffectManager : MonoBehaviour 
{
	private static EffectManager mSingleton;
	public static EffectManager GetInstance(){return mSingleton;}
	
	public GameObject[]		EffectPrefabs;
	
	void Awake()
	{
		mSingleton = this;
		//
		foreach(GameObject b in EffectPrefabs)
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
	
	public GameObject CreateEffect(EffectType MType,Vector2 Pos, Transform par = null)
	{
		GameObject effect = null;
		
		effect = GameObject.Instantiate(EffectPrefabs[(int)MType]) as GameObject;
		
		effect.transform.position = Pos;
		
		effect.SetActive(true);

		effect.transform.parent = par;
		
		return effect;
	}
}
