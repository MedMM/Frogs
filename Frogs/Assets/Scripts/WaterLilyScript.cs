using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLilyScript : MonoBehaviour
{
    public GameObject bridgePrefab; //Экземпляр мостика
    public SpriteRenderer spriteRenderer;

    GameObject verBridge;
    GameObject horBridge;

    [SerializeField] private bool isOccupied = false;
    public Vector2Int position = new Vector2Int(0,0);

    //Space for frog
    public Transform placeForFrog1;
    public Transform placeForFrog2;
    public float radius;
    public float bridgeDistance = 10;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 0));

        placeForFrog1 = GetComponent<Transform>().Find("SpaceForFrog_1");
        placeForFrog2 = GetComponent<Transform>().Find("SpaceForFrog_2");

        SetPlaceForFrog();

        //spaceForFrog1.transform.localPosition = new Vector2(-0.25f, 0.25f);
        //spaceForFrog2.transform.localPosition = new Vector2(0.25f, -0.25f);

        spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 300));

        buildBridge();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SetPlaceForFrog();

        FindOccupiedState();
    }

    public void SetPlaceForFrog()
    {
        placeForFrog1.transform.localPosition = new Vector2(Random.Range(-4, 4f), Random.Range(-4f, 4f));
        placeForFrog2.transform.localPosition = new Vector2(Random.Range(-4f, 4f), Random.Range(-4f, 4f));

        if (CheckIfDotInCircle())
        {
            SetPlaceForFrog();
        }
    }

    private bool CheckIfDotInCircle()
    {
        //Функция проверяет чтоб точки для жаб на кувшинке не были слишком рядом и жабы не запрыгивали друг на друга
        //(x - x0)^2 + (y - y0)^2 <= R^2
        if (Mathf.Pow((placeForFrog2.transform.position.x - placeForFrog1.transform.position.x), 2) + Mathf.Pow((placeForFrog2.transform.position.y - placeForFrog1.transform.position.y), 2) <= radius)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void buildBridge()
    {
       
        if (position.y < GameManager.columns - 1)
        {
            verBridge = Instantiate(bridgePrefab, new Vector3(transform.position.x, transform.position.y + 10f, -5), Quaternion.Euler(0, 0, 0));
            verBridge.transform.SetParent(gameObject.transform);
        }
        if (position.x < GameManager.rows - 1)
        {
            horBridge = Instantiate(bridgePrefab, new Vector3(transform.position.x + 10f, transform.position.y, -5), Quaternion.Euler(0, 0, 90));
            horBridge.transform.SetParent(gameObject.transform);
        }
    }

    private void FindOccupiedState()
    {
        isOccupied = Teams.GetFrogOnPosition(position)? true : false;
    }

    public void SetVerticalBridge(bool state)
    {
        verBridge.GetComponent<BridgeScript>().isActive = state;
    }

    public void SetHorizontalBridge(bool state)
    {
        horBridge.GetComponent<BridgeScript>().isActive = state;
    }

    public bool GetVerticalBridge()
    {
        return verBridge.GetComponent<BridgeScript>().isActive;
    }

    public bool GetHorizontalBridge()
    {
        return horBridge.GetComponent<BridgeScript>().isActive;
    }

    public bool GetOccupiedState()
    {
        return isOccupied;
    }

}
