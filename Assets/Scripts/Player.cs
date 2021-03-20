using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Cursor cursor = null;
    [SerializeField] private HalfLily halfLily = null;
    [SerializeField] private Frog frogInHand = null;
    [SerializeField] private Color teamColor = Color.white;
    [SerializeField] private bool chainReaction = false;

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
    }

    private void PlantFrog(Frog frog)
    {
        frog.onMainBase = false;
        frog.PlantFrog();
    }

    private void EndTurn()
    {
        chainReaction = false;
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
        if (chainReaction)
        {
            Debug.Log("Сначала заверши цепную реакцию");
            return;
        }

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
                            chainReaction = true;
                            var leftHand = Board.Instance.GetFrogAtPosition(lily.GetCoordinates());
                            PlantFrog(frogInHand);
                            TakeFrog(leftHand);
                        }
                        else
                        {
                            chainReaction = false;
                            PlantFrog(frogInHand);
                            EndTurn();
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
                        chainReaction = true;
                        var leftHand = Board.Instance.GetFrogAtPosition(lily.GetCoordinates());
                        MoveFrog(lily);
                        TakeFrog(leftHand);
                    }
                    else
                    {
                        chainReaction = false;
                        MoveFrog(lily);
                        EndTurn();
                    }
                }
            }
        }
        else
        {
            Debug.Log("Крутая лилия, согласен");
        }
    }

    public void ClickOnHalfLily(HalfLily halfLily)
    {
        Debug.Log(1);
        if (frogInHand && frogInHand.GetPlayer() == this )
        {
                Debug.Log(2);
            if (this.halfLily != halfLily && frogInHand.GetCoordinates() == halfLily.GetNeighborLily().GetCoordinates())
            {
                Debug.Log(3);
                halfLily.EndPath(frogInHand);
                EndTurn();
            }
        }
    }

    public void ClickOnBridge(Bridge bridge)
    {
        if (bridge.GetActiveState() == false && !chainReaction)
        {
            bridge.SetActiveState(true);
            EndTurn();
        }
    }
}
