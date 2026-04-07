using System;
using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static timer instance;
    public TextMeshProUGUI timerUI;
    public ScoreUI scoreUI;
    private TimeSpan timePlaying;
    private bool timerGoing;
    private float elapsedTime;
    string timertxt;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timerUI.text = "00:00:00.00";
        timerGoing = false;
        beginTimer();
    }

    public void beginTimer()
    {
        timerGoing = true;
        
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }
    public void endTimer()
    {
        timerGoing = false;
        scoreUI.endtimeAndFloor(timertxt, GameManager.Instance.level);
    }
    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timertxt = $"{timePlaying.ToString("hh':'mm':'ss'.'ff")}";
            timerUI.text = timertxt;
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
