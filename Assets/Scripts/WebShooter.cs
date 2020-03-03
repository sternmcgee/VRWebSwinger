using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class WebShooter : MonoBehaviour
{
    public float curledThreshold = 0.8f;
    public float uncurledThreshold = 0.2f;
    SteamVR_Behaviour_Skeleton skeleton = null;
    private bool isShooting = false;
    private bool shouldShoot = false;
    private bool shouldGetCurls = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldGetCurls)
        {
            StartCoroutine(GetCurls());
        }
        if (!isShooting)
        {
            StartCoroutine(ShootWeb());
        }        
    }

    private IEnumerator ShootWeb()
    {
        isShooting = true;

        //implement webshooting here

        yield return new WaitForSeconds(1.0f);
        isShooting = false;
    }

    private IEnumerator GetCurls()
    {
        shouldGetCurls = false;
        //get finger curl array
        float[] fingerCurls = skeleton.fingerCurls;
        //are middle and ring finger only curled?
        if (fingerCurls[2] > curledThreshold && fingerCurls[3] > curledThreshold && fingerCurls[0] < uncurledThreshold &&
            fingerCurls[1] < uncurledThreshold && fingerCurls[4] < uncurledThreshold)
        {
            //should shoot web
            shouldShoot = true;
        }
        else
        {
            shouldShoot = false;
        }

        yield return new WaitForSeconds(0.1f);
        shouldGetCurls = true;
    }
}
