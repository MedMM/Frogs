using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    public static GameObject[] frogs;
    public int numberOfteams;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GetAllFrogs();
        }
    }

    public static GameObject GetFrogOnPosition(Vector2Int position)
    {
        GetAllFrogs();

        foreach (GameObject frog in frogs)
        {
            if(frog.GetComponent<FrogScript>().position == position)
            {
                return frog;
            }
        }
        return null;
    }

    public static void GetAllFrogs()
    {
        frogs = GameObject.FindGameObjectsWithTag("Player");
    }
}
