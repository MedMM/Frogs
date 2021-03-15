using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private GameObject pointer = null;
    [SerializeField] private Board board = null;
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
        if(frogInHand != null)
        {
            frogInHand.MoveTo(lily);
            EndTurn();
        }
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
        frogInHand = null;
        HideCursor();
        Players.Instance.NextPlayer();
    }

    private void EnemyFrogClick()
    {
        Debug.Log("That's not my frog!");
    }

    public void SetTeamColor(Color newColor)
    {
        teamColor = newColor;
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
            PlaceFrog(lily);
        }
        else
        {
            Debug.Log($"{gameObject.name}. Click on lily ");
        }
    }

    public void ClickOnBridge(Bridge bridge)
    {
        if (bridge.GetActiveState() == false)
        {
            bridge.SetActiveState(true);
            EndTurn();
        }
    }
}
