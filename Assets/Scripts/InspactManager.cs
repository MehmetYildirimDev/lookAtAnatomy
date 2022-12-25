using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspactManager : MonoBehaviour
{
    [Header("Transform Parameters")]
    public float distance;
    public Transform playerSocket;
    Vector3 originalPos;
    
    
    bool onIspect=false;
    GameObject inspected;

    public Camera mainCamera;

    [Header("Rotate Parameters")]
    [SerializeField] private float rotateSpeed=350f;

  
    [Header("Zoom Parameters")]
    [SerializeField] private float originalFOV;
    [SerializeField] private float ScrollSpeed=10f;


    private void Awake()
    {
        originalFOV = mainCamera.fieldOfView;
    }

    private void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, forward,out hit, distance))
        {
            if (hit.transform.tag == "object" && !onIspect)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {

                    inspected = hit.transform.gameObject;
                    originalPos = hit.transform.position;
                    onIspect = true;

                    StartCoroutine(pickupItem());
                }
            }
        }

        if (onIspect)
        {

            inspected.transform.position = Vector3.Lerp(inspected.transform.position, playerSocket.position, 0.2f);

            playerSocket.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed);

            Zoom();
        }
        else if (inspected != null)
        {
            mainCamera.fieldOfView = originalFOV;
            inspected.transform.SetParent(null);
            inspected.transform.position = Vector3.Lerp(inspected.transform.position, originalPos, 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && onIspect)
        {
            onIspect = false;
            StartCoroutine(dropItem());
        }

    }

    private void Zoom()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.fieldOfView>30)
        {
            mainCamera.fieldOfView -= ScrollSpeed;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0 &&  mainCamera.fieldOfView < originalFOV)
        {
            mainCamera.fieldOfView += ScrollSpeed;
        }
        //
        //if (mainCamera.fieldOfView <= 30)  
        //    return;

        //mainCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel")* ScrollSpeed;
    }

    private IEnumerator pickupItem()
    {
        yield return new WaitForSeconds(0.2f);
        inspected.transform.SetParent(playerSocket);
    }

    private IEnumerator dropItem()
    {
        //objeyi aldigimiz yon bilgisini kaydetmiyoruz direk 0 0 0 birakiyoruz.///refactor icin bi bilgi
        inspected.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(0.2f);
    }

    public bool getonIspect()
    {
        return onIspect;
    }





}
