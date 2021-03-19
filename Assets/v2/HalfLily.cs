using UnityEngine;

public class HalfLily : MonoBehaviour
{
    [SerializeField] private Bridge bridgePrefab = null;
    [SerializeField] private Frog frogPrefab = null;
    [SerializeField] private Player playerPrefab = null;
    [SerializeField] private Lily neighborLily = null;
    [SerializeField] private Vector2Int coordinates = Vector2Int.zero;
    [SerializeField] private Transform spot1 = null;
    [SerializeField] private Transform spot2 = null;
    [SerializeField] private Transform spot3 = null;
    [SerializeField] private int arrivedFrogs = 0;

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
        player.SetHalfLily(this);
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

    public void EndPath(Frog frog)
    {
        switch (arrivedFrogs)
        {
            case 0:
                frog.gameObject.transform.position = spot1.position;
                break;
            case 1:
                frog.gameObject.transform.position = spot2.position;
                break;
            case 2:
                frog.gameObject.transform.position = spot3.position;
                break;

            default:
                break;
        }
    }

    public void SetNeighborLily(Lily lily)
    {
        neighborLily = lily;
    }

    public Lily GetNeighborLily()
    {
        return neighborLily;
    }

    public void SetCoordinates(Vector2Int newCoordinates)
    {
        coordinates = newCoordinates;
    }

    public Vector2Int GetCoordinates()
    {
        return coordinates;
    }
}
