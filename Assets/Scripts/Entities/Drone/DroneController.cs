using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    //Declaro variables
    bool _follow;
    public BoxCollider collider;
    ISteeringBehaviours _steering;
    public Transform PursitTarget;
    public Rigidbody TargetRb;
    public float TimePrediction;
    MoveComponent _move;
    [SerializeField] GameObject _particles;
    [SerializeField] GameManager _manager;

    //Asigno variables
    private void Awake()
    {
        _follow = false;
        _steering = new Pursit(transform, PursitTarget, TargetRb, TimePrediction);
        _move = new MoveComponent(GetComponent<Rigidbody>(), transform, 5f);
    }

    private void Update()
    {
        //Persigue al target si corresponde
        if (_follow)
        {
            Vector3 dir = _steering.GetDir();
            _move.Move(dir);
        }
    }

    //modificaciones al tocar el trigger
    private void OnTriggerEnter(Collider other)
    {
        _follow = true;
        collider.enabled = false;
        _particles.SetActive(false);
        _manager.ChangeObjective();
    }
}