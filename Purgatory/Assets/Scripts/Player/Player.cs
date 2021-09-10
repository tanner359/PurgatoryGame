using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ISavable
{
    public static Player instance;

    public GameObject currentPlayer;

    public Revolver revolver;

    #region UI
    public Texture2D Crosshair;
    #endregion

    #region Settings
    [Header("Settings")]
    public float speed = 1f;
    [Range(0f, 1f)]
    public float deadZone = 0.00f;
    #endregion

    #region Components
    private Player_Inputs inputs;
    private Rigidbody2D rb;
    private Animator animator;
    #endregion

    private Vector2 direction;

    public static bool isTargeting = false;

    private void OnEnable()
    {
        instance = this;

        if(inputs == null)
        {
            inputs = new Player_Inputs();
        }

        inputs.Player.Movement.performed += Movement;
        inputs.Player.Movement.canceled += Movement;        
        inputs.Player.TargetingMode.performed += ToggleTargeting;
        inputs.Player.SwitchDimension.performed += InitiateDimensionTravel;
        inputs.Player.Enable();

        Load();

        UpdateControllerData();

        CameraMaster.instance.SetPlayerCamTarget(currentPlayer.transform);
    }


    private void Start()
    {
        DontDestroyOnLoad(this);

        PlayerData data = SaveSystem.LoadPlayerData();
        if (data != null && (data.currentScene != SceneManager.GetActiveScene().name))
        {
            SceneManager.LoadScene(data.currentScene);
            return;
        }       
    }

    #region Saving + Loading

    public void Save()
    {
        PlayerData data = new PlayerData(this, revolver);
        SaveSystem.SavePlayerData(data);
        Notification_System.Send_SystemNotify("Player has been saved");
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        if(data == null) { return; }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            Destroy(players[i]);
        }

        revolver.bulletCount = data.bulletCount;
        CharacterDatabase characterDatabase = Resources.Load<CharacterDatabase>("Data/Character Database");
        GameObject character = characterDatabase.GetCharacter(data.characterID);
        GameObject GO = Instantiate(character, data.GetPosition(), Quaternion.identity, GameObject.Find("Characters").transform);
        GO.GetComponent<NPC>().enabled = false;
        GO.tag = "Player";
        currentPlayer = GO;
    }


    #endregion

    public void SwitchPlayer(GameObject player)
    {
        currentPlayer.GetComponent<NPC>().enabled = true;
        currentPlayer.tag = "NPC";
        currentPlayer = player;
        currentPlayer.tag = "Player";
        UpdateControllerData();
        CameraMaster.instance.SetPlayerCamTarget(player.transform);
        Cursor.SetCursor(default, default, CursorMode.Auto);
        isTargeting = false;
        Time.timeScale = 1f;
        revolver.bulletCount--;
        Save();
        SceneController.instance.SaveScene();
    }
    public void UpdateControllerData()
    {
        revolver = GameManager.instance.revolver;
        rb = currentPlayer.GetComponent<Rigidbody2D>();
        animator = currentPlayer.GetComponent<Animator>();
    }

    #region NOTIFICATIONS
    public ActionWindow.ButtonFunction Travel_Purgatory;
    public ActionWindow.ButtonFunction Travel_Living;
    
    public void LaunchPurgatory()
    {
        revolver.bulletCount--;
        Save();
        SceneController.instance.SaveScene();
        Laucher.LoadScene("Purgatory");
    }
    public void LaunchLiving()
    {
        Save();
        SceneController.instance.SaveScene();
        Laucher.LoadScene("Living Realm");
    }
    #endregion

    #region INPUTS
    private void InitiateDimensionTravel(InputAction.CallbackContext context)
    {
        if (Laucher.GetCurrentSceneName() == "Purgatory")
        {
            Travel_Living = LaunchLiving;
            Notification_System.Send_ActionWindow("Do you wish to travel to the Living Realm?", "Travel", Travel_Living);
            return;
        }

        if (revolver.bulletCount > 0)
        {
            Travel_Purgatory = LaunchPurgatory;
            Notification_System.Send_ActionWindow("Do you want to travel to Purgatory?\n\n\nWarning: This action consumes 1 Bullet.", "Travel", Travel_Purgatory);
            return;
        }
        Debug.Log("Not enough bullets for this action");
    }
    private void Movement(InputAction.CallbackContext context)
    {
        float xValue = context.ReadValue<Vector2>().x;
        float yValue = context.ReadValue<Vector2>().y;

        if (xValue < -deadZone && currentPlayer.transform.localScale.x > 0)
        {
            Flip();
        }
        else if (xValue > deadZone && currentPlayer.transform.localScale.x < 0)
        {
            Flip();
        }

        if (xValue > deadZone || xValue < -deadZone || yValue > deadZone || yValue < -deadZone) 
        {
            direction = context.ReadValue<Vector2>();
            animator.SetBool("Walking", true);
        }
        else
        {
            direction = Vector2.zero;
            animator.SetBool("Walking", false);
        }
        
    }
    private void Flip()
    {
        currentPlayer.transform.localScale = new Vector3(currentPlayer.transform.localScale.x * -1, currentPlayer.transform.localScale.y, currentPlayer.transform.localScale.z);
    }
    public void ToggleTargeting(InputAction.CallbackContext context)
    {
        Debug.Log("ToggleTargeting");
        if (!isTargeting)
        {
            Cursor.SetCursor(Crosshair, new Vector2(Crosshair.width / 2, Crosshair.height/2), CursorMode.Auto);
            isTargeting = true;
            Time.timeScale = 0.5f;
            return;
        }
        Cursor.SetCursor(default, default, CursorMode.Auto);
        isTargeting = false;
        Time.timeScale = 1f;
    }
    #endregion

    private void Update()
    {
        rb.velocity = new Vector2((direction.x * speed), rb.velocity.y);
    }
}
