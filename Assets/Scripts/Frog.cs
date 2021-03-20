using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private Vector2Int coordinates = new Vector2Int(20, 20);
    private Player player = null;
    public bool onMainBase = true;
    private float animationTime = 0.4f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -20);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Players.Instance.CurrentPlayer.ClickOnFrog(this);
        }
    }

    private void BreakBridge(Lily lily)
    {
        Vector2Int direction = coordinates - lily.GetCoordinates();

        if (direction == Vector2Int.down)
        {
            Board.Instance.GetLilyAtPosition(coordinates).GetVerticalBridge().SetActiveState(false);
        }
        if (direction == Vector2Int.left)
        {
            Board.Instance.GetLilyAtPosition(coordinates).GetHorizontalBridge().SetActiveState(false);
        }
        if (direction == Vector2Int.right)
        {
            Board.Instance.GetLilyAtPosition(coordinates - new Vector2Int(1, 0)).GetHorizontalBridge().SetActiveState(false);
        }
        if (direction == Vector2Int.up)
        {
            Board.Instance.GetLilyAtPosition(coordinates - new Vector2Int(0, 1)).GetVerticalBridge().SetActiveState(false);
        }
    }

    public void OutOfGame()
    {
        coordinates = new Vector2Int(20, 20);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
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
        Vector2Int oldPosition = coordinates;

        BreakBridge(lily);
        coordinates = lily.GetCoordinates();
        LeanTween.move(gameObject, lily.GetPosition(), animationTime);

        Board.Instance.GetLilyAtPosition(coordinates).SetOccupiedState(true);
        if (!Board.Instance.IsFrogOnThisCoordinatesExist(oldPosition))
        {
            Board.Instance.GetLilyAtPosition(oldPosition).SetOccupiedState(false);
        }
    }

    public void PlantFrog()
    {
        coordinates = player.GetHalfLily().GetNeighborLily().GetCoordinates();
        LeanTween.move(gameObject, player.GetHalfLily().GetNeighborLily().GetPosition(), animationTime);
        Board.Instance.GetLilyAtPosition(coordinates).SetOccupiedState(true);
    }
}
