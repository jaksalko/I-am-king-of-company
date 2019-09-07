using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using TMPro;


public class TestScript : MonoBehaviour
{
    int count = 0;
    int time = 0;
    bool reward;
    private void Start()
    {
        Debug.Log("START");
        
        StartCoroutine(Rountine());
    }
    IEnumerator Rountine()
    {
        while (true)
        {
            Debug.Log(reward);
            if (reward == false && time ==2)
            {
                Debug.Log(reward);
                reward = true;
                count++;
                Debug.Log(count + "  in Routine");
            }
            time++;
            yield return new WaitForSeconds(10f);
        }
    }
    private void Update()
    {
        if (time == 2 && !reward)
        {
            Debug.Log("in update" + count);
            count++;
            reward = true;
        }
    }
    private void OnApplicationPause(bool pause)
    {
        Debug.Log("AppPause");
        if (pause)
        {
            if (reward == false)
            {
                reward = true;
                
                Debug.Log(count + "  in Pause");
                count++;
            }
            PlayerPrefs.SetString("testtime", DateTime.Now.ToString());
            Debug.Log((DateTime.Parse(PlayerPrefs.GetString("testtime")) - DateTime.Now.Date).TotalSeconds/60);
            
        }
        else
        {
            if (reward == false)
            {
                reward = true;
                Debug.Log(reward + " in Game");
                count++;
                Debug.Log(count + "  in Game");
            }
        }
    }


}