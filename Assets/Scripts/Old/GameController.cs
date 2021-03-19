using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    //public enum Action
    //{
    //    Nothing,
    //    Card_DoubleJump,
    //    Card_Parachute,
    //    Card_DoubleBuild,
    //    Card_RemoveBridge
    //}

    //private static GameObject _gameObject;
    //public static bool clickedOnEmpty;

    //public GameObject endGameCanvas;
    //private RaycastHit2D hit;
    //public UI_Controller uI_Controller;
    //static UI_Controller staticUi;
    //public static GameObject activeElement; //Выбранный елемент
    //public static Teams.TeamName currentTeam = Teams.TeamName.Green_team; //Кто сейчас ходит
    //public static Action action = Action.Nothing;//Выбранная карточка
    //public static int numberOfMoves = 1; // Сколько шагов жаба должна ещё пройти
    //public static int numberOfBuilds = 1; // Сколько мостиков можно построить
    //public static bool canSetAction = true; //Можно ли выбрать другую карточку?

    //public Vector2 frogPosition; //Позиция activeElement лягушки
    //public Vector2 lilyPosition;//Позиция activeElement лилии

    //private void Update()
    //{
    //    Clicker();
    //}

    //private void Start()
    //{
    //    _gameObject = gameObject;

    //    staticUi = uI_Controller;
    //}

    //private void Clicker()
    //{
    //    clickedOnEmpty = false;

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

    //        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity);

    //        //Если выбрана карточка "убрать мостик"
    //        if (action == Action.Card_RemoveBridge && numberOfMoves == 0)
    //        {
    //            activeElement = null;

    //            if (hit.collider.gameObject.tag == "Bridge")
    //            {
    //                if (hit.collider.gameObject.GetComponent<Bridge>().GetActiveState())
    //                {
    //                    hit.collider.gameObject.GetComponent<Bridge>().SetActiveState(false);
    //                    //gameObject.GetComponent<Player>().UseCard(currentTeam, Action.Card_RemoveBridge);
    //                    EndTurn();
    //                }
    //            }
    //            return;
    //        }
    //        //Если выбрана карточка "Двойное строительство"
    //        if (action == Action.Card_DoubleBuild)
    //        {
    //            if (hit.collider.gameObject.tag == "Bridge" && !hit.collider.gameObject.GetComponent<Bridge>().GetActiveState())
    //            {
    //                numberOfBuilds -= 1;
    //                hit.collider.gameObject.GetComponent<Bridge>().SetActiveState(true);

    //                if(numberOfBuilds == 0)
    //                {
    //                    //gameObject.GetComponent<Player>().UseCard(currentTeam, Action.Card_DoubleBuild);
    //                    EndTurn();
    //                }
    //            }
    //            return;
    //        }

    //        if(action == Action.Card_Parachute)
    //        {
    //            //gameObject.GetComponent<Player>().UseCard(currentTeam, Action.Card_Parachute);
    //            EndTurn();
    //            return;
    //        }


    //        //If frog is active and lily klicked => move on this lily if she is a neighbor 
    //        if (activeElement && hit && activeElement.tag == "Player" && hit.collider.gameObject.tag == "Lily")
    //        {
    //            frogPosition = activeElement.GetComponent<FrogScript>().position;
    //            lilyPosition = hit.collider.gameObject.GetComponent<WaterLilyScript>().position;
    //            numberOfMoves -= 1;

    //            if (action == Action.Card_DoubleJump)
    //            {
    //                action = numberOfMoves == 0 ? Action.Nothing : action;
    //            }

    //            if (activeElement.GetComponent<FrogScript>().inBase)
    //            {
    //                activeElement.gameObject.GetComponent<FrogScript>().Move(FrogScript.Directions.Up);
    //            }
    //            if (frogPosition.x == lilyPosition.x - 1)
    //            {
    //                activeElement.gameObject.GetComponent<FrogScript>().Move(FrogScript.Directions.Right);
    //            }
    //            if (frogPosition.x == lilyPosition.x + 1)
    //            {
    //                activeElement.gameObject.GetComponent<FrogScript>().Move(FrogScript.Directions.Left);
    //            }
    //            if (frogPosition.y == lilyPosition.y - 1)
    //            {
    //                activeElement.gameObject.GetComponent<FrogScript>().Move(FrogScript.Directions.Up);
    //            }
    //            if (frogPosition.y == lilyPosition.y + 1)
    //            {
    //                activeElement.gameObject.GetComponent<FrogScript>().Move(FrogScript.Directions.Down);
    //            }
                
    //            return;
    //        }

    //        if (hit)
    //        {
    //            //Если клик был на жабу, дать ссылку на эту жабу переменной activeElement
    //            if (hit.collider.gameObject.tag == "Player")
    //            {
    //                if (hit.collider.gameObject.GetComponent<FrogScript>().teamName == currentTeam)
    //                {
    //                    activeElement = hit.collider.gameObject;
    //                    return;
    //                }
    //            }
    //            //If click on bridge, revert his state
    //            if (hit.collider.gameObject.tag == "Bridge")
    //            {
    //                //Если выбрана жаба, деактивировать выбор
    //                if (activeElement != null)
    //                {
    //                    activeElement = null;
    //                }
    //                else
    //                {
    //                    if (hit.collider.gameObject.GetComponent<Bridge>().GetActiveState() == false)
    //                    {
    //                        hit.collider.gameObject.GetComponent<Bridge>().SetActiveState(true);
    //                        EndTurn();
    //                    }
    //                }
    //                return;
    //            }

    //            if (hit.collider.gameObject.tag == "HalfLily")
    //            {
    //                hit.collider.gameObject.GetComponent<HalfLilyScript>().CompleteThePatch(activeElement);
    //                activeElement = null;
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            clickedOnEmpty = true;
    //            Debug.Log("Clicked on empty");
    //            SetAction(Action.Nothing);
    //        }
    //    }
    //}

    //public void SetCurrentTeam(int team)
    //{
    //    if (team == 10)
    //    {
    //        team = Random.Range(1, 3);
    //    }

    //    currentTeam = (Teams.TeamName)team;

    //    if((int)currentTeam == 1)
    //    {
    //        staticUi.ShowLowerFrog();
    //    }
    //    if((int)currentTeam == 2)
    //    {
    //        staticUi.ShowUpperFrog();
    //    }
    //}

    //public void SetAction(Action newAction)
    //{
    //    if (canSetAction)
    //    {
    //        if (newAction == Action.Card_RemoveBridge)
    //        {
    //            numberOfMoves = 1;
    //            numberOfBuilds = 0;
    //        }
    //        if (newAction == Action.Card_DoubleBuild)
    //        {
    //            numberOfMoves = 0;
    //            numberOfBuilds = 2;
    //        }
    //        if (newAction == Action.Card_DoubleJump)
    //        {
    //            numberOfMoves = 2;
    //            numberOfBuilds = 0;
    //        }
    //        if (newAction == Action.Card_Parachute)
    //        {
    //            numberOfMoves = 1;
    //            numberOfBuilds = 0;
    //        }
    //        activeElement = null;
    //        action = newAction;
    //    }
    //}

    //public void SetAction(int newAction)
    //{
    //    //action = (Actions)newAction;
    //    SetAction((Action)newAction);
    //}

    //public static void EndTurn()
    //{
    //    //Сбрасываем до стандартных значений
    //    numberOfMoves = 1;
    //    numberOfBuilds = 1;
    //    canSetAction = true;
    //    action = Action.Nothing;
    //    //Меняем игрока
    //    currentTeam = currentTeam == Teams.TeamName.Red_team ? Teams.TeamName.Green_team : Teams.TeamName.Red_team;

    //    //Отображаем аватар игрока который ходит
    //    if (currentTeam == Teams.TeamName.Red_team)
    //    {
    //        staticUi.ShowUpperFrog();
    //    }
    //    if (currentTeam == Teams.TeamName.Green_team)
    //    {
    //        staticUi.ShowLowerFrog();
    //    }

    //    Debug.Log($"Turn ended {currentTeam}");
    //    //Сбрасываем активный елемент
    //    activeElement = null;

    //    //Player.SetCardsOnPanel();
    //}

    //public static void SetCards(Player player)
    //{

    //}

    //public static void EndGame(Teams.TeamName teamName)
    //{
    //    staticUi.OpenEndGameCanvas();
    //    Debug.Log($"Победа за {teamName}");
    //}

    ////Передвинуть gameObject на targetPosition за time времени
    //public static IEnumerator DoMove(GameObject gameObject, float time, Vector2 targetPosition)
    //{
    //    Vector2 startPosition = gameObject.transform.position;
    //    float startTime = Time.realtimeSinceStartup;
    //    float fraction = 0f; while (fraction < 1f)
    //    {
    //        fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
    //        gameObject.transform.position = Vector2.Lerp(startPosition, targetPosition, fraction);
    //        yield return null;
    //    }
    //}

    ////Повернуть gameoOject к angle градусам за duration времени 
    //public static IEnumerator Rotate(GameObject gameObject, float duration, float angle)
    //{
    //    float startRotation = gameObject.transform.eulerAngles.z;
    //    float endRotation = angle;
    //    float t = 0.0f;
    //    while (t < duration)
    //    {
    //        t += Time.deltaTime;
    //        float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360f;
    //        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y,
    //        zRotation);
    //        yield return null;
    //    }
    //}
}