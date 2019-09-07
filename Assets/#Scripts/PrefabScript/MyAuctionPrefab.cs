using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAuctionPrefab : MonoBehaviour {
    XMLManager xMLManager;
    Single single;
    AudioSource audio;
    private void Start()
    {
        single = Single.instance;
        xMLManager = XMLManager.ins;
        audio = this.gameObject.GetComponent<AudioSource>();

    }
  
    public void Click_MyAuction()
    {
        audio.Play();
        xMLManager.itemDB.dailyList[xMLManager.itemDB.myAuctionList[int.Parse(gameObject.name)].Item_num].quan++;
        single.SetAlert(xMLManager.itemDB.dailyList[xMLManager.itemDB.myAuctionList[int.Parse(gameObject.name)].Item_num].itemName + " 을 획득하셨습니다.",Color.black);
        xMLManager.itemDB.myAuctionList.RemoveAt(int.Parse(gameObject.name));

        GameObject content = gameObject.transform.parent.gameObject;
        Destroy(gameObject);
        Debug.Log("child count : " + content.transform.childCount);
        for (int count = int.Parse(gameObject.name)+1; count < content.transform.childCount; count++)
        {
            content.transform.GetChild(count).name = (count - 1).ToString();
        }

       
        
        //quan++
        //remove list
        //single.alert
       
    }

}
