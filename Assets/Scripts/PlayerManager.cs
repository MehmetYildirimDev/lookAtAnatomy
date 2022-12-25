using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private Camera PlayerCamera;
    private float RotationX = 0;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float LookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float LookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float UpperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float LowerLookLimit = 80.0f;

    /// <summary>
    /// 
    /// </summary>
    public float distance;
    public Transform playerSocket;


    InspactManager inspactManager;
    private void Awake()
    {
        inspactManager = GameObject.Find("Main Camera").GetComponent<InspactManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!inspactManager.getonIspect())
        {
            HandleMouseLook();
        }
    }

    private void HandleMouseLook()
    {
        RotationX -= Input.GetAxis("Mouse Y") * LookSpeedY;
        RotationX = Mathf.Clamp(RotationX, -UpperLookLimit, LowerLookLimit);
        PlayerCamera.transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeedX, 0);
    }

}
