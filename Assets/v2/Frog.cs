using UnityEngine;

public class Frog : MonoBehaviour
{
    private Player player = null;
    private Vector2Int coordinates = Vector2Int.zero;
    public bool onMainBase = true;

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
        if (onMainBase)
        {
            onMainBase = false;
            MoveTo(player.GetHalfLily().GetNeighborLily());
            return;
        }

        coordinates = lily.GetCoordinates();
        transform.position = lily.transform.position;
    }

    public void MoveUp()
    {
        if (coordinates.y < Board.rows - 1)
        {
            MoveTo(Board.Instance.GetLilyAtPosition(coordinates + new Vector2Int(0, 1)));
        }
    }

    public void MoveDown()
    {
        if (coordinates.y > 0)
        {
            MoveTo(Board.Instance.GetLilyAtPosition(coordinates + new Vector2Int(0, -1)));
        }
    }

    public void MoveRight()
    {
        if (coordinates.x < Board.rows - 1)
        {
            MoveTo(Board.Instance.GetLilyAtPosition(coordinates + new Vector2Int(1, 0)));
        }
    }

    public void MoveLeft()
    {
        if (coordinates.x > 0)
        {
            MoveTo(Board.Instance.GetLilyAtPosition(coordinates + new Vector2Int(-1, 0)));
        }
    }
}
