using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialPrefab : MonoBehaviour {
    int step;
    public Text tut_text;
    public GameObject[] ui;
	// Use this for initialization
	void Start () {
        step = 0;
        
	}
	
	
    public void TutorialNext()//Click Anywhere
    {
        
        switch (step)
        {
            case 0:
                tut_text.text = "먼저 메인화면에 있는 UI에 대해 설명해드릴게요.";
                break;
            case 1:
                AddCanvas(0);
                AddCanvas(1);
                tut_text.text = "현재 자신의 직급(레벨)과\n그에 맞는 월급이 나타납니다.\n월급은 <color=yellow>매일보상</color>으로 지급되는 점 참고해주세요!";
                break;
            case 2:
                Destroy(ui[0].GetComponent<Canvas>());
                AddCanvas(2);
                tut_text.text = "주식/경매 : 주식거래를 이용해 <color=yellow>자본</color>을 늘리고, 경매장을 이용해 <color=yellow>원하는 물품</color>을 얻을 수 있는 주식/경매장으로 이동할 수 있습니다.";
                break;
            case 3:
                AddCanvas(3);
                tut_text.text = "상점 : 자신의 <color=yellow>업무능력</color>을 향상시키거나 <color=yellow>자본</color>을 늘리는데 도움이 되는 <color=orange>아이템</color>을 구매할 수 있는 상점으로 이동할 수 있습니다.";
                break;
            case 4:
                AddCanvas(4);
                tut_text.text = "창고 : 아이템을 <color=red>합성</color>하거나 코어를 <color=red>장착</color>할 수 있는 창고로 이동할 수 있습니다.";
                break;
            case 5:
                AddCanvas(5);
                tut_text.text = "보상함 : <color=yellow>매일보상</color>이나 전시회를 통한 <color=yellow>보상금</color>을 획득할 수 있는 보상함입니다.";
                break;
            case 6:
                Destroy(ui[5].GetComponent<Canvas>());
                //ui[6].SetActive(false);
                tut_text.text = "가장 중요한 <color=red>메인화면</color>입니다.";
                    break;
            case 7:
                tut_text.text = "이곳에서는 각 직급에 맞는 업무를 진행할 수 있습니다.";
                break;
            case 8:
                tut_text.text = "업무를 통해서만 <color=yellow>진급</color>이 가능하지만, <color=red>강등</color>을 당할 수도 있으니 신중하게 진행해야 합니다.";
                break;
            case 9:
                tut_text.text = "그럼 첫번째 업무방법에 대해 알아볼까요?";
                break;
            case 10:
                //ui[6].SetActive(true);
                ui[7].AddComponent<Canvas>();
                ui[7].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "가장 쉬운 난이도의 업무입니다.\n 지금 보이는 <color=red>아이콘의 순서</color>에 따라서,\n<color=red>화면에 보이는 물체를 클릭</color>하면\n<color=green>경험치</color>와 <color=#f111ff>피버점수</color>를 받을 수 있습니다.";
                break;
            case 11:
                tut_text.text = "간단하죠?";
                break;
            case 12:
                tut_text.text = "어느 수준으로 진급이 이루어지면\n업무의 난이도는 <color=red>상승</color>합니다.\n";
                break;
            case 13:
                tut_text.text = "그럼 저는 다시 도움이 필요할 때 나타나겠습니다 :)";
                break;
            case 14:
                tut_text.text = "안녕!";
                break;
            case 15:
                PlayerPrefs.SetString("load", "true");
                Destroy(ui[7].GetComponent<Canvas>());
                Destroy(this.gameObject);
                break;


        }
        step++;

    }
    private void AddCanvas(int index)
    {
        if(index >1)
            Destroy(ui[index - 1].GetComponent<Canvas>());

        ui[index].AddComponent<Canvas>();
        ui[index].GetComponent<Canvas>().overrideSorting = true;
    }
}
