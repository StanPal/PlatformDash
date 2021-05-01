using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public event System.Action onGoal;
    public GoalScreenManager ScreenManager;
    // Start is called before the first frame update

    public void Activate()
    {
        ScreenManager.gameObject.SetActive(true);
    }



}
