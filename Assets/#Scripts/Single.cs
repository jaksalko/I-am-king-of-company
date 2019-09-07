using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Single : MonoBehaviour {//GameManager singleton pattern
    public static Single instance = null;
    public GameObject prefab;
    public GameObject quit;
    XMLManager xMLManager;
    public bool quit_bool;
    public bool reward;
    private void Awake()
    {
       
        
      
        Debug.Log("Single Class Awake...");//Set instance
        if (instance == null)
        {
            Debug.Log("Single instance is null");
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Single instance is not Single.. Destroy gameobject!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);//Dont destroy this singleton gameobject :(


        
        xMLManager = XMLManager.ins;//get Xml singleton instance
        //Debug.Log(PlayerPrefs.GetString("load", "false"));
        if (PlayerPrefs.GetString("load", "false") == "true")
        {
            xMLManager.LoadItems();//load items once onload

           
        }
        StartCoroutine(Timer10());//start routine in game
        StartCoroutine(Timer60());
    }
    private void Update()
    {
        try
        {
            xMLManager.SaveItems();
            if (Application.platform == RuntimePlatform.Android)
            {

                if (Input.GetKey(KeyCode.Escape))
                {
                    quit_bool = true;
                    if (SceneManager.GetActiveScene().name == "MainScene" || SceneManager.GetActiveScene().name == "Lobby")
                    {
                       
                        if (quit_bool == true)
                        {
                            quit_bool = false;
                            GameObject quitPop = Instantiate(quit, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                            quitPop.transform.localScale = new Vector3(1, 1, 1);
                            
                        }
                     
                        
                    }
                    else if (SceneManager.GetActiveScene().name == "ChooseJobScene") { SceneManager.LoadScene("Lobby"); }
                    else { SceneManager.LoadScene("MainScene"); }

                    quit_bool = false;
                }
                

            }
        } catch (Exception e) { SetAlert(e.ToString(), Color.red); }
       
    }

    IEnumerator Timer10()//stock property update
    {
        while (true)
        {
            yield return new WaitUntil(() => PlayerPrefs.GetString("load", "false") != "false");
           
            for (int i = 0; i < xMLManager.itemDB.stockList.Count; i++)
            {
                xMLManager.itemDB.stockList[i].ChangePrice(UnityEngine.Random.Range(-1, 2));
            }
            xMLManager.itemDB.ComputeStockProperty();
         
            
            yield return new WaitForSeconds(10);
        }
    }
    IEnumerator Timer60()//auction property update
    {
        while (true)
        {
            yield return new WaitUntil(() => PlayerPrefs.GetString("load", "false") != "false");
            Debug.Log("START 60");
            if (GapMin() != 0)
            {
                for (int i = 0; i < xMLManager.itemDB.auctionList.Count; i++)
                {

                    if (UnityEngine.Random.Range(0, 100) < xMLManager.itemDB.auctionList[i].frequency)
                    {
                        xMLManager.itemDB.Set_price(i, GapMin());
                    }


                }
            }
            else if (GapMin() == 0 && !reward) {xMLManager.itemDB.InGame_DayOver();reward = true; }
         
            
           
            yield return new WaitForSeconds(60);
        }
    }
     
    public int GapMin()//오늘 하루 지난 시간 (단위 : 분)
    {
        int gapmin = (int)(DateTime.Now - DateTime.Now.Date).TotalMinutes;
        Debug.Log("gap:" + gapmin);
        return gapmin;
    }
    public void SetAlert(string text,Color color)
    {
        GameObject alert = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        alert.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        alert.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().color = color;
    }
    public void Quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    private void OnApplicationQuit()
    {
        Debug.Log("Save timequit");
        xMLManager.SaveItems();
        CheckReward();
        PlayerPrefs.Save();
        PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)//application off
        {
            Debug.Log("PAUSE");
            xMLManager.SaveItems();
            CheckReward();
            PlayerPrefs.Save();
            PlayerPrefs.SetString("SaveLastTime", System.DateTime.Now.ToString());
        }
        else//application on
        {
            Debug.Log("!!!!PAUSE");
            int[] auctionNum = new int[xMLManager.itemDB.auctionList.Count];//5
            int dailyitem = UnityEngine.Random.Range(0, 45);

            int daycheck = (int)(DateTime.Parse(PlayerPrefs.GetString("SaveLastTime", DateTime.Now.ToString())) - DateTime.Now.Date).TotalSeconds;
            if (daycheck < 0 && reward == false)//하루가 지남
            {
                reward = true;
                for (int i = 0; i < xMLManager.itemDB.auctionList.Count; i++)
                {
                    auctionNum[i] = UnityEngine.Random.Range(0, 45);
                }
                int saveToZero = (-daycheck) / 10;
                int zeroToNow = ((int)(DateTime.Now - DateTime.Now.Date).TotalSeconds) / 10;
                Debug.Log("In AppFalse saveTozero :" + saveToZero + "zeroToNow : "+ zeroToNow);
                xMLManager.itemDB.DayOver_Update(saveToZero, zeroToNow, auctionNum, dailyitem);

            }
            else if (daycheck >= 0)//하루가 지나지 않음
            {
                Debug.Log("Today");
                int save = ((int)(DateTime.Parse(PlayerPrefs.GetString("SaveLastTime", DateTime.Now.ToString())) - DateTime.Now.Date).TotalSeconds) / 10;
                int now = ((int)(DateTime.Now - DateTime.Now.Date).TotalSeconds) / 10;
                xMLManager.itemDB.Today_Update(save, now);
            }
        }
    }
    private void CheckReward()
    {
        
        int daycheck = (int)(DateTime.Parse(PlayerPrefs.GetString("SaveLastTime", DateTime.Now.ToString())) - DateTime.Now.Date).TotalSeconds;
        if (daycheck < 0 && reward == false)
        {
            Debug.Log("CALL REWARD");
            xMLManager.itemDB.InGame_DayOver();
            reward = true;
        }
    }
}
