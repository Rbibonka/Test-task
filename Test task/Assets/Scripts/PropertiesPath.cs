using UnityEngine;

public class PropertiesPath : MonoBehaviour
{
    [SerializeField]
    private GameObject planePrefab;

    [SerializeField]
    private GameObject planesParent;

    private Vector3[] movingPoints;
   
    private Vector3 startPosition;

    private float playerSpeed;

    private int interval = -5;

    public enum TypePath
    {
        line,
        loop
    }

    TypePath typePath = TypePath.loop;

    public float GetPlayerSpeed
    {
        get
        {
            return playerSpeed;
        }
    }

    public Vector3 GetStartPosition
    {
        get
        {
            return startPosition;
        }
    }

    public Vector3[] GetMovingPoints
    {
        get
        {
            return movingPoints;
        }
    }

    public TypePath GetTypePath
    {
        get
        {
            return typePath;
        }
    }

    private void Start()
    {
        LoadPointPath();
    }

    public void TakePropertiesPath(TypePath typePath, float playerSpeed)
    {
        this.typePath = typePath;

        this.playerSpeed = playerSpeed;
    }

    public void LoadPointPath()
    {
        DateJsonController getMovingPoints = new DateJsonController();

        movingPoints = getMovingPoints.LoadPoints();

        startPosition = movingPoints[0];

        DestroyPlanes();

        CreatePanels();
    }

    private void CreatePanels()
    {
        for (int i = 0; i < movingPoints.Length; i++)
        {
            GameObject plane = Instantiate(planePrefab, new Vector3(movingPoints[i].x, movingPoints[i].y - 0.5f, movingPoints[i].z), Quaternion.identity);

            plane.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f) );

            plane.transform.parent = planesParent.transform;
        }
    }

    private void DestroyPlanes()
    {
        while (true)
        {
            if (planesParent.transform.childCount > 1)
            {
                DestroyImmediate(planesParent.transform.GetChild(1).gameObject);
            }
            else
            {
                break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        try
        {
            int sigmentsNumber = 20;
            Vector3 preveousePoint = movingPoints[0];

            for (int p = 0; p < movingPoints.Length; p++)
            {
                for (int i = 0; i < sigmentsNumber; i++)
                {
                    float paremeter = (float)i / sigmentsNumber;

                    Vector3 point = Bezier.GetPoint(movingPoints[p], new Vector3(movingPoints[p].x - interval, movingPoints[p].y - interval, movingPoints[p].z), 
                        new Vector3(movingPoints[p + 1].x - interval, movingPoints[p + 1].y - interval, movingPoints[p + 1].z), movingPoints[p + 1], paremeter);
                   
                    Gizmos.DrawLine(preveousePoint, point);
                    
                    preveousePoint = point;
                }

                Gizmos.DrawLine(movingPoints[p], movingPoints[p + 1]);
            }
        }
        catch
        {

        }
    }
}
