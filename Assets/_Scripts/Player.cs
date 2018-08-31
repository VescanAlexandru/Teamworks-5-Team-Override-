using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool> { }

public class Player : NetworkBehaviour {

    /*[SyncVar(hook = "OnNameChanged")] public string playerName;
    [SyncVar(hook = "OnColorChanged")] public Color playerColor;
    [SyncVar (hook = "OnRoleChanged")] public PlayerManager.RoleEnum role;
    [SyncVar] public bool alive;*/

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
        //alive = true;
        if (isServer)
        {
            playerManager = GameObject.FindObjectOfType<PlayerManager>();
        }
    }
        

    /*[ServerCallback]
    void OnEnable()
    {
        if (!players.Contains(this))
            players.Add(this);
    }*/

    /*[ServerCallback]
    void OnDisable()
    {
        if (players.Contains(this))
            players.Remove(this);
    }*/
    
    //[Server]
    public void Eliminate()
    {
        DisablePlayer();
        Debug.Log("Player has been eliminated");
       // playerManager.RemovePlayer(this);
        //RpcProcessPlayerElimination();
        //show that this player is dead by placing player sideways on ground

        //this.GetComponent<Transform>().rotation.y ==
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

    /*public void SetAlive(bool alive)
    {
        this.alive = alive;
    }*/

    /*[ClientRpc]
    private void RpcProcessPlayerElimination()
    {
        Debug.Log("ProcessPlayerElimination");
    }*/

    /*void OnNameChanged(string value)
    {
        playerName = value;
        textMeshPro = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = value;
        if (isServer)
        {
            CmdAddToPlayerManager();
        }
        //gameObject.name = playerName;
        //GetComponentInChildren<Text>(true).text = playerName;
    }*/

    /*void OnColorChanged(Color value)
    {
        playerColor = value;

        Material newMaterial = new Material(playerMaterial);
        newMaterial.color = value;
        headRenderer.material = newMaterial;
        leftRenderer.material = newMaterial;
        rightRenderer.material = newMaterial;
        //GetComponentInChildren<RendererToggler>().ChangeColor(playerColor);
    }*/

    /*void OnRoleChanged(PlayerManager.RoleEnum value)
    {
        if (isLocalPlayer)
        {
            //for each other player in list
            //change color how it should be
        } else
        {
            //change color based on localRole
        }
    }*/

    /*[ClientRpc]
    void RpcSetLocalRole(PlayerManager.RoleEnum value)
    {
        playerManager.localRole = value;
    }

    [Command]
    private void CmdAddToPlayerManager()
    {
        if (playerManager != null)
        {
            playerManager.AddPlayer(this);
        }
    }*/
}
