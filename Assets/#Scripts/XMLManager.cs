using System.Collections;
using System.Collections.Generic;// let us use lists
using UnityEngine;
using System.Xml;               // basic xml attributes
using System.Xml.Serialization; // access xmlserializer
using System.IO;                //file management
using System.Text;
using UnityEngine.UI;

public class XMLManager : MonoBehaviour {
   
    public static XMLManager ins = null;//terrible singleton pattern
                                 // Use this for initialization
    private void Awake()
    {
        Debug.Log("XMLManager awake");
       
        if (ins == null)
        {
            Debug.Log("instance is null");
            ins = this;
        }
        else if (ins != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //list of items
    public ItemDatabase itemDB;

    //save function
    public void SaveItems() {
        //open new xml file
        string path;
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        if (Application.platform == RuntimePlatform.Android)
        {

            path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            using (FileStream stream = new FileStream(
            path + "/item_data.xml", FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
                serializer.Serialize(sw, itemDB); // put into sml files)
                sw.Close();//important :)
            }
        }
        else {
            using (FileStream stream = new FileStream(
             Application.dataPath + "/XML/item_data.xml", FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
                serializer.Serialize(sw, itemDB); // put into sml files)
                sw.Close();//important :)
            }
        }
     
            
    }
    //load function
    public void LoadItems() {
        string path;
        XmlSerializer serializer = new XmlSerializer(typeof(ItemDatabase));
        if (Application.platform == RuntimePlatform.Android)
        {

            path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            FileStream stream = new FileStream(
          path + "/item_data.xml", FileMode.Open);
            itemDB = serializer.Deserialize(stream) as ItemDatabase;
            stream.Close();
        }
        else {
            FileStream stream = new FileStream(
          Application.dataPath + "/XML/item_data.xml", FileMode.Open);
            itemDB = serializer.Deserialize(stream) as ItemDatabase;
            stream.Close();
        }
          
      
    }

   
}
[System.Serializable]
public class AuctionItem
{
    //public Image image;
    public int auctionNumber;//0~44
    public long price;//현재 (최고)가격
    public long payback;
    public float accel;//기울기
    public int state;//0 (x)or 1 (입찰) or 2 (낙찰)
    public float alpha;
    public long unit;
    public int frequency;

   
   
    public void Reset_function(long min, long max)
    {
        accel = (float)((Random.Range(0.5f, 2.0f) * max) - min)/(float)(1440*1440);
        alpha = min;
    }
    public long Set_price(int time)//state 0.1
    {
        if (state != 2)
        {
            long price = (int)(accel * time * time) + (int)alpha;

            if (price >= 0 && price < 10000) { price = (price / 1000) * 1000; unit = 1000; }
            else if (price >= 10000 && price < 1000000) { price = (price / 10000) * 10000; unit = 10000; }
            else if (price >= 1000000 && price < 10000000) { price = (price / 100000) * 100000; unit = 100000; }
            else if (price >= 10000000 && price < 100000000) { price = (price / 1000000) * 1000000; unit = 1000000; }
            else { price = (price / 10000000) * 10000000; unit = 10000000; }
            if (price > this.price)
            {
                
                if (state == 1)
                {
                    //입찰 때 가격을 다시 돌려줘야함
                    
                    state = 0;
                    payback = this.price;
                    this.price = price;
                    return payback;
                }
                this.price = price;
            }
        }
        return -1;//state != 1 payback이 아님 -> 낙찰 또는 x
       
    }
  
}
[System.Serializable]
public class StockItem
{

    public string stockName;
    public int price;
    public int startPrice;
    public int quan;
    public float percent;
    public int buyPrice;
    public void Initialize()
    {
        price = 800;
        startPrice = 800;
        quan = 0;
        percent = 0;
        buyPrice = 0;
    }
    public void Repeat_ChangePrice(int repeat)
    {
        for (int i = 0; i < repeat; i++)
        {
            ChangePrice(Random.Range(-1, 2));
        }
    }
    public void ChangePrice(int percent)
    {

        float change = price * (percent * 0.01f);

        //Debug.Log(change);
        price = price + (int)change;

        this.percent = ((float)(price - startPrice) / (float)startPrice) * 100f;
        //Debug.Log(this.percent);
    }
    public long ComputeUserBoughtStock()
    {
        return (quan * buyPrice);
    }
    public long ComputePLP()
    {
        return (quan * (price - buyPrice));
    }
    public float ComputePLR()
    {
        return ((float)((price - buyPrice) * 100) / (float)buyPrice);
    }
}
[System.Serializable]
public class DailyItem//0~8 9~17 18~26 27~35 36~44 45~53
{
    public string itemName;
    public long max_worthy;
    public long min_worthy;
    public int quan;

    public void Initialize()
    {
        quan = 0;
    }
    public void GetItem()
    {
        quan++;
    }
    public void SellItem()
    {
        quan--;
    }
}
[System.Serializable]
public class MyAuction
{
   
    public int Item_num {get; set;}
    public long Buy_price { get; set; }

}
[System.Serializable]
public class MyDisplay
{
    public int start_num;
    public int Start_num { get { return start_num; } set { start_num = value; } }
    public long profit;//데일리 수익
    public long Profit { get { return profit; } set { profit = value; } }
}
[System.Serializable]
public class Wallet
{
    public int shield;
    public long cash;
    public long stockProperty;
    public List<int> stockNumber = new List<int>();
    //public long totalProperty;
    public void Initialize()
    {
        shield = 0;
        cash = 10000000000;
        stockProperty = 0;
        stockNumber.Clear();
    }
    public long GetTotalProperty()
    {
        return (cash + stockProperty);
    }
}
[System.Serializable]
public class DailyGift
{
    public string style;//cash , item , display , payback
    public long cash;
    public int item_num;
    public long dp_profit;


    public DailyGift()
    {

    }
    public DailyGift(string type, long value)
    {
        switch (type)
        {
            case "cash":
                style = "cash";
                cash = value;
                item_num = -1;
                dp_profit = -1;
                break;
            case "item":
                style = "item";
                item_num = (int)value;
                cash = -1;
                dp_profit = -1;
                break;
            case "display":
                style = "display";
                dp_profit = value;
                cash = -1;
                item_num = -1;
                break;
            case "payback":
                style = "payback";
                cash = value;
                item_num = -1;
                dp_profit = -1;
                break;
        }

    }
}
[System.Serializable]
public class Stats
{//level
    List<Dictionary<string, object>> data;

    public int damage_num;//0-8
    public int criticalpercent_num;//9-19
    public int criticaldamage_num;//20-25
    public int feverdamage_num;//20-25
    public int[] core;

    public float damage;
    public float cri_percent;
    public float cri_damage;
    public float fever_damage;
    public void Initialize()
    {
        core = new int[3];
        damage_num = 0;
        criticaldamage_num = 20;
        criticalpercent_num = 9;
        feverdamage_num = 20;
        SetDamage();
    }
    public float GetCoreStat(int index)
    {
        data = CSVReader.Read("makemoneydatasheet3");
        float d = 0;
        float cp = 0;
        float cd = 0;
        for (int i = 0; i < 3; i++)
        {
            switch (core[i])
            {
                case 45:
                    d += 1;
                    break;
                case 46:
                    d += 3;
                    break;
                case 47:
                    d += 5;
                    break;
                case 48:
                    cp += 15;
                    break;
                case 49:
                    cp += 30;
                    break;
                case 50:
                    cp += 50;
                    break;
                case 51:
                    cd += 0.5f;
                    break;
                case 52:
                    cd += 2f;
                    break;
                case 53:
                    cd += 3f;
                    break;
                case 0:
                    break;
            }
        }
        if (index == 1)
            return d;
        else if (index == 2)
            return cp;
        else if (index == 3)
            return cd;
        else
            return 0;
    }
    public void SetDamage()
    {
        data = CSVReader.Read("makemoneydatasheet3");
        float d = 0;
        float cp = 0;
        float cd = 0 ;
        for (int i = 0; i<3;  i++)
        {
            switch (core[i])
            {
                case 45:
                    d += 1;
                    break;
                case 46:
                    d += 2;
                    break;
                case 47:
                    d += 4;
                    break;
                case 48:
                    cp += 10;
                    break;
                case 49:
                    cp += 30;
                    break;
                case 50:
                    cp += 50;
                    break;
                case 51:
                    cd += 1.5f;
                    break;
                case 52:
                    cd += 2.5f;
                    break;
                case 53:
                    cd += 3f;
                    break;
                case 0:
                    break;
            }
        }
        damage = float.Parse(data[damage_num]["stat"].ToString()) + d;
        cri_percent = float.Parse(data[criticalpercent_num]["stat"].ToString()) + cp;
        cri_damage = float.Parse(data[criticaldamage_num]["stat"].ToString()) + cd;
        fever_damage = float.Parse(data[feverdamage_num]["stat"].ToString());
    }
    
}

[System.Serializable]
public class ItemDatabase {
    List<Dictionary<string, object>> data;



    [XmlArray("StockList")]//customize list name 
    public List<StockItem> stockList = new List<StockItem>();
    [XmlArray("DailyList")]
    public List<DailyItem> dailyList = new List<DailyItem>();
    [XmlArray("AuctionList")]
    public List<AuctionItem> auctionList = new List<AuctionItem>();
    [XmlElement("Wallet")]
    public Wallet wallet = new Wallet();
    [XmlArray("MyAuctionList")]
    public List<MyAuction> myAuctionList = new List<MyAuction>();
    [XmlArray("DisplayList")]
    public List<MyDisplay> myDisplayList = new List<MyDisplay>();
    [XmlArray("DailyGiftList")]
    public List<DailyGift> dailyGiftList = new List<DailyGift>();
    [XmlElement("Stats")]
    public Stats stats = new Stats();

    public void Initialize()//NewGame
    {
        data = CSVReader.Read("makemoneydatasheet3");
        wallet.Initialize();
        stats.Initialize();
        for (int i = 0; i < stockList.Count; i++)
        {
            stockList[i].Initialize();//price = 800 quan = 0
            stockList[i].stockName = data[i]["stock"].ToString();
        }
        for (int i = 0; i < dailyList.Count; i++)
        {
            dailyList[i].Initialize();//quan = 0
            dailyList[i].itemName = data[i]["item"].ToString();
            dailyList[i].min_worthy = long.Parse(data[i]["minprice"].ToString());
            dailyList[i].max_worthy = long.Parse(data[i]["maxprice"].ToString());

        }
        for (int i = 0; i < auctionList.Count; i++)
        {
            ResetAuction(i);
            auctionList[i].Set_price(((int)(System.DateTime.Now - System.DateTime.Now.Date).TotalSeconds) / 60);
        }
        myAuctionList.Clear();
        myDisplayList.Clear();
        dailyGiftList.Clear();
    }
    public void StartDisplay(int num)//0~4
    {
        switch (num)
        {
            case 0:
                myDisplayList.Add(new MyDisplay() { Profit = 500000 , Start_num =0 });
                break;
            case 1:
                myDisplayList.Add(new MyDisplay() { Profit = 100000000, Start_num = 9 });
                break;
            case 2:
                myDisplayList.Add(new MyDisplay() { Profit = 150000000, Start_num = 18 });
                break;
            case 3:
                myDisplayList.Add(new MyDisplay() { Profit = 300000000, Start_num = 27 });
                break;
            case 4:
                myDisplayList.Add(new MyDisplay() { Profit = 500000000, Start_num = 36 });
                break;

        }
    }
    public void InGame_DayOver()//Gapmin == 0
    {
        data = CSVReader.Read("makemoneydatasheet3");
        int ran = Random.Range(0, 45);

        //dailyList[ran].quan++;
        dailyGiftList.Add(new DailyGift("item",ran));

        Debug.Log( ran+ "획득(인게임)");
        //wallet.cash += long.Parse(data[PlayerPrefs.GetInt("level", 0)]["salary"].ToString());
        dailyGiftList.Add(new DailyGift("cash", long.Parse(data[PlayerPrefs.GetInt("level", 0)]["salary"].ToString()) ));


        for (int dp = 0; dp < myDisplayList.Count; dp++)
        {
            dailyGiftList.Add(new DailyGift("display", myDisplayList[dp].Profit));
            //wallet.cash += myDisplayList[dp].Profit;
            for (int i = 0; i < 9; i++)
            {
                dailyList[myDisplayList[dp].Start_num + i].quan++;
            }
        }
        myDisplayList.Clear();


        for (int i = 0; i < auctionList.Count ; i++)
        {
            if (auctionList[i].state == 1)
            {
                auctionList[i].state = 2;
                Debug.Log("인게임 으로 인해 낙찰"+i);
                myAuctionList.Add(new MyAuction() { Item_num = auctionList[i].auctionNumber, Buy_price = auctionList[i].price });
            }
            ResetAuction(i);
        }
        for (int j = 0; j < stockList.Count; j++)
        {
            stockList[j].startPrice = stockList[j].price;
        }

    }
    public void DayOver_Update(int saveToZero,int zeroToNow, int[] auctionNum , int dailyitemNum)// 옥션 업데이트5 , 데일리 아이템지급 , 주식 시작값 설정15
    {
        data = CSVReader.Read("makemoneydatasheet3");
        //dailyList[dailyitemNum].quan++;
        dailyGiftList.Add(new DailyGift("item", dailyitemNum));

        Debug.Log(dailyitemNum + "획득(데이오버)");
        dailyGiftList.Add(new DailyGift("cash", long.Parse(data[PlayerPrefs.GetInt("level", 0)]["salary"].ToString())));
        //wallet.cash += long.Parse(data[PlayerPrefs.GetInt("level", 0)]["salary"].ToString());
        for (int dp = 0; dp < myDisplayList.Count; dp++)
        {
            //wallet.cash += myDisplayList[dp].Profit;
            dailyGiftList.Add(new DailyGift("display", myDisplayList[dp].Profit));
            for (int i = 0; i < 9; i++)
            {
                dailyList[myDisplayList[dp].Start_num + i].quan++;
            }
        }
        
        myDisplayList.Clear();
        for (int num = 0; num < auctionList.Count; num++)
        {

            //saveToZero
            for (int time = 1440 - saveToZero/6; time < 1440; time++)
            {
                if (Random.Range(0, 100) < auctionList[num].frequency)
                {
                    //auctionList[num].Set_price(time);
                    Set_price(num, time);
                }
               

            }

            //Zero
            if (auctionList[num].state == 1)
            {
                auctionList[num].state = 2;
                Debug.Log("데이오버 으로 인해 낙찰" + num);
                myAuctionList.Add(new MyAuction() { Item_num = auctionList[num].auctionNumber , Buy_price = auctionList[num].price});
            }
            ResetAuction(num);

            //zeroToNow
            for (int time = 0; time < zeroToNow/6; time++)
            {
                if (Random.Range(0, 100) < auctionList[num].frequency)
                {
                    //auctionList[num].Set_price(time);
                    Set_price(num, time);
                }
            }

        }





        for (int num = 0; num < stockList.Count; num++)
        {
            stockList[num].Repeat_ChangePrice(saveToZero);
            stockList[num].startPrice = stockList[num].price;
            stockList[num].Repeat_ChangePrice(zeroToNow);
        }
            

    }
    public void Today_Update(int save , int now)//saveToNow = now - save
    {
        for (int num = 0; num < stockList.Count; num++)
        {
            stockList[num].Repeat_ChangePrice(now-save);
        }

        for (int num = 0; num < auctionList.Count; num++)
        {

            for (int time = save/6; time < now/6; time++)
            {
                if (Random.Range(0, 100) < auctionList[num].frequency)
                {
                    //auctionList[num].Set_price(time);
                    Set_price(num, time);
                }
            }
              
            
        }
    }

    
    public long ComputeStockProperty()
    {
        wallet.stockProperty = 0;

        for (int i = 0; i < stockList.Count; i++)
        {
            wallet.stockProperty += (stockList[i].price * stockList[i].quan);
        }

        return wallet.stockProperty;
    }
    public long TotalUserBoughtStock()
    {
        long totalUserBoughtStock = 0;
        for (int i = 0; i < stockList.Count; i++)
        {
            totalUserBoughtStock += stockList[i].ComputeUserBoughtStock();
        }
        return totalUserBoughtStock;
    }
    public long TotalPLP()
    {
        long totalPLP = 0;
        for (int i = 0; i < stockList.Count; i++)
        {
            totalPLP += stockList[i].ComputePLP();
        }
        return totalPLP;
    }
    public float TotalPLR()
    {

        return ((float)((ComputeStockProperty() - TotalUserBoughtStock())*100)/(float)TotalUserBoughtStock());
    }
    public void ComputeUserHaveStock()
    {

        for (int i = 0; i < stockList.Count; i++)
        {
            if (stockList[i].quan != 0)
            {
                wallet.stockNumber.Add(i);
               
            }
        }
        
    }
    public void BuyStock(int stocknum , int quan)
    {
        stockList[stocknum].buyPrice = ((stockList[stocknum].buyPrice * stockList[stocknum].quan) + (stockList[stocknum].price * quan)) / (stockList[stocknum].quan + quan);
        if (stockList[stocknum].quan == 0)
        {
            wallet.stockNumber.Add(stocknum);
        }
        stockList[stocknum].quan += quan;
        
        wallet.cash -= (stockList[stocknum].price * quan);
        wallet.stockProperty = ComputeStockProperty();
        
           
        

    }
    public void SellStock(int stocknum, int quan)
    {
        //stockList[stocknum].buyPrice = ((stockList[stocknum].buyPrice * stockList[stocknum].quan) + (stockList[stocknum].price * quan)) / (stockList[stocknum].quan + quan);
        stockList[stocknum].quan -= quan;
        if (stockList[stocknum].quan == 0)
        {
            wallet.stockNumber.Remove(stocknum);
        }
        wallet.cash += (stockList[stocknum].price * quan);
        wallet.stockProperty = ComputeStockProperty();
    }

    //***********************************************DAILY*************************************************************//
    //
    //

    public void GetDailyItem()
    {
        dailyList[Random.Range(0, 45)].quan++;
       
    }

    //***********************************************AUCTION*************************************************************//
    //
    //

    public void Set_price(int list_num , int time)// call auctionlist[list_num].Set_price()
    {
        if (auctionList[list_num].Set_price(time) != -1 )//state 가 1이라서 payback
        {
            Debug.Log("payback" + auctionList[list_num].payback);
            dailyGiftList.Add(new DailyGift("payback", auctionList[list_num].payback));
            //wallet.cash += auctionList[list_num].payback;
        }

    }

    public void ResetAuction(int num)
    {
        auctionList[num].auctionNumber = Random.Range(0, 45);
        auctionList[num].frequency = Random.Range(10, 50);//10 ~ 50%
        auctionList[num].state = 0;
        auctionList[num].price = dailyList[auctionList[num].auctionNumber].min_worthy;
        auctionList[num].Reset_function(dailyList[auctionList[num].auctionNumber].min_worthy, dailyList[auctionList[num].auctionNumber].max_worthy);


    }
    

    
    public bool CheckCanDP(int start)//0 9 18 27 36
    {
        for (int i = start; i < start + 9; i++)
        {
            if (dailyList[i].quan == 0)
            {
                return false;
            }

        }
        return true;
    }

    public float GetCriticalDamage()
    {
        return stats.cri_damage;
    }
    public float GetCriticalPercent()
    {
        return stats.cri_percent;
    }
    public float GetFeverDamage()
    {
        return stats.fever_damage;
    }
    public float GetDamage()
    {
        return stats.damage;
    }
}
