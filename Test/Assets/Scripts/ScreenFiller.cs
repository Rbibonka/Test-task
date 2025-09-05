using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFiller : MonoBehaviour
{
    [SerializeField]
    private Image Image;

    float fadeDuration = 2f;

    public async UniTask Fade(float startAlpha, float endAlpha, CancellationToken ct)
    {
        var a = 0f;
        float timer = 0;

        while (Image.color.a != endAlpha)
        {
            a = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);

            var color = Image.color;
            color.a = a;
            Image.color = color;

            timer += Time.deltaTime;

            await UniTask.NextFrame(ct);
        }
    }
}