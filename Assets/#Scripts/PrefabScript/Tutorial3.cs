using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial3 : MonoBehaviour
{
    int step;
    public Text tut_text;
    public Text passclick;
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
                tut_text.text = "이제 회장님이 되기까지 얼마남지 않았습니다!";
                break;
            case 1:
                tut_text.text = "그럼 이사님의 업무에 대해 설명해드리겠습니다.";
                break;
            case 2:
                ui[0].SetActive(true);
                
                tut_text.text = "각 부서에서 이사님께 <color=yellow>의견</color>을 제시할 것입니다.";
                break;
            case 3:
                
                tut_text.text = "그럼 이사님께서는 <color=#00FF00>O</color> 또는 <color=red>X</color>로 의견에 대한\n<color=#00FF00>찬성</color>/<color=red>반대</color>를 나타낼겁니다.";
                break;
            case 4:
                ui[2].SetActive(true);
                passclick.text = "<color=#00FF00>클릭!</color>";
                tut_text.text = "<color=#00FF00>찬성</color>이면 그 의견을 <color=#00FF00>클릭!</color>";
                break;
            case 5:
                ui[2].GetComponent<Image>().sprite = Resources.Load<Sprite>("background/No");
                passclick.text = "<color=red>패스!</color>";
                tut_text.text = "<color=red>반대</color>면 <color=red>패스</color>하시면 됩니다.";
                break;
            case 6:
                passclick.text = "";
                ui[0].SetActive(false);
                tut_text.text = "여기서 조심해야 하는 점이 있습니다!!";
                break;
            case 7:
                ui[1].SetActive(true);
                ui[1].GetComponent<Image>().sprite = Resources.Load<Sprite>("background/bomb");
                tut_text.text = "<color=red>부서에서 이사님의 찬반여부와 상관없이\n정말 최악의 의견을 제시하는 경우가 있습니다.</color>";
                break;
            case 8:
                ui[3].SetActive(true);
                tut_text.text = "이렇게 느낌표가 아닌 <color=red>폭탄</color>으로 표시되는 의견은\n<color=red>찬성/반대와 상관없이</color> 절대 누르시면 <color=red>안됩니다!</color>";
                break;
            case 9:
                tut_text.text = "조언을 해드리자면 이사님께서는 <color=#00FF00>폭탄이 아닌 의견</color>에 <color=#00FF00>찬성</color>하는 경우에만 의견을 <color=#00FF00>클릭</color>하시면 됩니다.";
                break;
            case 10:
                ui[2].SetActive(false);
                ui[1].SetActive(false);
                ui[3].SetActive(false);
                tut_text.text = "정말정말 간단하죠?";
                break;
            case 11:
                tut_text.text = "간단한 일이지만 높은 위치에 계신만큼\n실수를 하시면 <color=red>강등</color>당하기 아주 쉽습니다.\n정말 신중하셔야 합니다.";
                break;
            case 12:
                tut_text.text = "그럼 저는 더욱 더 높으신 곳에 오셨을 때 찾아뵙겠습니다.";
                break;
            case 13:
                tut_text.text = "화이팅!!";
                break;
            case 14:
                PlayerPrefs.SetString("third", "true");
                Time.timeScale = 1;
                Destroy(this.gameObject);
                break;
        }
        step++;
    }
}
