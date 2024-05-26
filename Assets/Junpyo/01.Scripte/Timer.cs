using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timeElapsed;
    private bool isGameClear = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform.root.gameObject);
    }

    void Update()
    {
        if (isGameClear) return;

        timeElapsed += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(timeElapsed % 60F);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameClear()
    {
        isGameClear = true;
        timerText.color = Color.white;
        PlayerPrefs.SetFloat("ClearTime", timeElapsed);
    }

    public string GetTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(timeElapsed % 60F);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}