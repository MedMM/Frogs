using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Actions
    {
        Nothing,
        Jump,
        Build,
        Card_DoubleJump,
        Card_Parachute,
        Card_DoubleBuild,
        Card_RemoveBridge
    }

    [SerializeField]Actions action = Actions.Nothing;

    public GameObject waterLilyPrefab; //Экземпляр кувшинки
    public GameObject halfLilyPrefab; //Экземпляр кувшинки
    public static GameObject[,] liliesArray;  //Массив всех кувшинок на игровом поле
    public static int numbersOfPlayers = 2;
    public static int rows = 5;
    public static int columns = 5;
    public float scale = 20;
    public Vector2 LeftBottomLocation = new Vector2(0, 0);

    void Awake()
    {
        liliesArray = new GameObject[columns, rows];
        BuildLilies();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            DestroyAllChilds();
            BuildLilies();
        }
    }

    public void AAA_SetAction(int action)
    {
        this.action = (Actions)action;
    }

    void BuildLilies()
    {
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(waterLilyPrefab, new Vector2(LeftBottomLocation.x + scale * i, LeftBottomLocation.y + scale * j), Quaternion.Euler(0, 0, Random.Range(0, 1)), gameObject.transform);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<WaterLilyScript>().position.x = i;
                obj.GetComponent<WaterLilyScript>().position.y = j;

                liliesArray[i, j] = obj;
            }
        }

        GameObject player1Base = Instantiate(halfLilyPrefab, new Vector2(liliesArray[2, 0].transform.position.x, liliesArray[2, 0].transform.position.y - 15), Quaternion.Euler(0, 0, 0));
        player1Base.GetComponent<HalfLilyScript>().neighborLily = liliesArray[2, 0];
        player1Base.GetComponent<HalfLilyScript>().teamColor = new Color(95f / 255f, 125f / 255f, 70f / 255f);

        GameObject player2Base = Instantiate(halfLilyPrefab, new Vector2(liliesArray[2, 4].transform.position.x, liliesArray[2, 4].transform.position.y + 10), Quaternion.Euler(0, 0, 180));
        player2Base.GetComponent<HalfLilyScript>().neighborLily = liliesArray[2, 4];
        player2Base.GetComponent<HalfLilyScript>().teamColor = new Color(125f / 255f, 80f / 255f, 70f / 255f);
    }

    void DestroyAllChilds()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
