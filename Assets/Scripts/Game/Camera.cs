using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public void Move(Vector3 position)
    {
        position.z = -10;
        transform.position = position;
    }
}
