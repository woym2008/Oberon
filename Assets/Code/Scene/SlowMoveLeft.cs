using UnityEngine;
using System.Collections;

public class SlowMoveLeft : MonoBehaviour {

	public float speed = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate(Vector3.left * Time.deltaTime * speed);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other != null && other.gameObject != null)
		{
			TriangleShip ship = other.gameObject.GetComponent<TriangleShip>();
			
			if(ship != null)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
