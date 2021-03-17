using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject pointer = null;
    [SerializeField] private HalfLily halfLily = null;
    [SerializeField] private Frog frogInHand = null;
    [SerializeField] private Color teamColor = Color.white;

    private void Awake()
    {
        Players.Instance.RecordNewPlayer(this);
        HideCursor();
    }

    private void TakeFrog(Frog frog)
    {
        frogInHand = frog;
    }

    private void PlaceFrog(Lily lily)
    {
        frogInHand.MoveTo(lily);
        EndTurn();
    }

    private void PlantFrog(Frog frog)
    {
        frog.onMainBase = false;
        frog.PlantFrog();
    }

    private void HideCursor()
    {
        pointer.SetActive(false);
    }

    private void ShowCursor()
    {
        pointer.SetActive(true);
    }

    private void MoveCursor(Vector3 newPosition)
    {
        pointer.transform.position = newPosition;
        ShowCursor();
    }

    private void EndTurn()
    {
        return;
        frogInHand = null;
        HideCursor();
        Players.Instance.NextPlayer();
    }

    public void SetHalfLily(HalfLily newHalfLily)
    {
        if (halfLily == null)
            halfLily = newHalfLily;
    }

    public void SetTeamColor(Color newColor)
    {
        teamColor = newColor;
    }

    public HalfLily GetHalfLily()
    {
        return halfLily;
    }

    public void RecordFrogOnTeam(Frog frog)
    {
        frog.gameObject.name = $"Frog from Team_{Players.Instance.GetPlayerIndex(this)}";
        frog.gameObject.GetComponent<SpriteRenderer>().color = teamColor;
        frog.SetPlayer(this);
    }

    public void ClickOnFrog(Frog frog)
    {
        if (frog.GetPlayer() != this)
        {
            Debug.Log("Это чужая жаба, разворачивай!!");
            return;
        }

        if (frogInHand == null)
        {
            TakeFrog(frog);
            MoveCursor(frog.transform.position);
        }
        else
        {
            frogInHand = null;
            ClickOnFrog(frog);
        }
    }

    public void ClickOnLily(Lily lily)
    {
        if (frogInHand)
        {
            if (frogInHand.onMainBase)
            {
                PlantFrog(frogInHand);
                return;
            }

            if (Board.Instance.IsLilyNearbyToEachOther(lily, Board.Instance.GetLilyAtPosition(frogInHand.GetCoordinates())))
            {
                if (Board.Instance.IsFrogCanJumpOnLily(frogInHand.GetCoordinates(), lily))
                {
                    PlaceFrog(lily);
                    return;
                }
            }
        }
        else
        {
            Debug.Log("Крутая лилия, согласен");
        }
    }

    public void ClickOnBridge(Bridge bridge)
    {

        bridge.SetActiveState();

        //if (bridge.GetActiveState() == false)
        //{
        //    bridge.SetActiveState(true);
        //    EndTurn();
        //}
    }

    private void Update()
    {
        if (frogInHand != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
                frogInHand.MoveUp();
            if (Input.GetKeyDown(KeyCode.S))
                frogInHand.MoveDown();
            if (Input.GetKeyDown(KeyCode.D))
                frogInHand.MoveRight();
            if (Input.GetKeyDown(KeyCode.A))
                frogInHand.MoveLeft();
        }
    }
}
