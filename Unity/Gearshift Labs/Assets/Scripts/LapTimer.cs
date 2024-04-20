using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class LapTimer : MonoBehaviour
{
    private float startTime;
    private float elapsedTime;
    private bool hasStartedLap = false;
    private float bestTime;
    public GameObject lapText;
    public GameObject bestLapText;

    private int checkpoint;

    // Start is called before the first frame update
    void Start()
    {  
        checkpoint = 0;
        bestTime = float.MaxValue;
        loadTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStartedLap)
        {
            elapsedTime = Time.time - startTime;
            int minutes = (int)(elapsedTime/60);
            int seconds = (int)(elapsedTime-minutes*60);
            lapText.GetComponent<Text>().text = "Laikas: " + minutes + ":" + seconds + "\n" + "Kontrolinis taskas: " + checkpoint + "/3";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((elapsedTime != null && elapsedTime > 10 && checkpoint >= 3) || (hasStartedLap == false)){
            checkpoint = 0;
            int minutes = (int)(elapsedTime/60);
            int seconds = (int)(elapsedTime-minutes*60);
            if (elapsedTime < bestTime){
                bestLapText.GetComponent<Text>().text = "Rekordas: " + minutes + ":" + seconds;
                bestTime=elapsedTime;
                saveTime(bestTime);
            }
            hasStartedLap = true;
            startTime = Time.time;
        }

    }

    private void loadTime()
    {
        string filePath = Application.persistentDataPath + "/record.txt";
        try
        {
            string timeText = File.ReadAllText(filePath);
            float time;
            if (float.TryParse(timeText, out time))
            {
                int minutes = (int)(time / 60);
                int seconds = (int)(time - minutes * 60);
                bestLapText.GetComponent<Text>().text = "Rekordas: " + minutes + ":" + seconds;
            }
        }
        catch (Exception e)
        {
        }

    }

    private void saveTime(float time){
        string filePath = Application.persistentDataPath + "/record.txt";
        File.WriteAllText(filePath, time.ToString());

    }

    public void IncrementCheckPoint(){
        if(checkpoint < 3){
            checkpoint++;
        }
    }
}
