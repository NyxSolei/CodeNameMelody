using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera CMcamera; 
    public float offsetX = 2f; 
    public float offsetY = 1f; 
    private Transform player; 
    private CinemachineFramingTransposer transposer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        CMcamera.Follow = player;

        transposer = CMcamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        if (transposer != null)
        {
            transposer.m_TrackedObjectOffset.x = offsetX;
            transposer.m_TrackedObjectOffset.y = offsetY;
        }
    }
}
