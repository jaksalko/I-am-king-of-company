using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPrefab : MonoBehaviour {
    Single single;
    private void Start()
    {
        single = Single.instance;
    }
    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

            if (Input.GetKey(KeyCode.Escape))
            {
                single.quit_bool = false;
                Destroy(this.transform.gameObject);
            }
        }
    }
    // Use this for initialization
    public void Yes()
    {
        Debug.Log("quit");
        single.quit_bool = false;
        single.Quit();
        Destroy(this.transform.gameObject);

    }
    public void No()
    {
        single.quit_bool = false;
        Destroy(transform.gameObject);
    }
}
