using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private LapTimer timerScript;

    public GameObject StartLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        timerScript = StartLine.GetComponent<LapTimer>();
        timerScript.IncrementCheckPoint();
    }
}
