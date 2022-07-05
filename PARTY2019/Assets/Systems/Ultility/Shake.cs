using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    Vector3 OriginalRot;
    public float ShakeLerp;
    public float ShakeMagnetude;

    public enum _ShakeType
    {
        HandShake
    }
    public _ShakeType ShakeType;

    void Start()
    {
        OriginalRot = transform.eulerAngles;
        InvokeRepeating("Rotate", 0, .1f);
    }

    void LateUpdate()
    {
        //Rotate();
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(RotationEuler), ShakeLerp * Time.deltaTime);
    }
    Vector3 RotationEuler;
    void Rotate()
    {
        RotationEuler = new Vector3(
            OriginalRot.x + Random.Range(ShakeMagnetude * -1, ShakeMagnetude * 1),
            OriginalRot.x + Random.Range(ShakeMagnetude * -1, ShakeMagnetude * 1),
            OriginalRot.x + Random.Range(ShakeMagnetude * -1, ShakeMagnetude * 1));
    }
}
