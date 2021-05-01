using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalScreenManager : MonoBehaviour
{
    public Button playAgain;
    [SerializeField]
    string sceneName = "main";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        TwitchChat twitch = FindObjectOfType<TwitchChat>();
        twitch.CloseConnection();
        SceneManager.LoadScene(sceneName);
    }
}
