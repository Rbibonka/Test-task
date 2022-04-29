using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TravelTimer : MonoBehaviour
{
    [SerializeField]
    private Text watch;

    private string secondsText;
    private string minutesText;
    private string hourseText;

    private int normalTime = 60;

    Coroutine timerCoroutine;

    public void StartTimer()
    {
       timerCoroutine = StartCoroutine(IncreasingTime());
    }

    public void StopTimer()
    {
        StopCoroutine(timerCoroutine);
    }

    private string AddingZero(int time)
    {
        if (time < 10)
        {
            return "0" + time.ToString();
        }
        else
        {
            return time.ToString();
        }
    } 

    private IEnumerator IncreasingTime()
    {
        int hourse = 0;

        watch.text = "00:00:00";

        while (true)
        {
            for (int minutes = 0; minutes < normalTime; minutes++)
            {
                for (int seconds = 0; seconds < normalTime; seconds++)
                {
                    secondsText = AddingZero(seconds);

                    minutesText = AddingZero(minutes);

                    hourseText = AddingZero(hourse);

                    watch.text = $"{hourseText}:{minutesText}:{secondsText}";

                    yield return new WaitForSeconds(1f);
                }
            }
            hourse++;
        }
    }
}
