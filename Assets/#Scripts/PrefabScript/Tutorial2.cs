using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial2 : MonoBehaviour {
    int step;
    public Text tut_text;
    public GameObject munky;
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
                tut_text.text = "대리부터는 게임의 난이도가 조금 어려워집니다.";
                break;
            case 1:
                tut_text.text = "그리고 업무 중 <color=red>실수</color>를 한다면 <color=red>강등</color>도 당할 수 있습니다! 더욱 더 신중하게 행동해야겠죠?";
                break;
            case 2:
                tut_text.text = "그럼 <color=red>게임 방법</color>을 알아볼까요?\n<color=red>집중</color>해주세요!";
                break;
            case 3:
                tut_text.text = "이제부터는 크게 <color=red>두가지</color>의 행동을 해야합니다.";
                break;
            case 4:
                ui[0].SetActive(true);
                
                tut_text.text = "첫째로, 당신의 책상 옆에\n<color=green>O</color> 또는 <color=red>X</color> 가 나타납니다.";
                break;
            case 5:
                ui[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/OK");
                tut_text.text = "'<color=green>O</color>'일 경우, 빠르게 <color=red>클릭</color>하셔야 합니다.\n만약 제한된 시간 내에 수행하지 못하면 <color=red>업무 실패!</color>\n운이 나쁘면 <color=red>강등</color>을 당할 수도 있겠죠!?";
                break;
            case 6:
                ui[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Background/No");
                tut_text.text = "'<color=red>X</color>'일 경우 다음 업무가\n나올 때까지 <color=#33ffff>기다리시면</color> 됩니다.\n만약 <color=red>X</color>를 <color=red>누를</color> 경우 마찬가지로 <color=red>업무 실패!</color>";
                break;
            case 7:
                ui[0].SetActive(false);
                ui[1].AddComponent<Canvas>();
                ui[1].GetComponent<Canvas>().overrideSorting = true;
                ui[2].AddComponent<Canvas>();
                ui[2].GetComponent<Canvas>().overrideSorting = true;
                ui[3].AddComponent<Canvas>();
                ui[3].GetComponent<Canvas>().overrideSorting = true;
                ui[4].AddComponent<Canvas>();
                ui[4].GetComponent<Canvas>().overrideSorting = true;
                tut_text.text = "둘째로 아래 사원들의 '<color=red>과로</color>' 게이지를\n관리해줘야 합니다.\n게이지는 <color=green>일정한 속도</color>로 차오릅니다.";
                break;
            case 8:
                ui[0].SetActive(true);
                ui[1].transform.GetChild(0).gameObject.GetComponent<Slider>().value = 1;
                tut_text.text = "<color=red>각각의 게이지가 끝까지 차오르게 되면\nO/X 업무의 진행속도가 서서히 빨라지게 됩니다.</color>";
                break;
            case 9:
                ui[2].transform.GetChild(0).gameObject.GetComponent<Slider>().value = 1;
                ui[3].transform.GetChild(0).gameObject.GetComponent<Slider>().value = 1;
                ui[4].transform.GetChild(0).gameObject.GetComponent<Slider>().value = 1;
                tut_text.text = "4개가 <color=red>모두</color> 차오르게 되면\n<color=red>엄청난 속도</color>로 빨리지게 되겠죠?";
                break;
            case 10:
                ui[1].transform.GetChild(0).gameObject.GetComponent<Slider>().value = 0;
                ui[2].transform.GetChild(0).gameObject.GetComponent<Slider>().value = 0;
                ui[3].transform.GetChild(0).gameObject.GetComponent<Slider>().value = 0;
                ui[4].transform.GetChild(0).gameObject.GetComponent<Slider>().value = 0;
                tut_text.text = "반대로 과로 상태가 아닌 사원이 있으면\nO/X 업무의 진행속도는 <color=green>서서히 회복</color>됩니다.";
                break;
            case 11:
                ui[0].SetActive(false);
                Destroy(ui[1].GetComponent<Canvas>());
                Destroy(ui[2].GetComponent<Canvas>());
                Destroy(ui[3].GetComponent<Canvas>());
                Destroy(ui[4].GetComponent<Canvas>());
                ui[5].SetActive(true);
                ui[6].SetActive(true);
                ui[7].SetActive(true);
                ui[8].SetActive(true);
                tut_text.text = "<color=green>관리 방법</color>은 게이지 옆의 <color=green>사원들</color>을\n'<color=green>클릭!</color>' 해주시면 게이지가 <color=green>초기화</color> 됩니다.";
                break;
            case 12:
                munky.SetActive(false);
               
                break;
            case 13:
                munky.SetActive(true);

                ui[5].SetActive(false);
                ui[6].SetActive(false);
                ui[7].SetActive(false);
                ui[8].SetActive(false);
                tut_text.text = "간단하죠? 부디 강등 당하지 않고\n높은 곳 까지 올라가시길 바랍니다. :)";
                break;
            case 14:
                tut_text.text = "대리님 화이팅!";
                break;
            case 15:
                PlayerPrefs.SetString("second", "true");
                Time.timeScale = 1;
                Destroy(this.gameObject);
                break;

        }
        step++;
    }
}
