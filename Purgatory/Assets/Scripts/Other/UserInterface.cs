using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class UserInterface : MonoBehaviour
{
    public GameObject pauseMenu;

    private Player_Inputs inputs;
    private void Awake()
    {
        if (inputs == null)
        {
            inputs = new Player_Inputs();
        }

        inputs.Player.Pause.performed += Pause;
        inputs.Player.Pause.Enable();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Hello");
        if (!pauseMenu.activeInHierarchy)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
