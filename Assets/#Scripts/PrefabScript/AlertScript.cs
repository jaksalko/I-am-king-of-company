using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertScript : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
       
        DontDestroyOnLoad(gameObject);
        Destroy(gameObject, 5);
	}
	

}
