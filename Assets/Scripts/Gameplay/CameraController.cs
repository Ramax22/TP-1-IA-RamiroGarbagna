using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //vars
    public Transform Target;
    public int CameraDistance;

    private void Update()
    {
        //Básicamente hago que la camara siga al jugador
        if (Target != null)
        {
            var pos = new Vector3(Target.position.x, 15, Target.position.z - CameraDistance);
            transform.position = pos;
        }
    }
}