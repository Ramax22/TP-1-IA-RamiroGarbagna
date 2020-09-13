using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    //vars
    Animator _animator;

    private void Awake()
    {
        //vasr init
        _animator = GetComponent<Animator>();
    }

    //funcion para cambiar vars del animator
    public void Idle()
    {
        _animator.SetBool("isMoving", false);
    }

    //funcion para cambiar vars del animator
    public void Move()
    {
        _animator.SetBool("isMoving", true);
    }

    //funcion para cambiar vars del animator
    public void EnemyShoot()
    {
        _animator.SetTrigger("shoot");
    }
}