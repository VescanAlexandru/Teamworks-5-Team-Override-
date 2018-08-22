using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public int numPlayers;
    public int ratioPlayersToSab = 4; //1 sab for every 4 players

    private int numSab; //saboteur
    private int numInno; //innocent
    private int numDet; //detective

    public List<Player> playerList;
    public List<Material> colorList;

    private List<string> randomNameList = new List<string>
    {
        //25 girl names
        "Emma", "Olivia", "Ava", "Sophia", "Isabella", "Mia", "Charlotte", "Abigail", "Emily", "Harper",
        "Harper", "Amelia", "Evelyn", "Elizabeth", "Sofia", "Madison", "Avery", "Ella", "Scarlett", "Grace",
        "Chloe", "Victoria", "Riley", "Aria", "Lily",

        //25 boy names
        "Noah", "Liam", "William", "Mason", "James", "Benjamin", "Jacob", "Michael", "Elijah", "Ethan",
        "Alexander", "Oliver", "Daniel", "Lucas", "Matthew", "Aiden", "Jackson", "Logan", "David", "Joseph",
        "David", "Joseph", "Samuel", "Henry", "Owen"
    };


    // Use this for initialization
    void Start()
    {
        numDet = 1;
        numSab = numPlayers / ratioPlayersToSab;
        numInno = numPlayers - numDet - numSab;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GeneratePlayers(int numPlayers)
    {
        playerList = new List<Player>(numPlayers);

        //assigning one detective role
        playerList[Random.Range(0, numPlayers)].AssignRole("Detective");

        int randNum;
        //assigning random saboteur roles
        for (int i = 0; i < numSab; i++)
        {

            //keep searching for a random spot that doesn't already have a role
            do
            {
                randNum = Random.Range(0, numPlayers);
            }
            while (playerList[randNum].HasRole());

            playerList[randNum].AssignRole("Saboteur");

        }

        //assigning innocent roles for the rest of the players
        for (int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].HasRole())
            {
                playerList[i].AssignRole("Innocent");
            }
        }

    }

    public void GeneratePlayerNames()
    {
        int randNum;
        for (int i = 0; i < playerList.Count; i++)
        {
            randNum = Random.Range(0, randomNameList.Count);
            playerList[i].AssignName(randomNameList[randNum]);
            randomNameList.RemoveAt(randNum);

        }
    }

    public void AssignPlayerColors()
    {
        int randNum;
        for (int i = 0; i < playerList.Count; i++)
        {
            randNum = Random.Range(0, colorList.Count);
            playerList[i].SetColor(colorList[randNum]);
            colorList.RemoveAt(randNum);
        }
    }
}
