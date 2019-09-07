using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyStockManager : StockManager {
   
   
	// Use this for initialization
	void Start () {

        xMLManager = XMLManager.ins;
        sound = SoundSingleTon.instance;
	}
   public  void ClickMStock(Button button)
    {
        sound.btnclick.Play();
        string select = button.transform.GetChild(0).gameObject.GetComponent<Text>().text;
        Debug.Log("select : " + select);
        for (int i = 0; i < 15; i++)
        {
            if (select == stockInfo[i].transform.GetChild(0).gameObject.GetComponent<Text>().text)
            {
                Debug.Log("select : " + stockInfo[i].transform.GetChild(0).gameObject.GetComponent<Text>());
                ClickStock(stockInfo[i].GetComponent<Button>());
            }
        }
    }
    /*void SetMyStock(int num)
    {
        GameObject name = mStock[activated].transform.GetChild(0).gameObject;
        GameObject quan = mStock[activated].transform.GetChild(1).gameObject;       
        GameObject PLP = mStock[activated].transform.GetChild(2).gameObject;
        GameObject PLR = mStock[activated].transform.GetChild(3).gameObject;
        name.GetComponent<Text>().text = wallet.stocklist[num].name;
        quan.GetComponent<Text>().text = wallet.GetMyStock(num).ToString();
        //PLP.GetComponent<Text>().text = wallet.CalPLP(num).ToString();
        //PLR.GetComponent<Text>().text = wallet.CalPLR(num).ToString("N2") + "%";
    }*/
	// Update is called once per frame
	
}
