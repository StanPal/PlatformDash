using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    bool platformDir;
    public bool setPlatForm { get { return platformDir; } set { platformDir = value; } }
    private Vector3 posA;
    private Vector3 nexPos;
    private Vector3 posB;
    private Vector3 startPos;

    [SerializeField]
    private Transform childTransform;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform transformB;
    [SerializeField]
    private Transform transformA;
    // Start is called before the first frame update
    void Start()
    {
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        startPos = transformA.localPosition;
        nexPos = posB;
    }

    void Update()
    {
        if (platformDir)
        {
            PlatFormMoveUp();
        }
        else
        {
            PlatFormMoveDown();
        }
    }
    
    private void PlatFormMoveUp()
    {
        childTransform.localPosition =
           Vector3.MoveTowards(childTransform.localPosition, nexPos, speed * Time.deltaTime);  
    }

    private void PlatFormMoveDown()
    {
        childTransform.localPosition =
           Vector3.MoveTowards(childTransform.localPosition, startPos, speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        nexPos = nexPos != posA ? posA : posB;
    }
}
