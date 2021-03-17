using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lily : MonoBehaviour
{
    [SerializeField] private GameObject bridgePrefab = null;
    [SerializeField] private Vector2Int coordinates = Vector2Int.zero;
    private Bridge verBridge = null;
    private Bridge horBridge = null;
    public bool isOccupied = false;

    private void Start()
    {
        BuildBridges();
    }

    private void BuildBridges()
    {
        if (coordinates.y < Board.rows - 1)
        {
            var bridge = Instantiate(bridgePrefab, new Vector3(transform.position.x, transform.position.y + 10f, -5), Quaternion.Euler(0, 0, 0));
            bridge.transform.SetParent(gameObject.transform);
            verBridge = bridge.GetComponent<Bridge>();
        }
        if (coordinates.x < Board.rows - 1)
        {
            var bridge = Instantiate(bridgePrefab, new Vector3(transform.position.x + 10f, transform.position.y, -5), Quaternion.Euler(0, 0, 90));
            bridge.transform.SetParent(gameObject.transform);
            horBridge = bridge.GetComponent<Bridge>();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Players.Instance.CurrentPlayer.ClickOnLily(this);
        }
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    } 

    public void SetCoordinates(Vector2Int newCoordinates)
    {
        coordinates = newCoordinates;
    }

    public void SetCoordinates(int x, int y)
    {
        SetCoordinates(new Vector2Int(x, y));
    }

    public Bridge GetVerticalBridge()
    {
        return verBridge;
    }

    public Bridge GetHorizontalBridge()
    {
        return horBridge;
    }

}
