using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour {
    public Transform[] cloud;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < cloud.Length; i++)
        {
            cloud[i].Translate(-0.01f, 0, 0);
            if (cloud[i].localPosition.x < -1800f)
            {
                cloud[i].localPosition = new Vector3(6300f, cloud[i].localPosition.y, cloud[i].localPosition.z);
            }
        }
        
	}
}
