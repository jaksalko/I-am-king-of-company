using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class WalkwayPrefab : MonoBehaviour {
    GameObject user;
    bool isClicked;
    int collision_counter;
    int childcount;
    Vector3 mouse;
    // Use this for initialization
    void Start () {
        Debug.Log("start");
        user = this.transform.GetChild(0).gameObject;
        isClicked = false;
        collision_counter = 0;
        childcount = transform.childCount - 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.currentSelectedGameObject == user)
            {
                Debug.Log("clicked");
                isClicked = true;
            }
            mouse = Quaternion.Inverse(transform.localRotation) * (Input.mousePosition - new Vector3(450f, 800f, 0) - transform.localPosition);
            if (isClicked == true && (user.transform.localPosition.y > (mouse.y)))
            {
                user.transform.localPosition = new Vector3(0, mouse.y, 0);
            }
        }
        if (Input.GetMouseButton(0))
        {
            mouse = Quaternion.Inverse(transform.localRotation)*(Input.mousePosition - new Vector3(450f, 800f, 0) - transform.localPosition);
            if (isClicked == true && (user.transform.localPosition.y > (mouse.y)))
            {
                user.transform.localPosition = new Vector3(0, mouse.y, 0);
            }
           
        }
        if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        collision_counter++;
        if (collision_counter == childcount)
        {
            Destroy(gameObject);
        }
    }
}
