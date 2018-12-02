using UnityEngine;
using System.Collections;

public abstract class BossBase : MonoBehaviour
{
	private void Awake()
	{
        OnBossCreate();
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}

    virtual public void OnBossCreate()
    {
        ;
    }

    virtual public void OnBossShow()
    {
        ;
    }

    virtual public void OnBossStartFight()
    {
        ;
    }

    virtual public void OnBossDie()
    {
        ;
    }
}
