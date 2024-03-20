using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ETFRManager : MonoBehaviour
{
    [SerializeField] private Transform leftEye, rightEye;
    public Vector3 GazePoint {
        get {
            return gazePoint;
        }
    }
    private Vector3 gazePoint;


    public static ETFRManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        gazePoint = GetGazePoint(leftEye, rightEye);

        Shader.SetGlobalVector("_Gaze_Point", gazePoint);
        Debug.DrawLine(gazePoint, gazePoint + Vector3.up, Color.red);
    }



    public static Vector3 GetGazePoint(Transform leftEye, Transform rightEye)
    {
        // Get closest point between two rays
        // Rays are defined by leftEye's position and forward, and rightEye's position and forward

        // Formulas from https://palitri.com/vault/stuff/maths/Rays%20closest%20point.pdf
        
        Vector3 A = leftEye.position;
        Vector3 B = rightEye.position;

        Vector3 a = leftEye.forward;
        Vector3 b = rightEye.forward;

        Vector3 c = B - A;

        float p = Vector3.Dot(a, b);
        float q = Vector3.Dot(a, c);
        float r = Vector3.Dot(b, c);
        float s = Vector3.Dot(a, a);
        float t = Vector3.Dot(b, b);


        float denominator = s * t - p * p;

        float d = (-(p*r)+(q*t))/denominator;
        float e = ((p*q)-(r*s))/denominator;

        Vector3 dPos = A + (a*d);
        Vector3 ePos = B + (b*e);

        Debug.DrawLine(A, dPos, Color.green);
        Debug.DrawLine(B, ePos, Color.green);

        Vector3 result = (dPos+ePos)/2f;

        if (!float.IsNaN(result.x) && !float.IsNaN(result.y) && !float.IsNaN(result.z)) {
            return result;
        }

        return Vector3.zero;
    }
}
