using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DailyGiftPrefab : MonoBehaviour {
    XMLManager xMLManager;
    Single single;
    GameObject style;
    GameObject gift;
    AudioSource audio;
    // Use this for initialization
    void Start () {
        audio = this.gameObject.GetComponent<AudioSource>();
        xMLManager = XMLManager.ins;
        single = Single.instance;
        style = this.transform.GetChild(1).gameObject;
        gift = this.transform.GetChild(2).gameObject;

	}

    public void ClickPrefab()
    {
        audio.Play();
        xMLManager.itemDB.dailyGiftList.RemoveAt(int.Parse(gameObject.name));

        switch (style.name)
        {
            case "cash":
                xMLManager.itemDB.wallet.cash += long.Parse(gift.GetComponent<Text>().text.ToString());
                break;
            case "item":
                for (int i = 0; i < 45; i++)
                {
                    if (xMLManager.itemDB.dailyList[i].itemName == gift.GetComponent<Text>().text.ToString())
                    {
                        xMLManager.itemDB.dailyList[i].quan++;
                        single.SetAlert(xMLManager.itemDB.dailyList[i].itemName + " 을 획득하셨습니다.", Color.black);
                        break;
                    }
                }               
                break;
            case "display":
                xMLManager.itemDB.wallet.cash += long.Parse(gift.GetComponent<Text>().text.ToString());
                break;
            case "payback":
                xMLManager.itemDB.wallet.cash += long.Parse(gift.GetComponent<Text>().text.ToString());
                break;
        }
        GameObject content = gameObject.transform.parent.gameObject;
        Destroy(gameObject);
        Debug.Log("child count : " + content.transform.childCount);
        for (int count = int.Parse(gameObject.name) + 1; count < content.transform.childCount; count++)
        {
            content.transform.GetChild(count).name = (count - 1).ToString();
        }
    }
}
