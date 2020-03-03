using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class trackerDebug : MonoBehaviour
{
    public SteamVR_Input_Sources trackerSource;
    public SteamVR_Action_Pose trackerPose;

    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tracker_position = trackerPose.GetLocalPosition(trackerSource);
        print(tracker_position);
    }
}
