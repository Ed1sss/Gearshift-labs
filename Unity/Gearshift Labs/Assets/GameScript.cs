using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameScript : MonoBehaviour
{
    public listScript carlist;
    public CarController carController;
    //Speedo Stuff
    [Header("UI")]
    
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;
   
    public Text speedLabel; // The label that displays the speed;
    public Text GearLabel;
    public RectTransform arrow; // The arrow in the speedometer
    private float speed = 0.0f;

    //Music Stuff
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] songs;

    //Car stuff
    [Header("Car stuff")]
    public GameObject StartPos;
    private GameObject target;
    private Rigidbody targetbody;
    public CinemachineVirtualCamera Camera;
    private int CurrentGear = 1;
    private float[] gearRatios;
    private float[] maxSpeedsPerGear;
    private bool hasShifted = false;
    private void Start()
    {
        PlayRandomSong();
         
         target = Instantiate(carlist.Carlist[PlayerPrefs.GetInt("pointer")], StartPos.transform.position, StartPos.transform.rotation) as GameObject;
        AssignCamera();
        CarController carController = target.GetComponent<CarController>();
        targetbody = target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        hasShifted = false;
        if (!audioSource.isPlaying)
        {
            // Play a new random song

            PlayRandomSong();
            
        }
        if (carController != null)
        {
             gearRatios = carController.gearRatios;
             maxSpeedsPerGear = carController.maxSpeedsPerGear;

        }
        CarSpeed();
       
            if (Input.GetKeyDown(KeyCode.E) && CurrentGear< 5 && !hasShifted)
            {
                CurrentGear++;
                GearLabel.text = CurrentGear.ToString(); // upshift
                hasShifted = true; // Set the flag to true to indicate a shift action has been performed
            }
            else if (Input.GetKeyDown(KeyCode.Q) && CurrentGear > 0 && !hasShifted)
            {
                CurrentGear--;
                GearLabel.text = CurrentGear.ToString(); // downshift
                hasShifted = true; // Set the flag to true to indicate a shift action has been performed
            }


       
    }
        public void CarSpeed()
    {
        // 3.6f to convert in kilometers
        // ** The speed must be clamped by the car controller **
        speed = targetbody.velocity.magnitude * 3.6f;
        float engineRPM = CalculateEngineRPM(speed,CurrentGear);
        engineRPM *= 5f;
      
        float rpmRatio = engineRPM / (maxSpeedsPerGear[CurrentGear] * 1000f);
        float needleAngle = Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, rpmRatio);
      

        if (speedLabel != null)
            speedLabel.text = ((int)speed) + " km/h";
        if(CurrentGear ==0)
            arrow.localEulerAngles = new Vector3(0, 0, minSpeedArrowAngle);
        else
        if (arrow != null)
            arrow.localEulerAngles = new Vector3(0, 0, needleAngle);
        

    }
    private void PlayRandomSong()
    {
        // Check if there are songs in the array
        if (songs.Length == 0)
        {
            Debug.LogError("No songs found in the array!");
            return;
        }

        // Select a random song from the array
        int randomIndex = Random.Range(0, songs.Length);
        AudioClip randomSong = songs[randomIndex];

        // Set the selected song as the audio clip to play
        audioSource.clip = randomSong;

        // Play the selected song
        audioSource.Play();

    }
    private void AssignCamera()
    {
        Camera.Follow = (target.transform.Find("Camera").gameObject).transform;
        Camera.LookAt = (target.transform.Find("Camera").gameObject).transform;
       
    }
    private float CalculateEngineRPM(float currentSpeed, int currentGear)
    {
        // Get the maximum speed for the current gear
        float maxSpeedForGear = maxSpeedsPerGear[currentGear];

        // Calculate the maximum RPM for the current gear (assuming max speed is reached at 10 * 1000 RPM)
        float maxRPMForGear = (maxSpeedForGear * 1000) / 10;

        // Calculate the ratio of the current speed to the maximum speed for the current gear
        float speedRatio = currentSpeed / maxSpeedForGear;

        // Calculate the current RPM based on the speed ratio and maximum RPM for the gear
        float currentRPM = speedRatio * maxRPMForGear;

        return currentRPM;
    }
}
