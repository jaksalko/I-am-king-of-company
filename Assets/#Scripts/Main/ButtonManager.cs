using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour {
  
   /* private void Start()
    {
        List<Dictionary<string, object>> data;
        data = CSVReader.Read("makemoneydatasheet3");
        
    }*/
    public void GoStockScene()
    {
        SceneManager.LoadScene("StockScene");
    }
    public void GoStoreScene()
    {
        //
        SceneManager.LoadScene("StoreScene");
    }
    public void GoInvenScene()
    {
        SceneManager.LoadScene("InvenScene");
    }
}
