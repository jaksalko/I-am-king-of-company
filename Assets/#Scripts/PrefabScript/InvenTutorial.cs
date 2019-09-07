using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenTutorial : MonoBehaviour {
    int step;
    public Text tut_text;
    public GameObject[] ui;
    public GameObject mixbtn;
    public GameObject dpbtn;
    public GameObject mix;
    public GameObject dp;
    public GameObject sell;
    public GameObject munky;
    // Use this for initialization
    void Start()
    {
        step = 0;

    }

    public void TutorialNext()
    {
        switch (step)
        {
            case 0:
                tut_text.text = "이번에는 창고(인벤토리)에 대해 간단히 알려드릴게요.";
                break;
            case 1:
                tut_text.text = "창고에서는 가지고 있는\n물품(코어 포함)들을 <color=red>4가지 방법</color>으로\n다룰 수 있습니다.";
                break;
            case 2:
                tut_text.text = "첫번째로 '<color=#81c147>판매</color>'입니다.";
                break;
            case 3:
                ui[0].AddComponent<Canvas>();
                ui[0].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "'<color=#81c147>판매</color>'는 가지고 있는 물품을 판매하는 것인데요\n물품을 클릭하시면..";
                break;
            case 4:
                Destroy(ui[0].GetComponent<Canvas>());
                //munky.transform.localPosition = new Vector3(munky.transform.localPosition.x, -700f, munky.transform.localPosition.z);
                sell.SetActive(true);
                sell.AddComponent<Canvas>();
                sell.GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "물품을 팔기위한 <color=#81c147>팝업창</color>이 생성됩니다.";
                break;
            case 5:
                Destroy(sell.GetComponent<Canvas>());
                sell.transform.GetChild(3).gameObject.GetComponent<Text>().color = Color.white;
                sell.transform.GetChild(3).gameObject.AddComponent<Canvas>().overrideSorting = true;

                tut_text.text = "물품의 판매가는 해당물품의 <color=#85e9ea>최소가치</color>와 <color=red>최대가치</color> 사이에서 <color=#81c147>랜덤</color>하게 결정됩니다.";
                break;
            case 6:
                tut_text.text = "판매한 금액은 바로 <color=yellow>현금</color>으로 환산되므로 급한 경우 이용하면 유용하겠죠?";
                break;
            case 7:
                sell.transform.GetChild(3).gameObject.GetComponent<Text>().color = Color.black;
                Destroy(sell.transform.GetChild(3).gameObject.GetComponent<Canvas>());
                sell.SetActive(false);
                tut_text.text = "두번째는 '<color=#81c147>전시</color>'입니다.";
                break;
            case 8:
                dpbtn.SetActive(true);
                dpbtn.AddComponent<Canvas>();
                dpbtn.GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "<color=#81c147>전시</color>는 가지고 있는 물품들을 전시해서 <color=yellow>\n매일 보상</color>을 받을 수 있는 시스템인데요,\n'<color=red>전시 가능</color>'버튼을 클릭하시면..";
                break;
            case 9:
                
                
                dp.SetActive(true);
                dp.AddComponent<Canvas>();
                dp.GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "전시를 하기위한 <color=#81c147>팝업창</color>이 생성됩니다.";
                break;
            case 10:

                tut_text.text = "전시를 할 경우 <color=red>다음날 0시</color>에\n<color=yellow>보상함</color>으로 <color=yellow>보상</color>을 받으실 수 있습니다.\n각 카테고리마다 받을 수 있는 보상의 양이 다르니 참고하시기 바랍니다.";
                break;
            case 11:
                dp.SetActive(false);
                Destroy(dp.GetComponent<Canvas>());
                tut_text.text = "전시는 물품을 소모하여 보상을 받는 방식이 아닌, <color=red>회수형 방식</color>이니 해당 카테고리의 모든 물품을 모으신다면 <color=red>지속적</color>으로 보상을 받으실 수 있습니다.";
                break;
            case 12:
                ui[1].AddComponent<Canvas>();
                ui[1].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "<color=red>꼭 9개의 물품을 모아야\n'전시 가능'버튼이 활성화되는 점 잊지마세요!</color>";
                break;
            case 13:
                Destroy(ui[1].GetComponent<Canvas>());
                Destroy(dpbtn.GetComponent<Canvas>());
                dpbtn.SetActive(false);
                tut_text.text = "세번째로 <color=#81c147>'합성'</color>입니다.";
                break;
            case 14:
                mixbtn.AddComponent<Canvas>();
                mixbtn.GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "합성은 가지고 있는 <color=red>코어나 물품</color>을\n<color=#81c147>합성</color>하는 것인데요, '합성'버튼을 클릭하시면..";
                break;
            case 15:
                mix.AddComponent<Canvas>();
                mix.GetComponent<Canvas>().overrideSorting = true;
                mix.SetActive(true);
                tut_text.text = "하단에 <color=#81c147>합성 팝업창</color>이 나타납니다.";
                break;
            case 16:
                ui[2].AddComponent<Canvas>();
                ui[2].GetComponent<Canvas>().overrideSorting = true;
                ui[3].AddComponent<Canvas>();
                ui[3].GetComponent<Canvas>().overrideSorting = true;
                Destroy(mix.GetComponent<Canvas>());
                ui[5].AddComponent<Canvas>();
                ui[5].GetComponent<Canvas>().overrideSorting = true;
                ui[4].AddComponent<Canvas>();
                ui[4].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "합성을 위해서는 <color=red>같은 카테고리의 2개의 재료</color>가 필요합니다.";
                break;
            case 17:
                Destroy(ui[2].GetComponent<Canvas>());
                Destroy(ui[3].GetComponent<Canvas>());
                ui[1].AddComponent<Canvas>();
                ui[1].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "재료를 넣어준 뒤 합성을 하면\n같은 카테고리의 아이템을\n<color=yellow>랜덤</color>하게 얻을 수 있습니다.";
                break;
            case 18:
                Destroy(ui[1].GetComponent<Canvas>());
                ui[6].AddComponent<Canvas>();
                ui[6].GetComponent<Canvas>().overrideSorting = true;
                ui[7].AddComponent<Canvas>();
                ui[7].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "만약 <color=red>코어</color>를 합성하신다면,\n<color=red>더 높은 등급의 코어</color>를 획득하실 수 있습니다.\n하지만 코어가 <color=red>모두 파괴</color>될 수도 있으니 조심하세요!";
                break;
            case 19:
                Destroy(ui[4].GetComponent<Canvas>());
                Destroy(ui[5].GetComponent<Canvas>());
                Destroy(ui[6].GetComponent<Canvas>());
                Destroy(ui[7].GetComponent<Canvas>());
                Destroy(mixbtn.GetComponent<Canvas>());
                mix.SetActive(false);
                tut_text.text = "마지막으로 <color=#81c147>'코어장착'</color>입니다.";
                    break;
            case 20:
                ui[8].AddComponent<Canvas>();
                ui[8].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "'<color=red>코어</color>'는 <color=#81c147>업무능력</color>을 올려줄 수 있는 아주 좋은\n수단입니다. 코어의 종류는 <color=#FFCECE>경험 코어</color> <color=#FFFACE>깨달음 코어</color>\n<color=#FFCEF6>깨달음 확률</color> 코어 세가지가 있고,\n각각 <color=red>3레벨</color> 까지 강화가능합니다.";
                break;
            case 21:
                Destroy(ui[8].GetComponent<Canvas>());
                ui[6].AddComponent<Canvas>();
                ui[6].GetComponent<Canvas>().overrideSorting = true;
                ui[9].AddComponent<Canvas>();
                ui[9].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "코어를 장착하기 위해서는 하단에 있는 코어 창에 <color=red>슬롯을 선택</color>한 뒤 보유하고 있는 <color=red>코어를 클릭</color>하면 됩니다.";
                break;
            case 22:
                Destroy(ui[6].GetComponent<Canvas>());
                ui[10].AddComponent<Canvas>();
                ui[10].GetComponent<Canvas>().overrideSorting = true;
                ui[11].AddComponent<Canvas>();
                ui[11].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "또한 코어는 <color=red>현재 직급</color>에 따라\n장착할 수 있는 갯수가 다릅니다.\n1-4레벨 : <color=yellow>1개</color>  5-8레벨 : <color=orange>2개</color>  9-10레벨 : <color=red>3개</color>";
                break;
            case 23:
                ui[12].AddComponent<Canvas>();
                ui[12].GetComponent<Canvas>().overrideSorting = true;
                ui[13].AddComponent<Canvas>();
                ui[13].GetComponent<Canvas>().overrideSorting = true;
                ui[14].AddComponent<Canvas>();
                ui[14].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "더 빨리 진급하기 위해 <color=yellow>더 높은 등급</color>의 코어를 사용해보세요!";
                break;
            case 24:
                tut_text.text = "아! 물론 합성 도중 코어가 <color=red>모두 파괴</color> 되어도\n책임은 지지 않습니다 ^^";
                break;
            case 25:
                Destroy(ui[9].GetComponent<Canvas>());
                Destroy(ui[10].GetComponent<Canvas>());
                Destroy(ui[11].GetComponent<Canvas>());
                Destroy(ui[12].GetComponent<Canvas>());
                Destroy(ui[13].GetComponent<Canvas>());
                Destroy(ui[14].GetComponent<Canvas>());
                tut_text.text = "창고에 대한 설명은 여기까지입니다.";
                break;
            case 26:
                tut_text.text = "그럼 저는 다시 도움이 필요할 때 나타나겠습니다 :)";
                break;
            case 27:
                tut_text.text = "안녕!";
                break;
            case 28:
                PlayerPrefs.SetString("inven", "true");
                Destroy(this.gameObject);
                break;
               


        }
        step++;
    }
}
