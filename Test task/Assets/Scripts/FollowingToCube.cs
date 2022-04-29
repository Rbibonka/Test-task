using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingToCube : MonoBehaviour
{
    [SerializeField]
    private Transform cubePosition;

    private void Update()
    {
        transform.position = new Vector3(cubePosition.position.x, transform.position.y, cubePosition.position.z);
    }
}
