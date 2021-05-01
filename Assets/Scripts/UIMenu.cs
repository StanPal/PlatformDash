using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public static string ChannelName;
    [SerializeField] private TMPro.TMP_InputField ChannelInput;


    public void Connect()
    {
        ChannelName = ChannelInput.text;
        SceneManager.LoadScene(1);
    }


}
