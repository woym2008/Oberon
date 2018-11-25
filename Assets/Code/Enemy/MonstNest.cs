using UnityEngine;
using System.Collections;


public class MonstNest : MonoBehaviour 
{
	bool m_IsActivationByEnableArea = false;

	public MonsterType m_CurMonsterType = MonsterType.MonsterType_null;

	public bool m_EnableSpawnMonster = false;

	public int SpawnMonsterNum = 0;

	int m_curSpawnMonsterIndex = 0;

	public float SpawnIntervalTime = 0.2f;

	float m_curIntervalTime = 0.0f;

	public float WaitTime = 0.0f;

	public bool FlipY = false;

	public bool ReverseMove = false;

	void Awake()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		if(sr != null) sr.enabled = false;
	}
	// Use this for initialization
	void Start () {
		m_curIntervalTime = SpawnIntervalTime;

		SpriteRenderer spRender = this.gameObject.GetComponent<SpriteRenderer> ();
		if(spRender != null)
		{
			spRender.enabled = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if(m_EnableSpawnMonster == true)
		{
			if(WaitTime >0.0f)
			{
				WaitTime -= Time.deltaTime;
			}
			else
			{
				m_curIntervalTime -= Time.deltaTime;
				
				if(m_curIntervalTime <= 0.0f)
				{
					CreateMonster();
					
					m_curIntervalTime = SpawnIntervalTime;
					
					m_curSpawnMonsterIndex++;
				}
				
				if(m_curSpawnMonsterIndex >= SpawnMonsterNum)
				{
					m_EnableSpawnMonster = false;
				}
			}
		}
	}

	public bool IsActiveByEnableArea()
	{
		return m_IsActivationByEnableArea;
	}

	public void Activation()
	{
		m_IsActivationByEnableArea = true;

		m_EnableSpawnMonster = true;
	}

	public void UnActivation()
	{
		m_EnableSpawnMonster = false;
	}

	void CreateMonster()
	{
		GameObject monster = MonsterManager.GetInstance ().CreateMonster (m_CurMonsterType,this.transform.position,new Vector2(-1.0f,0.0f),FlipY);

		if(monster != null)
		{
			Monster mon = monster.GetComponent<Monster> ();
			if(mon != null)
			{
				mon.ReverseMove = ReverseMove;
			}
		}

	}
}
