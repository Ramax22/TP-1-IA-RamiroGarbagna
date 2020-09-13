using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Model _model;

    private void Awake()
    {
        _model = GetComponent<Model>();
    }

    //Función que detecta el input y envia la info al Model
    public void MoveController(Vector3 dir)
    {
        _model.Move(dir);
    }
}
