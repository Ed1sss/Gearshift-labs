using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour
{
     float horizontalInput, verticalInput;
     float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    private bool Reset;



    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
    private float speed = 0.0f;
    private int CurrentGear = 1;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;


    [SerializeField] private Rigidbody Carbody;
    [SerializeField] private AudioSource carAudioSource;


    [SerializeField] public float[] gearRatios = { 0.0f, 4.0f, 3.0f, 2.0f, 1.5f, 1.2f, 1.0f }; // Example gear ratios
    [SerializeField] public float[] maxSpeedsPerGear = { 0.001f, 20.0f, 50.0f, 90.0f, 140.0f, 200.0f, 240.0f }; // Example max speeds per gear

    private bool hasShifted = false;

    private void Update()
    {
        ResetCar();
        GetInput();
        HandleMotor();
        ApplyBreaking();
        HandleSteering();
        UpdateWheels();
        CarSounds();

        
    }

    private void GetInput()
    {
        hasShifted = false;
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
        ;
        if (Input.GetKey(KeyCode.Space))
        {
            isBreaking = true; // Full acceleration
            Debug.Log("IS BREAKING");
    
        }
        else
            isBreaking = false;
 

        if (Input.GetKeyDown(KeyCode.E) && CurrentGear < 5 && !hasShifted)
        {
            CurrentGear++;
            Debug.Log("Upshift");
            Debug.Log(maxSpeedsPerGear[CurrentGear]);
            hasShifted = true; // Set the flag to true to indicate a shift action has been performed
        }
        else if (Input.GetKeyDown(KeyCode.Q) && CurrentGear > 0 && !hasShifted)
        {
            CurrentGear--;
            Debug.Log("downshift");
            Debug.Log(maxSpeedsPerGear[CurrentGear]);
            hasShifted = true; // Set the flag to true to indicate a shift action has been performed
        }


    }
       private void HandleMotor()
        {
            float gearRatio = gearRatios[CurrentGear];
            rearLeftWheelCollider.motorTorque = verticalInput * motorForce*gearRatio;
            rearRightWheelCollider.motorTorque = verticalInput * motorForce * gearRatio;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce * gearRatio;
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce * gearRatio;

            float maxSpeed = maxSpeedsPerGear[CurrentGear];
            if (speed > maxSpeed)
            {
                // Apply braking force to limit speed
                isBreaking = true;
                currentbreakForce = breakForce*10;
            }
            else
            {
                isBreaking = false;
                currentbreakForce = 0f;
            }
           
    
        
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

    
    private void ApplyBreaking()
    {
        if (isBreaking)
        {
            currentbreakForce = breakForce;
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
        }
        else
        {
            currentbreakForce = 0f;
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
          
        }
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
