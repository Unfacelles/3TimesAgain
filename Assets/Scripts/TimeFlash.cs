using UnityEngine;
using UnityEngine.UI;

public class TimeFlash : MonoBehaviour
{
    public float flashDuration = 0.2f;

    private Image flashImage;
    private float timer;
    private bool isFlashing;

    void Start()
    {
        flashImage = GetComponent<Image>();
        SetAlpha(0f);
    }

    void Update()
    {
        if (isFlashing)
        {
            timer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / flashDuration);
            SetAlpha(alpha);

            if (timer <= 0f)
            {
                isFlashing = false;
                SetAlpha(0f);
            }
        }
    }

    public void TriggerFlash()
    {
        timer = flashDuration;
        isFlashing = true;
        SetAlpha(1f);
    }

    void SetAlpha(float alpha)
    {
        if (flashImage != null)
        {
            Color c = flashImage.color;
            c.a = alpha;
            flashImage.color = c;
        }
    }
}
