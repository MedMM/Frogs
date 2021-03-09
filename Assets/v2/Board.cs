using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Transform lilyHolder = null;
    [SerializeField] private GameObject lilyPrefab = null;
    [SerializeField] private GameObject halfLilyPrefab = null;
    [SerializeField] private float distanceBetweenLilies = 0f;
    private Lily[,] lilyArray = new Lily[rows, rows];
    public const int rows = 5;

    private void Awake()
    {
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
                obj.GetComponent<Lily>().SetCoordinates(new Vector2Int(i,j)); //Set coordinates in Lily script
                lilyArray[i, j] = obj.GetComponent<Lily>(); //Add lily to array
            }
        }

        GameObject player1Base = Instantiate(halfLilyPrefab, lilyArray[2, 0].gameObject.transform.position - new Vector3(0, 15, 0),
            Quaternion.Euler(0, 0, 0), gameObject.transform);

        GameObject player2Base = Instantiate(halfLilyPrefab, lilyArray[2, 4].gameObject.transform.position - new Vector3(0, -15, 0),
            Quaternion.Euler(0, 0, 180), gameObject.transform);
    }

    public Lily GetLilyAtPosition(int x, int y)
    {
        return lilyArray[x, y];
    }
}
