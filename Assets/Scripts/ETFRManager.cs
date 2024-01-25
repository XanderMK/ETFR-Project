using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETFRManager : MonoBehaviour
{
    [SerializeField] private Transform leftEye, rightEye;

    void Awake()
    {

    }

    void Update()
    {
        Vector3 gazePoint = GetGazePoint(leftEye, rightEye);
    }



    public static Vector3 GetGazePoint(Transform leftEye, Transform rightEye)
    {
        // Get closest point between two rays
        // Rays are defined by leftEye's position and forward, and rightEye's position and forward
        
        Vector3 leftEyeRay = leftEye.forward;
        Vector3 rightEyeRay = rightEye.forward;
        
        return new Vector3(1, 0, 1);
    }
}
