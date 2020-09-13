using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState<T> : FSMState<T>
{
    float _alertTimer;
    EnemyController _controller;
    Sight _sight;
    GameObject _exclamation;

    public AlertState(EnemyController controller, Sight sight, GameObject exclamation)
    {
        _controller = controller;
        _sight = sight;
        _exclamation = exclamation;
    }

    //Sobreescribo la función Awake de la clase FSMState
    public override void Awake()
    {
        _alertTimer = 1;
        _exclamation.SetActive(true);
    }

    //Sobreescribo la funcion de Execute de la clase FSMState
    public override void Execute()
    {
        _alertTimer -= Time.deltaTime;

        if (_alertTimer <= 0)
        {
            _sight.SetSawTargetOnce(); 
            _controller.ExecuteTree();
            _exclamation.SetActive(false);
        }
    }
    
    //Sobreescribo la funcion de Sleep de la clase FSMState
    public override void Sleep()
    {
    }
}