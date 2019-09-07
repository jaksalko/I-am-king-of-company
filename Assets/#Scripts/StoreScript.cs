using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StoreScript : MonoBehaviour {
    public GameObject tut;
    public Text[] stats; //직업 기본데미지 크확 크뎀 피뎀
    public GameObject[] upgrade;//child => price
    private Text[] upgradeText = new Text[4];
    
    XMLManager xml;
    Single single;
    SoundSingleTon sound;
    List<Dictionary<string, object>> data;

    public GameObject[] storeitem;
    public GameObject warningPopup;
    private int itemclick;

    public GameObject roulletPopup;
    public Image roulletimg;
    public Text roullettext;
    public Button getbtn;
    public GameObject getitem5;
    // Use this for initialization
    void Start () {
        data = CSVReader.Read("makemoneydatasheet3");
        xml = XMLManager.ins;
        sound = SoundSingleTon.instance;
        single = Single.instance;
        sound.bgm.Stop();
        if (PlayerPrefs.GetString("store", "false") == "false")
        {
            tut.SetActive(true);
        }
        #region 내정보 초기화
        if (PlayerPrefs.GetInt("level", 0) < 11) { stats[0].text = "회사원 " + "( " + data[PlayerPrefs.GetInt("level", 0)]["job"].ToString() + " )"; }
        else if (PlayerPrefs.GetInt("level", 0) >= 11) { stats[0].text ="축구선수 " +"( " + data[PlayerPrefs.GetInt("level", 0)]["job"].ToString() + " )"; }

        stats[1].text = "경험 증가량 : "+data[xml.itemDB.stats.damage_num]["stat"] +  "<color=red>" + " + "  + xml.itemDB.stats.GetCoreStat(1)+ "</color>";
        stats[2].text = "깨달음 확률 : "+ data[xml.itemDB.stats.criticalpercent_num]["stat"] + "<color=red>" + " + " + xml.itemDB.stats.GetCoreStat(2) + "</color>" + "%";
        stats[3].text = "깨달음 증가량 : "+ data[xml.itemDB.stats.criticaldamage_num]["stat"] + "<color=red>" + " + " + xml.itemDB.stats.GetCoreStat(3) + "</color>" + "배";
        stats[4].text = "피버 증가량 : "+ data[xml.itemDB.stats.feverdamage_num]["stat"] + "배";

        for (int i = 0; i < upgrade.Length; i++)
        {
            upgradeText[i] = upgrade[i].GetComponent<Text>();
        }
        if (xml.itemDB.stats.damage_num != 8)
            upgradeText[0].text = data[xml.itemDB.stats.damage_num + 1]["statprice"] + " 원";
        else { upgradeText[0].text = "최대"; }

        if (xml.itemDB.stats.criticalpercent_num != 19)
            upgradeText[1].text = data[xml.itemDB.stats.criticalpercent_num + 1]["statprice"] + " 원";
        else { upgradeText[1].text = "최대"; }
    
              
                    if (xml.itemDB.stats.criticaldamage_num != 25)
                        upgradeText[2].text = data[xml.itemDB.stats.criticaldamage_num + 1]["statprice"] + " 원";
                    else { upgradeText[2].text = "최대"; }
                
           
                    if (xml.itemDB.stats.feverdamage_num != 25)
                        upgradeText[3].text = data[xml.itemDB.stats.feverdamage_num + 1]["statprice"] + " 원";
                    else { upgradeText[3].text = "최대"; }
       
        #endregion
        #region 상점 UI
        for (int i = 0; i < storeitem.Length; i++)
        {
            storeitem[i].transform.GetChild(0).GetComponent<Text>().text = data[i]["storeitem"].ToString();
            storeitem[i].transform.GetChild(2).GetComponent<Text>().text = data[i]["storepricetext"].ToString();
        }
        #endregion
    }

    public void StoreitemClick()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < storeitem.Length; i++)
        {
            if (storeitem[i] == selected )
            {
                if (xml.itemDB.wallet.cash >= long.Parse(data[i]["storeprice"].ToString()))
                {
                    sound.btnclick.Play();
                    itemclick = i;
                    if (itemclick == 0)
                    {
                        WarningPopup("입찰 중인 물품이 있을 경우, 입찰금을 돌려받지 못합니다.\n그래도 경매장을 리셋하시겠습니까 ? ");
                    }
                    else if (itemclick == 1)
                    {
                        WarningPopup("(코어를 포함한) 아이템 중 하나가 랜덤하게 나옵니다.\n구매하시겠습니까?");
                    }
                    else if (itemclick == 2)
                    {
                        WarningPopup("직업 등급을 하락시키는 일이 발생했을 경우 강등을 방어해주는 아이템입니다.\n구매하시겠습니까?");
                    }
                    else if(itemclick == 3)
                    {
                        WarningPopup("(코어를 포함한) 아이템 중 5개가 랜덤하게 나옵니다.\n구매하시겠습니까?");
                    }
                    else if (itemclick == 4)
                    {
                        WarningPopup("현재 스텟을 모두 리셋하고 투자한 돈의 80%만 돌려받을 수 있습니다.\n구매하시겠습니까?");
                    }
                    else if (itemclick == 5)
                    {
                        WarningPopup("로또는 하루에 한번만 구매할 수 있습니다.\n구매하시겠습니까?");
                    }
                    else if (itemclick == 6)
                    {
                        WarningPopup("랜덤 코어는 3개의 코어 중 하나가 랜덤하게 나옵니다.\n구매하시겠습니까?");
                    }
                    else if (itemclick == 7)
                    {
                        WarningPopup("1레벨의 깨달음 코어를 얻을 수 있습니다.\n구매하시겠습니까?");
                    }
                    else if (itemclick == 8)
                    {
                        WarningPopup("1레벨의 경험 코어를 얻을 수 있습니다.\n구매하시겠습니까?");
                    }
                    else if (itemclick == 9)
                    {
                        WarningPopup("1레벨의 깨달음확률 코어를 얻을 수 있습니다.\n구매하시겠습니까?");
                    }
                }
                else
                {
                    sound.miss.Play();
                    single.SetAlert("가지고 있는 현금이 부족합니다.", Color.red);
                }
                
                break;
            }
        }
    }
  
    public void WarningPopup(string warning_msg)
    {
        warningPopup.SetActive(true);
        Text warning = warningPopup.transform.GetChild(2).GetComponent<Text>();
        warning.text = warning_msg;
    }
    public void RoulletPopup(string itemname,string path)
    {
       
        roulletPopup.SetActive(true);
        roullettext.text = itemname;
        roulletimg.sprite = Resources.Load<Sprite>(path);
    }
    public void GetButtonClick()
    {
        sound.btnclick.Play();
        roulletPopup.SetActive(false);
        getitem5.SetActive(false);

    }
    IEnumerator RoulletImage(int path, int start , int end ,string tt)
    {
        float sec = 0f;
        getbtn.interactable = false;
        for (int i = 0; i < 35; i++)
        {
            int ran = Random.Range(start, end);
            sec += 0.01f;
            roulletimg.sprite = Resources.Load<Sprite>("Item/"+ran);
            roullettext.text = xml.itemDB.dailyList[ran].itemName;
            sound.roullet.Play();
            yield return new WaitForSeconds(sec);//느리게 룰렛멈춤
           
        }
        roulletimg.sprite = Resources.Load<Sprite>("Item/" + path);
        roullettext.text = tt;
        Debug.Log("done");
        sound.gotcha.Play();
        getbtn.interactable = true;
    }
    public void StoreYes()
    {
        switch (itemclick)
        {
            case 0:
                int zeroToNow = ((int)(System.DateTime.Now - System.DateTime.Now.Date).TotalSeconds) / 60;
                for (int num = 0; num < xml.itemDB.auctionList.Count; num++)
                {
                    xml.itemDB.ResetAuction(num);
                    for (int time = 0; time < zeroToNow; time++)
                    {
                        if (Random.Range(0, 100) < xml.itemDB.auctionList[num].frequency)
                        {
                            //auctionList[num].Set_price(time);
                            xml.itemDB.Set_price(num, time);
                        }
                    }
                    
                }
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
                RoulletPopup("경매장이 초기화 되었습니다.", "Store/change");
                sound.storeget.Play();
                //single.SetAlert("경매장이 초기화 되었습니다.",Color.black);
                    break;

            case 1:
                float random = Random.Range(0f, 100f);
                for (int i = 0; i < xml.itemDB.dailyList.Count; i++)
                {
                    if (random <= float.Parse(data[i]["droppercent"].ToString()))
                    {
                        xml.itemDB.dailyList[i].quan++;
                        RoulletPopup("아이템 룰렛.", "Item/0");

                        StartCoroutine(RoulletImage(i,0,45, xml.itemDB.dailyList[i].itemName+ " 를 획득하셨습니다."));
                        break;
                    }
                }
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
               
                
                break;
            case 2:
                xml.itemDB.wallet.shield++;
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
                RoulletPopup("등급하락 방지권을 구매하셨습니다..", "Store/shield");
                sound.storeget.Play();
                break;
            case 3:
                string[] get_item = new string[5];
                int last = 0 ;
                for (int j = 0; j < 5; j++)
                {
                    float random_five = Random.Range(0f, 100f);
                    for (int i = 0; i < xml.itemDB.dailyList.Count; i++)
                    {
                        if (random_five <= float.Parse(data[i]["droppercent"].ToString()))
                        {
                            get_item[j] = data[i]["item"].ToString();
                            
                            xml.itemDB.dailyList[i].quan++;
                            last = i;
                            break;
                        }
                    }
                    getitem5.SetActive(true);
                    RoulletPopup("아이템 룰렛5.", "Item/0");
                    
                    StartCoroutine(RoulletImage(last, 0, 45, get_item[0] + ", " + get_item[1] + ", " + get_item[2] + ",\n" + get_item[3] + ", " + get_item[4] + " 를 획득하셨습니다."));
                    //single.SetAlert(get_item[0] + ", " + get_item[1] + ", " + get_item[2] + ", " + get_item[3] + ", " + get_item[4] + " 를 획득하셨습니다.", Color.black);
                }
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
                break;
            case 4://스텟리셋
                break;
            case 5://로또
                float lotto = Random.Range(0f, 100f);
                long lotto_cash = 0;
                if (lotto < 40) { single.SetAlert("꽝입니다! 내일도 도전해주세요~", Color.black); lotto_cash = 0; }
                else if (lotto < 90) { lotto_cash = 5000000; }
                else if (lotto < 99) { lotto_cash = 50000000; }
                else if (lotto < 99.5) { lotto_cash = 20000000; }
                else { lotto_cash = 100000000; }
                
                if (lotto >= 40) { RoulletPopup("축하합니다!\n" + lotto_cash + " 원에 당첨되셨습니다!", "Store/lotto");sound.gotcha.Play();  }
                xml.itemDB.wallet.cash += lotto_cash;
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
                break;
            case 6://랜덤코어
                int core;
                do { core = Random.Range(45, 54); } while (core == 46 || core == 47 || core==49 || core == 50 || core == 52 || core == 53);
                
                xml.itemDB.dailyList[core].quan++;

                RoulletPopup("코어 룰렛.", "Item/45");

                StartCoroutine(RoulletImage(core, 45, 54, xml.itemDB.dailyList[core].itemName));
                
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
                break;
            case 7://크리티컬
                xml.itemDB.dailyList[51].quan++;
                RoulletPopup("깨달음 코어를 획득하셨습니다.", "Item/51");
                sound.storeget.Play();
                //single.SetAlert("크리티컬 코어를 획득하셨습니다.", Color.black);
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
                break;
            case 8://데미지
                xml.itemDB.dailyList[45].quan++;
                RoulletPopup("경험 코어를 획득하셨습니다.", "Item/45");
                sound.storeget.Play();
                //single.SetAlert("데미지 코어를 획득하셨습니다.", Color.black);
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
                break;
            case 9://크확
                xml.itemDB.dailyList[48].quan++;
                RoulletPopup("깨달음확률 코어를 획득하셨습니다.", "Item/48");
                sound.storeget.Play();
                //single.SetAlert("크리티컬확률 코어를 획득하셨습니다.", Color.black);
                xml.itemDB.wallet.cash -= long.Parse(data[itemclick]["storeprice"].ToString());
                break;
        }
        warningPopup.SetActive(false);
    }
    public void StoreNo()
    {
        sound.btnclick.Play();
        warningPopup.SetActive(false);
    }
    public void UpgradeClick()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < upgrade.Length; i++)
        {
            if (selected == upgrade[i])
            {
                Upgrade(i);
                break;
            }
        }
    }
    public void Upgrade(int type)
    {
        switch (type)
        {
            case 0://기본 데미지
                if (xml.itemDB.wallet.cash >= long.Parse(data[xml.itemDB.stats.damage_num + 1]["statprice"].ToString()) && xml.itemDB.stats.damage_num < 8)
                {
                    sound.upgrade.Play();
                    xml.itemDB.wallet.cash -= long.Parse(data[xml.itemDB.stats.damage_num + 1]["statprice"].ToString());
                    xml.itemDB.stats.damage_num++;
                    xml.itemDB.stats.SetDamage();
                    stats[1].text = "경험 증가량 : " + data[xml.itemDB.stats.damage_num]["stat"] + "<color=red>" + " + " + xml.itemDB.stats.GetCoreStat(1) + "</color>";
                    if (xml.itemDB.stats.damage_num != 8)
                        upgradeText[0].text = data[xml.itemDB.stats.damage_num + 1]["statprice"] + " 원";
                    else { upgradeText[0].text = "최대"; }
                }
                else if (xml.itemDB.wallet.cash < long.Parse(data[xml.itemDB.stats.damage_num + 1]["statprice"].ToString()))
                {
                    sound.miss.Play();
                    single.SetAlert("가지고 있는 현금이 부족합니다.", Color.black);
                }
                else if (xml.itemDB.stats.damage_num == 8)
                {
                    sound.miss.Play();
                    single.SetAlert("해당 스텟이 최대치입니다.\n코어를 통해 더 향상시킬 수 있습니다.", Color.black);
                }
                break;
            case 1://크확
                if (xml.itemDB.wallet.cash >= long.Parse(data[xml.itemDB.stats.criticalpercent_num + 1]["statprice"].ToString()) && xml.itemDB.stats.criticalpercent_num < 19)
                {
                    sound.upgrade.Play();
                    xml.itemDB.wallet.cash -= long.Parse(data[xml.itemDB.stats.criticalpercent_num + 1]["statprice"].ToString());
                    xml.itemDB.stats.criticalpercent_num++;
                    xml.itemDB.stats.SetDamage();
                    stats[2].text = "깨달음 확률 : " + data[xml.itemDB.stats.criticalpercent_num]["stat"] + "<color=red>" + " + " + xml.itemDB.stats.GetCoreStat(2) + "</color>" + "%";
                    if (xml.itemDB.stats.criticalpercent_num != 19)
                        upgradeText[1].text = data[xml.itemDB.stats.criticalpercent_num + 1]["statprice"] + " 원";
                    else { upgradeText[1].text = "최대"; }
                }
                else if (xml.itemDB.wallet.cash < long.Parse(data[xml.itemDB.stats.criticalpercent_num + 1]["statprice"].ToString()))
                {
                    sound.miss.Play();
                    single.SetAlert("가지고 있는 현금이 부족합니다.", Color.black);
                }
                else if (xml.itemDB.stats.criticalpercent_num == 19)
                {
                    sound.miss.Play();
                    single.SetAlert("해당 스텟이 최대치입니다.\n코어를 통해 더 향상시킬 수 있습니다.", Color.black);
                }
                break;
            case 2://크뎀
                if ( xml.itemDB.stats.criticaldamage_num < 25 && xml.itemDB.wallet.cash >= long.Parse(data[xml.itemDB.stats.criticaldamage_num + 1]["statprice"].ToString()) )
                {
                    sound.upgrade.Play();
                    xml.itemDB.wallet.cash -= long.Parse(data[xml.itemDB.stats.criticaldamage_num + 1]["statprice"].ToString());
                    xml.itemDB.stats.criticaldamage_num++;
                    xml.itemDB.stats.SetDamage();
                    stats[3].text = "깨달음 증가량 : " + data[xml.itemDB.stats.criticaldamage_num]["stat"] + "<color=red>" + " + " + xml.itemDB.stats.GetCoreStat(3) + "</color>" + "배";
                    if (xml.itemDB.stats.criticaldamage_num != 25)
                        upgradeText[2].text = data[xml.itemDB.stats.criticaldamage_num + 1]["statprice"] + " 원";
                    else { upgradeText[2].text = "최대"; }
                }
                else if (xml.itemDB.wallet.cash < long.Parse(data[xml.itemDB.stats.criticaldamage_num + 1]["statprice"].ToString()))
                {
                    sound.miss.Play();
                    single.SetAlert("가지고 있는 현금이 부족합니다.", Color.black);
                }
                else if (xml.itemDB.stats.criticaldamage_num == 25)
                {
                    sound.miss.Play();
                    single.SetAlert("해당 스텟이 최대치입니다.\n코어를 통해 더 향상시킬 수 있습니다.", Color.black);
                }
                break;
            case 3://피뎀
                if ( xml.itemDB.stats.feverdamage_num < 25 && xml.itemDB.wallet.cash >= long.Parse(data[xml.itemDB.stats.feverdamage_num + 1]["statprice"].ToString()) )
                {
                    sound.upgrade.Play();
                    xml.itemDB.wallet.cash -= long.Parse(data[xml.itemDB.stats.feverdamage_num + 1]["statprice"].ToString());
                    xml.itemDB.stats.feverdamage_num++;
                    xml.itemDB.stats.SetDamage();
                    stats[4].text = "피버 증가량 : " + data[xml.itemDB.stats.feverdamage_num]["stat"] + "배";
                    if (xml.itemDB.stats.feverdamage_num != 25)
                        upgradeText[3].text = data[xml.itemDB.stats.feverdamage_num + 1]["statprice"] + " 원";
                    else { upgradeText[3].text = "최대"; }
                }
                else if (xml.itemDB.wallet.cash < long.Parse(data[xml.itemDB.stats.feverdamage_num + 1]["statprice"].ToString()))
                {
                    sound.miss.Play();
                    single.SetAlert("가지고 있는 현금이 부족합니다.", Color.black);
                }
                else if (xml.itemDB.stats.feverdamage_num == 25)
                {
                    sound.miss.Play();
                    single.SetAlert("해당 스텟이 최대치입니다.\n코어를 통해 더 향상시킬 수 있습니다.", Color.black);
                }
                break;
        }
    }
    public void ExitClick()
    {
        sound.btnclick.Play();
            SceneManager.LoadScene("MainScene");
        
    }
   
}
