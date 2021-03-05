using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    public GameObject cardPanel;
    public GameObject upperFrog;
    public GameObject lowerFrog;
    public GameObject endGameCanvas;
    public Text winnerText;
    public float frogAppearTime = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ShowUpperFrog();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShowLowerFrog();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Greeting());
        }

    }

    public void A_CardPanelOpener(bool state)
    {
        cardPanel.GetComponent<Animator>().SetBool("isPanelOpen", state);
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void ShowUpperFrog()
    {
        StartCoroutine(DoMove(upperFrog, frogAppearTime, new Vector3(40, 106 + 36, 0)));
        StartCoroutine(DoMove(lowerFrog, frogAppearTime, new Vector3(40, -130 + 36, 0)));
    }

    public void ShowLowerFrog()
    {
        StartCoroutine(DoMove(upperFrog, frogAppearTime, new Vector3(40, 130 + 36, 0)));
        StartCoroutine(DoMove(lowerFrog, frogAppearTime, new Vector3(40, -106 + 36, 0)));
    }

    public void OpenEndGameCanvas()
    {
        winnerText.text = "Green Player Won!";


        OpenPanel(endGameCanvas);
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator DoMove(GameObject gameObject, float time, Vector2 targetPosition) {

        Vector2 startPosition = gameObject.transform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f; while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            gameObject.transform.position = Vector2.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }
    }

    private IEnumerator Greeting()
    {
        StartCoroutine(DoMove(upperFrog, 1, new Vector3(40, 100 + 36, 0)));
        StartCoroutine(DoMove(lowerFrog, 1, new Vector3(40, -100 + 36, 0)));

        yield return new WaitForSeconds(2);

        StartCoroutine(DoMove(upperFrog, 2, new Vector3(40, 130 + 36, 0)));
        StartCoroutine(DoMove(lowerFrog, 2, new Vector3(40, -130 + 36, 0)));


        yield return null;
    }
}
