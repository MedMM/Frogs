using UnityEngine;
using System.Collections;

public class MouseControls : MonoBehaviour
{
    RaycastHit2D hit;
    public static GameObject activeElement;

    public Vector2 frogPosition;
    public Vector2 lilyPosition;

    void Update()
    {
        Clicker();
    }

    void Clicker()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, Mathf.Infinity);

            //If frog is active and lily klicked => move on this lily if she is a neighbor 
            if (activeElement && hit && activeElement.tag == "Player" && hit.collider.gameObject.tag == "Lily")
            {
                frogPosition = activeElement.GetComponent<FrogScript>().position;
                lilyPosition = hit.collider.gameObject.GetComponent<WaterLilyScript>().position;

                if (activeElement.GetComponent<FrogScript>().inBase)
                {
                    activeElement.GetComponent<FrogScript>()._Move(FrogScript.Directions.Up);
                    return;
                }

                if (frogPosition.x == lilyPosition.x - 1)
                {
                    activeElement.gameObject.GetComponent<FrogScript>()._Move(FrogScript.Directions.Right);
                    return;
                }

                if (frogPosition.x == lilyPosition.x +1)
                {
                    activeElement.gameObject.GetComponent<FrogScript>()._Move(FrogScript.Directions.Left);
                    return;
                }

                if (frogPosition.y == lilyPosition.y - 1)
                {
                    activeElement.gameObject.GetComponent<FrogScript>()._Move(FrogScript.Directions.Up);
                    return;
                }

                if (frogPosition.y == lilyPosition.y + 1 )
                {
                    activeElement.gameObject.GetComponent<FrogScript>()._Move(FrogScript.Directions.Down);
                    return;
                }

            }

            //Set player selection state on false after every click
            if (activeElement && activeElement.tag == "Player")
            {
                activeElement.gameObject.GetComponent<FrogScript>().SetSelectedState(false);
                activeElement = null;
            }

            if (hit)
            {
                //If click on frog, set his state on Active 
                if (hit.collider.gameObject.tag == "Player")
                {
                    activeElement = hit.collider.gameObject;
                    activeElement.gameObject.GetComponent<FrogScript>().SetSelectedState(true);
                    return;
                }
                //If click on bridge, revert his state
                if (hit.collider.gameObject.tag == "Bridge")
                {
                    activeElement = hit.collider.gameObject;
                    activeElement.gameObject.GetComponent<BridgeScript>().SetActiveState();
                    return;
                }
            }
            else
            {
                Debug.Log("Clicked on empty");
                GetComponent<GameManager>().AAA_SetAction(0);
            }

        }
    }
}