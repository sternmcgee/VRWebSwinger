using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Rope : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float TilesPerMeter;//how much you want to steach the texture 
    public Material CableMaterial;//texture you want to streach 

    void Update()
    {
        transform.position = (StartPoint.position + EndPoint.position) / 2;
        transform.localScale = new Vector3(transform.localScale.x, Vector3.Distance(EndPoint.position, StartPoint.position) / 2, transform.localScale.z);
        transform.rotation = Quaternion.LookRotation(EndPoint.position - StartPoint.position) * Quaternion.Euler(90, 0, 0);
        CableMaterial.mainTextureScale = new Vector2(CableMaterial.mainTextureScale.x, Vector3.Distance(EndPoint.position, StartPoint.position) / TilesPerMeter);
    }
}
