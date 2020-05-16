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

    void BuildBridge()
    {
        bridge = Instantiate(bridgePrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, -5), Quaternion.Euler(0, 0, 0));
        bridge.transform.SetParent(gameObject.transform);
    }

    public void SetBridgeState(bool state)
    {
        bridge.GetComponent<BridgeScript>().isActive = state;
    }

    public bool GetBridgeState()
    {
        return bridge.GetComponent<BridgeScript>().isActive;
    }

}
