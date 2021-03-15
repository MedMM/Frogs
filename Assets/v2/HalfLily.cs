using UnityEngine;

public class HalfLily : MonoBehaviour
{
    [SerializeField] private Bridge bridgePrefab = null;
    [SerializeField] private Frog frogPrefab = null;
    [SerializeField] private Lily neighborLily = null;
    [SerializeField] private Player playerPrefab = null;
    [SerializeField] private Transform spot1 = null;
    [SerializeField] private Transform spot2 = null;
    [SerializeField] private Transform spot3 = null;

    private void Start()
    {
        BuildBridge();
        var player = Instantiate(playerPrefab);
        var frog1 = Instantiate(frogPrefab, spot1.transform.position, spot1.transform.rotation);
        var frog2 = Instantiate(frogPrefab, spot2.transform.position, spot2.transform.rotation);
        var frog3 = Instantiate(frogPrefab, spot3.transform.position, spot3.transform.rotation);
        player.RecordFrogOnTeam(frog1);
        player.RecordFrogOnTeam(frog2);
        player.RecordFrogOnTeam(frog3);
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Half lily clicked");
        }
    }

    private void BuildBridge()
    {
        Vector3 bridgePosition = new Vector3(0, 1.25f, -5);
        var bridge = Instantiate(bridgePrefab, bridgePosition, Quaternion.Euler(0, 0, 0));
        bridge.transform.SetParent(gameObject.transform);
        bridge.transform.localPosition = bridgePosition;
    }

    public void SetNeighborLily(Lily lily)
    {
        neighborLily = lily;
    }

    public Lily GetNeighborLily()
    {
        return neighborLily;
    }
}
