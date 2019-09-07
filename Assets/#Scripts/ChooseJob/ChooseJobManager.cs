using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChooseJobManager : MonoBehaviour {
    public Text job;
    public Toggle[] toggles;
    //List<Dictionary<string, object>> data;
    XMLManager xMLManager;
    Single single;
    int level;
    // Use this for initialization
    void Start () {
       // data = CSVReader.Read("makemoneydatasheet3");
        xMLManager = XMLManager.ins;
        single = Single.instance;
       
    }

    public void JobSelect(Toggle toggle)
    {
        switch (toggle.ToString())
        {
            case "Officeman (UnityEngine.UI.Toggle)":
                level = 0;
                job.text = "회사원 으로 게임을 시작!";
                break;

            case "Talent (UnityEngine.UI.Toggle)":
                level = 22;
                job.text = "배우 로 게임을 시작!";
                break;

            case "Athletic (UnityEngine.UI.Toggle)":
                level = 11; 
                job.text = "축구선수 로 게임을 시작!";
                break;

        }

    }

    public void GameStart()
    {
        try
        {
            xMLManager.itemDB.Initialize();
            //xMLManager.SaveItems();
            //PlayerPrefs.DeleteAll();//level exp savelasttime load 
            
            PlayerPrefs.SetInt("level", level);
            PlayerPrefs.SetFloat("exp", 0);
            if (PlayerPrefs.GetString("load", "false") == "false")//처음
            {
                PlayerPrefs.SetString("load", "first");//file 있음
            }
            else
            {
                if (PlayerPrefs.GetString("load", "false") != "true")
                {
                    //stay first... need tutorial
                }
                else//true 처음도 아니고 튜토리얼을 안봐도됨
                {
                    PlayerPrefs.SetString("load", "true");//file 있음
                }
            }
           
            


            AutoFade.LoadLevel("MainScene", 1, 1, Color.black);
        } catch (Exception e) { single.SetAlert(e.ToString(), Color.red); }
       
    }
}
