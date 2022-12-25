using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour
{

    public float WeaponRange = 50f;
    [SerializeField]private Camera fpsCam;



    void Update()
    {
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * WeaponRange, Color.green);

    }
}