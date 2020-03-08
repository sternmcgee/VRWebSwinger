//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Valve.VR;

//public class Web : MonoBehaviour
//{
//    public SteamVR_TrackedObject controller;
//    public LineRenderer line;
//    public Rigidbody player;
//    private Vector3 target;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        var device = SteamVR_Controller.Input((int)controller.index);
//        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
//        {
//            RaycastHit hit;
//            if(Physics.Raycast(controller.transform.position, controller.transform.forward, out hit))
//            {

//                if(hit.collider.tag.Equals("donthit")){
//                    line.enabled = true; 
//                    line.SetPosition(0, controller.transform.position);
//                    target = hit.point; 
//                    line.SetPosition(1, target);
//                    line.material.mainTextureOffset = Vector2.zero;
//                }
                
//            }
//        } else if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
//        {
//            if(line.enabled)
//            {
//                line.SetPosition(0, controller.transform.position);
//                line.material.mainTextureOffset = new Vector2(line.material.mainTextureOffset.x + Random.Range(0.01f, -0.5f),0f);
//                player.AddForce((target - controller.transform.position).normalized * 20f);
//            }
            


//        }else
//        {
//            line.enabled = false;

//        }
        
//    }
//}
