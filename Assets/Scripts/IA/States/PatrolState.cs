using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatrolState<T> : FSMState<T>
{
    //Creo una lista de Waypoints
    List<Transform> _waypoints;
    //Creo una variable que indique el waypoint al que quiero ir
    int _nextPoint;
    //Referencia a la entidad donde pertenece este state
    Transform _entity;
    //Creo la distancia minima del waypoint
    const float WaypointDistance = 3f;
    //Referencia al Controller de la entidad
    EnemyController _controller;

    //Agarro objetos con el constructor
    public PatrolState(List<Transform> wp, Transform e, EnemyController eC)
    {
        _waypoints = wp;
        _entity = e;
        _controller = eC;
    }

    //Sobreescribo la función Awake de la clase FSMState
    public override void Awake()
    {

    }

    //Sobreescribo la funcion de Execute de la clase FSMState
    public override void Execute()
    {
        //Calculo la distancia entre mi posición y el próximo punto
        var point = _waypoints[_nextPoint];
        var dir = point.position - _entity.position;

        //Si la distancia la posicion de la entidad y la del waypoint es menor a lo establecido en la constante, apunto al siguente
        if (dir.magnitude < WaypointDistance)
        {
            if (_nextPoint < _waypoints.Count - 1) _nextPoint++;
            else _nextPoint = 0;

            //Paso al estado al azar
            _controller.ExecuteTree();
        }
        else
        {
            //Le digo al controller que me quiero mover
            _controller.MoveController(dir.normalized);
        }
    }

    //Sobreescribo la funcion de Sleep de la clase FSMState
    public override void Sleep()
    {

    }
}