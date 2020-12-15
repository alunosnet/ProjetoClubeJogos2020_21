using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] bool Inside = false;
    [SerializeField] bool Transition = false;
    [SerializeField] float TransitionTime = 1.0f;
    [SerializeField] float InsideDistanceY = 3;
    [SerializeField] float InsideDistanceZ = -7;
    [SerializeField] float OutsideDistanceY = 5;
    [SerializeField] float OutsideDistanceZ = -10;
    private CinemachineVirtualCamera cm;
    private CinemachineTransposer tp;
    MouseLook mouseLook;
    CharacterController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        mouseLook = GetComponent<MouseLook>();
        cm = FindObjectOfType<CinemachineVirtualCamera>();
        if (cm != null)
            tp = cm.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Transition)
        {
            if (Inside)
            {
                OutsideDistanceY = tp.m_FollowOffset.y;
                OutsideDistanceZ = tp.m_FollowOffset.z;
                StartCoroutine("ZoomIn");
                //tp.m_FollowOffset.y = InsideDistanceY;
                //tp.m_FollowOffset.z = InsideDistanceZ;
            }
            else
            {
                InsideDistanceY = tp.m_FollowOffset.y;
                InsideDistanceZ = tp.m_FollowOffset.z;
                StartCoroutine("ZoomOut");
                //tp.m_FollowOffset.y = OutsideDistanceY;
                //tp.m_FollowOffset.z = OutsideDistanceZ;
            }
            Transition = false;
        }
    }
    IEnumerator ZoomIn()
    {
        float CurrentTime=0;
        float IncY = (tp.m_FollowOffset.y - InsideDistanceY)/TransitionTime;
        float IncZ = (tp.m_FollowOffset.z - InsideDistanceZ) / TransitionTime;
        while (CurrentTime < TransitionTime)
        {
            tp.m_FollowOffset.y -= IncY * Time.deltaTime;
            tp.m_FollowOffset.z -= IncZ * Time.deltaTime;
            mouseLook.minCameraY = player.height;
            mouseLook.maxCameraY = player.height+tp.m_FollowOffset.y;
            yield return null;
            CurrentTime += Time.deltaTime;
        }
        
    }
    IEnumerator ZoomOut()
    {
        float CurrentTime = 0;
        float IncY = (tp.m_FollowOffset.y - OutsideDistanceY) / TransitionTime;
        float IncZ = (tp.m_FollowOffset.z - OutsideDistanceZ) / TransitionTime;
        while (CurrentTime < TransitionTime)
        {
            tp.m_FollowOffset.y -= IncY * Time.deltaTime;
            tp.m_FollowOffset.z -= IncZ * Time.deltaTime;
            mouseLook.minCameraY = player.height;
            mouseLook.maxCameraY = player.height+tp.m_FollowOffset.y;

            yield return null;
            CurrentTime += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Inside"))
        {
            if(Inside==false)
            {
                Transition = true;
                Inside = true;
            }    
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Inside"))
        {
            if (Inside == true)
            {
                Transition = true;
                Inside = false;
            }
        }
    }
}
