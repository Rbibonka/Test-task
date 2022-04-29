using UnityEngine;
using System.IO;

public class DateJsonController
{
    private string jsonFilePath = Application.streamingAssetsPath + "/Points.json";

    private MovingPoints movingPoints;

    public void SavePoints(Vector3[] setMovingPoints)
    {
        movingPoints.points = setMovingPoints;

        if (File.Exists(jsonFilePath))
        {
            File.WriteAllText(jsonFilePath, JsonUtility.ToJson(movingPoints));
        }
        else
        {
            Debug.Log("Файл не найден!");
        }
    }

    public Vector3[] LoadPoints()
    {
        if (File.Exists(jsonFilePath))
        {
             movingPoints = JsonUtility.FromJson<MovingPoints>(File.ReadAllText(jsonFilePath));

            return movingPoints.points;
        }
        else
        {
            Debug.Log("Файл не найден!");

            return ReturnEmptyArray();
        }
    }

    private Vector3[] ReturnEmptyArray()
    {
        Vector3[] emptyArrayVectors = new Vector3[3];

        for (int i = 0; i < emptyArrayVectors.Length; i++)
        {
            emptyArrayVectors[i] = new Vector3(0,0,0);
        }

        return emptyArrayVectors;
    }
}
