using UnityEngine;

public class WorkinWithMovingPoints 
{
    private float currentPositionX;
    private float currentPositionZ;

    private int interval = 5;

    Vector3[] movingPoints;

    Vector3 startPosition;

    public Vector3 GetStartPosition
    {
        get
        {
            return startPosition;
        }
    }

    public Vector3[] RandomGenerationMovingPoints(int quantityPoints)
    {
        movingPoints = new Vector3[quantityPoints];

        startPosition = GetStartedPosition();

        currentPositionX = startPosition.x;

        currentPositionZ = startPosition.z;

        movingPoints[0] = startPosition;

        Debug.Log(movingPoints[0].ToString());

        for (int i = 1; i < movingPoints.Length; i++)
        {
            currentPositionX = GetCurrentPosition(currentPositionX);

            currentPositionZ = GetCurrentPosition(currentPositionZ);

            movingPoints[i] = new Vector3(currentPositionX, 0, currentPositionZ);

            Debug.Log(movingPoints[i].ToString());
        }

        SavePointsInJson();

        return movingPoints;
    }

    private void SavePointsInJson()
    {
        DateJsonController savingJsonData = new DateJsonController();

        savingJsonData.SavePoints(movingPoints);
    }

    private float GetCurrentPosition(float currentPosition)
    {
        currentPosition = Random.Range(currentPosition, interval);

        interval += 4;

        return currentPosition;
    }

    private Vector3 GetStartedPosition()
    {
        try
        {
            Vector3 startPosition = GameObject.FindGameObjectWithTag("Cube").transform.position;

            return startPosition;
        }
        catch
        {
            Debug.Log("Объект Cube не найден!");

            return new Vector3(0,0,0);
        }
    }
}
