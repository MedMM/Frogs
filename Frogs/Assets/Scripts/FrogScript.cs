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
    
    public Vector2Int position;

    public bool isSelected = false; //Жаба выбрана курсором? 
    public bool inBase = true;

    public GameObject mainLily;

    public Color defaultStateColor; //Обычный цвет жабы
    public Color selectedStateColor; //Цвет жабы когда выбрана

    //Переменные которые используются методом frogCanMove();  Поместил тут для отладки
    public bool onUp = false;
    public bool onDown = false;
    public bool onLeft = false;
    public bool onRight = false;

    void Start()
    {
        selectedStateColor = new Color(defaultStateColor.r + 0.2f, defaultStateColor.g + 0.2f, defaultStateColor.b + 0.2f);
        inBase = true;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
        GetComponent<SpriteRenderer>().color = isSelected ? selectedStateColor : defaultStateColor;
        KeyboardMoving();
    }

    void KeyboardMoving()
    {
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _Move(Directions.Up);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _Move(Directions.Down);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _Move(Directions.Left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _Move(Directions.Right);
            }
        }
    }

    public void SetSelectedState(bool state)
    {
        isSelected = state;
    }

    private void Move()
    {
        transform.position = GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().placeForFrog1.transform.position;
    }

    private void Move2()
    {
        transform.position = GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().placeForFrog2.transform.position;
    }

    public void _Move(Directions direction)
    {
        //IsFrogCanMove(mainLily.GetComponent<HalfLilyScript>().GetComponent<WaterLilyScript>().position, Directions.Down
        if (inBase)
        {
            //Можно высадиться если стоит мостик и на соседней лилии никого не стоит+
            if (mainLily.GetComponent<HalfLilyScript>().GetBridgeState() &&
                mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().GetOccupiedState() == false)
            {
                PlantFrog();
                return;
            }
            return;
        }

        //Если направление хода вверх и жаба в рамках игрового поля и стоит мостик
        if (direction == Directions.Up && position.y < GameManager.columns - 1 &&
            GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().GetVerticalBridge())
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //Если на лилии сверху уже стоит жаба
            if (GameManager.liliesArray[position.x, position.y + 1].GetComponent<WaterLilyScript>().GetOccupiedState() == true)
            {
                Debug.Log("up occupied");

                //Проверка можно ли провести цепную реакцию
                if (IsFrogCanMove(new Vector2Int(position.x, position.y + 1), Directions.Down))
                {
                    GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetVerticalBridge(false);
                    GameManager.liliesArray[position.x, position.y + 1].GetComponent<WaterLilyScript>().SetPlaceForFrog();

                    Teams.GetFrogOnPosition(new Vector2Int(position.x, position.y + 1)).GetComponent<FrogScript>().SetSelectedState(true);
                    MouseControls.activeElement = Teams.GetFrogOnPosition(new Vector2Int(position.x, position.y + 1));
                    this.SetSelectedState(false);

                    position.y += 1;
                    Move2();
                }
            }
            else
            {
                GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetVerticalBridge(false);
                position.y += 1;
                Move();
            }

        }

        if (direction == Directions.Down && position.y > 0 &&
            GameManager.liliesArray[position.x, position.y - 1].GetComponent<WaterLilyScript>().GetVerticalBridge())
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            if (GameManager.liliesArray[position.x, position.y - 1].GetComponent<WaterLilyScript>().GetOccupiedState() == true)
            {
                if (IsFrogCanMove(new Vector2Int(position.x, position.y - 1), Directions.Up))
                {
                    GameManager.liliesArray[position.x, position.y - 1].GetComponent<WaterLilyScript>().SetVerticalBridge(false);
                    GameManager.liliesArray[position.x, position.y - 1].GetComponent<WaterLilyScript>().SetPlaceForFrog();

                    Teams.GetFrogOnPosition(new Vector2Int(position.x, position.y - 1)).GetComponent<FrogScript>().SetSelectedState(true);
                    MouseControls.activeElement = Teams.GetFrogOnPosition(new Vector2Int(position.x, position.y - 1));
                    this.SetSelectedState(false);

                    position.y -= 1;
                    Move2();
                }
            }
            else
            {
                position.y -= 1;
                GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetVerticalBridge(false);
                Move();
            }

        }

        if (direction == Directions.Left && position.x > 0 &&
            GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().GetHorizontalBridge())
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            if (GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().GetOccupiedState() == true)
            {
                if (IsFrogCanMove(new Vector2Int(position.x - 1, position.y), Directions.Right))
                {
                    GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().SetHorizontalBridge(false);
                    GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().SetPlaceForFrog();

                    Teams.GetFrogOnPosition(new Vector2Int(position.x - 1, position.y)).GetComponent<FrogScript>().SetSelectedState(true);
                    MouseControls.activeElement = Teams.GetFrogOnPosition(new Vector2Int(position.x - 1, position.y));
                    this.SetSelectedState(false);

                    position.x -= 1;
                    Move2();
                }
            }
            else
            {
                GameManager.liliesArray[position.x - 1, position.y].GetComponent<WaterLilyScript>().SetHorizontalBridge(false);
                position.x -= 1;
                Move();
            }
        }

        if (direction == Directions.Right && position.x < GameManager.rows - 1 &&
            GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().GetHorizontalBridge())
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
            if (GameManager.liliesArray[position.x + 1, position.y].GetComponent<WaterLilyScript>().GetOccupiedState() == true)
            {
                if (IsFrogCanMove(new Vector2Int(position.x + 1, position.y), Directions.Left))
                {
                    GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetHorizontalBridge(false);
                    GameManager.liliesArray[position.x + 1, position.y].GetComponent<WaterLilyScript>().SetPlaceForFrog();

                    Teams.GetFrogOnPosition(new Vector2Int(position.x + 1, position.y)).GetComponent<FrogScript>().SetSelectedState(true);
                    MouseControls.activeElement = Teams.GetFrogOnPosition(new Vector2Int(position.x + 1, position.y));
                    this.SetSelectedState(false);

                    position.x += 1;
                    Move2();
                }

            }
            else
            {
                GameManager.liliesArray[position.x, position.y].GetComponent<WaterLilyScript>().SetHorizontalBridge(false);
                position.x += 1;
                Move();
            }
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
        return onUp || onDown || onLeft || onRight;
    }

    public void PlantFrog()
    {
        inBase = false;
        mainLily.GetComponent<HalfLilyScript>().SetBridgeState(false);

        position = mainLily.GetComponent<HalfLilyScript>().neighborLily.GetComponent<WaterLilyScript>().position;

        Move();
    }
}
