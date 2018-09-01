using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{
    [SyncVar] public int ratioPlayersToSab = 3;
    [SyncVar] public int numSab;
    [SyncVar] public int numInno;

    public static List<Player> playerList;

    public enum RoleEnum { Innocent, Saboteur };

    [ServerCallback]
    private void Start()
    {
        numSab = 0;
        numInno = 0;
    }

    [Server]
    public void AddPlayer(Player player)
    {
        playerList.Add(player);
        if (numInno / numSab > ratioPlayersToSab && numInno % numSab == ratioPlayersToSab - 1)
        {
            player.role = RoleEnum.Saboteur;
        } else if (numSab * ratioPlayersToSab > numInno)
        {
            player.role = RoleEnum.Innocent;
        } else
        {
            if (Random.Range(0, 1) > 1.0f / ratioPlayersToSab)
            {
                player.role = RoleEnum.Innocent;
            } else
            {
                player.role = RoleEnum.Saboteur;
            }
        }
    }

    [Server]
    public void RemovePlayer(Player player)
    {
        if (player.role == RoleEnum.Innocent)
        {
            numInno--;
        } else
        {
            numSab--;
        }

        playerList.Remove(player);
    }
}
