using UnityEngine;
using System.Collections;

public enum MonsterType
{
	MonsterType_null = -1,
	MonsterType_1 = 0,
	MonsterType_2,
	MonsterType_3,
	MonsterType_4,

	MonsterType_5,
	MonsterType_6,
	MonsterType_7,
	MonsterType_8,

	MonsterType_9,
	MonsterType_10,
	MonsterType_11,
}


public class MonsterManager : MonoBehaviour {

	// Use this for initialization
	private static MonsterManager mSingleton;
	public static MonsterManager GetInstance(){return mSingleton;}
	
	//  刷怪的时间间隔
	public const  float fMinSpawnGap = 1f;
	public const  float fStandardGap = 3.2f;
	public const  float fNewRuleGap = 25f;
	public const float Time_upbound = 50;	//40s is upbound
	public float fSpawnGap = 3.2f;
	
	public Transform[] 			FourDiagonalsNode;
	public BoxCollider2D[]		SpawnAreas;
	public Rect					BattleField;

	public GameObject[]		MonsterPrefabs;

	public float TimeCounter = 0;
	public float TotalGameTime = 0;

	public bool SpawnEnable = true;

	void Awake() 
	{
		mSingleton = this;
		//
		//MonsterPrefabs[(int)MonsterType.SuicideMonster] = GameObject.Instantiate(Resources.Load("Monster.prefab")) as GameObject;
		//MonsterPrefabs[(int)MonsterType.ShootOnceMonster] = GameObject.Instantiate(Resources.Load("Monster.prefab"))as GameObject;
		//MonsterPrefabs[(int)MonsterType.HitAndRunMonster] = GameObject.Instantiate(Resources.Load("Monster.prefab"))as GameObject;
		//MonsterPrefabs[(int)MonsterType.BigMonster] = GameObject.Instantiate(Resources.Load("Monster.prefab"))as GameObject;
		//
		foreach(GameObject m in MonsterPrefabs)
		{
			m.SetActive(false);
		}

		//
		//RuleMgr = new SpawnRuleMgr();
		//
		BattleField = new Rect();
		BattleField.center = Vector2.zero;
		BattleField.size = new Vector2(4.6f,4.6f);
	}

	void Start () 
	{
		Reset();


		//////
		/// 
		SpawnEnable = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(SpawnEnable == false) return;

		TotalGameTime += Time.time;

		/*
		//如果是常态模式下，计时
		if(RuleMgr.CurRule == NormalSpawnRule.GetInstance())
		{
			TimeCounter += Time.deltaTime;
			
			if(TimeCounter>fNewRuleGap)
			{
				//2种出生怪物的规则
				switch(Mathf.Min(Random.Range(0,2),1))
				{
				case 0: 
					RuleMgr.SetCurRule(DiagonalSpawnRule.GetInstance());
					break;
				case 1: 
					RuleMgr.SetCurRule(SinSpawnRule.GetInstance());
					break;
				}
				//
				TimeCounter = 0;
			}
		}

		RuleMgr.Update();
		*/
	}

	void Reset()
	{
		TotalGameTime = 0;
		TimeCounter = 0;
		SpawnEnable = true;
		fSpawnGap = fStandardGap;
		//RuleMgr.SetCurRule(NormalSpawnRule.GetInstance());
		
	}

	public Vector2 GetRandomPosInBattleField()
	{
		Vector2 target = Random.insideUnitCircle;
		target.Scale(BattleField.size);
		return target;
	}

	/*
	public void NormalSpawn()
	{
		MonsterType mt = CalcMonsterToSpawn(Time.time);
		//1 随机位置
		int spawnAreaIndex = Random.Range(0,SpawnAreas.Length - 1);
		Rect rect = new Rect();
		rect.size = new Vector2(SpawnAreas[spawnAreaIndex].bounds.size.x,
		                        SpawnAreas[spawnAreaIndex].bounds.size.y);
		rect.center = new Vector2(SpawnAreas[spawnAreaIndex].bounds.center.x,
		                          SpawnAreas[spawnAreaIndex].bounds.center.y);	
		Vector2	BornPos = new Vector2(Random.Range(rect.xMin,rect.xMax),
		                              Random.Range(rect.yMin,rect.yMax));
		//2 初始方向	交给了think组建

		GameObject go = CreateMonster(mt,BornPos,Vector2.zero);
		//3 初始一些怪物的属性
		Monster monster = go.GetComponent<Monster>();

	}

	public void DiagonalSpawn()
	{
		Vector2[] FourDiagonals = new Vector2[FourDiagonalsNode.Length];

		int i = 0;
		foreach(Transform tr in FourDiagonalsNode)
		{
			FourDiagonals[i++] = new Vector2(tr.position.x,tr.position.y);
		}


		Vector2 CenterPos = Vector2.zero;
		//
		foreach(Vector2 BornPos in FourDiagonals )
		{
			GameObject go = CreateMonster(MonsterType.SuicideMonster,BornPos,CenterPos);

			//3 初始一些怪物的属性
			Monster monster = go.GetComponent<Monster>();

			Vector2 dir = CenterPos - BornPos;

			dir.Normalize();

			dir = dir * 30;
			
			if(monster != null)
			{
				monster.MoveTo(dir + BornPos,2.2f);
			}
		}
	}
	*/

	/*
	public void SinSpawn(int callCounter,int maxCall)
	{
		Vector2 rangeY_R = Vector2.zero;
		rangeY_R = new Vector2(	SpawnAreas[0].bounds.min.y,
		                        SpawnAreas[0].bounds.max.y);

		Vector2 rangeY_L = Vector2.zero;
		rangeY_L = new Vector2(	SpawnAreas[1].bounds.max.y,
		                        SpawnAreas[1].bounds.min.y);

		//需要在n次内，射满一周期 2pi = maxcall
		float OneCall_PI = 3.14f*2 / maxCall;

		float unit_Y = Mathf.Cos(OneCall_PI * callCounter);

		float nY_R = unit_Y * SpawnAreas[0].bounds.size.y * 0.4f;
		float nY_L = - unit_Y * SpawnAreas[1].bounds.size.y * 0.4f;

		float nX_R = SpawnAreas[0].transform.position.x;
		float nX_L = SpawnAreas[1].transform.position.x;
		
		Vector2 BronL = new Vector2(nX_L,nY_L);
		Vector2 BronR = new Vector2(nX_R,nY_R);

		{	//L
			Vector2 target = BronL + Vector2.right * 30;

			GameObject go = CreateMonster(MonsterType.SuicideMonster,
			                              BronL,
			                              target);

			Monster monster = go.GetComponent<Monster>();
			
			if(monster != null)
			{
				monster.MoveTo(target,3.2f);
			}
		}

		{	//R
			Vector2 target = BronR - Vector2.right * 30;

			GameObject go = CreateMonster(MonsterType.SuicideMonster,
			                              BronR,
			                              target);
			
			Monster monster = go.GetComponent<Monster>();
			
			if(monster != null)
			{
				monster.MoveTo(BronR - Vector2.right * 30,3.2f);
			}
		}
		
	}
	*/



	public GameObject CreateMonster(MonsterType MType,Vector2 Pos,Vector2 lootAt,bool flipY = false)
	{
		//1 new monster
		GameObject monster = null;

		monster = GameObject.Instantiate(MonsterPrefabs[(int)MType]) as GameObject;

		monster.transform.position = Pos;

		//monster.transform.up = new Vector3(lootAt.x,lootAt.y,0) - monster.transform.position;
		if(flipY)
		{
			monster.transform.localScale = new Vector3(monster.transform.localScale.x,-monster.transform.localScale.y,monster.transform.localScale.z);
		}

		monster.SetActive(true);

		return monster;

	}

	public void UpdateSpawnGap(float GameTotalTime)
	{

		fSpawnGap = Mathf.Max((Time_upbound - GameTotalTime)/Time_upbound * fStandardGap,fMinSpawnGap);

	}
	//传入总时间，得到一个怪物的类型
	//怪物的类型的生成几率应该随着总时间的提升而某些升高和降低
	/*
	public MonsterType	CalcMonsterToSpawn(float GameTotalTime)
	{
		// 测试的时候，先都按照平均值
		int mt = Random.Range((int)MonsterType.SmallMonster,
		                                 (int)MonsterType.BigMonster + 1);

		return (MonsterType) Mathf.Min (mt,(int)MonsterType.BigMonster);
	}
	*/

	/*
	/// <summary>
	/// T////////////////////////////////////////////////////////////	/// </summary>
	public SpawnRuleMgr RuleMgr;

	public void ChangeToNormalRule()
	{

		if(RuleMgr.LastRule != NormalSpawnRule.GetInstance())
		{
			RuleMgr.SetRule(NormalSpawnRule.GetInstance());
		}
		else
		{
			RuleMgr.RevertToPreviousState();
		}
	}
	*/
}

#region SpawnRule

public class SpawnRule
{
	public virtual void Enter(){}
	public virtual void Update(){}
	public virtual void Exit(){}		
}
/*
public class NormalSpawnRule : SpawnRule
{
	static NormalSpawnRule instance = null;
	public static NormalSpawnRule GetInstance()
	{
		if(instance == null) instance = new NormalSpawnRule();
		return instance;
	}
	public override void Enter ()
	{

		base.Enter ();

		TimerCounter = 0;
		
	}

	public override void Update ()
	{
		base.Update ();
		//
		TimerCounter -= Time.deltaTime;

		if(TimerCounter <= 0)
		{
//			MonsterManager.GetInstance().UpdateSpawnGap(GameManager.getInstance().GetGameRunTime());

			MonsterManager.GetInstance().NormalSpawn();

			TimerCounter = MonsterManager.GetInstance().fSpawnGap;
		}
	}

	public override void Exit ()
	{
		base.Exit ();

		TimerCounter = 0;
	}
	//
	float TimerCounter;

}
*/

/*
public class DiagonalSpawnRule: SpawnRule
{
	static DiagonalSpawnRule instance = null;
	public static DiagonalSpawnRule GetInstance()
	{
		if(instance == null) instance = new DiagonalSpawnRule();
		return instance;
	}
	public override void Enter ()
	{
		base.Enter ();

		TimerCounter = 0;
	}

	int callCounter = 0;

	public override void Update ()
	{
		base.Update ();


		TimerCounter -= Time.deltaTime;

		if(TimerCounter <= 0)
		{
			TimerCounter = 1.5f;
			//
			MonsterManager.GetInstance().DiagonalSpawn();	
			//
			callCounter++;
			//
			if(callCounter == CallCounterBound)
			{
				MonsterManager.GetInstance().ChangeToNormalRule();
			}
		}
	}

	public override void Exit()
	{
		base.Exit();

		TimerCounter = 0;

		callCounter = 0;
		
	}

	float TimerCounter;

	const int CallCounterBound = 10;
	
}
*/
/// <summary>
/// /// </summary>
///  
/*
public class SinSpawnRule: SpawnRule
{
	static SinSpawnRule instance = null;
	public static SinSpawnRule GetInstance()
	{
		if(instance == null) instance = new SinSpawnRule();
		return instance;
	}
	public override void Enter ()
	{
		base.Enter ();
		
		TimerCounter = 0;
	}
	
	int callCounter = 0;
	
	public override void Update ()
	{
		base.Update ();
		//
		TimerCounter -= Time.deltaTime;
		
		if(TimerCounter <= 0)
		{
			TimerCounter = 1f;
			//
			MonsterManager.GetInstance().SinSpawn(callCounter,CallCounterBound);	
			//
			callCounter++;
			//
			if(callCounter == CallCounterBound)
			{
				MonsterManager.GetInstance().ChangeToNormalRule();
			}
		}
	}
	
	public override void Exit()
	{
		base.Exit();
		
		TimerCounter = 0;
		
		callCounter = 0;
		
	}
	
	float TimerCounter;
	
	const int CallCounterBound = 15;
	
}

	//
public class SpawnRuleMgr
{
	public void SetRule(SpawnRule rule)
	{
		LastRule = CurRule;

		CurRule.Exit();

		CurRule = rule;

		CurRule.Enter();
	}
	public void Update()
	{
		CurRule.Update();
	}
	//
	public void SetCurRule(SpawnRule rule)
	{
		CurRule = rule;
	}
	//
	public void SetLastRule(SpawnRule rule)
	{
		LastRule = rule;
	}

	public void  RevertToPreviousState()
	{
		SetRule(LastRule);
	}

	public bool IsInRule(SpawnRule st)
	{
		if (CurRule.GetType()  == st.GetType())
	    {
	    	return true;
		}
		return false;
	}

	public bool IsInLastRule(SpawnRule st)
	{
		if (LastRule.GetType()  == st.GetType())
		{
			return true;
		}
		return false;
	}

	public SpawnRule	LastRule;
	public SpawnRule	CurRule;
}
*/


#endregion














