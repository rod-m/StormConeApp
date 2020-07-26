using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvaser : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform watchCam;
    public Text debugText;
    public bool debugMessages = true;
    void Start()
    {
        watchCam = Camera.current.transform;
        //debugText = gameObject.GetComponent<Text>();
        if (debugMessages)
        {
            if (debugText == null)
            {
                debugMessages = false;
                return;
            }
            else
            {
                debugText.text = "Start Debug";
            }
            if (watchCam == null && debugMessages)
            {
                debugMessages = false;
                debugText.text = "No Camera";
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debugMessages)
        {
           // debugText.text = "X: " + watchCam.position.x + " Z: " + watchCam.position.z;
        }
    }
}
