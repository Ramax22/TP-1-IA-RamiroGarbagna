using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Referencia a la FSM
    FSM<string> _fsm;
    //Creo una lista que contenga todos los waypoints
    public List<Transform> Waypoints;
    //Referencia al model
    Model _model;
    //Referencia al Sight
    public Sight Sight;
    //Referencia al objeto del texto
    public GameObject ExclamationMark;

    //Keys de estados de la fsm
    const string _idleKey = "idle";
    const string _patrolKey = "patrol";
    const string _shootKey = "shoot";
    const string _alertKey = "alert";
    const string _searchKey = "search";

    //--- desicion tree ---
    QuestionNode _isSeeingPlayerQuestionNode;
    QuestionNode _isFirstTimeQuestionNode;

    Roulette<string> _actionRoulette;
    Dictionary<string, int> _statesRoulette;

    private void Start()
    {
        //Agarro el model
        _model = GetComponent<Model>();

        //Creo la FSM
        _fsm = new FSM<string>();

        //Creo los estados
        IdleState<string> idle = new IdleState<string>(this);
        PatrolState<string> patrol = new PatrolState<string>(Waypoints, transform, this);
        ShootState<string> shoot = new ShootState<string>(this);
        AlertState<string> alert = new AlertState<string>(this, Sight, ExclamationMark);
        SearchState<string> search = new SearchState<string>(this);

        //Creo las transiciones
        idle.AddTransition(_patrolKey, patrol);
        idle.AddTransition(_shootKey, shoot);
        idle.AddTransition(_alertKey, alert);
        idle.AddTransition(_searchKey, search);
        idle.AddTransition(_idleKey, idle); //se tienen a si mismos por si llega a tener que volverse a ejecutar con el random

        patrol.AddTransition(_idleKey, idle);
        patrol.AddTransition(_shootKey, shoot);
        patrol.AddTransition(_alertKey, alert);
        patrol.AddTransition(_searchKey, search);

        alert.AddTransition(_idleKey, idle);
        alert.AddTransition(_shootKey, shoot);
        alert.AddTransition(_patrolKey, patrol);
        alert.AddTransition(_alertKey, alert);
        alert.AddTransition(_searchKey, search);

        search.AddTransition(_searchKey, search);
        search.AddTransition(_idleKey, idle);
        search.AddTransition(_shootKey, shoot);
        search.AddTransition(_patrolKey, patrol);
        search.AddTransition(_alertKey, alert);

        //diccionario de todos los estados de idle para la roulette
        _statesRoulette = new Dictionary<string, int>();
        _statesRoulette.Add(_idleKey, 30);
        _statesRoulette.Add(_patrolKey, 70);
        _statesRoulette.Add(_searchKey, 50); 

        //inicializo la FSM
        _fsm.SetInitialState(idle);

        //Inicializo los nodos del Desicion Tree
        ActionNode _shootActionNode = new ActionNode(ChangeToShootState); //Aca pasarle función
        ActionNode _alertActionNode = new ActionNode(ChangeToAlertState);
        ActionNode _randomStateActionNode = new ActionNode(ChangeToRandomState);

        _isFirstTimeQuestionNode = new QuestionNode(Sight.SawTargetOnce, _shootActionNode, _alertActionNode);
        _isSeeingPlayerQuestionNode = new QuestionNode(Sight.IsSeeingTarget, _isFirstTimeQuestionNode, _randomStateActionNode);

        Sight.SawTarget.AddListener(ExecuteTree);

        // Roulette
        _actionRoulette = new Roulette<string>();
    }

    private void Update()
    {
        //Ejecuto la fsm
        _fsm.OnUpdate();
    }

    //Función que envia la info al Model sobre el movimiento
    public void MoveController(Vector3 dir)
    {
        _model.Move(dir);
    }

    //Funcion para que el body mire hacia la direccion
    public void LookController(Vector3 dir)
    {
        _model.Look(dir);
    }

    //Funcion que le dice al Model que dispare
    public void ShootController()
    {
        _model.EnemyShoot();
    }

    //cambia al estado Shoot
    void ChangeToShootState()
    {
        _fsm.Transition(_shootKey);
    }

    //Cambia a lestado de alerta
    void ChangeToAlertState()
    {
        _fsm.Transition(_alertKey);
    }

    //cambia a un estado aleatrio
    void ChangeToRandomState()
    {
        string value = ExecuteStatesRoulette();
        _fsm.Transition(value);
    }

    //Funcion que ejecuta todo el arbol
    public void ExecuteTree()
    {
        _isSeeingPlayerQuestionNode.Execute();
    }

    //Roulette
    string ExecuteStatesRoulette()
    {
        return _actionRoulette.Run(_statesRoulette);
    }
}