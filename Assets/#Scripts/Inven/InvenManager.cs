using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
public class InvenManager : MonoBehaviour {
    XMLManager xMLManager;
    Single single;
    SoundSingleTon sound;
    // List<Dictionary<string, object>> data;
    public GameObject coreselectBlind;
    public GameObject tut;

    public GameObject[] inven;//012345
    public Text[] itemquan;
    public GameObject[] dpBtn;
    public GameObject popup;
    public Canvas canvas;
    public GameObject prefab;
    string str = "NULL";
    bool mix;
    bool coremode;
    int corenum;//0,1,2
    public GameObject dpPopup;//2 icon 3 textmesh
    
    Image dpicon;
    TextMeshProUGUI dptext;

    public GameObject[] page;
    public Text pagenum;
    int pagetext;

    public GameObject[] core;//0 button 1 name 2 option
    public Text[] corename;
    public Text[] coreoption;

    public GameObject mixpopup;
    public GameObject[] mixitem;
    public Button mix_btn;
    private int mix_num;//0 and 1
    private int mix_first;
    private int mix_second;

    private Animator whiteblind_1;//mixitem 0 child
    private Animator whiteblind_2;//mixitem 1 child
    public Animator blind;//animator component
    public GameObject resultpopup;
    public Text resultText;

    int resultnum;
    bool destroy;
	// Use this for initialization
	void Start () {
        destroy = false;
        if (PlayerPrefs.GetString("inven", "false") == "false")
        {
            tut.SetActive(true);
        }
   //     data = CSVReader.Read("makemoneydatasheet3");
        whiteblind_1 = mixitem[0].transform.GetChild(0).gameObject.GetComponent<Animator>();
        whiteblind_2 = mixitem[1].transform.GetChild(0).gameObject.GetComponent<Animator>();
        blind = blind.GetComponent<Animator>();
        pagetext = 1;
        mix_num = 0;
        resultnum = 0;
        pagenum.text = pagetext.ToString();
        mix = false;
        coremode = false;
        xMLManager = XMLManager.ins;
        single = Single.instance;
        sound = SoundSingleTon.instance;
        sound.bgm.Stop();
        dpicon = dpPopup.transform.GetChild(2).GetComponent<Image>();
        dptext = dpPopup.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        if (PlayerPrefs.GetInt("level", 0) % 11 <= 3)//코어 한칸
        {
            
            core[1].GetComponent<Button>().interactable = false;
            core[1].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/lock") as Sprite;
            corename[1].text = "5등급 이상\n사용가능";
            coreoption[1].text = "--";

            core[2].GetComponent<Button>().interactable = false;
            core[2].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/lock") as Sprite;
            corename[2].text = "8등급 이상\n사용가능";
            coreoption[2].text = "--";
        }
        else if (PlayerPrefs.GetInt("level", 0) % 11 <= 7)//코어 두칸 0 1
        {
            core[2].GetComponent<Button>().interactable = false;
            core[2].transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/lock") as Sprite;
            corename[2].text = "8등급 이상\n사용가능";
            coreoption[2].text = "--";
        }
        else if (PlayerPrefs.GetInt("level", 0) % 11 <= 9)
        {
            //all open
        }
        else
        {
            //==10
        }
        for (int i = 0; i < core.Length; i++)
        {
            if (core[i].GetComponent<Button>().interactable)
            {
                CoreUI(i);
            }         
        }
        for (int i = 0; i < inven.Length; i++)
        {
            for (int j = 0; j < inven[i].transform.childCount ; j++)
            {
                itemquan[i* inven[i].transform.childCount + j ] = inven[i].transform.GetChild(j).GetChild(0).GetComponent<Text>();
            }
        }

      
        for (int i = 0; i < xMLManager.itemDB.dailyList.Count; i++)
        {
            itemquan[i].text =  xMLManager.itemDB.dailyList[i].quan.ToString();
        }
        for (int i = 0; i < 5; i++)
        {
            ActiveDpButton(i);
        }
      

    }
	

    public void PageUp()
    {
        if (pagetext == page.Length)
        {
            //nothing
        }
        else
        {
            sound.btnclick.Play();
            page[pagetext-1].SetActive(false);
            pagetext++;
            page[pagetext-1].SetActive(true);
            
        }
        pagenum.text = pagetext.ToString();
    }
    public void PageDown()
    {
        if (pagetext == 1)
        {
            //nothing
        }
        else
        {
            sound.btnclick.Play();
            page[pagetext-1].SetActive(false);
            pagetext--;
            page[pagetext-1].SetActive(true);

        }
        pagenum.text = pagetext.ToString();
    }
    public void BackButton()
    {
        sound.btnclick.Play();
        if (mix)
        {
            mix = false;
            if (mix_num == 1)
            {
                xMLManager.itemDB.dailyList[mix_first].quan++;
                itemquan[mix_first].text = (xMLManager.itemDB.dailyList[mix_first].quan).ToString();
            }
            else if (mix_num == 2)
            {
                xMLManager.itemDB.dailyList[mix_first].quan++;
                xMLManager.itemDB.dailyList[mix_second].quan++;
                itemquan[mix_first].text = (xMLManager.itemDB.dailyList[mix_first].quan).ToString();
                itemquan[mix_second].text = (xMLManager.itemDB.dailyList[mix_second].quan).ToString();
            }
            mixitem[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
            mixitem[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
            mixitem[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");


            mix_btn.interactable = false;
            mix_num = 0;

            mixpopup.SetActive(false);
        }
        SceneManager.LoadScene("MainScene");
    }
    public void ItemMix(int type,long avg)
    {
        List<long> list = new List<long>();
        List<float> value_avg = new List<float>();
        float biggest=0;
        float unit = 0;
        for (int i = 0; i < 9; i++)
        {
            if (list.Contains(xMLManager.itemDB.dailyList[(type * 9) + i].max_worthy)==false)
            {
                list.Add(xMLManager.itemDB.dailyList[(type * 9) + i].max_worthy);
                Debug.Log(xMLManager.itemDB.dailyList[(type * 9) + i].max_worthy);
            }
            
        }//get value in list

        for (int j = 0; j < list.Count; j++)
        {
            if (list[j] >= avg)
            {
                value_avg.Add(((float)list[j] / (float)avg));
            }
            else
            {
                value_avg.Add(((float)avg / (float)list[j]));
            }
        }//compute value/avg

        for (int k = 0; k < value_avg.Count; k++)
        {
            if (biggest < value_avg[k])
            {
                biggest = value_avg[k];
            }
        }//get biggest
        
        for (int k = 0; k < value_avg.Count; k++)
        {
            value_avg[k] = (biggest / value_avg[k]);
            unit += value_avg[k];

        }
        unit = 100 / unit;//get unit
        value_avg[0] = value_avg[0] * unit;
        Debug.Log(value_avg[0]);
        for (int k = 1; k < value_avg.Count; k++)
        {
            value_avg[k] = (value_avg[k] * unit) + value_avg[k-1];
            Debug.Log(value_avg[k]);
        }//get percent
        


            float random = Random.Range(0f, 100f);

        for (int i = 0; i < list.Count; i++)
        {
            if (random < value_avg[i])
            {
                biggest = list[i];
                Debug.Log(biggest);
                break;
            }
        }
        list.Clear();
        for (int j = type * 9; j < (type + 1) * 9; j++)
        {
            if (biggest == xMLManager.itemDB.dailyList[j].max_worthy)
            {
                list.Add(j);
                Debug.Log(j);
            }
        }
        int index = Random.Range(0, list.Count);
        resultnum = (int)list[index];
        xMLManager.itemDB.dailyList[(int)list[index]].quan++;
        //itemquan[resultnum].text = (xMLManager.itemDB.dailyList[resultnum].quan).ToString();
        mixitem[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/"+(int)list[index]);
        resultText.text = xMLManager.itemDB.dailyList[(int)list[index]].itemName + " 을 획득하셨습니다.\n" +"<color=black>"+ "예상가치 : " + xMLManager.itemDB.dailyList[(int)list[index]].min_worthy+" 원"+"</color>";

    }
    public void CoreMix(int success, int keep)
    {
        int random = Random.Range(0, 100);
        int itemnum = 0;
        

        //success
        if (random < success)
        {
            if (random % 2 == 0)
            {
                itemnum = mix_first + 1;
                xMLManager.itemDB.dailyList[mix_first + 1].quan++;//1번 업글

            }
            else
            {
                xMLManager.itemDB.dailyList[mix_second + 1].quan++;//2번 업글
                itemnum = mix_second + 1;
            }
            resultnum = itemnum;
            //itemquan[itemnum].text = (xMLManager.itemDB.dailyList[itemnum].quan).ToString();

            mixitem[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/" + itemnum);
            resultText.text = "<color=black>코어강화 성공!</color>\n"+ xMLManager.itemDB.dailyList[itemnum].itemName + " 을 획득하셨습니다.";
        }
        else if (random < keep)
        {
            destroy = true;
            if (random % 2 == 0)
            {
                itemnum = mix_first;
                xMLManager.itemDB.dailyList[mix_first].quan++;//1번 유지
                resultText.text = "<color=black>" + xMLManager.itemDB.dailyList[mix_second].itemName + " 이 파괴되었습니다.</color>";
            }
            else
            {
                itemnum = mix_second;
                xMLManager.itemDB.dailyList[mix_second].quan++;//2번 유지
                resultText.text = "<color=black>" + xMLManager.itemDB.dailyList[mix_first].itemName + " 이 파괴되었습니다.</color>";

            }
            resultnum = itemnum;
            //itemquan[itemnum].text = (xMLManager.itemDB.dailyList[itemnum].quan).ToString();

            mixitem[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/" + itemnum);
            
        }
        else
        {
            //둘다파괴
            destroy = true;
            resultnum = mix_first;
            mixitem[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
            resultText.text = "<color=black>" + "두 코어 모두 파괴되었습니다.^^;;" + "</color>";
        }
       

       

        mix_btn.interactable = false;
        mix_num = 0;
        mix = false;
        //mixpopup.SetActive(false);
        StartCoroutine(Mix());

    }
    public void MixClick()
    {
        sound.btnclick.Play();
        if (mix_second / 9 == 5)//core mix
        {
            
            
            if (mix_first % 3 == 0 && mix_second % 3 == 0)//11
            {
               
                
                CoreMix(20, 90);
            }
            else if (mix_first % 3 == 0 && mix_second % 3 == 1)//12
            {
               

                
                CoreMix(20, 90);
            }
            else if (mix_first % 3 == 1 && mix_second % 3 == 0)//21
            {
            
                CoreMix(20, 90);
            }
            else if (mix_first / 3 == 1 && mix_second / 3 == 1)//22
            {
            
               
                CoreMix(20, 90);
            }
            else { sound.miss.Play();  single.SetAlert("최대레벨 코어가 포함되어 있습니다.",Color.black); }//최대레벨코어포함

         
          
         
        }
        else//itemmix
        {
           
          
            ItemMix(mix_second / 9, (xMLManager.itemDB.dailyList[mix_first].max_worthy + xMLManager.itemDB.dailyList[mix_second].max_worthy) / 2);
            //itemquan[mix_second].text = xMLManager.itemDB.dailyList[mix_second].quan.ToString();
            
            mix_num = 0;
            mix_btn.interactable = false;
            mix = false;

            //mixpopup.SetActive(false);
            StartCoroutine(Mix());
            
        }

    }
    IEnumerator Mix()
    {
        Debug.Log("one");
        whiteblind_1.SetBool("Mix", true);
        whiteblind_2.SetBool("Mix", true);
        blind.SetBool("blind", true);
        iTween.ShakePosition(mixitem[0], new Vector3(0.1f, 0.1f, 0), 2f);
        iTween.ShakePosition(mixitem[1], new Vector3(0.1f, 0.1f, 0), 2f);

        yield return new WaitForSeconds(2f);
        Debug.Log("two");
        mixitem[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
        mixitem[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
        whiteblind_2.SetBool("Mix", false);
        whiteblind_1.SetBool("Mix", false);
        blind.SetBool("blind", false);
        yield return new WaitForSeconds(1f);
        
        itemquan[resultnum].text = (xMLManager.itemDB.dailyList[resultnum].quan).ToString();
        resultpopup.SetActive(true);
        if (destroy)
        {
            sound.destroy.Play();
            destroy = false;
        }
        else
        {
            sound.gotcha.Play();
        }

    }
    public void ExitResultPopup()
    {
        sound.btnclick.Play();
        resultpopup.SetActive(false);
        mixpopup.SetActive(false);
    }
    public void FirstMixOut()
    {
        sound.btnclick.Play();
        if (mix_num == 1)
        {
            mixitem[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
            xMLManager.itemDB.dailyList[mix_first].quan++;
            itemquan[mix_first].text = (xMLManager.itemDB.dailyList[mix_first].quan).ToString();
            mix_num = 0;
        }

    }
    public void SecondMixOut()
    {
        sound.btnclick.Play();
        if (mix_num == 2)
        {
            mixitem[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
            xMLManager.itemDB.dailyList[mix_second].quan++;
            itemquan[mix_second].text = (xMLManager.itemDB.dailyList[mix_second].quan).ToString();
            mix_num = 1;
            mix_btn.interactable = false;
        }
    }
    public void CancelMix()
    {
        sound.btnclick.Play();
        mix = false;
        if (mix_num == 1)
        {
            xMLManager.itemDB.dailyList[mix_first].quan++;
            itemquan[mix_first].text = (xMLManager.itemDB.dailyList[mix_first].quan).ToString();
        }
        else if (mix_num == 2)
        {
            xMLManager.itemDB.dailyList[mix_first].quan++;
            xMLManager.itemDB.dailyList[mix_second].quan++;
            itemquan[mix_first].text = (xMLManager.itemDB.dailyList[mix_first].quan).ToString();
            itemquan[mix_second].text = (xMLManager.itemDB.dailyList[mix_second].quan).ToString();
        }
        mixitem[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
        mixitem[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
        mixitem[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");

        mix_btn.interactable = false;
        mix_num = 0;

        mixpopup.SetActive(false);
    }
    public void ListClick()
    {
        
        if (mix == false && coremode == false)
        {

            try { } catch (System.Exception e) { single.SetAlert(e.ToString(), Color.black); }
            sound.btnclick.Play();
            str = EventSystem.current.currentSelectedGameObject.name;
            //Debug.Log(button.gameObject);
            popup.transform.GetChild(3).GetComponent<Text>().text = "예상 가치\n" + xMLManager.itemDB.dailyList[int.Parse(str)].min_worthy + " 원 - " + xMLManager.itemDB.dailyList[int.Parse(str)].max_worthy + " 원";
            popup.transform.GetChild(2).GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
            popup.SetActive(true);
        }
        else if (mix)
        {
            str = EventSystem.current.currentSelectedGameObject.name;

          
            if (xMLManager.itemDB.dailyList[int.Parse(str)].quan == 0) { sound.miss.Play(); single.SetAlert("아이템을 보유하고 있지 않습니다. 아이템은 상점이나 경매장 또는 일일보상으로 획득할 수 있습니다.", Color.black); }
            else
            {
                if (mix_num == 0)
                {
                    sound.btnclick.Play();
                    mixitem[mix_num].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/" + str);

                    mix_first = int.Parse(str);
                    xMLManager.itemDB.dailyList[mix_first].quan--;
                    itemquan[mix_first].text = (xMLManager.itemDB.dailyList[mix_first].quan).ToString();
                    mix_num++;
                }
                else if (mix_num == 1)
                {
                    mix_second = int.Parse(str);

                    if (mix_first / 9 != mix_second / 9)
                    {
                        single.SetAlert("같은 종류 아이템만 합성 가능 합니다. 다시 선택해주세요.", Color.black);
                        sound.miss.Play();
                    }
                    else
                    {
                        sound.btnclick.Play();
                        mix_num = 2;
                        xMLManager.itemDB.dailyList[mix_second].quan--;
                        itemquan[mix_second].text = (xMLManager.itemDB.dailyList[mix_second].quan).ToString();
                        mixitem[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/" + str);
                        mix_btn.interactable = true;
                    }

                }
                else if (mix_num == 2 && (mix_first / 9 != int.Parse(str) / 9))
                {
                    single.SetAlert("같은 종류 아이템만 합성 가능 합니다. 다시 선택해주세요.", Color.black);
                    sound.miss.Play();
                }
                else if (mix_num == 2 && (mix_first / 9 == int.Parse(str) / 9))
                {
                    sound.btnclick.Play();
                    xMLManager.itemDB.dailyList[mix_second].quan++;
                    itemquan[mix_second].text = (xMLManager.itemDB.dailyList[mix_second].quan).ToString();

                    mix_second = int.Parse(str);

                    xMLManager.itemDB.dailyList[mix_second].quan--;
                    itemquan[mix_second].text = (xMLManager.itemDB.dailyList[mix_second].quan).ToString();
                    mixitem[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/" + str);
                    mix_btn.interactable = true;
                }
            }
          


        }
        else if (coremode)
        {
            coreselectBlind.SetActive(false);
            str = EventSystem.current.currentSelectedGameObject.name;
            if (int.Parse(str) < 45) { sound.miss.Play(); single.SetAlert("코어만 클릭가능", Color.black); }
            else if (xMLManager.itemDB.dailyList[int.Parse(str)].quan > 0)
            {
                sound.btnclick.Play();
                xMLManager.itemDB.dailyList[int.Parse(str)].quan--;//장착하면 - 해제하면 +
                itemquan[int.Parse(str)].text = xMLManager.itemDB.dailyList[int.Parse(str)].quan.ToString();

                xMLManager.itemDB.stats.core[corenum] = int.Parse(str);
                xMLManager.itemDB.stats.SetDamage();
                CoreUI(corenum);
            }
            else { sound.miss.Play(); single.SetAlert("코어를 보유하고 있지 않습니다. 코어는 상점또는 일일보상으로 획득할 수 있습니다.", Color.black); }
            coremode = false;
        }
       
    }
    private void CoreUI(int num)// num is core[] index
    {
        
        if (xMLManager.itemDB.stats.core[num] >= 45)//'core slot[num]' has coreitem
        {
            core[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/" + xMLManager.itemDB.stats.core[num]);
            switch (xMLManager.itemDB.stats.core[num])
            {

                case 45:
                    corename[num].text = "경험\n1단계";
                    coreoption[num].text = "경험증가량\n+1";
                    break;
                case 46:
                    corename[num].text = "경험\n2단계";
                    coreoption[num].text = "경험증가량\n+2";
                    break;
                case 47:
                    corename[num].text = "경험\n3단계";
                    coreoption[num].text = "경험증가량\n+4";
                    break;
                case 48:
                    corename[num].text = "깨달음확률\n1단계";
                    coreoption[num].text = "깨달음확률\n+10%";
                    break;
                case 49:
                    corename[num].text = "깨달음확률\n2단계";
                    coreoption[num].text = "깨달음확률\n+30%";
                    break;
                case 50:
                    corename[num].text = "깨달음확률\n3단계";
                    coreoption[num].text = "깨달음확률\n+50%";
                    break;
                case 51:
                    corename[num].text = "깨달음\n1단계";
                    coreoption[num].text = "깨달음증가량\n+150%";
                    break;
                case 52:
                    corename[num].text = "깨달음\n2단계";
                    coreoption[num].text = "깨달음증가량\n+250%";
                    break;
                case 53:
                    corename[num].text = "깨달음\n3단계";
                    coreoption[num].text = "깨달음증가량\n+300%";
                    break;
            }
        }
        else if (xMLManager.itemDB.stats.core[num] < 45)//it means core slot is empty(must value is 0)
        {
            core[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/nocore");
            corename[num].text = "장착 가능";
            coreoption[num].text = "--";
        }
        
    }
    public void CoreClick()//click core slot
    {
        sound.btnclick.Play();
        corenum = int.Parse(EventSystem.current.currentSelectedGameObject.name.ToString()) - 1;//0 1 2
        if (xMLManager.itemDB.stats.core[corenum] >= 45)
        {
            
            xMLManager.itemDB.dailyList[xMLManager.itemDB.stats.core[corenum]].quan++;//장착하면 - 해제하면 +
            itemquan[xMLManager.itemDB.stats.core[corenum]].text = xMLManager.itemDB.dailyList[xMLManager.itemDB.stats.core[corenum]].quan.ToString();
            xMLManager.itemDB.stats.core[corenum] = 0;//해제
            core[corenum].GetComponent<Image>().sprite = Resources.Load<Sprite>("Item/nocore");
            corename[corenum].text = "장착 가능";
            coreoption[corenum].text = "--";

        }
        else
        {
            coreselectBlind.SetActive(true);
            page[pagetext - 1].SetActive(false);
            pagetext = 1;
            pagenum.text = pagetext.ToString();
            page[pagetext - 1].SetActive(true);
            coremode = true;
        }
        
       
    }
    public void DPPopupClick()
    {
        sound.btnclick.Play();
        str = EventSystem.current.currentSelectedGameObject.name;
        switch (str)
        {
            case "Stone":
                dpicon.sprite = Resources.Load<Sprite>("Assets/Resources/Item/0.png");
                dptext.text = "돌멩이 전시회를 여시겠습니까?\n 10000000 원의 수익이\n예상됩니다.";
                break;
            case "Jewel":
                dpicon.sprite = Resources.Load<Sprite>("Assets/Resources/Item/9.png");
                dptext.text = "보석 전시회를 여시겠습니까?\n 50000000 원의 수익이\n예상됩니다.";
                break;
            case "Fossil":
                dpicon.sprite = Resources.Load<Sprite>("Assets/Resources/Item/18.png");
                dptext.text = "화석 전시회를 여시겠습니까?\n 90000000 원의 수익이\n예상됩니다.";
                break;
            case "Chinaware":
                dpicon.sprite = Resources.Load<Sprite>("Assets/Resources/Item/27.png");
                dptext.text = "도자기 전시회를 여시겠습니까?\n 200000000 원의 수익이\n예상됩니다.";
                break;
            case "Paint":
                dpicon.sprite = Resources.Load<Sprite>("Assets/Resources/Item/36.png");
                dptext.text = "그림 전시회를 여시겠습니까?\n 400000000 원의 수익이\n예상됩니다.";
                break;


        }
        dpPopup.SetActive(true);
    }
    public void SellClick()
    {

        if (xMLManager.itemDB.dailyList[int.Parse(str)].quan != 0)
        {
            sound.sell.Play();
            xMLManager.itemDB.dailyList[int.Parse(str)].quan--;
            itemquan[int.Parse(str)].text = xMLManager.itemDB.dailyList[int.Parse(str)].quan.ToString();
            popup.SetActive(false);
            int sellprice = Random.Range((int)xMLManager.itemDB.dailyList[int.Parse(str)].min_worthy, (int)xMLManager.itemDB.dailyList[int.Parse(str)].max_worthy);
            sellprice = (sellprice / 10000) * 10000;
            xMLManager.itemDB.wallet.cash += sellprice;
            single.SetAlert(xMLManager.itemDB.dailyList[int.Parse(str)].itemName + " 을 " + sellprice + " 원에 판매 하셨습니다.", Color.black);
        }
        else {
            sound.miss.Play();
            Debug.Log("quan 0 :<");
        }
     
    }
    public void DPClick()
    {
        sound.sell.Play();
        switch (str)
        {
            case "Stone":
                single.SetAlert("돌멩이 전시회를 시작했습니다.\n보상은 다음날 받으실 수 있습니다.",Color.black);
                Display(0);
                break;
            case "Jewel":
                single.SetAlert("보석 전시회를 시작했습니다.\n보상은 다음날 받으실 수 있습니다.", Color.black);
                Display(1);
                break;
            case "Fossil":
                single.SetAlert("화석 전시회를 시작했습니다.\n보상은 다음날 받으실 수 있습니다.", Color.black);
                Display(2);
                break;
            case "Chinaware":
                single.SetAlert("도자기 전시회를 시작했습니다.\n보상은 다음날 받으실 수 있습니다.", Color.black);
                Display(3);
                break;
            case "Paint":
                single.SetAlert("그림 전시회를 시작했습니다.\n보상은 다음날 받으실 수 있습니다.", Color.black);
                Display(4);
                break;

        }
        dpPopup.SetActive(false);
    }
    public void Display(int num)
    {
        xMLManager.itemDB.StartDisplay(num);
        for (int i = num * 9; i < (num + 1) * 9; i++)
        {
            xMLManager.itemDB.dailyList[i].quan--;
            itemquan[i].text = xMLManager.itemDB.dailyList[i].quan.ToString();
        }
        ActiveDpButton(num);
    }
    public void XButton()
    {
        sound.btnclick.Play();
        popup.SetActive(false);
        dpPopup.SetActive(false);
    }
    public void ActiveDpButton(int start)
    {

        if (xMLManager.itemDB.CheckCanDP((start*9)))
        {
            
            dpBtn[start].SetActive(true);
        }
        else { dpBtn[start].SetActive(false); }
    }
    public void MixModeOn()//click mix button
    {
        sound.btnclick.Play();
        if (!mix)
        {
            coremode = false;
            coreselectBlind.SetActive(false);
            mix = true;
            mixpopup.SetActive(true);
        }
        else
        {
            
                mix = false;
                if (mix_num == 1)
                {
                    xMLManager.itemDB.dailyList[mix_first].quan++;
                    itemquan[mix_first].text = (xMLManager.itemDB.dailyList[mix_first].quan).ToString();
                }
                else if (mix_num == 2)
                {
                    xMLManager.itemDB.dailyList[mix_first].quan++;
                    xMLManager.itemDB.dailyList[mix_second].quan++;
                    itemquan[mix_first].text = (xMLManager.itemDB.dailyList[mix_first].quan).ToString();
                    itemquan[mix_second].text = (xMLManager.itemDB.dailyList[mix_second].quan).ToString();
                }
                mixitem[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
                mixitem[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");
            mixitem[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("item/mixslot");

            mix_btn.interactable = false;
                mix_num = 0;

                mixpopup.SetActive(false);
            
            
        }
        
    }

    
}
