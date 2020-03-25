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
    private bool isOnCoolDown = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        //{
        //    if (Player.instance.hands[handIndex] != null)
        //    {
        //        SteamVR_Behaviour_Skeleton skeleton = Player.instance.hands[handIndex].skeleton;
        //        if (skeleton != null)
        //        {
        //            //Debug.LogFormat("{0:0.00}, {1:0.00}, {2:0.00}, {3:0.00}, {4:0.00}", skeleton.thumbCurl, skeleton.indexCurl, skeleton.middleCurl, skeleton.ringCurl, skeleton.pinkyCurl);

        //            if ((skeleton.indexCurl <= openFingerAmount && skeleton.pinkyCurl <= openPinkyAmount &&
        //                skeleton.thumbCurl <= openFingerAmount) && (skeleton.ringCurl >= closedFingerAmount && skeleton.middleCurl >= closedFingerAmount))
        //            {
        //                WebShootSignRecognized(true);
        //            }
        //            else
        //            {
        //                WebShootSignRecognized(false);
        //            }
        //        }
        //    }
        //}
    }

    private void WebShootSignRecognized(bool currentWebShootState)
    {
        if (lastWebShootState == false && currentWebShootState == true)
        {
            //implement webshooting here
            Debug.Log("Web was shot!");

            //shoot web
            //if (!isOnCoolDown)
            //    StartCoroutine(ShootWeb());
        }

        lastWebShootState = currentWebShootState;
    }

    private IEnumerator ShootWeb()
    {
        isOnCoolDown = true;

        //implement webshooting here
        Debug.Log("Web was shot!");

        yield return new WaitForSeconds(0.5f);
        isOnCoolDown = false;
    }
}
