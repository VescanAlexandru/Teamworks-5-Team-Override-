using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameStateEnum { WaitingForPlayers, Starting, Playing, GameOver};
    public GameStateEnum state;
    public PlayerManager playerManager;
    private float startingClock;
    private float gameOverClock;
    private float restartClock;

	// Use this for initialization
	void Start () {
        state = GameStateEnum.WaitingForPlayers;
	}
	
	// Update is called once per frame
	void Update () {
		if (state == GameStateEnum.WaitingForPlayers)
        {
            if (playerManager.playerList.Count >= playerManager.numPlayers)
            {
                state = GameStateEnum.Starting;
                playerManager.GeneratePlayers();
                startingClock = 10.0f;
            }
        } else if (state == GameStateEnum.Starting)
        {
            startingClock = startingClock - Time.deltaTime;
            if (startingClock < 0)
            {
                state = GameStateEnum.Playing;
            }
        } else if (state == GameStateEnum.Playing)
        {

        } else if (state == GameStateEnum.GameOver)
        {
            gameOverClock = gameOverClock = Time.deltaTime;
            if (gameOverClock < 0)
            {
                if (playerManager.HasAllPlayers())
                {
                    state = GameStateEnum.Starting;
                    playerManager.GeneratePlayers();
                    restartClock = 10.0f;
                } else
                {
                    state = GameStateEnum.WaitingForPlayers;
                }
            }
        }
	}

    public void GameOver()
    {
        state = GameStateEnum.GameOver;
        gameOverClock = 15.0f;
    }
}
