using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class WebShooter : MonoBehaviour
{
    public float closedFingerAmount = 0.1f;
    public float openFingerAmount = 0.9f;
    private bool lastWebShootState = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            if (Player.instance.hands[handIndex] != null)
            {
                SteamVR_Behaviour_Skeleton skeleton = Player.instance.hands[handIndex].skeleton;
                if (skeleton != null)
                {
                    //Debug.LogFormat("{0:0.00}, {1:0.00}, {2:0.00}, {3:0.00}, {4:0.00}", skeleton.thumbCurl, skeleton.indexCurl, skeleton.middleCurl, skeleton.ringCurl, skeleton.pinkyCurl);

                    if ((skeleton.indexCurl <= openFingerAmount && skeleton.pinkyCurl <= openFingerAmount &&
                        skeleton.thumbCurl >= openFingerAmount) && (skeleton.ringCurl >= closedFingerAmount && skeleton.middleCurl >= closedFingerAmount))
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
    }

    private void WebShootSignRecognized(bool currentWebShootState)
    {
        if (lastWebShootState == false && currentWebShootState == true)
        {
            Debug.Log("Web Shooting Gesture Recognized!");
            //shoot web
        }

        lastWebShootState = currentWebShootState;
    }

    private IEnumerator ShootWeb()
    {
        //implement webshooting here
        yield return new WaitForSeconds(1.0f);
    }
}
