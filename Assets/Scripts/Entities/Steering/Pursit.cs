using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursit : ISteeringBehaviours
{
    Transform _entity;
    Transform _target;
    Rigidbody _rbTarget;
    float _time; //saber cuanto tiempo a futuro queremos adelantarnos al target

    public Pursit(Transform entity, Transform target, Rigidbody rbTarget, float time)
    {
        _entity = entity;
        _target = target;
        _rbTarget = rbTarget;
        _time = time;
    }

    public Vector3 GetDir()
    {
        //obtenog la velocidad
        var vel = _rbTarget.velocity.magnitude;
        //Obtengo en donde esta mi objetivo, y hacia donde esta mirando
        Vector3 targetPosition = _target.transform.position + _target.transform.forward * vel * _time;
        Vector3 dir = (targetPosition - _entity.position).normalized;

        return dir;
    }
}