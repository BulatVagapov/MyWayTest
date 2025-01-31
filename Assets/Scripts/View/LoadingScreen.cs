using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image loadingProgressFillImage;
    public Image LoadingProgressFillImage => loadingProgressFillImage;

    public void SetLoadingImageFillAmount(float progress, float maxProgress)
    {
        loadingProgressFillImage.fillAmount = progress / maxProgress;
    }
}
