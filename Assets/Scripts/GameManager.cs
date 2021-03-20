using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timeBeforeNewGameStarts = 2f;

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OverGame(Player winner)
    {
        Debug.Log($"Game Finished! {winner.gameObject.name} finished first");
        Debug.Log("New game starts...");
        Invoke("ReloadScene", timeBeforeNewGameStarts);
    }


    
}
