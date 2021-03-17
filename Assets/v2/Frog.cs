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
        Board.Instance.GetLilyAtPosition(coordinates).isOccupied = false;
        coordinates = lily.GetCoordinates();
        transform.position = lily.transform.position;
        Board.Instance.GetLilyAtPosition(coordinates).isOccupied = true;
    }

    public void PlantFrog()
    {
        coordinates = player.GetHalfLily().GetNeighborLily().GetCoordinates();
        transform.position = player.GetHalfLily().GetNeighborLily().transform.position;
        Board.Instance.GetLilyAtPosition(coordinates).isOccupied = true;
    }

    public void MoveUp()
    {
        if (coordinates.y < Board.rows - 1)
        {
            if (Board.Instance.GetLilyAtPosition(coordinates).GetVerticalBridge().GetActiveState())
            {
                Board.Instance.GetLilyAtPosition(coordinates).GetVerticalBridge().SetActiveState(false);
                MoveTo(Board.Instance.GetLilyAtPosition(coordinates + new Vector2Int(0, 1)));
            }
        }
    }

    public void MoveDown()
    {
        if (coordinates.y > 0)
        {
            if (Board.Instance.GetLilyAtPosition(coordinates - new Vector2Int(0,1)).GetVerticalBridge().GetActiveState())
            {
                Board.Instance.GetLilyAtPosition(coordinates - new Vector2Int(0, 1)).GetVerticalBridge().SetActiveState(false);
                MoveTo(Board.Instance.GetLilyAtPosition(coordinates + new Vector2Int(0, -1)));
            }
        }
    }

    public void MoveRight()
    {
        if (coordinates.x < Board.rows - 1)
        {
            if (Board.Instance.GetLilyAtPosition(coordinates).GetHorizontalBridge().GetActiveState())
            {
                Board.Instance.GetLilyAtPosition(coordinates).GetHorizontalBridge().SetActiveState(false);
                MoveTo(Board.Instance.GetLilyAtPosition(coordinates + new Vector2Int(1, 0)));
            }
        }
    }

    public void MoveLeft()
    {
        if (coordinates.x > 0)
        {
            if (Board.Instance.GetLilyAtPosition(coordinates - new Vector2Int(1, 0)).GetHorizontalBridge().GetActiveState())
            {
                Board.Instance.GetLilyAtPosition(coordinates - new Vector2Int(1, 0)).GetHorizontalBridge().SetActiveState(false);
                MoveTo(Board.Instance.GetLilyAtPosition(coordinates + new Vector2Int(-1, 0)));
            }
        }
    }
}
