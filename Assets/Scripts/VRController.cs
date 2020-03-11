using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRController : MonoBehaviour
{
    public float gravity = 30.0f;
    public float sensitivity = 0.1f;
    public float minSpeed = 0.1f;
    public float maxSpeed = 1.0f;
    public float smoothSpeed = 1.0f;
    public float smoothTime = 1.0f;

    public SteamVR_Action_Boolean movePress = null;

    [SerializeField] private float speed = 0.0f;

    public GameObject leftHand = null;
    public GameObject rightHand = null;

    private CharacterController characterController = null;
    public CapsuleCollider playerCollider = null;
    private Transform cameraRig = null;
    private Transform head = null;
    private float prevDistance = 0f;
    private float currDistance = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
        if (leftHand == null)
            Debug.Log("Error: Left hand reference not assigned.");
        if (rightHand == null)
            Debug.Log("Error: Right hand reference not assigned.");
    }

    // Update is called once per frame
    private void Update()
    {
        HandleHeight();
        CalculateMovement();
    }

    private void HandleHeight()
    {
        // get the head in local space
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        playerCollider.height = headHeight;

        // calculate capsule height
        Vector3 newCenter = Vector3.zero;
        newCenter.y = playerCollider.height / 2;
        //newCenter.y += playerCollider.radius;
        newCenter.y += cameraRig.localPosition.y;

        // move capsule in local space
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        // apply
        playerCollider.center = newCenter;
    }

    private void CalculateMovement()
    {
        //return if move actions are null
        if (movePress == null)
            return;

        // determine movement orientation with sum of controller forward vectors

        float orientationAverage = (leftHand.transform.eulerAngles.y + rightHand.transform.eulerAngles.y) / 2;
        Vector3 orientationEuler = new Vector3(0.0f, orientationAverage, 0.0f);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        // if not moving
        if (movePress.GetStateUp(SteamVR_Input_Sources.Any))
            speed = 0;

        // if button pressed
        if (movePress.GetState(SteamVR_Input_Sources.LeftHand) && movePress.GetState(SteamVR_Input_Sources.RightHand))
        {
            //get initial distance between controllers if first function call
            if (prevDistance == 0f)
            {
                prevDistance = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);
                return;
            }

            //get current distance between controllers
            currDistance = Vector3.Distance(leftHand.transform.position, rightHand.transform.position);

            //calculate move value based on difference of new and old distance 
            float moveValue = Mathf.Abs(currDistance - prevDistance);
            prevDistance = currDistance;

            // add and clamp
            //speed = moveValue * sensitivity;
            speed = Mathf.SmoothDamp(speed, moveValue * sensitivity, ref smoothSpeed, smoothTime);
            speed = Mathf.Clamp(speed, 0, maxSpeed);

            // orientation
            movement += orientation * (speed * Vector3.forward);
        }

        // gravity
        movement.y -= gravity * Time.deltaTime;

        // apply
        Player.instance.GetComponent<Rigidbody>().AddForce(movement * Time.deltaTime);
        //characterController.Move(movement * Time.deltaTime);
    }
}