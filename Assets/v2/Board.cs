using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform lilyHolder = null;
    [SerializeField] private GameObject lilyPrefab = null;
    [SerializeField] private GameObject halfLilyPrefab = null;
    [SerializeField] private float distanceBetweenLilies = 0f;
    private static Board instance = null;
    public static Board Instance { get { return instance; } }
    private Lily[,] lilyArray = new Lily[rows, rows];

    public const int rows = 5;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        BuildBoard();
    }

    private void BuildBoard()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector2 position = new Vector2((i - 2) * distanceBetweenLilies, (j - 2) * distanceBetweenLilies); //Calculate lily position
                var obj = Instantiate(lilyPrefab, position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 1)), lilyHolder); //Instantiate
                obj.GetComponent<Lily>().SetCoordinates(new Vector2Int(i, j)); //Set coordinates in Lily script
                lilyArray[i, j] = obj.GetComponent<Lily>(); //Add lily to array
            }
        }

        GameObject player1Base = Instantiate(halfLilyPrefab, lilyArray[2, 0].gameObject.transform.position - new Vector3(0, 15, 0),
            Quaternion.Euler(0, 0, 0), gameObject.transform);
        player1Base.GetComponent<HalfLily>().SetCoordinates(new Vector2Int(2, -1));
        player1Base.GetComponent<HalfLily>().SetNeighborLily(GetLilyAtPosition(2, 0));

        GameObject player2Base = Instantiate(halfLilyPrefab, lilyArray[2, 4].gameObject.transform.position - new Vector3(0, -15, 0),
            Quaternion.Euler(0, 0, 180), gameObject.transform);
        player2Base.GetComponent<HalfLily>().SetCoordinates(new Vector2Int(2, 5));
        player2Base.GetComponent<HalfLily>().SetNeighborLily(GetLilyAtPosition(2, 4));
    }

    public Lily GetLilyAtPosition(int x, int y)
    {
        return lilyArray[x, y];
    }

    public Lily GetLilyAtPosition(Vector2Int coordinates)
    {
        return GetLilyAtPosition(coordinates.x, coordinates.y);
    }

    public bool IsLilyExist(Vector2Int coordinates)
    {
        return (coordinates.x >= 0 && coordinates.x < rows &&
                coordinates.y >= 0 && coordinates.y < rows);
    }

    public bool IsLilyNearbyToEachOther(Lily lily1, Lily lily2)
    {
        var pos1 = lily1.GetCoordinates();
        var pos2 = lily2.GetCoordinates();
        return (pos1.x == pos2.x && Mathf.Abs(pos1.y - pos2.y) == 1) ||
                (pos1.y == pos2.y && Mathf.Abs(pos1.x - pos2.x) == 1);
    }

    public bool IsFrogCanJumpOnLily(Vector2Int frogPosition, Lily lily)
    {
        Vector2Int direction = frogPosition - lily.GetCoordinates(); //Откуда прыгает жаба с frogCoordinates
        bool state = false;

        if (!IsBridgesAllowToJumpOnLily(frogPosition, lily))
        {
            return false;
        }

        if (!lily.isOccupied)
        {
            return true;
        }

        //Проверяет может ли жаба упрыгнуть вверх
        //Сверху лилии стоит мостик и на лилию прыгают не сверху?
        if (IsLilyExist(lily.GetCoordinates() + Vector2Int.up))
        {
            Lily upLily = GetLilyAtPosition(lily.GetCoordinates() + Vector2Int.up);

            if (lily.GetVerticalBridge().GetActiveState() && direction != Vector2Int.up)
            {
                //Если на лилии сверху другая жаба
                if (upLily.isOccupied)
                {
                    state |= IsFrogCanJumpOnLily(lily.GetCoordinates(), upLily);
                }
                else
                {
                    return true;
                }
            }
        }
        //Проверяет может ли жаба упрыгнуть вправо
        if (IsLilyExist(lily.GetCoordinates() + Vector2Int.right))
        {
            Lily rightLily = GetLilyAtPosition(lily.GetCoordinates() + Vector2Int.right);

            if (lily.GetHorizontalBridge().GetActiveState() && direction != Vector2Int.right)
            {
                //Если на лилии справа другая жаба
                if (rightLily.isOccupied)
                {
                    state |= IsFrogCanJumpOnLily(lily.GetCoordinates(), rightLily);
                }
                else
                {
                    return true;
                }
            }
        }
        //Проверяет может ли жаба упрыгнуть влево
        if (IsLilyExist(lily.GetCoordinates() + Vector2Int.left))
        {
            Lily leftLily = GetLilyAtPosition(lily.GetCoordinates() + Vector2Int.left);

            if (leftLily.GetHorizontalBridge().GetActiveState() && direction != Vector2Int.left)
            {
                //Если на лилии слева другая жаба
                if (leftLily.isOccupied)
                {
                    state |= IsFrogCanJumpOnLily(lily.GetCoordinates(), leftLily);
                }
                else
                {
                    return true;
                }
            }
        }
        //Проверяет может ли жаба упрыгнуть вниз
        if (IsLilyExist(lily.GetCoordinates() + Vector2Int.down))
        {
            Lily downLily = GetLilyAtPosition(lily.GetCoordinates() + Vector2Int.down);

            if (downLily.GetVerticalBridge().GetActiveState() && direction != Vector2Int.down)
            {
                //Если на лилии снизу другая жаба
                if (downLily.isOccupied)
                {
                    state |= IsFrogCanJumpOnLily(lily.GetCoordinates(), downLily);
                }
                else
                {
                    return true;
                }
            }
        }
        return state;
    }

    public bool IsBridgesAllowToJumpOnLily(Vector2Int frogPosition, Lily lily)
    {
        Vector2Int direction = frogPosition - lily.GetCoordinates();

        if (direction == Vector2Int.up)
        {
            return lily.GetVerticalBridge().GetActiveState();
        }

        if (direction == Vector2Int.right)
        {
            return lily.GetHorizontalBridge().GetActiveState();
        }

        if (direction == Vector2Int.left)
        {
            return GetLilyAtPosition(lily.GetCoordinates() + Vector2Int.left).GetHorizontalBridge().GetActiveState();
        }

        if (direction == Vector2Int.down)
        {
            return GetLilyAtPosition(lily.GetCoordinates() + Vector2Int.down).GetVerticalBridge().GetActiveState();
        }

        throw new System.Exception("Unexpected Values ");
    }
}
