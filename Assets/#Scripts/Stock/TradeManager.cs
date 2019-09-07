using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEditor;

public class TradeManager : StockManager {
  

    // Use this for initialization
    void Start()
    {
        quan = 0;
        xMLManager = XMLManager.ins;
        sound = SoundSingleTon.instance;
    }
    private void Update()
    {
        if (tradePopup.activeSelf)
        {
            UpdateTradePopup();
        }
    }
    public void NumPAD()
    {
        string str = EventSystem.current.currentSelectedGameObject.name;
        int num = 0;
        switch (str)
        {
            case "0":
                num = 0;
                break;
            case "1":
                num = 1;
                break;
            case "2":
                num = 2;
                break;
            case "3":
                num = 3;
                break;
            case "4":
                num = 4;
                break;
            case "5":
                num = 5;
                break;
            case "6":
                num = 6;
                break;
            case "7":
                num = 7;
                break;
            case "8":
                num = 8;
                break;
            case "9":
                num = 9;
                break;
            case "Back":
                break;
        }
        if (str != "Back")
        {
            if (quan == 0)
            { sound.btnclick.Play(); quan = num; }
            else if (quan > 0 && quan < 10)
            {
                sound.btnclick.Play();
                quan = (quan * 10) + num;
            }
            else if (quan >= 10 && quan < 100)
            {
                sound.btnclick.Play();
                quan = (quan * 10) + num;
            }
            else {
                sound.miss.Play();
                Debug.Log("최대 999개");
            }
        }
        else if (str == "Back")
        {
            sound.btnclick.Play();
            if (quan == 0) { }
            else if (quan > 0 && quan < 10)
            {
                quan = 0;

            }
            else if (quan >= 10 && quan < 100)
            {
                quan = quan / 10;
            }
            else if (quan >= 100)
            {
                quan = quan / 10;
            }
        }
        sum.text = "수량 : " + quan.ToString() + " 개";
        totalPrice.text = "합산 : " + (pp*quan);
    }

    public void SetActivatedMyStock()
    {
        for (int i = 0; i < 15; i++)
        {
            mStock[i].SetActive(false);
        }
        for (int i = 0; i < xMLManager.itemDB.wallet.stockNumber.Count ; i++)
        {
            mStock[i].SetActive(true);
        }
    }

    public void BuyStock()//buy button
    {
        sound.btnclick.Play();
        if (pp * quan <= xMLManager.itemDB.wallet.cash)
        {
            //buy
            xMLManager.itemDB.BuyStock(stocknum, quan);
            
            SetActivatedMyStock();
            myProperty.text = "자산 : " + xMLManager.itemDB.wallet.cash;
            myStock.text = "보유 : " + xMLManager.itemDB.stockList[stocknum].quan + " 개";

           

            //Debug.Log(wallet.GetMyStock(stocknum));
        }
        else {
            //can't buy
        }
    }
    public void SellStock()//sell button
    {
        if (quan <= xMLManager.itemDB.stockList[stocknum].quan)
        {
            sound.btnclick.Play();
            //sell
            xMLManager.itemDB.SellStock(stocknum, quan);

            SetActivatedMyStock();
            myProperty.text = "자산 : " + xMLManager.itemDB.wallet.cash;
            myStock.text = "보유 : " + xMLManager.itemDB.stockList[stocknum].quan + " 개";




            //Debug.Log(wallet.GetMyStock(stocknum));

        }
    }
    public void ClosePopup()
    {
        sound.btnclick.Play();
        quan = 0;
        totalPrice.text = "합산 : 0";

        tradePopup.SetActive(false);
    }


}
