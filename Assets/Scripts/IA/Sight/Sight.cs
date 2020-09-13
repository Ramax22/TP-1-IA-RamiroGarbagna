using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Sight : MonoBehaviour
{
    public Transform target;
    public float range;
    public float angle;
    public LayerMask mask;
    public UnityEvent SawTarget;
    bool _sawTagetOnce = false; //esto es para saber si es la primera vez que lo vio al target o no.
    bool _isSeeingTarget = false; //para saber si Actualmente esta viendo al objetivo o no

    private void Update()
    {
        IsInSight();
    }

    void IsInSight()
    {
        //Calculamos la distancia entre esta entidad y el objetivo
        Vector3 diff = target.position - transform.position;
        float distance = diff.magnitude;
        if (distance > range)
        {
            _isSeeingTarget = false;
            return;
        }

        //Calculamos si esta en el angulo de vision de la entidad
        float angleToTarget = Vector3.Angle(transform.forward, diff.normalized);
        if (angleToTarget > angle / 2)
        {
            _isSeeingTarget = false;
            return;
        }

        //Calculamos que no haya un osbtaculo entre la entidad y el objetivo
        if (Physics.Raycast(transform.position, diff.normalized, distance, mask))
        {
            _isSeeingTarget = false;
            return;
        }

        if (!_isSeeingTarget)
        {
            _isSeeingTarget = true;
            SawTarget.Invoke(); //forma de que esto se invoque solamente 1 vez?
        }
    }

    public bool IsSeeingTarget()
    {
        return _isSeeingTarget;
    }

    public bool SawTargetOnce()
    {
        return _sawTagetOnce;
    }

    public void SetSawTargetOnce()
    {
        _sawTagetOnce = true;
    }
}