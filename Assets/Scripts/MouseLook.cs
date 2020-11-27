using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 5f;
    [SerializeField] float SmoothDampX = 0.2f;
    [SerializeField] float SmoothDampY = 0.2f;
    [SerializeField] float minCameraY = 1;
    [SerializeField] float maxCameraY = 7;
    public Transform playerBody;
    private float xRotation = 0;
    private float yRotation;
    private CinemachineVirtualCamera cm;
    private CinemachineTransposer tp;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cm = FindObjectOfType<CinemachineVirtualCamera>();
        if (cm != null)
            tp=cm.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;// * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;// * Time.deltaTime;

        if (tp != null)
        {
            tp.m_FollowOffset.y = Mathf.SmoothStep(tp.m_FollowOffset.y, tp.m_FollowOffset.y + mouseY, SmoothDampY);
            tp.m_FollowOffset.y = Mathf.Clamp(tp.m_FollowOffset.y, minCameraY, maxCameraY);
        }
        // xRotation -= mouseY;
        // xRotation = Mathf.Clamp(xRotation, -90, 90f);


        float ty = Mathf.SmoothStep(transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.y + mouseX, SmoothDampX);
        transform.localRotation = Quaternion.Euler(0,ty, 0f);
        //playerBody.Rotate(mouseX *Vector3.up );
        
       
       


    }

}


