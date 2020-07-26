using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvaser : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform watchCam;
    private Text debugText;
    public bool debugMessages = true;
    void Start()
    {
        watchCam = Camera.current.transform;
        debugText = GetComponent<Text>();
        if (debugMessages)
        {
            if (debugText == null)
            {
                debugMessages = false;
                debugText.text = "No Text Canvas";
            }
            else
            {
                debugText.text = "Start Debug";
            }
            if (watchCam == null)
            {
                debugMessages = false;
                debugText.text = "No Camera";
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        debugText.text = "X: " + watchCam.position.x + " Z: " + watchCam.position.z;
    }
}
