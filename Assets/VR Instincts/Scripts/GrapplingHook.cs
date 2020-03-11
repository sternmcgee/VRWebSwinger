using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public bool attached;//are we attached to somthing?
    public bool retracting;//are we retracting?
    public Transform retractionPoint;//where should we aim the forces 
    public float RetractionSpeed;//how fast should we retract 
    public GameObject Gun;//the gun object
    private GameObject CameraRig;

    // Update is called once per frame
    void Update()
    {
        if (retracting)
        {
            //when retracting move the hook towards the gun
            transform.position = Vector3.MoveTowards(transform.position, retractionPoint.position, 2.0f) + retractionPoint.forward * .05f;
            transform.rotation = retractionPoint.rotation;
        }
        if (attached)
        {
            //if attached to something apply force on the player.
            CameraRig.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.position - retractionPoint.position) * RetractionSpeed, ForceMode.VelocityChange);


        }
        else
        {
            //if we are not attached check the space in front of the hook to see if we are going to hit something 
            RaycastHit Hit;
            Debug.DrawRay(transform.position, transform.forward * GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime, Color.blue, 3);
            if (Physics.Raycast(transform.position, transform.forward, out Hit, GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime))
            {
                if (!attached && !retracting && !Hit.collider.GetComponent<GrapplingHookGun>())
                {
                    transform.position = Hit.point;//if we hit something stick to it
                    GetComponent<Rigidbody>().isKinematic = true;
                    attached = true;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!attached && !retracting && !collision.collider.GetComponent<GrapplingHookGun>())
        {//if we haven't attached to anything, attach to the collision

            GetComponent<Rigidbody>().isKinematic = true;
            attached = true;

        }
        if (collision.collider.GetComponent<GrapplingHookGun>())//fix bugs with the interaction system 
        {

            collision.collider.GetComponent<Interactable>().touchCount--;
        }

    }
    private void OnCollisionExit(Collision collision)//fix interaction issues
    {
        if (collision.collider.GetComponent<GrapplingHookGun>())
        {
            collision.collider.GetComponent<Interactable>().touchCount++;
        }

    }
    public void Retract()
    {
        GetComponent<Rigidbody>().isKinematic = true;//set our state to retracting
        attached = false;
        retracting = true;
        //retractionSpring.SetActive(true);
    }
    public void Fire()//get ready to fire
    {
        CameraRig = Gun.GetComponent<Interactable>().GrippedBy;
        GetComponent<Rigidbody>().isKinematic = false;
        retracting = false;
        //retractionSpring.SetActive(false);
    }
}
