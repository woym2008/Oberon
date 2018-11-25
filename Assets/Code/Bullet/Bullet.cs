using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	
	// Use this for initialization
	public Vector2 V
	{
		set
		{
			value.Normalize();
			mV = new Vector2(value.x,value.y);
		}
		//
		get
		{
			return mV;
		}
	}
	
	public void SetTarget(Vector2 target)
	{
		mV = target - new Vector2(transform.position.x,transform.position.y);
		mV.Normalize();
	}
	
	public float Speed;
	
	protected Vector2 mV;
	
	public bool CanFly = false;
	
	protected float safeDeleteTime = 8;
	
	public void Fly(){ CanFly = true;}
	public void Stop(){ CanFly = false;}
	
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!CanFly) return ;
		transform.position = transform.position + (new Vector3(mV.x,mV.y,0) * Speed * Time.deltaTime);
		//
		safeDeleteTime -= Time.deltaTime ;
		if(safeDeleteTime <= 0)
		{
			Destroy(this.gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(CanFly == false) return;
		
		if(other != null && other.gameObject != null && 
		   other.gameObject.tag == "Monster")
		{
			other.gameObject.SendMessage("Die",1.0f);
			
			Destroy(this.gameObject);
		}
	}

	public void DestroySelf()
	{
		Destroy(this.gameObject);
	}
}
