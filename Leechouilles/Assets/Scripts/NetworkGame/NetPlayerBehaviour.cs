using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class NetPlayerBehaviour : NetworkBehaviour
{
    public static NetPlayerBehaviour instance;

    private void Awake()
    {
        instance = this;
    }

    public Dictionary<int, Player> players = new Dictionary<int, Player>();

    public void SetPlayerAlien(int playerID)
    {
        if (!base.IsServer)
        {
            return;
        }

        players[playerID].isAlien = true;
    }

    public void DamagePlayer(int playerID, int damage, int attackerID)
    {
        if(!base.IsServer)
        {
            return;
        }

        players[playerID].health -= damage;

        if (players[playerID].health <= 0)
        {
            PlayerKilled(playerID, attackerID);
        }
    }

    void PlayerKilled(int playerID, int killerID)
    {
        print("Player : " + playerID.ToString() + " was killed by " + killerID.ToString());
    }

    public class Player
    {
        public bool isAlien;
        public int health;
    }
}
