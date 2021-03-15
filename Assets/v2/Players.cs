using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    private Player _currentPlayer;
    private Color[] teamColors = new Color[] { Color.green, Color.red, Color.yellow, Color.blue };
    //private 
    private static Players instance = null;

    public static Players Instance { get { return instance; } }
    public Player CurrentPlayer { get { return _currentPlayer; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    public void RecordNewPlayer(Player newPlayer)
    {
        players.Add(newPlayer);
        newPlayer.SetTeamColor(teamColors[players.Count]);

        if (_currentPlayer == null)
        {
            _currentPlayer = players[0];
        }

        newPlayer.gameObject.name = $"Player from Team_{GetPlayerIndex(newPlayer)}";
    }

    public void NextPlayer()
    {
        if (_currentPlayer == players[0])
        {
            _currentPlayer = players[1];
        }
        else
        {
            _currentPlayer = players[0];
        }

        Debug.Log($"Now Player{GetPlayerIndex(CurrentPlayer)}'s turn!");
    }

    public int GetPlayerIndex(Player player)
    {
        return players.IndexOf(player);
    }
}
