using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //vars
    [SerializeField] GameObject _objective;
    [SerializeField] Text _objectiveText;

    private void Awake()
    {
        //si alguna variable esta vacia, tiro error
        if (!_objective) Debug.LogError("El objeto esta vacío");
        if (!_objectiveText) Debug.LogError("El objeto esta vacío");
    }

    //al tocar el primer objetivo, cambio al segundo objetivo
    public void ChangeObjective()
    {
        _objective.SetActive(true);
        _objectiveText.text = "\nCURRENT OBJECTIVE: ESCAPE TO THE NORTH WEST";
    }

    //Funcion para ganar el juego
    public void WinGame()
    {
        SceneManager.LoadScene(1);
    }

    //Funcion para perder el juego
    public void LoseGame()
    {
        SceneManager.LoadScene(2);
    }
}