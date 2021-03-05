using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MenuManager : MonoBehaviour
{
    public GameObject CanvasMenu;
    public GameObject CanvasMultiplayer;
    public GameObject CanvasMultiplayerLobby;
    public GameObject CanvasAbout;
    public GameObject CanvasRules;
    public Text lobbyCode;

    private static Random random = new Random();

    void Start()
    {
        GenerateRoomCode();
    }

    public void OpenCanvas(GameObject canvas)
    {
        HideAllCanvases();
        canvas.SetActive(true);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void HideAllCanvases()
    {
        CanvasMenu.SetActive(false);
        CanvasMultiplayer.SetActive(false);
        CanvasMultiplayerLobby.SetActive(false);
        CanvasAbout.SetActive(false);
        CanvasRules.SetActive(false);
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void GenerateRoomCode()
    {
        lobbyCode.text = "Your lobby code: " + RandomString(5);
    }

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Range(1, length).Select(_ => chars[random.Next(chars.Length)]).ToArray());
    }
}
