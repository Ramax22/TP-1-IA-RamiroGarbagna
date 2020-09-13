using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent
{
    //Declaro las variables
    GameManager _manager;
    Transform _entity;

    //Asigno las variables
    public ShootComponent(Transform e, GameManager manager)
    {
        _entity = e;
        _manager = manager;
    }

    public void EnemyShoot(Transform target, LayerMask mask)
    {
        //Mirar al objetivo
        _entity.LookAt(target);
        //Calculamos la distancia entre esta entidad y el objetivo
        Vector3 myPos = _entity.position;
        myPos.y = 1;
        Vector3 diff = target.position - myPos;
        float distance = diff.magnitude;

        //Calculamos que no haya un osbtaculo entre la entidad y el objetivo
        if (Physics.Raycast(myPos, diff.normalized, distance, mask))
        {
            //si logra dispararle al objetivo, el juego termina
            _manager.LoseGame();
        }
    }
}