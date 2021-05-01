using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    public Transform playerTransform;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 temp = transform.position;

        // set camera postion to player position
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;

        transform.position = temp;
    }
}
