using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class WebShooter : MonoBehaviour
{
    public float closedFingerAmount = 0.9f;
    public float openFingerAmount = 0.1f;
    public float openPinkyAmount = 0.4f;
    private bool lastWebShootState = false;
    public Hand webHand = null;
    public bool shootActionState = false;

    // Start is called before the first frame update
    void Start()
    {
        if (webHand == null)
            Debug.LogError("Error: Web Hand reference not assigned on the WebShooter instance.");
    }

    // Update is called once per frame
    void Update()
    {
        if (webHand != null)
        {
            SteamVR_Behaviour_Skeleton skeleton = webHand.skeleton;
            if (skeleton != null)
            {
                if ((skeleton.indexCurl <= openFingerAmount && skeleton.pinkyCurl <= openPinkyAmount && skeleton.thumbCurl <= openFingerAmount) && (skeleton.ringCurl >= closedFingerAmount && skeleton.middleCurl >= closedFingerAmount))
                {
                    WebShootSignRecognized(true);
                }
                else
                {
                    WebShootSignRecognized(false);
                }
            }
        }
    }

    private void WebShootSignRecognized(bool currentWebShootState)
    {
        if (lastWebShootState == false && currentWebShootState == true)
        {
            Debug.Log("Web was shot!");

            //send shoot web event           
            shootActionState = true;
        }
        if (lastWebShootState == true && currentWebShootState == false)
        {
            Debug.Log("Web was released!");

            //send release web event
            shootActionState = false;
        }

        lastWebShootState = currentWebShootState;
    }
}
