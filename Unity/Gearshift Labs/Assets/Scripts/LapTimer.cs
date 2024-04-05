using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class LapTimer : MonoBehaviour
{
    private float startTime;
    private float elapsedTime;
    private bool hasStartedLap = false;
    private float bestTime;
    public GameObject lapText;
    public GameObject bestLapText;

    // Start is called before the first frame update
    void Start()
    {
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
            lapText.GetComponent<Text>().text = "Laikas: " + minutes + ":" + seconds;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(elapsedTime != null && elapsedTime > 10 && elapsedTime < bestTime){
            int minutes = (int)(elapsedTime/60);
            int seconds = (int)(elapsedTime-minutes*60);
            bestLapText.GetComponent<Text>().text = "Rekordas: " + minutes + ":" + seconds;
            bestTime=elapsedTime;
            saveTime(bestTime);
        }
        hasStartedLap = true;
        startTime = Time.time;

    }

    private void loadTime(){
        string filePath = Application.persistentDataPath + "/record.txt";
        string timeText = File.ReadAllText(filePath);
        float time;
        if (float.TryParse(timeText, out time)){
            int minutes = (int)(time/60);
            int seconds = (int)(time-minutes*60);
            bestLapText.GetComponent<Text>().text = "Rekordas: " + minutes + ":" + seconds;
        }
    }

    private void saveTime(float time){
        string filePath = Application.persistentDataPath + "/record.txt";
        File.WriteAllText(filePath, time.ToString());

    }
}
