using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FPSMovement))]
public class FPSMovementEditor : Editor
{
    void OnSceneGUI()
    {
        FPSMovement Mov = (FPSMovement)target;

        GroundDetection(Mov);
    }

    void GroundDetection(FPSMovement Mov)
    {
        Vector3 StartRay = Mov.transform.position + Vector3.up;
        Vector3 EndRay = Mov.transform.position - (Vector3.up * Mov.PlayerPhysics.GroundDistance);

        if(Physics.Linecast(StartRay, EndRay, Mov.PlayerPhysics.GroundLayers))
        {
            Handles.color = Color.green;
            Handles.DrawLine(StartRay, EndRay);
            Handles.DrawWireDisc(EndRay, Vector3.up, 1);
        } else
        {
            Handles.color = Color.blue;
            Handles.DrawLine(StartRay, EndRay);
            Handles.DrawWireDisc(EndRay, Vector3.up, 1);
        }
    }
}
