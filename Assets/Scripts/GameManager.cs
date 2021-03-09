using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void BuildLilies()
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

        //Разместить игрока 1
        GameObject player1Base = Instantiate(halfLilyPrefab, new Vector2(liliesArray[2, 0].transform.position.x, liliesArray[2, 0].transform.position.y - 15), Quaternion.Euler(0, 0, 0));
        player1Base.GetComponent<HalfLilyScript>().neighborLily = liliesArray[2, 0]; //Соседняя от базы лилия
        player1Base.GetComponent<HalfLilyScript>().teamColor = new Color(90f / 255f, 120f / 200f, 60f / 255f); //Цвет команды

        GameObject player2Base = Instantiate(halfLilyPrefab, new Vector2(liliesArray[2, 4].transform.position.x, liliesArray[2, 4].transform.position.y + 10), Quaternion.Euler(0, 0, 180));
        player2Base.GetComponent<HalfLilyScript>().neighborLily = liliesArray[2, 4];
        player2Base.GetComponent<HalfLilyScript>().teamColor = new Color(180f / 255f, 120 / 255f, 40 / 255f);
    }
}
