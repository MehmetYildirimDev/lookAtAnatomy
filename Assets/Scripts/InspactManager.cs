using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspactManager : MonoBehaviour
{
    public float distance;
    public Transform playerSocket;

    Vector3 originalPos;
    bool onIspect=false;
    GameObject inspected;

    public Camera mainCamera;

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
            playerSocket.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 125f);
        }
        else if (inspected != null)
        {
            inspected.transform.SetParent(null);
            inspected.transform.position = Vector3.Lerp(inspected.transform.position, originalPos, 0.2f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && onIspect)
        {
            onIspect = false;
            StartCoroutine(dropItem());
        }

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

     

}
