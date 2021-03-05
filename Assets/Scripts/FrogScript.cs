using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    public enum Directions
    {
        Up,
        Down,
        Left,
        Right
    }

    public Vector2Int position = new Vector2Int(100,100); //Позиция жабы на доске
    public bool isSelected = false; //Жаба активна? (Выбранна?)
    public bool inBase = true; //Жаба находится в главной лилии?
    public GameObject mainLily; //Главная лилия
    public Color defaultStateColor; //Обычный цвет жабы
    public Color selectedStateColor; //Цвет жабы когда выбрана
    public Teams.TeamName teamName; //Имя команды
    private float frogAnimationSpeed = 0.3f;
    private float frogRotationSpeed = 0.2f;


    //Переменные которые используются методом frogCanMove();  Поместил тут для отладки
    [SerializeField] private bool onUp = false;
    [SerializeField] private bool onDown = false;
    [SerializeField] private bool onLeft = false;
    [SerializeField] private bool onRight = false;

    void Start()
    {
        selectedStateColor = new Color(defaultStateColor.r + 0.3f, defaultStateColor.g + 0.3f, defaultStateColor.b + 0.3f);
        inBase = true;

    }

    void Update()
    {
        IsFrogActive();
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        GetComponent<SpriteRenderer>().color = isSelected ? selectedStateColor : defaultStateColor;
    }

    void IsFrogActive()
    {
        isSelected = GameController.activeElement == gameObject;
    }

    private void MoveToPos1()
    {
        StartCoroutine(DoMove(gameObject, frogAnimationSpeed, GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().placeForFrog1.transform.position));
        //transform.position = GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().placeForFrog1.transform.position;
    }

    private void MoveToPos2()
    {
        StartCoroutine(DoMove(gameObject, frogAnimationSpeed, GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().placeForFrog2.transform.position));
    }

    public void Move(Directions direction)
    {
        if (inBase)
        {
            //Если стоит мостик
            if (mainLily.GetComponent<HalfLilyScript>().GetBridgeState())
            {
                StartCoroutine(Rotate(0.5f, mainLily.transform.rotation.z == 0 ? 0 : 180f));

                //Если соседняя лилия занята
                if (mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().GetOccupiedState())
                {
                    //Если можно провести цепную реакцию
                    if (IsFrogCanMove(mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().position, Directions.Down) )
                    {
                        //Сломать мостик
                        mainLily.GetComponent<HalfLilyScript>().SetBridgeState(false); 

                        GameManager.liliesArray[
                            mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().position.x,
                            mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().position.y
                            ].GetComponent<WaterLilyScript>().SetPlaceForFrog(); 

                        //Сделать жабу на той лилии активной
                        GameController.activeElement = Teams.GetFrogOnPosition(mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().position);

                        inBase = false;
                        position = mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().position;
                        MoveToPos2();
                    }
                        return;
                }
                else
                {
                    inBase = false;
                    mainLily.GetComponent<HalfLilyScript>().SetBridgeState(false);
                    position = mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().position;
                    MoveToPos1();
                    //GameController.EndTurn();
                }
            }
            else
            {
                return;
            }
        }

        //Если направление хода вверх и жаба в рамках игрового поля 
        else if (direction == Directions.Up && position.y < GameManager.columns - 1)
        {
           //Если мостика нет
            if (!GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().GetVerticalBridge())
            {
                //Ничего не делать 
                return;
            }

            //transform.rotation = Quaternion.Euler(0, 0, 0);
            StartCoroutine(Rotate(frogRotationSpeed, 0));
            //Если на лилии сверху уже стоит жаба
            if (GameManager.liliesArray[position.x, position.y + 1].GetComponent<WaterLilyScript>().GetOccupiedState() == true)
            {
                //Проверка можно ли провести цепную реакцию
                if (IsFrogCanMove(new Vector2Int(position.x, position.y + 1), Directions.Down))
                {
                    GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetVerticalBridge(false);
                    GameManager.liliesArray[position.x, position.y + 1].GetComponent<WaterLilyScript>().SetPlaceForFrog();

                    GameController.activeElement = Teams.GetFrogOnPosition(new Vector2Int(position.x, position.y + 1));

                    position.y += 1;
                    MoveToPos2();
                    
                }
                return;
            }
            else
            {
                GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetVerticalBridge(false);
                position.y += 1;
                MoveToPos1();
            }

        }

        else if (direction == Directions.Down && position.y > 0)
        {
           if(!GameManager.liliesArray[position.x, position.y - 1].GetComponent<WaterLilyScript>().GetVerticalBridge())
           {
                return;
           }

            //transform.rotation = Quaternion.Euler(0, 0, 180);
            StartCoroutine(Rotate(frogRotationSpeed, 180));
            if (GameManager.liliesArray[position.x, position.y - 1].GetComponent<WaterLilyScript>().GetOccupiedState() == true)
            {
                if (IsFrogCanMove(new Vector2Int(position.x, position.y - 1), Directions.Up))
                {
                    GameManager.liliesArray[position.x, position.y - 1].GetComponent<WaterLilyScript>().SetVerticalBridge(false);
                    GameManager.liliesArray[position.x, position.y - 1].GetComponent<WaterLilyScript>().SetPlaceForFrog();

                    GameController.activeElement = Teams.GetFrogOnPosition(new Vector2Int(position.x, position.y - 1));

                    position.y -= 1;
                    MoveToPos2();
                }
                    return;
            }
            else
            {
                position.y -= 1;
                GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetVerticalBridge(false);
                MoveToPos1();
            }

        }

        else if (direction == Directions.Left && position.x > 0)
        {

            if(!GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().GetHorizontalBridge())
            {
                return;
            }

            //transform.rotation = Quaternion.Euler(0, 0, 90);
            StartCoroutine(Rotate(frogRotationSpeed, 90));
            if (GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().GetOccupiedState() == true)
            {
                if (IsFrogCanMove(new Vector2Int(position.x - 1, position.y), Directions.Right))
                {
                    GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().SetHorizontalBridge(false);
                    GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().SetPlaceForFrog();

                    GameController.activeElement = Teams.GetFrogOnPosition(new Vector2Int(position.x - 1, position.y));

                    position.x -= 1;
                    MoveToPos2();
                }
                    return;
            }
            else
            {
                GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().SetHorizontalBridge(false);
                position.x -= 1;
                MoveToPos1();
            }
        }

        else if (direction == Directions.Right && position.x < GameManager.rows - 1)
        {

            if(!GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().GetHorizontalBridge())
            {
                return;
            }


            //transform.rotation = Quaternion.Euler(0, 0, -90);
            StartCoroutine(Rotate(frogRotationSpeed, -90));
            if (GameManager.liliesArray[position.x + 1, position.y].GetComponent<WaterLilyScript>().GetOccupiedState() == true)
            {
                if (IsFrogCanMove(new Vector2Int(position.x + 1, position.y), Directions.Left))
                {
                    GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetHorizontalBridge(false);
                    GameManager.liliesArray[position.x + 1, position.y].GetComponent<WaterLilyScript>().SetPlaceForFrog();

                    GameController.activeElement = Teams.GetFrogOnPosition(new Vector2Int(position.x + 1, position.y));

                    position.x += 1;
                    MoveToPos2();
                }
                    return;

            }
            else
            {
                GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetHorizontalBridge(false);
                position.x += 1;
                MoveToPos1();
            }
        }

        if (GameController.action == GameController.Action.Nothing)
        {
            GameController.EndTurn();
            return;
        }

    }

    //Может ли жаба с координатами frogPosition упрыгнуть если прыгнуть на неё со стороны direction?
    public bool IsFrogCanMove(Vector2Int frogPosition, Directions direction)
    {
        onUp = false;
        onDown = false;
        onLeft = false;
        onRight = false;

        //Может ли жаба упрыгнуть вверх?
        //Если жаба в игровом поле и мостик вверх стоит и на неё прыгают не сверху
        if (frogPosition.y < GameManager.columns - 1 &&
            GameManager.liliesArray[frogPosition.x, frogPosition.y].GetComponent<WaterLilyScript>().GetVerticalBridge()
            && direction != Directions.Up)
        {
            //Если сверху уже стоит другая жаба, она сможет упрыгнуть? 
            if (GameManager.liliesArray[frogPosition.x, frogPosition.y + 1].GetComponent<WaterLilyScript>().GetOccupiedState())
            {
                onUp = IsFrogCanMove(frogPosition + new Vector2Int(0, 1), Directions.Down);
            }
            else
            {
                onUp = true;
            }
        }

        //может ли идти жаба вниз
        if (frogPosition.y > 0 &&
            GameManager.liliesArray[frogPosition.x, frogPosition.y - 1].GetComponent<WaterLilyScript>().GetVerticalBridge()
            && direction != Directions.Down)
        {
            if (GameManager.liliesArray[frogPosition.x, frogPosition.y - 1].GetComponent<WaterLilyScript>().GetOccupiedState())
            {
                onDown = IsFrogCanMove(frogPosition + new Vector2Int(0, -1), Directions.Up);
            }
            else
            {
                onDown = true;
            }
        }

        //Может ли идти жаба влево
        if (frogPosition.x > 0 &&
            GameManager.liliesArray[frogPosition.x - 1, frogPosition.y].GetComponent<WaterLilyScript>().GetHorizontalBridge()
            && direction != Directions.Left)
        {
            if (GameManager.liliesArray[frogPosition.x - 1, frogPosition.y].GetComponent<WaterLilyScript>().GetOccupiedState())
            {
                onLeft = IsFrogCanMove(frogPosition + new Vector2Int(-1, 0), Directions.Right);
            }
            else
            {
                onLeft = true;
            }
        }

        //Может ли идти жаба вправо
        if (frogPosition.x < GameManager.rows - 1 &&
            GameManager.liliesArray[frogPosition.x, frogPosition.y].GetComponent<WaterLilyScript>().GetHorizontalBridge()
            && direction != Directions.Right)
        {
            if (GameManager.liliesArray[frogPosition.x + 1, frogPosition.y].GetComponent<WaterLilyScript>().GetOccupiedState())
            {
                onRight = IsFrogCanMove(frogPosition + new Vector2Int(+1, 0), Directions.Left);
            }
            else
            {
                onRight = true;
            }
        }

        //Возвращает true если жаба может пойти в одно из направлений

        if(onUp || onDown || onLeft || onRight)
        {
            //Если была проведена цепная реакция
            GameController.numberOfMoves = 1;
        }

        return onUp || onDown || onLeft || onRight;
    }

    private IEnumerator DoMove(GameObject gameObject, float time, Vector2 targetPosition)
    {
        Vector2 startPosition = gameObject.transform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f; while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            gameObject.transform.position = Vector2.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }
    }

    private IEnumerator Rotate(float duration, float angle)
    {
        float startRotation = transform.eulerAngles.z;
        float endRotation =  angle;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float zRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360f;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
            zRotation);
            yield return null;
        }
    }

}
