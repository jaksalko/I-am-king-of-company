using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System;

public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{




    protected static T instance = null;

    public static T Instance
    {
        get
        {
            instance = FindObjectOfType(typeof(T)) as T;

            if (instance == null)
            {
                instance = new GameObject("@" + typeof(T).ToString(),
                                           typeof(T)).GetComponent<T>();

                DontDestroyOnLoad(instance);
                Debug.Log(instance);
            }
            return instance;
        }
    }







}