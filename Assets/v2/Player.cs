using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Cursor cursor = null;
    [SerializeField] private HalfLily halfLily = null;
    [SerializeField] private Frog frogInHand = null;
    [SerializeField] private Color teamColor = Color.white;

    private void Awake()
    {
        Players.Instance.RecordNewPlayer(this);
        cursor.Hide();
    }

    private void TakeFrog(Frog frog)
    {
        frogInHand = frog;
        cursor.SetGameObjectToFollow(frog.gameObject);
        cursor.Show();
    }

    private void MoveFrog(Lily lily)
    {
        frogInHand.MoveTo(lily);
        EndTurn();
    }

    private void PlantFrog(Frog frog)
    {
        frog.onMainBase = false;
        frog.PlantFrog();
    }

    private void EndTurn()
    {
        return;
        frogInHand = null;
        cursor.Hide();
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
                if (Board.Instance.IsLilyNearbyToEachOther(lily.GetCoordinates(), halfLily.GetCoordinates()))
                {
                    if (Board.Instance.IsFrogCanJumpOnLily(halfLily.GetCoordinates(), lily, true))
                    {
                        if (lily.GetOccupiedState())
                        {
                            var leftHand = Board.Instance.GetFrogAtPosition(lily.GetCoordinates());
                            PlantFrog(frogInHand);
                            TakeFrog(leftHand);
                        }
                        else
                        {
                            PlantFrog(frogInHand);
                        }
                    }

                }
                return;
            }

            if (Board.Instance.IsLilyNearbyToEachOther(lily, Board.Instance.GetLilyAtPosition(frogInHand.GetCoordinates())))
            {
                if (Board.Instance.IsFrogCanJumpOnLily(frogInHand.GetCoordinates(), lily))
                {
                    if (lily.GetOccupiedState())
                    {
                        var leftHand = Board.Instance.GetFrogAtPosition(lily.GetCoordinates());
                        MoveFrog(lily);
                        TakeFrog(leftHand);
                        return;
                    }
                    else
                    {
                        MoveFrog(lily);
                        return;
                    }
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
}
