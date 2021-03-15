using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Vector2Int coordinates = Vector2Int.zero;
    private Board board = null;
    private Player player = null;
    public bool onMainBase { get; set; } = true;

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Players.Instance.CurrentPlayer.ClickOnFrog(this);
        }
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }

    public void MoveTo(Lily lily)
    {
        coordinates = lily.GetCoordinates();
        transform.position = lily.transform.position;
    }
}
