using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class LobbyManager : MonoBehaviour {
    public GameObject loadbtn;
    private SoundSingleTon sound;
    private XMLManager xml;
    public Text text;
    List<Dictionary<string, object>> data;
    private void Start()
    {
        sound = SoundSingleTon.instance;
        sound.lobby.Play();
        xml = XMLManager.ins;
        data = CSVReader.Read("makemoneydatasheet3");
        text.text = PlayerPrefs.GetString("SaveLastTime", "Default");
        //PlayerPrefs.SetString("inven", "false");

        //PlayerPrefs.SetString("stock", "false");
        

        if (PlayerPrefs.GetString("load", "false") == "false")
        {
            loadbtn.SetActive(false);
        }
        else
        {
            loadbtn.SetActive(true);                        
        }
       
    }
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
    private void CheckDayOver()
    {
        int[] auctionNum = new int[xml.itemDB.auctionList.Count];//5
        float random = UnityEngine.Random.Range(0f, 100f);
        int dailyitem = 0 ;
        for (int i = 0; i < xml.itemDB.dailyList.Count; i++)
        {
            if (random <= float.Parse(data[i]["droppercent"].ToString()))
            {
                dailyitem = i;
            }
        }

        int daycheck = (int)(DateTime.Parse(PlayerPrefs.GetString("SaveLastTime", DateTime.Now.ToString())) - DateTime.Now.Date).TotalSeconds;
        if (daycheck < 0)//하루가 지남
        {
            for (int i = 0; i < xml.itemDB.auctionList.Count; i++)
            {
                auctionNum[i] = UnityEngine.Random.Range(0, 45);
            }
            int saveToZero = (-daycheck) / 10;
            int zeroToNow = ((int)(DateTime.Now - DateTime.Now.Date).TotalSeconds) / 10;

            xml.itemDB.DayOver_Update(saveToZero, zeroToNow, auctionNum, dailyitem);

        }
        else if (daycheck >= 0)//하루가 지나지 않음
        {
            
            int save = ((int)(DateTime.Parse(PlayerPrefs.GetString("SaveLastTime", DateTime.Now.ToString())) - DateTime.Now.Date).TotalSeconds) / 10;
            int now = ((int)(DateTime.Now - DateTime.Now.Date).TotalSeconds) / 10;
            xml.itemDB.Today_Update(save,now);
        }
    }
   




    public void LoadButton()
    {
        
        AutoFade.LoadLevel("MainScene", 1, 1, Color.black);
    }
    public void ResetButton()
    {
        AutoFade.LoadLevel("ChooseJobScene", 1, 1, Color.black);
    }
   
}
