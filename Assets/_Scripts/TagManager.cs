using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour {

    public PlayerManager playerManager;
    public GameManager gameManager;
    public Transform localPlayer;
	
	// Update is called once per frame
	void Update () {
		foreach (Player player in playerManager.playerList)
        {
            player.playerNameText.transform.LookAt(localPlayer.transform);
        }
	}

    public void ResetColors()
    {
        foreach (Player player in playerManager.playerList)
        {
            if (player.role == PlayerManager.RoleEnum.Innocent)
            {
                player.playerNameText.color = Color.green;
            }
            else
            {
                player.playerNameText.color = Color.red;
            }
        }
    }
}
