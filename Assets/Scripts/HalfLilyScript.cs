using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfLilyScript : MonoBehaviour
{
    public GameObject bridgePrefab;
    public GameObject frogPrefab;
    public GameObject neighborLily;
    public GameObject bridge;

    public Transform spot1;
    public Transform spot2;
    public Transform spot3;

    int numberOfArrivals = 0; //Количество пришедших жаб к финишу

    public Color teamColor;

    private void Start()
    {
        GameObject frog1 = Instantiate(frogPrefab, spot1.transform.position, spot1.rotation);
        frog1.GetComponent<FrogScript>().mainLily = gameObject;
        frog1.GetComponent<FrogScript>().defaultStateColor = teamColor;

        GameObject frog2 = Instantiate(frogPrefab, spot2.transform.position, spot2.rotation);
        frog2.GetComponent<FrogScript>().mainLily = gameObject;
        frog2.GetComponent<FrogScript>().defaultStateColor = teamColor;

        GameObject frog3 = Instantiate(frogPrefab, spot3.transform.position, spot3.rotation);
        frog3.GetComponent<FrogScript>().mainLily = gameObject;
        frog3.GetComponent<FrogScript>().defaultStateColor = teamColor;

        BuildBridge();
    }

    private void BuildBridge()
    {
        bridge = Instantiate(bridgePrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, -5), Quaternion.Euler(0, 0, 0));
        bridge.transform.SetParent(gameObject.transform);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }
    }

    public void SetBridgeState(bool state)
    {
        bridge.GetComponent<BridgeScript>().SetActiveState(state);
    }

    public bool GetBridgeState()
    {
        return bridge.GetComponent<BridgeScript>().GetActiveState();
    }

    public void CompleteThePatch(GameObject frog)
    {
        if(frog == null)
        {
            Debug.Log("Frog unselected");
        }
        else
        {
            if(frog.GetComponent<FrogScript>().position == neighborLily.GetComponent<WaterLilyScript>().position && GetBridgeState())
            {
                numberOfArrivals += 1;
                if(numberOfArrivals == 1)
                {
                    frog.transform.position = spot1.position;
                }
                if(numberOfArrivals == 2)
                {
                    frog.transform.position = spot2.position;
                }
                if(numberOfArrivals == 3)
                {
                    frog.transform.position = spot3.position;
                }
                SetBridgeState(false);

                frog.GetComponent<BoxCollider2D>().enabled = false;
                frog.transform.rotation = Quaternion.Euler(0, 0, 180);
                frog.GetComponent<FrogScript>().position = new Vector2Int(20, 20);

                Debug.Log($"Frog {frog} finished the game!");
                GameController.EndTurn();
            }
            else
            {
                Debug.Log("Сначала доберись");
            }
        }
    }
}
