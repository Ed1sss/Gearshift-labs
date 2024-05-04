using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public GameObject RotateObject;
    public listScript carlist; 
    public float rotatespeed;
    public int carpointer = 0;
    private void Awake()
    {
        PlayerPrefs.SetInt("pointer", 0);
        carpointer = PlayerPrefs.GetInt("pointer");

        GameObject childObject = Instantiate(carlist.Carlist[carpointer], Vector3.zero, Quaternion.identity) as GameObject;
        childObject.transform.parent = RotateObject.transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion currentRotation = RotateObject.transform.rotation;

        // Calculate the new rotation based on the desired Y rotation (in degrees)
        float newYRotation = currentRotation.eulerAngles.y + rotatespeed * Time.deltaTime;

        // Create a new rotation quaternion with the updated Y rotation
        Quaternion newRotation = Quaternion.Euler(0f, newYRotation, 0f);

        // Apply the new rotation to the RotateObject
        RotateObject.transform.rotation = newRotation;
    }
    public void Rightbutton()
    {
        if (carpointer < carlist.Carlist.Length - 1) {
            DestroyCar();
            carpointer++;
            PlayerPrefs.SetInt("pointer", carpointer);
            GameObject childObject = Instantiate(carlist.Carlist[carpointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = RotateObject.transform;
        }
    }

    public void Leftbutton()
    {
        if (carpointer >0)
        {
            DestroyCar();
            carpointer--;
            PlayerPrefs.SetInt("pointer", carpointer);
            GameObject childObject = Instantiate(carlist.Carlist[carpointer], Vector3.zero, Quaternion.identity) as GameObject;
            childObject.transform.parent = RotateObject.transform;
        }
    }
    public void DestroyCar()
    {

        GameObject car = RotateObject.transform.GetChild(1).gameObject;
        Destroy(car);
    }
    public void PlayGame()
    {

        SceneManager.LoadScene(3);

    }

}
