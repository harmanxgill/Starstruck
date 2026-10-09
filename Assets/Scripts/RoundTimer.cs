using UnityEngine;
using TMPro;

public class RoundTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float roundDuration = 180f;

    private float remainingTime;

    void Start()
    {
        remainingTime = roundDuration;
        UpdateDisplay();
    }

    void Update()
    {
        if (remainingTime <= 0f) return;

        remainingTime = Mathf.Max(
            0f, remainingTime - Time.deltaTime
        );

        UpdateDisplay();

        if (remainingTime <= 0f)
        {
            timerText.color = Color.red;
            Debug.Log("Practical finished!");
        }
    }

    void UpdateDisplay()
    {
        int seconds = Mathf.CeilToInt(remainingTime);
        timerText.text = $"{seconds / 60:00}:{seconds % 60:00}";
    }
}