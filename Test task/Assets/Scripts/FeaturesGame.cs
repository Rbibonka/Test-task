using UnityEngine;
using UnityEngine.UI;

public class FeaturesGame : MonoBehaviour
{
    [SerializeField]
    private InputField quantityPointsField;

    [SerializeField]
    private Toggle loopCheck;

    [SerializeField]
    private PropertiesPath propertiesPath;

    [SerializeField]
    private Slider sliderSpeed;

    private int quantityPoints = 0;

    private float playerSpeed;

    PropertiesPath.TypePath typePath = PropertiesPath.TypePath.loop;

    private void Start()
    {
        GiveProperties();
    }

    public void ChangedSlider()
    {
        playerSpeed = sliderSpeed.value;

        GiveProperties();
    }

    public void ChangedToggleLoop()
    {
        if (loopCheck.isOn)
        {
            typePath = PropertiesPath.TypePath.loop;
        }
        else
        {
            typePath = PropertiesPath.TypePath.line;
        }

        GiveProperties();
    }

    public void EndChangedText()
    {
        quantityPoints = int.Parse(quantityPointsField.text);

        if (quantityPoints <= 0)
        {
            quantityPoints = 0;

            Debug.Log("Число должно быть больше нуля");
        }
    }
    
    public void CreateRandomMovingPoints()
    {
        WorkinWithMovingPoints generatingMovingPoints = new WorkinWithMovingPoints();

        generatingMovingPoints.RandomGenerationMovingPoints(quantityPoints);

        propertiesPath.LoadPointPath();
    }

    private void GiveProperties()
    {
        propertiesPath.TakePropertiesPath(typePath, playerSpeed);
    }
}
