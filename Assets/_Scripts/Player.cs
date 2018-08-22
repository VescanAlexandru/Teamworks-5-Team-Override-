using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class Player : NetworkBehaviour {

    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;
    //public GameObject body;


    public PlayerManager playerManager;
    private bool alive;
    private string playerName;
    private string role;
    private Material playerMaterial;

    private bool hasRole;

    public TextMeshProUGUI playerNameText;


    // Use this for initialization
    void Start () {
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
        alive = true;
        hasRole = false;
    }
	
	// Update is called once per frame
	void Update () {
        //turn everyone's tags so that it is perpendicular to the player's vision
    }
    public void AssignName(string n)
    {
        playerName = n;
        playerNameText.GetComponent<TextMeshProUGUI>().text = playerName;
    }

    public string GetName()
    {
        return playerName;
    }

    public void AssignRole(string r)
    {
        role = r;
        hasRole = true;
        switch (role)
        {
            case "Saboteur":
                for (int i = 0; i < playerManager.playerList.Count; i++)
                {
                    if (playerManager.playerList[i].GetRole() == "Innocent")
                    {
                        playerManager.playerList[i].GetComponent<TextMeshProUGUI>().color = Color.green;
                    }
                    else if (playerManager.playerList[i].GetRole() == "Saboteur")
                    {
                        playerManager.playerList[i].GetComponent<TextMeshProUGUI>().color = Color.red;
                    }
                }
                break;
            case "Innocent":
                for (int i = 0; i < playerManager.playerList.Count; i++)
                {
                    playerManager.playerList[i].GetComponent<TextMeshProUGUI>().color = Color.green;
                }
                break;
        }
    }

    public string GetRole()
    {
        return role;
    }

    public bool HasRole()
    {
        return hasRole;
    }

    public void SetColor(Material m)
    {
        playerMaterial = m;
        this.GetComponentInParent<MeshRenderer>().material = m;
    }

    public Material GetMaterial()
    {
        return playerMaterial;
    }
    
    public void Eliminate()
    {
        alive = false;
        Debug.Log("Player has been eliminated");
        //show that this player is dead by placing player sideways on ground

        //this.GetComponent<Transform>().rotation.y ==
    }
}
