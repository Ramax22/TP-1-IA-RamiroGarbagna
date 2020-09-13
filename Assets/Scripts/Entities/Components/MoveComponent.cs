using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent
{
    //Declaro las variables
    Rigidbody _rb;
    Transform _transform;
    float _speed;

    //asigno las variables medainte el constructor
    public MoveComponent(Rigidbody rb, Transform transform, float speed)
    {
        _rb = rb;
        _transform = transform;
        _speed = speed;
    }

    //Funcion para mover
    public void Move(Vector3 dir)
    {
        _rb.velocity = dir * _speed;
        _transform.forward = dir;
    }

    //Funcion para mirar hacia una dirección
    public void Look(Vector3 dir)
    {
        _transform.forward = dir;
    }
}