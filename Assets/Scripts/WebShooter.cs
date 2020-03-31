using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class WebShooter : MonoBehaviour
{
    public float closedFingerAmount = 0.7f;
    public float openFingerAmount = 0.3f;
    public float openPinkyAmount = 0.4f;
    private bool lastWebShootState = false;
    public GameObject grabbingHand = null;
    public bool shootStateUp = false;
    public bool shootStateDown = false;
    private Interactable interactable = null;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponentInChildren<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.gripped)
        {            
            if (grabbingHand == null)
            {
                if (interactable.Hand == SteamVR_Input_Sources.LeftHand)
                {
                    grabbingHand = GameObject.Find("Controller (left)");
                }
                else
                {
                    grabbingHand = GameObject.Find("Controller (right)");
                }
            }
            
            SteamVR_Behaviour_Skeleton skeleton = grabbingHand.GetComponent<GripController>().HandSkeleton;
            if (skeleton != null)
            {
                if ((skeleton.indexCurl <= openFingerAmount && skeleton.pinkyCurl <= openPinkyAmount && skeleton.thumbCurl <= openFingerAmount) && (skeleton.ringCurl >= closedFingerAmount && skeleton.middleCurl >= closedFingerAmount))
                {
                    StartCoroutine(WebShootSignRecognized(true));
                }
                else
                {
                    StartCoroutine(WebShootSignRecognized(false));
                }
            }
        }
    }

    private IEnumerator WebShootSignRecognized(bool currentWebShootState)
    {        
        if (lastWebShootState == false && currentWebShootState == true)
        {
            Debug.Log("Web was shot!");

            //send shoot web event           
            shootStateDown = true;
        }
        if (lastWebShootState == true && currentWebShootState == false)
        {
            Debug.Log("Web was released!");

            //send release web event
            shootStateUp = true;
        }

        lastWebShootState = currentWebShootState;
        yield return new WaitForSeconds(0.05f);
        shootStateDown = false;
        shootStateUp = false;
    }
}
