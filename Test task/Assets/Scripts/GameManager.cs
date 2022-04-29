using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private GameObject stopButton;

    [Header("Scripts")]
    [SerializeField]
    private MovementAlonePath movementAlonePath;

    [SerializeField]
    private TravelTimer travelTimer;

    public void StartGame()
    {
        ChangeVisibleUI();

        travelTimer.StartTimer();

        movementAlonePath.StartMoving();
    }

    public void StopGame()
    {
        ChangeVisibleUI();

        movementAlonePath.StopMoving();

        StopTimer();
    }

    public void StopTimer()
    {
        travelTimer.StopTimer();
    }

    private void ChangeVisibleUI()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);

        gameObject.SetActive(!gameObject.activeSelf);

        stopButton.SetActive(!stopButton.activeSelf);
    }
}
