using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Logo : MonoBehaviour
{
    public TextMeshProUGUI logo;
    private int count = 0;
    public GameObject heart;

    public GameObject audio;
    private AudioSource audioplay;
    // Use this for initialization
    void Start()
    {

        audioplay = audio.GetComponent<AudioSource>();
        StartCoroutine(MakeLogo());

    }

    // Update is called once per frame
    IEnumerator MakeLogo()
    {

        while (true)
        {
            switch (count)
            {
                case 0:
                    logo.text = "";
                    break;
                case 1:
                    logo.text = "2";
                    audioplay.Play();
                    break;
                case 2:
                    logo.text = "2D";
                    audioplay.Play();
                    break;
                case 3:
                    logo.text = "2DB";
                    audioplay.Play();
                    break;
                case 4:
                    logo.text = "2DBF";
                    audioplay.Play();
                    heart.SetActive(true);
                    break;
                case 5:
                    break;

            }
            count++;
            if (count == 6)
            {
                StopAllCoroutines();
                AutoFade.LoadLevel("Lobby", 1, 1, Color.black);
            }

            yield return new WaitForSeconds(0.7f);
        }

    }
}
