using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class ClickWork : MonoBehaviour
{
    private SoundSingleTon sound;

    #region BASIC UI DO NOT FIX
    public GameObject[] ui;
    public GameObject tutorial;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public TextMeshProUGUI time;
    public Text job;
    public Text salary;
    public TextMeshProUGUI property;
    public Text giftnum;
    public Slider workslider;
    public TextMeshProUGUI figure;
    public Text shield_num;
    public Slider feverslider;
    public TextMeshProUGUI feverfigure;
    private int level = 0;
    private float needExp = 100;
    bool locker;
    bool fever;
    bool critical;
    int fevergage;//max 35
    public GameObject[] effect;// 0 salary 1 teamboss 2 supervisor 3 boss 4 nojob 
    Text effectText;
    public GameObject[] character;//0 salary 1 teamboss 2 supervisor 3 boss 4 nojob
    public GameObject giftPopup;
    List<Dictionary<string, object>> data;
    XMLManager xMLManager;

    public GameObject dailyGift;
    public GameObject content;
    Animator effectAnimator;//center effect
    public GameObject downpopup;
    public Text downtext;

    #endregion

    #region SALARY CHAR OBJECT & VARIABLE
    public GameObject[] internBtn;//0 copy 1 computer 2 water
    public GameObject[] workprefab;//copy computer water
    public GameObject worklist;//list prefab button
    public List<int> work_type = new List<int>();
    public List<Animator> workEffect = new List<Animator>();
    int click_count;
    int work_level;
    Coroutine workeffect_coroutine;
    #endregion

    #region SUPERVISOR OBJECT & VARIABLE
    public GameObject[] supervisorBtn;//0 me 1 left 2 right
    bool ok;
    bool boom;
    Coroutine ideaup_coroutine;
    #endregion

    #region TEAMBOSS OBJECT & VARIABLE
    public GameObject myworkBtn;
    public Slider[] teamboss_workgage;//ABCD
    private int[] worker_value;
    public GameObject[] workerbtn;
    float wait;
    Coroutine teambossgage_coroutine;
    Coroutine teambossup_coroutine;
    #endregion

    #region NOJOB OBJECT & VARIABLE
    public GameObject apply;
    Animator apply_animator;
    public GameObject nojobPopup;
    int apply_count;
    #endregion

    private void Start()
    {
        sound = SoundSingleTon.instance;
        sound.lobby.Stop();
        if (PlayerPrefs.GetString("load") == "first")
        {
            Debug.Log("first");
            tutorial.SetActive(true);
        }
        fever = false;
        critical = false;
        xMLManager = XMLManager.ins;
        data = CSVReader.Read("makemoneydatasheet3");
        locker = true;
        level = PlayerPrefs.GetInt("level", 0);
        needExp = (int)data[PlayerPrefs.GetInt("level", 0)]["exp"];
        StartCoroutine(Clock());
        StartCoroutine(FeverTime());

        #region INITIALIZE BASIC UI
        shield_num.text = "강등보호권 : "+xMLManager.itemDB.wallet.shield.ToString()+"개";
        figure.text = PlayerPrefs.GetFloat("exp", 0).ToString() + "/" + data[PlayerPrefs.GetInt("level", 0)]["exp"].ToString();
        feverfigure.text = fevergage + "/35";
        workslider.value = PlayerPrefs.GetFloat("exp", 0) / needExp;
        feverslider.value = fevergage / 35;

        job.text = data[PlayerPrefs.GetInt("level", 0)]["job"].ToString();
        salary.text = "월급 : " + data[PlayerPrefs.GetInt("level", 0)]["str"].ToString();



        property.text = xMLManager.itemDB.wallet.GetTotalProperty() + " 원";


        #endregion


        #region INITIALIZE BACKGROUND
        if (PlayerPrefs.GetInt("level", 0) <= 1)
        {
            CharLinkSetting(0);
        }
        else if (PlayerPrefs.GetInt("level", 0) <= 4)
        {
            CharLinkSetting(1);
        }
        else if (PlayerPrefs.GetInt("level", 0) <= 7)
        {
            CharLinkSetting(2);
        }
        else if (PlayerPrefs.GetInt("level", 0) <= 9)
        {
            CharLinkSetting(3);
        }
        else if (PlayerPrefs.GetInt("level", 0) == 10)
        {
            CharLinkSetting(4);
        }
        #endregion


    }

    private void Update()
    {
        giftnum.text = xMLManager.itemDB.dailyGiftList.Count.ToString();
        feverslider.value = fevergage / 35f;
        if (!fever)
        {
            feverfigure.text = fevergage + "/35";
        }
        else
        {
            feverfigure.text = "피버!!";
        }

        property.text = xMLManager.itemDB.wallet.GetTotalProperty() + " 원";

        if (effectAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Effect"))
        {
            //Debug.Log("true");
            effectAnimator.SetInteger("state", 0);
        }
    }
    
    IEnumerator FeverTime()
    {
        while (true)
        {
            if (fever)
            {
                if (fevergage >= 3)
                {
                    fevergage -= 3;
                }
                else
                {
                    fevergage = 0;
                    fever = false;
                }

            }
            else
            {
                if (fevergage >= 1)
                {
                    fevergage -= 1;
                }
                else
                {
                    fevergage = 0;
                }

            }
            yield return new WaitForSeconds(1f);
        }
    }//all type
    IEnumerator WorkEffect()
    {
        
        Debug.Log("work effcet count " + workEffect.Count + "click count" + click_count);

        if (workEffect[click_count] != null)
        {
            workEffect[click_count].SetBool("Click", true);
        }


        click_count++;
        if (click_count == work_type.Count)
        {
            Destroy(worklist.transform.GetChild(0).gameObject);
            if (work_level != 4) { work_level++; }
            else { work_level = 1; }
            character[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/intern3");
            SetWork();
            yield break;
        }


        yield return new WaitForSeconds(0.25f);
        character[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/intern3");
        Debug.Log("destroy in we");
        Destroy(worklist.transform.GetChild(0).gameObject);
        
    }  //for salarywork InternClick
    IEnumerator Clock()
    {
        while (true)
        {
            //time.text = System.DateTime.Now.ToShortTimeString();
            time.text = System.DateTime.Now.ToString("HH : mm");
            yield return new WaitForSeconds(60);
        }
    }//all type
    IEnumerator IdeaUp()
    {
        while (character[2].activeSelf)
        {

            int okno = UnityEngine.Random.Range(0, 2);
            int bomb = UnityEngine.Random.Range(0, 2);

            if (okno == 0)
            {
                ok = true; supervisorBtn[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("background/Ok");
            }

            else
            {
                ok = false; supervisorBtn[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("background/No");
            }


            int side = UnityEngine.Random.Range(1, 3);
            if (bomb == 0)
            {
                boom = false; supervisorBtn[side].GetComponent<Image>().sprite = Resources.Load<Sprite>("background/idea_yellow");
            }
            else
            {
                boom = true; supervisorBtn[side].GetComponent<Image>().sprite = Resources.Load<Sprite>("background/bomb");
            }
            supervisorBtn[side].SetActive(true);


            yield return new WaitForSeconds(0.4f);

            supervisorBtn[0].SetActive(true);

            yield return new WaitForSeconds(1f);
            if (ok && !boom)//ok 이고 붐이 아닌데 안눌렀다 -> miss
            {
                fevergage = 0;
                fever = false;
                Miss_LevelDown();
                MissEffect();
            }
            else//나머지는
            {
                GetFeverGage();
                GetExpEffect();
                Compute_GetExp();
            }

            Debug.Log("endroutine");
            supervisorBtn[0].SetActive(false);
            supervisorBtn[1].SetActive(false);
            supervisorBtn[2].SetActive(false);

        }
    }
    IEnumerator TeamBossWorkUp()
    {
        while (character[1].activeSelf)
        {
            int random = UnityEngine.Random.Range(0, 2);
            if (random == 0)
            {
                ok = true;
                myworkBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/OK");
            }
            else
            {
                ok = false;
                myworkBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("background/No");
            }
            myworkBtn.SetActive(true);

            yield return new WaitForSeconds(wait);

            if (ok)
            {
                fevergage = 0;
                fever = false;
                Miss_LevelDown();
                MissEffect();
            }
            else
            {
                GetFeverGage();
                GetExpEffect();
                Compute_GetExp();
            }
            myworkBtn.SetActive(false);
        }
    }
    IEnumerator TeamBoss_WorkGage()//ABCD
    {
        while (character[1].activeSelf)
        {
            bool overwork = false;
            int index = UnityEngine.Random.Range(0, worker_value.Length);
            if (teamboss_workgage[index].value != 1)
            {
                worker_value[index]++;
                teamboss_workgage[index].value = worker_value[index] / 10f;
                
            }
           
            for (int i = 0; i < worker_value.Length; i++)
            {
                if (teamboss_workgage[index].value == 1)
                {
                    overwork = true;
                    if (wait > 0.4f)
                    {
                        wait -= 0.1f;
                    }
                }
            }
            if (!overwork)
            {
                if (wait < 1f)
                {
                    wait += 0.1f;
                }

            }

            yield return new WaitForSeconds(0.2f);
        }
    }
    private void GetExpEffect()
    {
        if (!fever)
        {
            if (!critical)
            {
                effectText.text = "좋아요!"; effectAnimator.SetInteger("state", 1);
            }             
            else
            {
                effectText.text = "좋아요!"; effectAnimator.SetInteger("state", 3);
            }
        }

        else { effectText.text = "피버!"; effectAnimator.SetInteger("state", 2); }


        if (effectAnimator.GetInteger("state") == 1)
        {
            Debug.Log("play");
            effectAnimator.Play("Effect", -1, 0f);
        }
        else if (effectAnimator.GetInteger("state") == 2)
        {
            Debug.Log("play");
            effectAnimator.Play("Fever", -1, 0f);
        }
        else if (effectAnimator.GetInteger("state") == 3)
        {
            Debug.Log("play");
            effectAnimator.Play("Critical", -1, 0f);
        }
    }
    private void MissEffect()
    {
        effectText.text = "다시!";
        effectAnimator.SetInteger("state", 3);
        if (effectAnimator.GetInteger("state") == 3)
        {
            Debug.Log("play");
            effectAnimator.Play("Miss", -1, 0f);
        }
    }
    private void GetFeverGage()
    {
        if (!fever)
        {
            if (fevergage <= 33)
            {
                fevergage += 2;
            }
            else
            {
                fevergage = 35;
            }
            if (fevergage == 35)
            {
                fever = true;
            }

        }
    }
    private void Compute_GetExp()
    {
        sound.good.Play();
        if (UnityEngine.Random.Range(0, 100) < xMLManager.itemDB.GetCriticalPercent())//크리티컬
        {
            critical = true;
            if (character[0].activeSelf)
            {
                SetInternImage(work_type[click_count], true);
            }
            
            if (fever)
            {
                PlayerPrefs.SetFloat("exp", PlayerPrefs.GetFloat("exp", 0) + xMLManager.itemDB.GetDamage() * xMLManager.itemDB.GetCriticalDamage() * xMLManager.itemDB.GetFeverDamage());
                
            }
            else
            {
                PlayerPrefs.SetFloat("exp", PlayerPrefs.GetFloat("exp", 0) + xMLManager.itemDB.GetDamage() * xMLManager.itemDB.GetCriticalDamage());
            }
        }
        else
        {
            critical = false;
            if (character[0].activeSelf)
            {
                SetInternImage(work_type[click_count], false);
            }

                
            if (fever) { PlayerPrefs.SetFloat("exp", PlayerPrefs.GetFloat("exp", 0) + xMLManager.itemDB.GetDamage() * xMLManager.itemDB.GetFeverDamage()); }
            else { PlayerPrefs.SetFloat("exp", PlayerPrefs.GetFloat("exp", 0) + xMLManager.itemDB.GetDamage()); }
        }

        if (PlayerPrefs.GetFloat("exp", 0) > needExp)
        {
            LevelUp(PlayerPrefs.GetInt("level", 0) + 1);
        }
        workslider.value = PlayerPrefs.GetFloat("exp", 0) / needExp;
        figure.text = PlayerPrefs.GetFloat("exp", 0).ToString() + "/" + needExp.ToString();
    }
    private void Miss_LevelDown()
    {
        sound.miss.Play();
        int ran = UnityEngine.Random.Range(0, 100);
        if (ran < int.Parse(data[PlayerPrefs.GetInt("level")]["downpercent"].ToString()) && xMLManager.itemDB.wallet.shield == 0)//강등
        {

            ran = UnityEngine.Random.Range(0, 100);
            if (ran < 5)//연봉 삭제 직급-level 10
            {
                PlayerPrefs.SetInt("combackLevel", PlayerPrefs.GetInt("level", 0));
                PlayerPrefs.SetInt("level", 11);
                LevelDown();
            }
            else//강등
            {
                LevelDown();
            }

        }
        else if (ran < int.Parse(data[PlayerPrefs.GetInt("level")]["downpercent"].ToString()) && xMLManager.itemDB.wallet.shield >0)//강등방어
        {
            xMLManager.itemDB.wallet.shield = xMLManager.itemDB.wallet.shield - 1;
            downpopup.SetActive(true);
            downtext.text = "강등 방어권을 사용하였습니다.\n남은 갯수 : "+ xMLManager.itemDB.wallet.shield.ToString();
            shield_num.text = "강등보호권 : " + xMLManager.itemDB.wallet.shield.ToString() + "개";
            Time.timeScale = 0;
        }

    }
    private void LevelUp(int level)//In compute_getexp()
    {
        
        PlayerPrefs.SetInt("level", level);
        if (PlayerPrefs.GetInt("level", 0) <= 1)
        {
            //NOT EXIST COROUTINE TO STOP
            CharLinkSetting(0);
        }
        else if (PlayerPrefs.GetInt("level", 0) <= 4)//0 ---> 1 or 1 ---> 1
        {
            character[0].SetActive(false);

            if (workeffect_coroutine != null)
            {
                StopCoroutine(workeffect_coroutine);
                workeffect_coroutine = null;
            }

            

            CharLinkSetting(1);// if null start coroutine}
            
        }
        else if (PlayerPrefs.GetInt("level", 0) <= 7)//1 ---> 2 or 2 ---> 2
        {
            if (teambossgage_coroutine != null)
            {
                StopCoroutine(teambossgage_coroutine);
                teambossgage_coroutine = null;
            }
            if (teambossup_coroutine != null)
            {
                StopCoroutine(teambossup_coroutine);
                teambossup_coroutine = null;
            }
            character[1].SetActive(false);
            CharLinkSetting(2);// if null start coroutine
        }
        else if (PlayerPrefs.GetInt("level", 0) <= 9)//2 -> 3 or 3 -> 3
        {
            if (ideaup_coroutine != null)
            {
                StopCoroutine(ideaup_coroutine);
                ideaup_coroutine = null;

            }
            character[2].SetActive(false);
            
            CharLinkSetting(3);// if null start coroutine

        }

        needExp = (int)data[PlayerPrefs.GetInt("level", 0)]["exp"];
        salary.text = "월급 : " + data[PlayerPrefs.GetInt("level", 0)]["str"].ToString();
        job.text = data[PlayerPrefs.GetInt("level", 0)]["job"].ToString();
        workslider.value = PlayerPrefs.GetFloat("exp", 0) / needExp;
        figure.text = PlayerPrefs.GetFloat("exp", 0).ToString() + "/" + needExp.ToString();
        downpopup.SetActive(true);
        downtext.text = "'" + data[PlayerPrefs.GetInt("level")]["job"].ToString() + "' 으로 진급하셨습니다.\n\n" + "<size=30>높이 올라갈수록 떨어지기 쉽습니다.\n더 신경써야 해요 :)</size>";
        sound.bgm.Stop();
        sound.promote.Play();
        Time.timeScale = 0;
    }
    private void LevelDown()//In Miss_LevelDown()
    {
        
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 0) - 1);
        PlayerPrefs.SetFloat("exp", float.Parse(data[PlayerPrefs.GetInt("level") - 1]["exp"].ToString()));
        if (PlayerPrefs.GetInt("level", 0) <= 1)//샐러리로 강등
        {
            if (teambossgage_coroutine != null)
            {
                StopCoroutine(teambossgage_coroutine);
                teambossgage_coroutine = null;
            }
            if (teambossup_coroutine != null)
            {
                StopCoroutine(teambossup_coroutine);
                teambossup_coroutine = null;
            }
            character[1].SetActive(false);
            CharLinkSetting(0);
        }
        else if (PlayerPrefs.GetInt("level", 0) <= 4)//팀보스로 강등
        {
            if (ideaup_coroutine != null)
            {
                StopCoroutine(ideaup_coroutine);
                ideaup_coroutine = null;

            }
            character[2].SetActive(false);
            CharLinkSetting(1);


        }
        else if (PlayerPrefs.GetInt("level", 0) <= 7)//슈퍼바이저로 강등
        {

            character[3].SetActive(false);
            CharLinkSetting(2);
        }
        else if (PlayerPrefs.GetInt("level", 0) <= 9)//보스로 강등
        {            
            CharLinkSetting(3);
        }
        else if (PlayerPrefs.GetInt("level", 0) == 10)//실직자로 강등
        {
            if (teambossgage_coroutine != null)
            {
                Debug.Log("tbg");
                StopCoroutine(teambossgage_coroutine);
                teambossgage_coroutine = null;
            }
            if (teambossup_coroutine != null)
            {
                Debug.Log("tbu");
                StopCoroutine(teambossup_coroutine);
                teambossup_coroutine = null;
            }
            if (ideaup_coroutine != null)
            {
                Debug.Log("iup");
                StopCoroutine(ideaup_coroutine);
                ideaup_coroutine = null;

            }
            for (int i = 0; i < character.Length - 1; i++)
            {
                if (character[i].activeSelf)
                {
                    character[i].SetActive(false);
                    break;
                }
            }
            PlayerPrefs.SetFloat("exp", 0);
            nojobPopup.SetActive(true);
            CharLinkSetting(4);
        }
        needExp = (int)data[PlayerPrefs.GetInt("level", 0)]["exp"];
        salary.text = "월급 : " + data[PlayerPrefs.GetInt("level", 0)]["str"].ToString();
        job.text = data[PlayerPrefs.GetInt("level", 0)]["job"].ToString();
        workslider.value = PlayerPrefs.GetFloat("exp", 0) / needExp;
        figure.text = PlayerPrefs.GetFloat("exp", 0).ToString() + "/" + needExp.ToString();
        downpopup.SetActive(true);
        downtext.text = "'" + data[PlayerPrefs.GetInt("level")]["job"].ToString() + "' 으로 강등되었습니다.\n\n" + "<size=30>일의 속도도 중요하지만\n정확하게 하는 것이 더 중요합니다.^^</size>";

        sound.bgm.Stop();
        sound.demote.Play();
        Time.timeScale = 0;

    }
    private void SetInternImage(int type, bool critical)
    {
        switch (type)
        {
            case 0://copy
                if (critical)
                {
                    character[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/clickPrint_2");
                }
                else
                {
                    character[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/clickPrint_1");
                }
                break;
            case 1://computer
                if (critical)
                {
                    character[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/clickCom_2");
                }
                else
                {
                    character[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/clickCom_1");
                }
                break;
            case 2://water
                if (critical)
                {
                    character[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/clickWater_2");
                }
                else
                {
                    character[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/clickWater_1");
                }
                break;
        }
    }
    public void InternWork(GameObject gameObject)// copy computer water // routine
    {
        Debug.Log("click");
       
        if (locker)
        {
            if (gameObject == internBtn[work_type[click_count]])
            {
               
                Compute_GetExp();
                Debug.Log("same start coroutine");
                workeffect_coroutine = StartCoroutine(WorkEffect());

               

                GetFeverGage();

                GetExpEffect();


                


            }
            else//틀림
            {
                
                if (workeffect_coroutine != null)
                {
                    StopCoroutine(workeffect_coroutine);
                    workeffect_coroutine = null;
                }
                fevergage = 0;
                fever = false;


                Miss_LevelDown();
                MissEffect();

                work_level = 1;


                for (int i = 0; i < worklist.transform.childCount; i++)
                {
                    Debug.Log("destroy" + i);
                    Destroy(worklist.transform.GetChild(i).gameObject);
                }



                SetWork();
            }
        }




    }
    public void TeamBossWork()
    {
        if (ok)
        {
            GetFeverGage();
            GetFeverGage();
            GetExpEffect();
            Compute_GetExp();
        }
        else
        {
            fevergage = 0;
            fever = false;
            Miss_LevelDown();
            MissEffect();
        }

        myworkBtn.SetActive(false);


        if (teambossup_coroutine != null)
        {
            StopCoroutine(teambossup_coroutine);
            teambossup_coroutine = null;
        }
        if (teambossup_coroutine == null)
        {
            teambossup_coroutine = StartCoroutine(TeamBossWorkUp());

        }


    }
    public void SuperVisorWork()
    {
        if (!boom)
        {
            if (ok)
            {
                GetFeverGage();
                GetExpEffect();
                Compute_GetExp();
            }

            else
            {
                fevergage = 0;
                fever = false;
                Miss_LevelDown();
                MissEffect();
            }

        }
        else
        {
            fevergage = 0;
            fever = false;
            Miss_LevelDown();
            MissEffect();
        }


        supervisorBtn[0].SetActive(false);
        supervisorBtn[1].SetActive(false);
        supervisorBtn[2].SetActive(false);


        if (ideaup_coroutine != null)
        {
            StopCoroutine(ideaup_coroutine);
            ideaup_coroutine = null;
        }
        if (ideaup_coroutine == null)
        {
            ideaup_coroutine = StartCoroutine(IdeaUp());

        }
       

    }

    public void SetWork()
    {
        
        locker = false;
       
        work_type.Clear();
        workEffect.Clear();
        click_count = 0;
        switch (work_level)
        {
            case 1://틀리거나 , 중간에 시간이 다지난후 다시처음부터


                work_type = new List<int>(new int[3]);
                workEffect = new List<Animator>(new Animator[3]);
                break;
            case 2:
                work_type = new List<int>(new int[5]);
                workEffect = new List<Animator>(new Animator[5]);
                break;
            case 3:
                work_type = new List<int>(new int[7]);
                workEffect = new List<Animator>(new Animator[7]);
                break;
            case 4:
                work_type = new List<int>(new int[9]);
                workEffect = new List<Animator>(new Animator[9]);
                break;
        }
        Debug.Log("level" + work_level + "count" + work_type.Count);
        for (int i = 0; i < work_type.Count; i++)
        {
            Debug.Log(i + "make");
            work_type[i] = UnityEngine.Random.Range(0, internBtn.Length);
            GameObject type = Instantiate(workprefab[work_type[i]]);
            type.transform.SetParent(worklist.transform, false);
            workEffect[i] = type.GetComponent<Animator>();
        }
        locker = true;

    }
    public void GiftPopup()
    {
        sound.btnclick.Play();
        try
        {
            Debug.Log("listcount:" + xMLManager.itemDB);
            for (int i = 0; i < xMLManager.itemDB.dailyGiftList.Count; i++)
            {
                Debug.Log("count:" + i);
                GameObject item = Instantiate(dailyGift);
                item.name = i.ToString();
                item.transform.GetChild(1).name = xMLManager.itemDB.dailyGiftList[i].style;
                item.transform.GetChild(1).GetComponent<Text>().text = xMLManager.itemDB.dailyGiftList[i].style;

                switch (xMLManager.itemDB.dailyGiftList[i].style)
                {
                    case "cash":
                        item.transform.GetChild(1).GetComponent<Text>().text = "월급";
                        item.transform.GetChild(2).GetComponent<Text>().text = xMLManager.itemDB.dailyGiftList[i].cash.ToString();
                        item.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Store/cash");
                        break;
                    case "item":
                        item.transform.GetChild(1).GetComponent<Text>().text = "데일리 보상";
                        item.transform.GetChild(2).GetComponent<Text>().text = xMLManager.itemDB.dailyList[xMLManager.itemDB.dailyGiftList[i].item_num].itemName;
                        item.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("item/"+ xMLManager.itemDB.dailyGiftList[i].item_num.ToString());
                        break;
                    case "display":
                        item.transform.GetChild(1).GetComponent<Text>().text = "전시회 수익";
                        item.transform.GetChild(2).GetComponent<Text>().text = xMLManager.itemDB.dailyGiftList[i].dp_profit.ToString();
                        item.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Store/cash");
                        break;
                    case "payback":
                        item.transform.GetChild(1).GetComponent<Text>().text = "유찰금";
                        item.transform.GetChild(2).GetComponent<Text>().text = xMLManager.itemDB.dailyGiftList[i].cash.ToString();
                        item.transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Store/cash");
                        break;

                }
                item.transform.SetParent(content.transform, false);
            }
            giftPopup.SetActive(true);
            Time.timeScale = 0;
            Debug.Log(content.transform.childCount);
        }
        catch (Exception e) { Debug.Log(e); }

    }
    public void ExitGift()
    {
        Time.timeScale = 1;
        sound.btnclick.Play();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }

        giftPopup.SetActive(false);
    }
    public void ExitDownPopup()
    {
        sound.bgm.Play();
        downpopup.SetActive(false);
        if (PlayerPrefs.GetString("second", "false") == "false" && PlayerPrefs.GetInt("level", 0) == 2)//대리로 업
        {
            tutorial2.SetActive(true);
        }
        else if (PlayerPrefs.GetString("third", "false") == "false" && PlayerPrefs.GetInt("level", 0) == 5)//이사
        {
            tutorial3.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }
    public void TestLevel11BTN()
    {
        character[4].SetActive(false);
        LevelUp(PlayerPrefs.GetInt("combackLevel", 0));
    }
    private void CharLinkSetting(int charnum)
    {
        character[charnum].SetActive(true);
        if (sound.bgm != null)
        {
            sound.bgm.Stop();
        }
        effectAnimator = effect[charnum].transform.GetComponent<Animator>();
        effectText = effect[charnum].transform.GetComponent<Text>();
        switch (charnum)
        {
            case 0:
                if (workeffect_coroutine != null)
                {
                    StopCoroutine(workeffect_coroutine);
                    workeffect_coroutine = null;
                }
                sound.bgm = sound.stage1;
        
                sound.bgm.Play();
                for (int i = 0; i < worklist.transform.childCount; i++)
                {
                    Debug.Log("destroy" + i);
                    Destroy(worklist.transform.GetChild(i).gameObject);
                }
                click_count = 0;
                work_level = 1;
                SetWork();
                //workeffect_coroutine = StartCoroutine(WorkEffect());  // Call by InternWork() 
                break;
            case 1:
                sound.bgm = sound.stage2;
                sound.bgm.Play();
                worker_value = new int[4];
                for (int i = 0; i < 4; i++)
                {
                    worker_value[i] = 0;
                    teamboss_workgage[i].value = worker_value[i] / 10f;
                }
                wait = 1f;
                if(teambossup_coroutine == null)
                    teambossup_coroutine = StartCoroutine(TeamBossWorkUp());

                if(teambossgage_coroutine == null)
                    teambossgage_coroutine = StartCoroutine(TeamBoss_WorkGage());
                
                break;
            case 2:
                sound.bgm = sound.stage3;
                sound.bgm.Play();
                if (ideaup_coroutine == null)
                    ideaup_coroutine = StartCoroutine(IdeaUp());
                break;
            case 3:
                sound.bgm = sound.stage4;
                sound.bgm.Play();
                break;
            case 4:
                for (int i = 0;i< ui.Length; i++)
                {
                    ui[i].GetComponent<Image>().color = Color.gray;
                    if (ui[i].GetComponent<Button>() != null) { ui[i].GetComponent<Button>().interactable = false; }
                }
                apply_animator = apply.GetComponent<Animator>();
                apply_count = 0;
                sound.bgm = sound.stage5;
                sound.bgm.Play();
                break;
        }
    }
    public void TeamGageWorkerClick(GameObject btn)
    {
        for (int i = 0; i < workerbtn.Length; i++)
        {
            if (btn == workerbtn[i])
            {
                worker_value[i] = 0;
                teamboss_workgage[i].value = worker_value[i] / 10f;
                break;
            }
        }
    }
    public void NojobClick()
    {

        if (apply_count < 4)
        {
            apply_count++;           
        }
        else
        {
            apply_count = 0;           
        }
        apply.GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/apply" + apply_count.ToString());

        PlayerPrefs.SetFloat("exp", PlayerPrefs.GetFloat("exp", 0) + 1);
        workslider.value = PlayerPrefs.GetFloat("exp", 0) / needExp;
        figure.text = PlayerPrefs.GetFloat("exp", 0).ToString() + "/" + needExp.ToString();

        if (PlayerPrefs.GetFloat("exp", 0) == needExp)
        {
            character[4].SetActive(false);
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("combackLevel", 0));
            PlayerPrefs.SetFloat("exp", float.Parse(data[PlayerPrefs.GetInt("level") - 1]["exp"].ToString()));
            LevelUp(PlayerPrefs.GetInt("combackLevel", 0));//levelup에서 setint level
        }
       
    }
    public void NojobPopupExit()
    {
        nojobPopup.SetActive(false);
    }
}
