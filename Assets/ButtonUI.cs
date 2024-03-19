using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonUI : MonoBehaviour
{
    [SerializeField] private string newSimulation = "Test scene";
    public void NewSimulationButton()
    {
        SceneManager.LoadScene(newSimulation);
    }
}
