using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool> { }

public class Player : NetworkBehaviour {

    [SyncVar (hook = "OnNameChanged")] public string playerName;
    [SyncVar (hook = "OnColorChanged")] public Color playerColor;
    [SyncVar (hook = "OnRoleChanged")] public PlayerManager.RoleEnum role;
    [SyncVar] public bool alive;

    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;
    public MeshRenderer headRenderer;
    public MeshRenderer leftRenderer;
    public MeshRenderer rightRenderer;
    private TextMeshProUGUI textMeshPro;

    public Material playerMaterial;

    static List<Player> players = new List<Player>();

    private PlayerManager playerManager;

    public TextMeshProUGUI playerNameText;
    private static PlayerManager.RoleEnum localRole;
    private bool colorChanged;
    private bool nameChanged;
    private bool roleChanged;


    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;


    // Use this for initialization
    void Start () {
        EnablePlayer();

        if (isLocalPlayer)
        {
            GameObject playerContainer = GameObject.Find("PlayerContainer");
            if (playerContainer != null)
            {
                GameObject rig = GameObject.Find("VRCameraRig");
                if (rig != null)
                {
                    MyInputManager myInputManager = rig.GetComponent<MyInputManager>();
                    if (myInputManager != null)
                    {
                        myInputManager.SetPlayer(this);
                    } else
                    {
                        Debug.Log("manager null");
                    }
                } else
                {
                    Debug.Log("rig null");
                }
            } else
            {
                Debug.Log("PlayerContainer is null");
            }
        }
        playerNameText = GameObject.Find("PlayerTag").GetComponent<TextMeshProUGUI>();
        alive = true;
        if (isServer)
        {
            playerManager = GameObject.FindObjectOfType<PlayerManager>();
        }
        nameChanged = false;
        colorChanged = false;
        roleChanged = false;
    }

    private void Update()
    {
        if (!nameChanged && playerName != null)
        {
            OnNameChanged(playerName);
        }
        if (!colorChanged && playerColor != null)
        {
            OnColorChanged(playerColor);
        }
        if (!roleChanged && role == PlayerManager.RoleEnum.Saboteur || role == PlayerManager.RoleEnum.Innocent)
        {
            OnRoleChanged(role);
        }
    }

    [Server]
    public void SetPlayerManager(PlayerManager manager)
    {
        playerManager = manager;
    }


    [ServerCallback]
    void OnEnable()
    {
        if (!players.Contains(this))
            players.Add(this);
    }

    [ServerCallback]
    void OnDisable()
    {
        if (players.Contains(this))
            players.Remove(this);
    }
    
    [Server]
    public void Eliminate()
    {
        DisablePlayer();
        alive = false;
        Debug.Log("Player has been eliminated");
        playerManager.RemovePlayer(this);
        head.GetComponent<Rigidbody>().isKinematic = false;
        head.GetComponent<Rigidbody>().useGravity = true;
        head.GetComponent<SphereCollider>().isTrigger = false;
        leftHand.GetComponent<Rigidbody>().isKinematic = false;
        leftHand.GetComponent<Rigidbody>().useGravity = true;
        leftHand.GetComponent<SphereCollider>().isTrigger = false;
        rightHand.GetComponent<Rigidbody>().isKinematic = false;
        rightHand.GetComponent<Rigidbody>().useGravity = true;
        rightHand.GetComponent<SphereCollider>().isTrigger = false;
        RpcProcessPlayerElimination();
        //show that this player is dead by placing player sideways on ground
    }

    void DisablePlayer()
    {
        onToggleShared.Invoke(false);

        if (isLocalPlayer)
            onToggleLocal.Invoke(false);
        else
            onToggleRemote.Invoke(false);
    }

    void EnablePlayer()
    {
        onToggleShared.Invoke(true);

        if (isLocalPlayer)
            onToggleLocal.Invoke(true);
        else
            onToggleRemote.Invoke(true);
    }

    [ClientRpc]
    private void RpcProcessPlayerElimination()
    {
        Debug.Log("ProcessPlayerElimination");

        head.GetComponent<Rigidbody>().isKinematic = false;
        head.GetComponent<Rigidbody>().useGravity = true;
        head.GetComponent<SphereCollider>().isTrigger = false;
        leftHand.GetComponent<Rigidbody>().isKinematic = false;
        leftHand.GetComponent<Rigidbody>().useGravity = true;
        leftHand.GetComponent<SphereCollider>().isTrigger = false;
        rightHand.GetComponent<Rigidbody>().isKinematic = false;
        rightHand.GetComponent<Rigidbody>().useGravity = true;
        rightHand.GetComponent<SphereCollider>().isTrigger = false;
    }

    void OnNameChanged(string value)
    {
        playerName = value;
        textMeshPro = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = value;
        if (isServer)
        {
            AddToPlayerManager();
        }
        //gameObject.name = playerName;
        //GetComponentInChildren<Text>(true).text = playerName;
        nameChanged = true;
    }

    void OnColorChanged(Color value)
    {
        playerColor = value;

        Material newMaterial = new Material(playerMaterial);
        newMaterial.color = value;
        headRenderer.material = newMaterial;
        leftRenderer.material = newMaterial;
        rightRenderer.material = newMaterial;
        //GetComponentInChildren<RendererToggler>().ChangeColor(playerColor);
        colorChanged = true;
    }

    void OnRoleChanged(PlayerManager.RoleEnum value)
    {
        roleChanged = true;
        Debug.Log(value);
        if (isLocalPlayer)
        {
            localRole = value;
            Debug.Log("Local: " + value);
        }
        else
        {
            if (localRole == PlayerManager.RoleEnum.Saboteur && value == PlayerManager.RoleEnum.Saboteur)
            {
                textMeshPro = gameObject.GetComponentInChildren<TextMeshProUGUI>();
                textMeshPro.color = Color.red;
            }
        }
    }

    [Server]
    private void AddToPlayerManager()
    {
        if (playerManager != null)
        {
            playerManager.AddPlayer(this);
        }
    }
}
