using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineComposer cineComposer;
    [SerializeField] float rotateSpeed;
    [SerializeField] float Xmin, Xmax, Ymin, Ymax;
    private void Start()
    {
        cineComposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
    }
    private void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        cineComposer.m_TrackedObjectOffset.x += vertical;
        cineComposer.m_TrackedObjectOffset.z += horizontal;
        cineComposer.m_TrackedObjectOffset.z = Mathf.Clamp(cineComposer.m_TrackedObjectOffset.z,Ymin,Ymax);
        cineComposer.m_TrackedObjectOffset.x = Mathf.Clamp(cineComposer.m_TrackedObjectOffset.x, Xmin, Xmax);
    }
}