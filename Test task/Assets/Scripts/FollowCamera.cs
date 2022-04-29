using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;
    
    private float angle = 0f;
    private float radius = 10f;
    private float angularSpeed = 0.5f;

    private void Update()
    {
        transform.position = new Vector3(cube.transform.position.x + Mathf.Cos(angle) * radius, transform.position.y, cube.transform.position.z + Mathf.Sin(angle) * radius);

        transform.LookAt(cube.transform);

        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f)
        {
            angle = 0f;
        }
    }
}
