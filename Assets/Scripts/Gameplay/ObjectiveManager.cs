using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    [SerializeField] GameManager _manager;
    private void OnTriggerEnter(Collider other)
    {
        _manager.WinGame();
    }
}