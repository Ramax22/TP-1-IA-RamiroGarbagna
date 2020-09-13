using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState<T> : FSMState<T>
{
    //Referencia al Enemy Controller
    EnemyController _controller;

    //Agarro objetos con el constructor
    public ShootState(EnemyController eC)
    {
        _controller = eC;
    }

    //Sobreescribo la función Awake de la clase FSMState
    public override void Awake()
    {
        _controller.ShootController();
    }

    //Sobreescribo la funcion de Execute de la clase FSMState
    public override void Execute()
    {


    }

    //Sobreescribo la funcion de Sleep de la clase FSMState
    public override void Sleep()
    {
    }
}