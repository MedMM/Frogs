using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject lilyPrefab = null;
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
                var obj = Instantiate(lilyPrefab, position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 1)), gameObject.transform); //Instantiate
                obj.GetComponent<Lily>().SetCoordinates(i, j); //Set coordinates in Lily script
                lilyArray[i, j] = obj.GetComponent<Lily>(); //Add lily to array
            }
        }
    }
}
