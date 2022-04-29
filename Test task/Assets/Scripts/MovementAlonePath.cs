using System.Collections;
using UnityEngine;

public class MovementAlonePath : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private PropertiesPath propertiesPath;

    [SerializeField]
    private FollowCamera followCamera;

    private Coroutine movingToPointsCoroutine;

    private PropertiesPath.TypePath typePath;

    private int interval = -5;

    private float speed = 0.005f;

    private Vector3 startPlayerPosition;

    private void Start()
    {
        if (propertiesPath != null)
        {
            typePath = propertiesPath.GetTypePath;

            startPlayerPosition = propertiesPath.GetStartPosition;

            speed = propertiesPath.GetPlayerSpeed;
        }
        else
        {
            Debug.Log("Объект не найден!");
        }
    }

    public void StopMoving()
    {
        StopCoroutine(movingToPointsCoroutine);

        transform.position = startPlayerPosition;
    }

    public void StartMoving()
    {
        typePath = propertiesPath.GetTypePath;

        speed = propertiesPath.GetPlayerSpeed;

        Vector3[] movingPoints = propertiesPath.GetMovingPoints;
       
        movingToPointsCoroutine = StartCoroutine(MovingToPoints(movingPoints));
    }

    public void StopCube()
    {
        gameManager.StopTimer();
    }

    private IEnumerator MovingToPoints(Vector3[] movingPoints)
    {

        if (typePath == PropertiesPath.TypePath.loop)
        {
            while (true)
            {
                for (int i = 0; i < movingPoints.Length; i++)
                {
                    for (float t = 0; t <= 1; t += speed)
                    {
                        if (i == movingPoints.Length - 1)
                        {
                            transform.position = Bezier.GetPoint(movingPoints[movingPoints.Length - 1], new Vector3(movingPoints[movingPoints.Length - 1].x, movingPoints[movingPoints.Length - 1].y - interval + 5, movingPoints[movingPoints.Length - 1].z),
                                new Vector3(movingPoints[0].x, movingPoints[0].y - interval + 5, movingPoints[0].z), movingPoints[0], t);
                        }
                        else
                        {
                            transform.position = Bezier.GetPoint(movingPoints[i], new Vector3(movingPoints[i].x - interval, movingPoints[i].y - interval, movingPoints[i].z),
                            new Vector3(movingPoints[i + 1].x - interval, movingPoints[i + 1].y - interval, movingPoints[i + 1].z), movingPoints[i + 1], t);
                        }

                        yield return null;
                    }
                }
            }
        }else if (typePath == PropertiesPath.TypePath.line)
        {
            for (int i = 0; i < movingPoints.Length - 1; i++)
            {
                for (float t = 0; t <= 1; t += speed)
                {
                    transform.position = Bezier.GetPoint(movingPoints[i], new Vector3(movingPoints[i].x - interval, movingPoints[i].y - interval, movingPoints[i].z),
                        new Vector3(movingPoints[i + 1].x - interval, movingPoints[i + 1].y - interval, movingPoints[i + 1].z), movingPoints[i + 1], t);

                    yield return null;
                }
            }

            StopCube();
        }
        else
        {
            Debug.Log("Невозможный тип пути!");
        }
    }
}
