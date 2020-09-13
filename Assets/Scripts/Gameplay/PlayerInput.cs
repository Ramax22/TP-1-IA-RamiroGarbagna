using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //Referencio al controller del player
    Controller _controller;

    private void Awake() {
        //Agarro referencia al controller del player
        _controller = GetComponent<Controller>();
    }

    void Update()
    {
        //Agarro el input de movimiento del player
        GetMovementInput();
    }

    //Agarro los input del movimiento, y envio info al controller de la entidad
    void GetMovementInput()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");

            var dir = new Vector3(x, 0f, z);

            _controller.MoveController(dir);
        }
    }
}