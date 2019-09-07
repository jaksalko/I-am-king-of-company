using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleTon : MonoBehaviour {
    public static SoundSingleTon instance = null;
    public AudioSource lobby;
    public AudioSource stage1;//0-1
    public AudioSource stage2;//2-4
    public AudioSource stage3;//5-7
    public AudioSource stage4;//8-9
    public AudioSource stage5;//10
    public AudioSource btnclick;
    public AudioSource destroy;
    public AudioSource gotcha;
    public AudioSource demote;
    public AudioSource promote;
    public AudioSource good;
    public AudioSource miss;
    public AudioSource sell;
    public AudioSource upgrade;
    public AudioSource storeget;
    public AudioSource roullet;

    public AudioSource bgm;

    private void Awake()
    {
        if (instance == null)
        {
            Debug.Log("Single instance is null");
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Single instance is not Single.. Destroy gameobject!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);//Dont destroy this singleton gameobject :(


    }
   
}
