using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Messenger : MonoBehaviour
{
    private static Messenger instance;
    public GameObject messagePrefab;
    public RectTransform messageParent; 

    public static Messenger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Messenger>();
            }
            return instance; 
        }
    }

    public void CreateMessage(string chatMessage, Color messageColor)
    {
        GameObject cm = Instantiate(messagePrefab, messagePrefab.transform.position, Quaternion.identity);
        cm.transform.SetParent(messageParent);
        cm.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        cm.transform.GetChild(0).GetComponent<Text>().text = chatMessage;
        cm.transform.GetChild(0).GetComponent<Text>().color = messageColor;
    }
}
