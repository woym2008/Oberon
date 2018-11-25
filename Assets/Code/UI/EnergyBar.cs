using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour {

	public Image BlueEnergy = null;
	public Image GreenEnergy = null;
	public Image YellowEnergy = null;

	public GameObject m_HoldObj = null;
	public GameObject m_ReleaseObj = null;

	Animator UIAnimator = null;

	EnemyBulletType CurBulletType = EnemyBulletType.EnemyBullet_Blue;

	float fillamount = 0.0f;

	// Use this for initialization
	void Start () {
		UIAnimator = this.gameObject.GetComponent<Animator> ();

		ClearEnergy ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void ClearEnergy()
	{
		/*
		BlueEnergy.fillAmount = 0.0f;
		GreenEnergy.fillAmount = 0.0f;
		YellowEnergy.fillAmount = 0.0f;

		BlueEnergy.gameObject.SetActive (false);
		GreenEnergy.gameObject.SetActive (false);
		YellowEnergy.gameObject.SetActive (false);
		*/
		BlueEnergy.fillAmount = 0.0f;
		GreenEnergy.fillAmount = 0.0f;
		YellowEnergy.fillAmount = 0.0f;

		BlueEnergy.gameObject.SetActive(true);
		
		GreenEnergy.gameObject.SetActive(false);
		
		YellowEnergy.gameObject.SetActive(false);
		
		if(UIAnimator != null)
		{
			UIAnimator.SetBool("blue",true);
			
			UIAnimator.SetBool("Blink",false);
		}

		if(m_HoldObj != null)
		{
			m_HoldObj.SetActive(false);
		}
		
		if(m_ReleaseObj != null)
		{
			m_ReleaseObj.SetActive(false);
		}
	}

	public void SetEnergy(float newamount)
	{
		BlueEnergy.fillAmount = newamount;
		/*
		switch(CurBulletType)
		{
		case EnemyBulletType.EnemyBullet_Blue:
		{
			BlueEnergy.fillAmount = newamount;
		}
			break;
		case EnemyBulletType.EnemyBullet_Green:
		{
			GreenEnergy.fillAmount = newamount;
		}
			break;
		case EnemyBulletType.EnemyBullet_Yellow:
		{
			YellowEnergy.fillAmount = newamount;
		}
			break;
		}
		*/
	}

	public void HideReleaseF()
	{
		if(m_HoldObj != null)
		{
			m_HoldObj.SetActive(false);
		}
		
		if(m_ReleaseObj != null)
		{
			m_ReleaseObj.SetActive(false);
		}
	}

	public void ShowReleaseF()
	{
		if(m_HoldObj != null && m_HoldObj.activeSelf == true)
		{
			m_HoldObj.SetActive(false);
		}

		if(m_ReleaseObj != null && m_ReleaseObj.activeSelf == false)
		{
			m_ReleaseObj.SetActive(true);
		}
	}

	public void EnergyEnough()
	{
		//BlueEnergy.fillAmount = 1.0f;

		if(UIAnimator != null)
		{	
			UIAnimator.SetBool("Blink",true);
		}

		if(m_HoldObj != null)
		{
			m_HoldObj.SetActive(true);
		}
		/*
		switch(CurBulletType)
		{
		case EnemyBulletType.EnemyBullet_Blue:
		{
			BlueEnergy.fillAmount = 1.0f;
		}
			break;
		case EnemyBulletType.EnemyBullet_Green:
		{
			GreenEnergy.fillAmount = 1.0f;
		}
			break;
		case EnemyBulletType.EnemyBullet_Yellow:
		{
			YellowEnergy.fillAmount = 1.0f;
		}
			break;
		}
		*/
	}

	public void EnergyNotFull()
	{
		if(UIAnimator != null)
		{	
			UIAnimator.SetBool("Blink",false);
		}

		if(m_HoldObj != null)
		{
			m_HoldObj.SetActive(false);
		}

		if(m_ReleaseObj != null)
		{
			m_ReleaseObj.SetActive(false);
		}
	}

	//public void EnergyUsed()
	//{}

	public void ChangeBulletType(EnemyBulletType type)
	{
		if (BlueEnergy == null) 
		{
			return;
		}
		if (GreenEnergy == null) 
		{
			return;
		}
		if (YellowEnergy == null) 
		{
			return;
		}

		CurBulletType = type;

		switch(type)
		{
			case EnemyBulletType.EnemyBullet_Blue:
			{
				BlueEnergy.gameObject.SetActive(true);

				GreenEnergy.gameObject.SetActive(false);

				YellowEnergy.gameObject.SetActive(false);

				if(UIAnimator != null)
				{
						UIAnimator.SetBool("blue",true);

						UIAnimator.SetBool("green",false);

						UIAnimator.SetBool("yellow",false);
				}
			}
			break;
		case EnemyBulletType.EnemyBullet_Green:
			{
				GreenEnergy.gameObject.SetActive(true);

				BlueEnergy.gameObject.SetActive(false);
					
				YellowEnergy.gameObject.SetActive(false);
				
				if(UIAnimator != null)
				{
					UIAnimator.SetBool("green",true);
					
					UIAnimator.SetBool("blue",false);
					
					UIAnimator.SetBool("yellow",false);
				}
			}
			break;
		case EnemyBulletType.EnemyBullet_Yellow:
			{
				YellowEnergy.gameObject.SetActive(true);

				GreenEnergy.gameObject.SetActive(false);
					
				BlueEnergy.gameObject.SetActive(false);
				
				if(UIAnimator != null)
				{
					UIAnimator.SetBool("yellow",true);
					
					UIAnimator.SetBool("green",false);
					
					UIAnimator.SetBool("blue",false);
				}
			}
			break;
		}
	}
}
