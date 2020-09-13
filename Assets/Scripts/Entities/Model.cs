using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    //Variables
    MoveComponent _move;
    IdleComponent _idle;
    ShootComponent _shoot;
    Rigidbody _rb;
    View _view;
    public Transform Target;
    public LayerMask ShootMask;
    float _speed = 5f;
    [SerializeField] GameManager _manager;

    //Inicializo algunas vars
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _idle = new IdleComponent();
        _move = new MoveComponent(_rb, transform, _speed);
        _shoot = new ShootComponent(transform, _manager);

        _view = GetComponent<View>();
    }

    private void Update()
    {
        //Si su velocidad es 0, entonces tiene que poner la anim de Idel
        if (Mathf.Abs(_rb.velocity.x) <= 0.9f && Mathf.Abs(_rb.velocity.z) <= 0.9f)
        {
            _idle.Idle();
            _view.Idle();
        }
    }

    //Funcion para mover la entidad
    public void Move(Vector3 dir)
    {
        _move.Move(dir);
        _view.Move();
    }

    //Funcion para mirar hacia un objetivo 
    public void Look(Vector3 dir)
    {
        _move.Look(dir);
    }

    //Metodo de disparo exclusivo del enemigo
    public void EnemyShoot()
    {
        _shoot.EnemyShoot(Target, ShootMask);
        _view.EnemyShoot();
    }
}
