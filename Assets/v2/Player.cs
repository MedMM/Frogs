using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Board board = null;
    [SerializeField] private Frog frogInHand = null;
    [SerializeField] private Color teamColor = Color.white;

    public void Awake()
    {
        Players.Instance.RecordNewPlayer(this);
    }

    public void SetTeamColor(Color newColor)
    {
        teamColor = newColor;
    }

    public void TakeFrog(Frog frog)
    {
        frogInHand = frog;
    }

    public void RecordFrogOnTeam(Frog frog)
    {
        frog.gameObject.name = $"Frog {Players.Instance.GetPlayerIndex(this)}";
        frog.gameObject.GetComponent<SpriteRenderer>().color = teamColor;
        frog.SetPlayer(this);
    }

    public void ClickOnFrog(Frog frog)
    {
        if (frogInHand == null)
        {
            TakeFrog(frog);
        }
        else
        {
            Debug.Log($"{gameObject}. Click on frog");
        }
    }

    public void ClickOnLily(Lily lily)
    {
        if (frogInHand)
        {
            frogInHand.MoveTo(lily);
            Debug.Log($"Frog Moved");
        }
        else
        {
            Debug.Log($"{gameObject}. Click on lily ");
        }
    }
}
