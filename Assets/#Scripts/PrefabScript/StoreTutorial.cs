using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreTutorial : MonoBehaviour {
    int step;
    public Text tut_text;
    public GameObject[] ui;
    // Use this for initialization
    void Start()
    {
        step = 0;

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void NextStep()
    {
        switch (step)
        {
            case 0: 
                tut_text.text = "이번에는 <color=red>상점</color>과 \n<color=blue>내 정보</color>에 대해서 간단히 알려드릴게요.";
                break;
            case 1:
                tut_text.text = "여기서는 게임에 도움이 되는 \n아이템 구매와 자신의 업무 능력을\n향상시킬 수 있어요.";
                break;
            case 2:
                ui[0].AddComponent<Canvas>();
                ui[0].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "먼저 <color=red>상점</color>에서는 \n<color=red>아이템</color>이나 <color=red>코어</color>를 뽑거나,\n 게임을 <color=green>보조</color>할 수 있는<color=green>티켓</color>들을\n구매할 수 있습니다.";
                break;
            case 3:
                Destroy(ui[0].GetComponent<Canvas>());
                tut_text.text = "그리고 <color=#00ffff>내 정보</color>에서는 \n현재 자신의 <color=#00ffff>능력치</color>를 확인할 수 있고,\n각각의 <color=#00ffff>능력치</color>를 올려줄 수 있어요.";
                break;
            case 4:
                ui[1].GetComponent<Text>().color = Color.white;
                ui[1].AddComponent<Canvas>();
                ui[1].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "능력치는 일을 할 때마다\n 얻을 수 있는 <color=green>경험</color>";
                break;
            case 5:
                ui[1].GetComponent<Text>().color = Color.black;
                Destroy(ui[1].GetComponent<Canvas>());
                ui[2].GetComponent<Text>().color = Color.white;
                ui[2].AddComponent<Canvas>();
                ui[2].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "일을 할 때 경험을 더 많이\n 얻을 수 있는 <color=red>'깨달음'</color>";
                break;
            case 6:
                ui[2].GetComponent<Text>().color = Color.black;
                Destroy(ui[2].GetComponent<Canvas>());
                ui[3].GetComponent<Text>().color = Color.white;
                ui[3].AddComponent<Canvas>();
                ui[3].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "'깨달음'의 확률을\n 올려주는 <color=red>깨달음 확률</color>";
                break;
            case 7:
                ui[3].GetComponent<Text>().color = Color.black;
                Destroy(ui[3].GetComponent<Canvas>());
                ui[4].GetComponent<Text>().color = Color.white;
                ui[4].AddComponent<Canvas>();
                ui[4].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "피버타임 때 얻을 수 있는 경험을\n 올려주는 <color=red>피버 증가량</color>";
                break;
            case 8:
                ui[4].GetComponent<Text>().color = Color.black;
                Destroy(ui[4].GetComponent<Canvas>());
                tut_text.text = "이렇게 4종류가 있습니다.";
                break;
            case 9:
                for (int i = 5; i < ui.Length; i++)
                {
                    ui[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.white;
                    ui[i].GetComponent<Text>().color = Color.yellow;
                    ui[i].AddComponent<Canvas>();
                    ui[i].GetComponent<Canvas>().overrideSorting = true;
                }
               
                tut_text.text = "각각의 능력치는 가운데 버튼을 이용해 상승시킬 수 있고,\n능력치가 오를 때마다\n 다음 단계로 가기위한 <color=yellow>비용</color>은 <color=red>증가</color>합니다.";
                break;
            case 10:
                tut_text.text = "'<color=green>경험</color>'은 진급을 위한 필수요소이니, \n빠르게 올릴수록 게임진행에\n도움이 되겠죠?";
                break;
            case 11:
                for (int i = 5; i < ui.Length; i++)
                {
                    ui[i].transform.GetChild(0).gameObject.GetComponent<Image>().color = Color.black;
                    ui[i].GetComponent<Text>().color = Color.black;
                    Destroy(ui[i].GetComponent<Canvas>());
                }
                tut_text.text = "상점과 내정보에 대한 설명은 여기까지입니다.";
                break;
            case 12:
                tut_text.text = "그럼 저는 다시 도움이 필요할 때 나타나겠습니다 :)";
                break;
            case 13:
                tut_text.text = "안녕!";
                break;
            case 14:
                PlayerPrefs.SetString("store", "true");
                
                Destroy(this.gameObject);
                break;
        }
        step++;
    }
}
