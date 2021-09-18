using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour, ISavable
{
    public static Player instance;

    public GameObject defaultCharacter;

    public GameObject currentCharacter;

    public Revolver revolver;

    public CinemachineVirtualCamera playerCam;

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
    public Player_Inputs inputs;
    public Rigidbody2D rb;
    public Animator animator;
    #endregion

    private Vector2 direction;

    public static bool isTargeting = false;

    private void OnEnable()
    {
        if (inputs == null)
        {
            inputs = new Player_Inputs();
        }
        inputs.Player.Movement.performed += Movement;
        inputs.Player.Movement.canceled += Movement;
        inputs.Player.TargetingMode.performed += ToggleTargeting;
        inputs.Player.SwitchDimension.performed += InitiateDimensionTravel;
        inputs.Player.Enable();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Destroy Copy");
            Destroy(gameObject);
            return;
        }

        Load();
    }
    private void Start()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        if(data != null)
        {
            currentCharacter.transform.position = new Vector2(data.position[0], data.position[1]);
        }
    }

    private void OnDisable()
    {
        inputs.Player.Disable();
    }

    #region Saving + Loading

    public void Save()
    {
        PlayerData data = new PlayerData(this, revolver);
        SaveSystem.SavePlayerData(data);
    }

    public void Load()
    {       
        PlayerData data = SaveSystem.LoadPlayerData();
        if(data == null) { RunSetup(); return; }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            Destroy(players[i]);
        }

        revolver.bulletCount = data.bulletCount;
        CharacterDatabase characterDatabase = Resources.Load<CharacterDatabase>("Data/Character Database");
        GameObject character = characterDatabase.GetCharacter(data.characterID);
        GameObject GO = Instantiate(character, Vector3.zero, Quaternion.identity, GameObject.Find("Characters").transform);
        GO.name = character.name;
        GO.GetComponent<IControllable>().EnableControl(this);
        RunSetup();
    }


    #endregion

    public void SwitchPlayer(GameObject target)
    {
        currentCharacter.GetComponent<IControllable>().RevokeControl(this);
        target.GetComponent<IControllable>().EnableControl(this);
        RunSetup();
        playerCam.Follow = target.transform;
        Cursor.SetCursor(default, default, CursorMode.Auto);
        isTargeting = false;
        Time.timeScale = 1f;
        revolver.bulletCount--;
    }
    public void RunSetup()
    {
        if(currentCharacter == null)
        {
            GameObject GO = Instantiate(defaultCharacter, Vector3.zero, Quaternion.identity);
            GO.name = defaultCharacter.name;
            currentCharacter = GO;
            currentCharacter.GetComponent<IControllable>().EnableControl(this);
        }
        rb = currentCharacter.GetComponent<Rigidbody2D>();
        animator = currentCharacter.GetComponent<Animator>();
        playerCam.Follow = currentCharacter.transform;    
    }

    #region NOTIFICATIONS
    public ActionWindow.ButtonFunction Travel_Purgatory;
    public ActionWindow.ButtonFunction Travel_Living;
    
    public void LaunchPurgatory()
    {
        revolver.bulletCount--;
        GameManager.SaveGame();
        StartCoroutine(Laucher.LoadScene("Purgatory", 2));
    }
    public void LaunchLiving()
    {
        GameManager.SaveGame();
        StartCoroutine(Laucher.LoadScene("Living Realm", 2));
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
        Notification_System.Send_SystemNotify("Not enough bullets for this action", Color.red);
    }
    private void Movement(InputAction.CallbackContext context)
    {
        float xValue = context.ReadValue<Vector2>().x;
        float yValue = context.ReadValue<Vector2>().y;

        if (xValue < -deadZone && currentCharacter.transform.localScale.x > 0)
        {
            Flip();
        }
        else if (xValue > deadZone && currentCharacter.transform.localScale.x < 0)
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
        currentCharacter.transform.localScale = new Vector3(currentCharacter.transform.localScale.x * -1, currentCharacter.transform.localScale.y, currentCharacter.transform.localScale.z);
    }
    public void ToggleTargeting(InputAction.CallbackContext context)
    {
        if (!isTargeting)
        {
            Cursor.SetCursor(Crosshair, new Vector2(Crosshair.width / 2, Crosshair.height/2), CursorMode.ForceSoftware);
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
