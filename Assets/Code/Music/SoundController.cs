using UnityEngine;
using System.Collections;

public enum SoundType
{
	Sound_Fire = 0,
	Sound_Fire2,
	Sound_Fire3,
	Sound_Fire2_Bomb,
	Sound_EnemyFire_1,
	Sound_KillMonster,
	Sound_SelectButton,
	Sound_HeroDie,
	Sound_PowerFull,
	Sound_EnemyBomb,
	Sound_BigBomb,
	Sound_BossHit,
	Sound_ShipRush,
	Sound_ShipAbsorb,
	Sound_ShipReduce,
}

public class SoundController : MonoBehaviour {

	private AudioSource m_AudioSource;
	      
	private AudioClip m_CurSoundClip;

	public AudioClip m_FireSound;
	public float m_FireDealyTime = 0.0f;

	public AudioClip m_Fire2Sound;
	public float m_Fire2DealyTime = 0.0f;

	public AudioClip m_Fire3Sound;
	public float m_Fire3DealyTime = 0.0f;

	public AudioClip m_Fire2BombSound;
	public float m_Fire2BombDealyTime = 0.0f;

	public AudioClip m_EnemyFire1Sound;
	public float m_EnemyFire1DealyTime = 0.0f;

	public AudioClip m_KillMonsterSound;
	public float m_KillMonsterDealyTime = 0.0f;

	public AudioClip m_SelectButtonSound;
	public float m_SelectButtonDealyTime = 0.0f;

	public AudioClip m_HeroDieSound;
	public float m_HeroDieDealyTime = 0.0f;

	public AudioClip m_PowerfullSound;
	public float m_PowerfullTime = 0.0f;

	public AudioClip m_EnemyBombSound;
	public float m_EnemyBombTime = 0.0f;

	public AudioClip m_BigBombSound;
	public float m_BigBombTime = 0.0f;

	public AudioClip m_BossHitSound;
	public float m_BossHitTime = 0.0f;

	public AudioClip m_ShipRushSound;
	public float m_ShipRushTime = 0.0f;

	public AudioClip m_ShipAbsorbSound;
	public float m_ShipAbsortTime = 0.0f;

	public AudioClip m_ShipReduceSound;
	public float m_ShipReduceTime = 0.0f;

	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject);
		m_AudioSource = this.gameObject.AddComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		m_AudioSource = this.gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void PlaySound(SoundType type)
	{
		float delaytime = 0.0f;
		switch(type)
		{
			case SoundType.Sound_Fire:
			{
				delaytime = m_FireDealyTime;
			}
				break;
			case SoundType.Sound_Fire2:
			{
				delaytime = m_Fire2DealyTime;
			}
				break;
			case SoundType.Sound_Fire3:
			{
				delaytime = m_Fire3DealyTime;
			}
				break;
			case SoundType.Sound_Fire2_Bomb:
			{
				delaytime = m_Fire2BombDealyTime;
			}
				break;
			case SoundType.Sound_EnemyFire_1:
			{
				delaytime = m_EnemyFire1DealyTime;
			}
				break;
			case SoundType.Sound_KillMonster:
			{
				delaytime = m_KillMonsterDealyTime;
			}
				break;
			case SoundType.Sound_SelectButton:
			{
				delaytime = m_SelectButtonDealyTime;
			}
				break;
			case SoundType.Sound_HeroDie:
			{
				delaytime = m_HeroDieDealyTime;
			}
				break;
			case SoundType.Sound_PowerFull:
			{
				delaytime = m_PowerfullTime;
			}
				break;
			case SoundType.Sound_EnemyBomb:
			{
				delaytime = m_EnemyBombTime;
			}
				break;
			case SoundType.Sound_BigBomb:
			{
				delaytime = m_BigBombTime;
			}
				break;
			case SoundType.Sound_BossHit:
			{
				delaytime = m_BossHitTime;
			}
				break;
			case SoundType.Sound_ShipRush:
			{
				delaytime = m_ShipRushTime;
			}
				break;
			case SoundType.Sound_ShipAbsorb:
			{
				delaytime = m_ShipAbsortTime;
			}
				break;
			case SoundType.Sound_ShipReduce:
			{
				delaytime = m_ShipReduceTime;
			}
				break;
		}

		if(delaytime == 0.0f)
		{
			PlaySoundDirect(type);
		}
		else
		{
			StartCoroutine(waitPlaySound(type, delaytime));
		}	
	}

	IEnumerator waitPlaySound(SoundType type, float delay)
	{
		yield return new WaitForSeconds(delay);
		PlaySoundDirect(type);
	}

	public void PlaySoundDirect(SoundType type)
	{
		switch(type)
		{
		case SoundType.Sound_Fire:
				{
					if(m_FireSound != null)
					{
					m_AudioSource.PlayOneShot(m_FireSound,MusicManager.GetInstance().m_SoundVolum);
							//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
					}
				}
				break;
		case SoundType.Sound_Fire2:
		{
			if(m_Fire2Sound != null)
			{
				m_AudioSource.PlayOneShot(m_Fire2Sound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		case SoundType.Sound_Fire3:
		{
			if(m_Fire3Sound != null)
			{
				m_AudioSource.PlayOneShot(m_Fire3Sound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		case SoundType.Sound_Fire2_Bomb:
		{
			if(m_Fire2BombSound != null)
			{
				m_AudioSource.PlayOneShot(m_Fire3Sound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		case SoundType.Sound_EnemyFire_1:
		{
			if(m_EnemyFire1Sound != null)
			{
				m_AudioSource.PlayOneShot(m_EnemyFire1Sound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_AttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		case SoundType.Sound_KillMonster:
				{
					if(m_KillMonsterSound != null)
					{
				m_AudioSource.PlayOneShot(m_KillMonsterSound,MusicManager.GetInstance().m_SoundVolum);
							//AudioSource.PlayClipAtPoint(m_BeAttackSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
					}
				}
				break;
		case SoundType.Sound_HeroDie:
				{
					if(m_HeroDieSound != null)
					{
				m_AudioSource.PlayOneShot(m_HeroDieSound,MusicManager.GetInstance().m_SoundVolum);
					}
				}
				break;
		case SoundType.Sound_SelectButton:
				{
					if(m_SelectButtonSound != null)
					{
				m_AudioSource.PlayOneShot(m_SelectButtonSound,MusicManager.GetInstance().m_SoundVolum);
							//AudioSource.PlayClipAtPoint(m_UseSkillSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
					}
				}
				break;
		case SoundType.Sound_PowerFull:
			{
				if(m_PowerfullSound != null)
				{
					m_AudioSource.PlayOneShot(m_PowerfullSound,MusicManager.GetInstance().m_SoundVolum);
					//AudioSource.PlayClipAtPoint(m_UseSkillSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
				}
			}
				break;
		case SoundType.Sound_EnemyBomb:
		{
			if(m_EnemyBombSound != null)
			{
				m_AudioSource.PlayOneShot(m_EnemyBombSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_UseSkillSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		case SoundType.Sound_BigBomb:
			{
				if(m_BigBombSound != null)
				{
					m_AudioSource.PlayOneShot(m_BigBombSound,MusicManager.GetInstance().m_SoundVolum);
					//AudioSource.PlayClipAtPoint(m_UseSkillSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
				}
			}
			break;
		case SoundType.Sound_BossHit:
		{
			if(m_BossHitSound != null)
			{
				m_AudioSource.PlayOneShot(m_BossHitSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_UseSkillSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		case SoundType.Sound_ShipRush:
		{
			if(m_ShipRushSound != null)
			{
				m_AudioSource.PlayOneShot(m_ShipRushSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_UseSkillSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		case SoundType.Sound_ShipAbsorb:
		{
			if(m_ShipAbsorbSound != null)
			{
				m_AudioSource.PlayOneShot(m_ShipAbsorbSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_UseSkillSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		case SoundType.Sound_ShipReduce:
		{
			if(m_ShipReduceSound != null)
			{
				m_AudioSource.PlayOneShot(m_ShipReduceSound,MusicManager.GetInstance().m_SoundVolum);
				//AudioSource.PlayClipAtPoint(m_UseSkillSound,this.gameObject.transform.position,MusicManager.GetInstance().m_SoundVolum);
			}
		}
			break;
		}
	}
}
