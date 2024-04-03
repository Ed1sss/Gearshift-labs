using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking, isHandbrake;
    private bool Reset;

    public AudioSource audioSource;
    public AudioClip[] songs;

    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle,HandbreakForce;
    private float speed = 0.0f;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;


    [SerializeField] private Rigidbody Carbody;
    [SerializeField] private AudioSource carAudioSource;
    [SerializeField] private float startingPitch = 1f; // Starting pitch of the engine sound

    private void FixedUpdate()
    {
        ResetCar();
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        CarSounds();

        if (!audioSource.isPlaying)
        {
            // Play a new random song
            PlayRandomSong();
        }
    }

    private void GetInput()
    {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input (active only while holding down W)
        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f; // Full acceleration

      
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f; // Full reverse
        }
        else
        {
            verticalInput = 0f; // No acceleration or reverse
        }
        
        //Reset Input
        Reset = Input.GetKey(KeyCode.R);

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);
        
 

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

    private void CarSounds()
    {
        float minSpeed = 0f, maxSpeed = 180f;
        float minPitch = 0.5f, maxPitch = 10f;
        speed = Carbody.velocity.magnitude * 3.6f;
        float pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.InverseLerp(minSpeed, maxSpeed, speed));

        // Set the pitch of the car sound
        carAudioSource.pitch = pitch;
    }
    private void ResetCar()
    {
        if (Reset)
            Carbody.transform.rotation = Quaternion.identity;
 
    }

    private void HandleMotor()
    {

        rearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        rearRightWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        // Adjust braking force based on whether handbrake is engaged
        if (isHandbrake)
        {
            currentbreakForce = HandbreakForce * breakForce;
            ApplyHandbrake();
        }

        ApplyBreaking();
        
    }
    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }
    private void ApplyHandbrake()
    {
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }


    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }
   

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;

        // Check if this wheel belongs to the right side
        bool isRightWheel = (wheelTransform == frontRightWheelTransform || wheelTransform == rearRightWheelTransform);

        // Set the desired rotation based on whether it's a right wheel or not
        if (isRightWheel)
        {
            // Adjust the Z rotation of the wheel collider
            wheelCollider.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }

        // Obtain the world pose information after adjusting the collider rotation
        wheelCollider.GetWorldPose(out pos, out rot);

        // Set the rotation of the wheel
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
        isRightWheel = false;
    }
}
