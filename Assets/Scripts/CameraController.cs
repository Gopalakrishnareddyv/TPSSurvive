using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineComposer cineComposer;
    [SerializeField] float rotateSpeed;
    private void Start()
    {
        cineComposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
    }
    private void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        cineComposer.m_TrackedObjectOffset.y += vertical;
        cineComposer.m_TrackedObjectOffset.x += horizontal;
        cineComposer.m_TrackedObjectOffset.y = Mathf.Clamp(cineComposer.m_TrackedObjectOffset.y,2f, 7f);
        cineComposer.m_TrackedObjectOffset.x = Mathf.Clamp(cineComposer.m_TrackedObjectOffset.x, -4f, 4f);
    }
}