using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GrapplingHookGun : MonoBehaviour
{
    public float BulletSpeed;//how fast to fire the hook
    public GameObject Rope;//refrence to the rope 
    public GameObject ActiveHook;//the hook with a rigidbody
    public GameObject StaticHook;//the hook without a rigidbody
    public Transform ConnectionPoint;//point that we should calculate forces with (barrel of the gun)
    public SteamVR_Action_Boolean FireAction;//SteamVR action for fireing
    public SteamVR_Input_Sources handSource;
    public Hand hand;

    private bool Grappling;//remeber if we are grappling 
    // Start is called before the first frame update
    void Start()
    {
        //make sure all thee right things are visible
        StaticHook.SetActive(true);
        Rope.SetActive(false);
        ActiveHook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Grappling)//if we are grappling
        {
            Rope.SetActive(true);//switch on or off the right objects
            ActiveHook.SetActive(true);
            StaticHook.SetActive(false);
            if (ActiveHook.GetComponent<GrapplingHook>().attached)//if the hook is attached to an object
            {
                Player.instance.GetComponent<Rigidbody>().useGravity = false;
                //GetComponent<Interactable>().GrippedBy.GetComponent<Rigidbody>().useGravity = false;//Disable gravity for more awsome grapples 
                //GetComponent<Interactable>().GrippedBy.GetComponent<Player>().DisableMovment = true;//this is to activate an alternate movment system for while I'm in the air. It's basicly the walking system but without the speed regulating part.
            }
            if (FireAction.GetStateUp(handSource))//if we let go of the trigger
            {
                Player.instance.GetComponent<Rigidbody>().useGravity = true;
                //GetComponent<Interactable>().GrippedBy.GetComponent<Rigidbody>().useGravity = true;//reactivate gravity

                //GetComponent<Interactable>().GrippedBy.GetComponent<Player>().DisableMovment = false;//turn off alternate movement system
                ActiveHook.GetComponent<GrapplingHook>().Retract();//tell the hook to detach and retract
                Grappling = false;
            }
        }
        else//if we arn't grappling
        {
            StaticHook.SetActive(true);//hide and show the relevent objects
            Rope.SetActive(false);
            ActiveHook.SetActive(false);
            if (FireAction.GetStateDown(handSource))//check if we want to fire
            {

                Grappling = true;//set grappling to true
                ActiveHook.transform.position = StaticHook.transform.position + StaticHook.transform.forward * .1f;//set the active grappling hooks position to the tip of the gun
                ActiveHook.transform.rotation = StaticHook.transform.rotation;//set rotation
                ActiveHook.GetComponent<Rigidbody>().isKinematic = false;//make sure it's active
                ActiveHook.GetComponent<Rigidbody>().velocity = BulletSpeed * ActiveHook.transform.forward;//set it's velocity 
                ActiveHook.SetActive(true);//set the hook as active
                ActiveHook.GetComponent<GrapplingHook>().Fire();//fire the hook
            }
        }
    }
}