using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IdleState<T> : FSMState<T>
{
    EnemyController _controller;

    //Creo un timer para esperar el estado de Idle
    float _timer;

    public IdleState(EnemyController controller)
    {
        _controller = controller;
    }

    //Sobreescribo la función Awake de la clase FSMState
    public override void Awake()
    {
        //Seteo el timer a un numero aleatorio entre 1 y 5
        _timer = Random.Range(1, 5);
    }

    //Sobreescribo la funcion de Execute de la clase FSMState
    public override void Execute()
    {
        //Espero X segundos en el estado de Idle, si pasan, cambio a patrol
        if (_timer > 0) _timer -= Time.deltaTime;
        else _controller.ExecuteTree();
    }

    //Sobreescribo la funcion de Sleep de la clase FSMState
    public override void Sleep()
    {
        //Reseteo el timer a un numero aleatorio entre 1 y 5
        _timer = Random.Range(1, 5);
    }
}