using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StockTutorial : MonoBehaviour {
    int step;
    public Text tut_text;
    public GameObject auction;
    public GameObject trade;
    public GameObject[] page;
    public GameObject[] pagebtn;
    public GameObject[] ui;
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
                tut_text.text = "이번에는 <color=#81c147>경매장</color>과 <color=#81c147>주식</color>에 대해\n간단히 알려드릴게요.";
                break;
            case 1:
                ui[0].AddComponent<Canvas>();
                ui[0].GetComponent<Canvas>().overrideSorting = true;
                pagebtn[0].AddComponent<Canvas>();
                pagebtn[0].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "먼저 <color=#81c147>경매장</color>입니다.";
                break;
            case 2:
                tut_text.text = "경매장에는 <color=red>매일 0시</color>에\n<color=#81c147>5개의 물품</color>이 리셋됩니다.";
                break;
            case 3:
                Destroy(ui[0].GetComponent<Canvas>());
                auction.SetActive(true);
                auction.AddComponent<Canvas>();
                auction.GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "이 물품들을 구매하기 위해서는\n물품을 클릭하시면\n'<color=#81c147>경매 팝업창</color>'이 나타나는데,";
                break;
            case 4:

                tut_text.text = "<color=#81c147>입찰(주문)</color>과 <color=red>최고가 낙찰\n</color> 두가지 방법으로 구매가 가능합니다.";
                break;
            case 5:
                Destroy(auction.GetComponent<Canvas>());
                ui[1].AddComponent<Canvas>();
                ui[1].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "<color=red>최고가 낙찰</color>은 물품의 <color=red>최고가</color>를 주고\n바로 물품을 얻을 수 있는 방법입니다.\n<color=red>높은 금액</color>을 지불하는 대신\n물품을 <color=red>바로</color> 얻을 수 있는 방법이죠.";
                break;
            case 6:
                Destroy(ui[1].GetComponent<Canvas>());
                ui[2].AddComponent<Canvas>();
                ui[2].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "<color=#81c147>입찰(주문)</color>은 물품의\n<color=red>현재가보다 높은 가격</color>에 주문해\n<color=red>다음날 0시까지 다음 주문이 없을 경우</color>\n물품을 획득할 수 있는 방법입니다.";
                break;
            case 7:
                tut_text.text = " <color=red>비교적 적은 금액</color>을 지불하고 얻을 수 있지만\n계속해서 주문이 들어온다면 <color=red>물품을 얻지 못하거나\n최고가보다 더 높은금액</color>을 지불하고\n구매해야 할수도 있죠.";
                break;
            case 8:
                Destroy(ui[2].GetComponent<Canvas>());
                auction.SetActive(false);
                ui[3].AddComponent<Canvas>();
                ui[3].GetComponent<Canvas>().overrideSorting = true;
                ui[3].GetComponent<Text>().color = Color.white;
                tut_text.text = "만약 <color=red>낙찰</color>에 성공했다면 '<color=#81c147>내 경매</color>'에서 낙찰된 물품을 <color=#81c147>수령</color>할 수 있습니다.";               
                break;
            case 9:
                tut_text.text = "<color=#81c147>경매장</color>은 자주 드랍되지 않는 물품을\n얻을 수 있는 좋은 수단이니 매일 눈여겨 보면\n큰 도움이될 수 있습니다 :)";
                break;
            case 10:
                Destroy(ui[3].GetComponent<Canvas>());
                ui[3].GetComponent<Text>().color = Color.black;
                page[0].SetActive(false);
                page[1].SetActive(true);
                Destroy(pagebtn[0].GetComponent<Canvas>());
                pagebtn[1].AddComponent<Canvas>();
                pagebtn[1].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "다음으로 <color=#81c147>주식</color>에 대해 알아볼까요?";
                break;
            case 11:
                tut_text.text = "<color=#81c147>주식시장</color>에서는 주식을\n<color=red>사</color>거나 <color=blue>팔</color> 수 있습니다.";
                break;
            case 12:
                trade.SetActive(true);
                trade.AddComponent<Canvas>();
                trade.GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "<color=#81c147>주식</color>을 거래하기 위해서는\n원하는 종목을 <color=red>클릭</color>하면\n'<color=#81c147>주식 팝업창</color>'이 생기는데,";
                break;
            case 13:
                Destroy(trade.GetComponent<Canvas>());
                ui[4].AddComponent<Canvas>();
                ui[4].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "<color=#81c147>키패드</color>를 통해\n원하는 만큼의 <color=#81c147>수량</color>을 입력한 후,";
                break;
            case 14:
                Destroy(ui[4].GetComponent<Canvas>());
                ui[5].AddComponent<Canvas>();
                ui[5].GetComponent<Canvas>().overrideSorting = true;
                ui[6].AddComponent<Canvas>();
                ui[6].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "<color=red>구매</color> 또는 <color=blue>판매</color>를 클릭하면 됩니다";
                break;
            case 15:
                Destroy(ui[5].GetComponent<Canvas>());
                Destroy(ui[6].GetComponent<Canvas>());
                Destroy(trade.GetComponent<Canvas>());
                trade.SetActive(false);
                page[1].SetActive(false);
                page[2].SetActive(true);
                Destroy(pagebtn[1].GetComponent<Canvas>());
                pagebtn[2].AddComponent<Canvas>();
                pagebtn[2].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "구매한 주식은 '<color=#81c147>내 주식</color>'에서\n더 <color=red>구매</color>하거나 보유한 해당 주식을 <color=blue>판매</color>할 수 있습니다.";
                break;
            case 16:
                tut_text.text = "주식은 <color=yellow>자금</color>을 늘리는데\n<color=red>엄청난 기여</color>를 할 수도 있지만\n반대로 <color=blue>잃을 확률</color>도 존재하므로\n신중하게 거래해주시기 바랍니다 :)";
                break;
            case 17:               
                tut_text.text = "<color=#81c147>주식</color>과 <color=#81c147>경매장</color>에 대한 설명은 여기까지입니다.";
                break;
            case 18:
                tut_text.text = "그럼 저는 다시 도움이 필요할 때 나타나겠습니다 :)";
                break;
            case 19:
                tut_text.text = "안녕!";
                break;
            case 20:
                Destroy(pagebtn[2].GetComponent<Canvas>());
                
                PlayerPrefs.SetString("stock", "true");
                Destroy(this.gameObject);
                break;

        }
        step++;
    }
}
