using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] AudioSource AudioClick;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        AudioClick.Play();
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene(0);
        AudioClick.Play();
    }
}
