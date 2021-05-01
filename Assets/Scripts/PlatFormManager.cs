using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatFormManager : MonoBehaviour
{
    public List<GameObject> movingPlatFormList;

    bool movementDir;
    public bool setMovementDir { get { return movementDir; } set { movementDir = value; } }


    private void Update()
    {
        MoveAllPlatforms();
    }

    public void MoveAllPlatforms()
    {
        foreach (var item in movingPlatFormList)
        {
            item.GetComponent<MovingPlatform>().setPlatForm = setMovementDir;            
        }
    }
}
