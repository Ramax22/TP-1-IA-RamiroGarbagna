using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState<T> : FSMState<T>
{
    float _timer;
    Dictionary<Vector3, int> _dirRouletteDic;
    Roulette<Vector3> _dirRoulette;
    EnemyController _controller;

    public SearchState(EnemyController controller)
    {
        _dirRouletteDic = new Dictionary<Vector3, int>();
        _dirRouletteDic.Add(Vector3.forward, 25);
        _dirRouletteDic.Add(Vector3.back, 25);
        _dirRouletteDic.Add(Vector3.left, 25);
        _dirRouletteDic.Add(Vector3.right, 25);

        _dirRoulette = new Roulette<Vector3>();

        _controller = controller;
    }


    //Sobreescribo la función Awake de la clase FSMState
    public override void Awake()
    {

        _timer = Random.Range(1, 5);
        _controller.LookController(_dirRoulette.Run(_dirRouletteDic));
    }

    //Sobreescribo la funcion de Execute de la clase FSMState
    public override void Execute()
    {

        if (_timer > 0) _timer -= Time.deltaTime;
        else _controller.ExecuteTree();
    }

    //Sobreescribo la funcion de Sleep de la clase FSMState
    public override void Sleep()
    {

    }
}