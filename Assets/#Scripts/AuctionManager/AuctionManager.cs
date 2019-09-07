using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.EventSystems;
public class AuctionManager : MonoBehaviour {
    XMLManager xMLManager;
    Single single;
    SoundSingleTon sound;
    public GameObject[] auctionItem;// 0 image 1 name 2 text 3 time
    public GameObject[] myAuctionItem;
    Image[] image = new Image[5];
    Text[] auctionname = new Text[5];
    Text[] price = new Text[5];
    Text[] time = new Text[5];
    public GameObject auctionPopup;// 2 image 3 name 4 price 5 maxprice
    private Text auctionpriceText;
    long orderprice;
    int auctionnum;
    int ordernum;

    public ScrollRect scrollview;
    public GameObject content;
    public GameObject myauctionItem;
    // Use this for initialization
    void Start () {
        xMLManager = XMLManager.ins;
        single = Single.instance;
        sound = SoundSingleTon.instance;
        for (int i = 0; i < auctionItem.Length; i++)
        {
            image[i] = auctionItem[i].transform.GetChild(0).GetComponent<Image>();
            auctionname[i] = auctionItem[i].transform.GetChild(1).GetComponent<Text>();
            price[i] = auctionItem[i].transform.GetChild(2).GetComponent<Text>();
            time[i] = auctionItem[i].transform.GetChild(3).GetComponent<Text>();
            
            int number = xMLManager.itemDB.auctionList[i].auctionNumber;
            auctionname[i].text = xMLManager.itemDB.dailyList[number].itemName;
            Debug.Log("number : " + number);
            image[i].sprite = Resources.Load<Sprite>("Item/"+number);
            //image
            
        }
        Debug.Log("listcount2:" + xMLManager.itemDB.myAuctionList.Count);
        for (int i = 0; i < xMLManager.itemDB.myAuctionList.Count; i++)
        {
            Debug.Log("count:" + i);
            GameObject item = Instantiate(myauctionItem);
            item.name = i.ToString();
            item.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + xMLManager.itemDB.myAuctionList[i].Item_num);
            item.transform.GetChild(1).GetComponent<Text>().text = xMLManager.itemDB.dailyList[xMLManager.itemDB.myAuctionList[i].Item_num].itemName;
            item.transform.GetChild(2).GetComponent<Text>().text = xMLManager.itemDB.myAuctionList[i].Buy_price.ToString();
            item.transform.GetChild(3).GetComponent<Text>().text = "낙찰";
            
            item.transform.SetParent(content.transform, false);

        }

    }
    private void Update()
    {
        if (single.GapMin() == 0 && !single.reward)//정각 초기화
        {
            single.reward = true;
            xMLManager.itemDB.InGame_DayOver();
            for (int i = 0; i < auctionItem.Length; i++)
            {
                image[i] = auctionItem[i].transform.GetChild(0).GetComponent<Image>();
                auctionname[i] = auctionItem[i].transform.GetChild(1).GetComponent<Text>();
                price[i] = auctionItem[i].transform.GetChild(2).GetComponent<Text>();
                time[i] = auctionItem[i].transform.GetChild(3).GetComponent<Text>();

                int number = xMLManager.itemDB.auctionList[i].auctionNumber;
                auctionname[i].text = xMLManager.itemDB.dailyList[number].itemName;
                Debug.Log("number : " + number);
                image[i].sprite = Resources.Load<Sprite>("Item/" + number);
                //image

            }
            
            for (int i = 0; i < xMLManager.itemDB.myAuctionList.Count; i++)
            {
                Debug.Log("count:" + i);
                GameObject item = Instantiate(myauctionItem);
                item.name = i.ToString();
                item.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + xMLManager.itemDB.myAuctionList[i].Item_num);
                item.transform.GetChild(1).GetComponent<Text>().text = xMLManager.itemDB.dailyList[xMLManager.itemDB.myAuctionList[i].Item_num].itemName;
                item.transform.GetChild(2).GetComponent<Text>().text = xMLManager.itemDB.myAuctionList[i].Buy_price.ToString();
                item.transform.GetChild(3).GetComponent<Text>().text = "낙찰";

                item.transform.SetParent(content.transform, false);

            }
           
        }
        
        for (int i = 0; i < auctionItem.Length; i++)
        {
            price[i].text = xMLManager.itemDB.auctionList[i].price.ToString();


            if (xMLManager.itemDB.auctionList[i].state == 0)        { time[i].text = LeftTime();  auctionItem[i].GetComponent<Button>().interactable = true; }
            else if (xMLManager.itemDB.auctionList[i].state == 1)   { time[i].text = "입찰";      auctionItem[i].GetComponent<Button>().interactable = false; }
            else if (xMLManager.itemDB.auctionList[i].state == 2)   { time[i].text = "낙찰";      auctionItem[i].GetComponent<Button>().interactable = false; }

            //myAuctionItem[i].SetActive(false);

        }
       

    }
    public string LeftTime()
    {
        string lefttime;

        if (DateTime.Now.Hour != 23)
        {
            lefttime = (23 - DateTime.Now.Hour) + " : " + (59 - DateTime.Now.Minute);

        }
        else { lefttime = (59 - DateTime.Now.Minute) + " : " + (59 - DateTime.Now.Second); }
        return lefttime;
    }
    public void OrderItem(GameObject gameObject)
    {
        sound.btnclick.Play();
        auctionPopup.SetActive(true);
        orderprice = long.Parse(gameObject.transform.GetChild(2).GetComponent<Text>().text.ToString());

        GameObject image = auctionPopup.transform.GetChild(2).gameObject;
        GameObject name = auctionPopup.transform.GetChild(3).gameObject;
        auctionpriceText = auctionPopup.transform.GetChild(4).gameObject.GetComponent<Text>();//orderprice
        Text maxprice = auctionPopup.transform.GetChild(5).gameObject.GetComponent<Text>();


        auctionpriceText.text = "주문가 : " + orderprice + " 원";
        image.GetComponent<Image>().sprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        name.GetComponent<Text>().text = gameObject.transform.GetChild(1).GetComponent<Text>().text;
        
       
        auctionnum = int.Parse(EventSystem.current.currentSelectedGameObject.name);
        ordernum = xMLManager.itemDB.auctionList[auctionnum].auctionNumber;
        maxprice.text = "최고가 : " + xMLManager.itemDB.dailyList[xMLManager.itemDB.auctionList[auctionnum].auctionNumber].max_worthy * 2 + " 원";

    }
    public void UpButton()
    {
        sound.btnclick.Play();
        long price;
        long unit;
        price = orderprice;
        if (price >= 0 && price < 10000)                    { unit = 1000; }
        else if (price >= 10000 && price < 1000000)         { unit = 10000; }
        else if (price >= 1000000 && price < 10000000)      { unit = 100000; }
        else if (price >= 10000000 && price < 100000000)    { unit = 1000000; }
        else                                                { unit = 10000000; }
        Debug.Log(price + "  " + unit);
        orderprice = (price + unit);
        auctionpriceText.text = "주문가 : " + orderprice + " 원";
    }
    public void DownButton()
    {
        sound.btnclick.Play();
        long price;
        long unit;
        price = orderprice;
        if (price >= 0 && price < 10000) { unit = 1000; }
        else if (price >= 10000 && price < 1000000) { unit = 10000; }
        else if (price >= 1000000 && price < 10000000) { unit = 100000; }
        else if (price >= 10000000 && price < 100000000) { unit = 1000000; }
        else { unit = 10000000; }
        Debug.Log(price + "  " + unit);
        orderprice = (price - unit);
        auctionpriceText.text = "주문가 : " + orderprice + " 원";
    }
    public void ExitButton()
    {
        sound.btnclick.Play();
        auctionPopup.SetActive(false);
    }
    public void OrderButton()
    {
        long pay = orderprice;
        if (xMLManager.itemDB.wallet.cash >= pay && pay > xMLManager.itemDB.auctionList[auctionnum].price)
        {
            sound.storeget.Play();
            xMLManager.itemDB.auctionList[auctionnum].Reset_function(xMLManager.itemDB.dailyList[ordernum].min_worthy, xMLManager.itemDB.dailyList[ordernum].max_worthy);
            xMLManager.itemDB.auctionList[auctionnum].price = pay;
            xMLManager.itemDB.wallet.cash -= pay;//payback when 1 -> 0

            //update myAuction
            xMLManager.itemDB.auctionList[auctionnum].state = 1;

            //UpdateMyAuction(pay);
            single.SetAlert(xMLManager.itemDB.dailyList[ordernum].itemName + "을 " + pay + " 에 입찰하셨습니다.",Color.black);
            auctionPopup.SetActive(false);
        } else if (xMLManager.itemDB.wallet.cash < pay) { sound.miss.Play(); single.SetAlert("가지고 있는 현금이 부족합니다.",Color.red); iTween.ShakePosition(auctionPopup, new Vector3(0.2f, 0, 0), 1); }
        else if (pay <= xMLManager.itemDB.auctionList[auctionnum].price) { sound.miss.Play(); single.SetAlert("현재가보다 더 높은 금액으로 입찰해주세요.", Color.red); iTween.ShakePosition(auctionPopup, new Vector3(0.2f, 0, 0), 1); }
        
    }
    public void MaxOrderButton()
    {
        if (xMLManager.itemDB.wallet.cash >= (xMLManager.itemDB.dailyList[ordernum].max_worthy *2))
        {
            sound.storeget.Play();
            auctionItem[auctionnum].GetComponent<Button>().interactable = false;
            
            xMLManager.itemDB.wallet.cash -= (xMLManager.itemDB.dailyList[ordernum].max_worthy*2);
            xMLManager.itemDB.auctionList[auctionnum].state = 2;
            xMLManager.itemDB.auctionList[auctionnum].price = xMLManager.itemDB.dailyList[ordernum].max_worthy * 2;


            xMLManager.itemDB.myAuctionList.Add(new MyAuction() { Item_num = ordernum , Buy_price = xMLManager.itemDB.dailyList[ordernum].max_worthy * 2 });
            GameObject item = Instantiate(myauctionItem);
            item.name = (xMLManager.itemDB.myAuctionList.Count - 1).ToString();
            item.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/"+xMLManager.itemDB.myAuctionList.Last().Item_num);
            item.transform.GetChild(1).GetComponent<Text>().text = xMLManager.itemDB.dailyList[xMLManager.itemDB.myAuctionList.Last().Item_num].itemName;
            item.transform.GetChild(2).GetComponent<Text>().text = xMLManager.itemDB.myAuctionList.Last().Buy_price.ToString();
            item.transform.GetChild(3).GetComponent<Text>().text = "낙찰";
            
            item.transform.SetParent(content.transform, false);
            //update myauction
            

            single.SetAlert(xMLManager.itemDB.dailyList[ordernum].itemName + "을 " + xMLManager.itemDB.dailyList[ordernum].max_worthy * 2 + " 에 최고가 낙찰하셨습니다.",Color.black);
            //UpdateMyAuction(xMLManager.itemDB.dailyList[ordernum].max_worthy);
            auctionPopup.SetActive(false);
        }
        else if (xMLManager.itemDB.wallet.cash < (xMLManager.itemDB.dailyList[ordernum].max_worthy * 2)) {sound.miss.Play(); single.SetAlert("가지고 있는 현금이 부족합니다.\n"+ (xMLManager.itemDB.dailyList[ordernum].max_worthy * 2)+ " 원 의 현금이 필요합니다"
            , Color.red); iTween.ShakePosition(auctionPopup, new Vector3(0.2f, 0, 0), 1); }
        

        
    }

   

    
}
