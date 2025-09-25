using UnityEngine;
using TMPro;
using System;

public class CountdownTimerTMP : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text penaltyText;
    [SerializeField] private int startSeconds = 60;
    [SerializeField] private int penaltySeconds = 0;
    [SerializeField] private bool autoStart = true;

    private float remaining;
    private bool running;

    public Action OnTimerEnded;

    void Awake()
    {
        remaining = startSeconds;
        UpdateUI();
    }

    void Start()
    {
        if (autoStart) running = true;
    }

    void Update()
    {
        if (!running) return;
        remaining -= Time.deltaTime;          // ✅ obeys Time.timeScale
        if (remaining <= 0f)
        {
            remaining = 0f;
            running = false;
            OnTimerEnded?.Invoke();
        }
        UpdateUI();
    }

    public void AddPenalty(int seconds)
    {
        penaltySeconds += seconds;
        remaining = Mathf.Max(0f, remaining - seconds);
        UpdateUI();
    }

    public void StartTimer() => running = true;
    public void StopTimer() => running = false;
    public void ResetTimer(int newStartSeconds)
    {
        startSeconds = newStartSeconds;
        penaltySeconds = 0;
        remaining = startSeconds;
        running = false;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (timerText != null) timerText.text = $"Time: {Mathf.CeilToInt(remaining):00}";
        if (penaltyText != null) penaltyText.text = $"Penalty: {penaltySeconds}s";
    }
}
