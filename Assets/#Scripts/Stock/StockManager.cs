using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
public class StockManager : MonoBehaviour {

    public GameObject[] screen;  //  0:news 1:trade 2:mystock
    public GameObject tut;
    public Text stockName;
    public Text selectP;
    public Text myProperty;
    public Text myStock;
    public Text sum;
    public Text totalPrice;
    public GameObject tradePopup;
    public GameObject[] stockInfo;//0name 1price 2variation
    
    
    public int quan;
    public int pp;
    public int stocknum;

    public int activated = 0;
    public GameObject totalInfo;//Total , TotalStock , NowMyStock , TPLP , TPLR
    public GameObject[] mStock;//0~n
    Text totalProperty;
    Text totalStock;
    Text nowStock;
    Text TPLP;
    Text TPLR;

    public XMLManager xMLManager;
    public SoundSingleTon sound;
    private void Start()
    {

        sound = SoundSingleTon.instance;
        sound.bgm.Stop();
        if (PlayerPrefs.GetString("stock", "false") == "false")
        {
            tut.SetActive(true);
        }
        //wallet = GameManager.Instance.wallet;
        //stocklist = GameManager.Instance.stocklist;
        xMLManager = XMLManager.ins;

    }
    private void Update()
    {
        UpdateTradeUI();
        UpdateMyStockUI();
       
       
        
    }
    #region ChangeScreen
    public void ClickNews()
    { sound.btnclick.Play(); screen[0].SetActive(true); screen[1].SetActive(false); screen[2].SetActive(false); }
    public void ClickTrade()
    { sound.btnclick.Play(); screen[1].SetActive(true); screen[0].SetActive(false); screen[2].SetActive(false); }
    public void ClickMyStock()
    {
        sound.btnclick.Play(); screen[2].SetActive(true); screen[1].SetActive(false); screen[0].SetActive(false);
        for (int i = 0; i < 15; i++)
        {
            mStock[i].SetActive(false);
        }
        for (int i = 0; i < xMLManager.itemDB.wallet.stockNumber.Count; i++)
        {
            mStock[i].SetActive(true);
        }
    }
    public void goMain()
    { sound.btnclick.Play(); SceneManager.LoadScene("MainScene"); Debug.Log("click"); screen[0].SetActive(true); screen[1].SetActive(false); screen[2].SetActive(false); }
    #endregion
    #region UpdateUI
    public void UpdateTradeUI()
    {   
            for (int i = 0; i < 15; i++)
            {
                GameObject price = stockInfo[i].transform.GetChild(1).gameObject;
                GameObject name = stockInfo[i].transform.GetChild(0).gameObject;
                GameObject percent = stockInfo[i].transform.GetChild(2).gameObject;
                price.GetComponent<Text>().text = xMLManager.itemDB.stockList[i].price.ToString();
                name.GetComponent<Text>().text = xMLManager.itemDB.stockList[i].stockName.ToString();
                percent.GetComponent<Text>().text = xMLManager.itemDB.stockList[i].percent.ToString("N2") + "%";
                if (xMLManager.itemDB.stockList[i].percent < 0)
                {
                    percent.GetComponent<Text>().color = Color.blue;
                }
                else if (xMLManager.itemDB.stockList[i].percent > 0)
                {
                    percent.GetComponent<Text>().color = Color.red;
                }
                else
                {
                    percent.GetComponent<Text>().color = Color.gray;
                }


            }       
    }
    public void UpdateMyStockUI()
    {
        totalProperty = totalInfo.transform.GetChild(0).gameObject.GetComponent<Text>();
        totalStock = totalInfo.transform.GetChild(1).gameObject.GetComponent<Text>();
        nowStock = totalInfo.transform.GetChild(2).gameObject.GetComponent<Text>();
        TPLP = totalInfo.transform.GetChild(3).gameObject.GetComponent<Text>();
        TPLR = totalInfo.transform.GetChild(4).gameObject.GetComponent<Text>();


        totalProperty.text = "나의 총 자산 : " + xMLManager.itemDB.wallet.GetTotalProperty().ToString();
        totalStock.text = "총 매수 : " + xMLManager.itemDB.TotalUserBoughtStock().ToString();//샀을때 = buyprice
        nowStock.text = "총 평가 : " + xMLManager.itemDB.ComputeStockProperty().ToString();//현재 = price
        TPLP.text = "손익금 : " + xMLManager.itemDB.TotalPLP().ToString();
        TPLR.text = "손익룰 : " + xMLManager.itemDB.TotalPLR().ToString("N2") + "%";
        if (xMLManager.itemDB.TotalPLP() < 0)
        {
            TPLP.color = Color.blue;
            TPLR.color = Color.blue;
        }
        else if (xMLManager.itemDB.TotalPLP() > 0)
        {
            TPLP.color = Color.red;
            TPLR.color = Color.red;
        }
        else
        {
            TPLP.color = Color.gray;
            TPLR.color = Color.gray;
        }

        for (int i = 0 ; i < xMLManager.itemDB.wallet.stockNumber.Count ; i++)
        {
            
            Text name = mStock[i].transform.GetChild(0).gameObject.GetComponent<Text>();
            Text quan = mStock[i].transform.GetChild(1).gameObject.GetComponent<Text>();
            Text PLP = mStock[i].transform.GetChild(2).gameObject.GetComponent<Text>();
            Text PLR = mStock[i].transform.GetChild(3).gameObject.GetComponent<Text>();
            name.text = xMLManager.itemDB.stockList[xMLManager.itemDB.wallet.stockNumber[i]].stockName;
            quan.text = xMLManager.itemDB.stockList[xMLManager.itemDB.wallet.stockNumber[i]].quan.ToString();
            PLP.text = xMLManager.itemDB.stockList[xMLManager.itemDB.wallet.stockNumber[i]].ComputePLP().ToString();
            PLR.text = xMLManager.itemDB.stockList[xMLManager.itemDB.wallet.stockNumber[i]].ComputePLR().ToString("N2") +"%";
            if (xMLManager.itemDB.stockList[xMLManager.itemDB.wallet.stockNumber[i]].ComputePLP() < 0)
            {
                PLP.color = Color.blue;
                PLR.color = Color.blue;
            }
            else if (xMLManager.itemDB.stockList[xMLManager.itemDB.wallet.stockNumber[i]].ComputePLP() > 0)
            {
                PLP.color = Color.red;
                PLR.color = Color.red;
            }
            else
            {
                PLP.color = Color.gray;
                PLR.color = Color.gray;
            }

        }
        
    }
    public void UpdateTradePopup()
    {

        pp = xMLManager.itemDB.stockList[stocknum].price;
                selectP.text = "현재가 : " + pp.ToString();
                totalPrice.text = "합산 : " + (pp * quan);
            
       
     
    }
    
    #endregion
    public void ClickStock(Button button)
    {
        sound.btnclick.Play();
        string select = button.transform.GetChild(0).GetComponent<Text>().text;
        for (int i = 0; i < 15; i++)
        {
            if (select == xMLManager.itemDB.stockList[i].stockName)
            {
                stockName.text = select;
                selectP.text = "현재가 : " + xMLManager.itemDB.stockList[i].price;
                myProperty.text = "자산 : " + xMLManager.itemDB.wallet.cash;
                sum.text = "수량 : 0개";
                myStock.text = "보유 : " + xMLManager.itemDB.stockList[i].quan + " 개";
                Debug.Log(i);
                stocknum = i;
                pp = xMLManager.itemDB.stockList[i].price;
            }
        }
        tradePopup.SetActive(true);
        /* Debug.Log("select:" + select);
         GameObject selectPrice = button.transform.GetChild(1).gameObject;
         pp = Convert.ToInt32(selectPrice.GetComponent<Text>().text);
         string str = EventSystem.current.currentSelectedGameObject.name;
         str = str.Replace("Stock", "");

         Debug.Log(str);
         stocknum = Convert.ToInt32(str);
         stocknum = stocknum - 1;
         Debug.Log("stocknum : " + stocknum);
         Debug.Log(pp);*/





    }

}
