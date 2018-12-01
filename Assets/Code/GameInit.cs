using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameManager.getInstance().StartGame();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
