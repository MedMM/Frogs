using UnityEngine;

public class HalfLily : MonoBehaviour
{
    [SerializeField] private BridgeScript bridgePrefab = null;
    [SerializeField] private Frog frogPrefab = null;
    [SerializeField] private Player playerPrefab = null;
    [SerializeField] private Transform spot1 = null;
    [SerializeField] private Transform spot2 = null;
    [SerializeField] private Transform spot3 = null;

    private void Start()
    {
        BuildBridge();
        var player = Instantiate(playerPrefab, gameObject.transform);
        var frog1 = Instantiate(frogPrefab, spot1.transform.position, spot1.transform.rotation, player.transform);
        var frog2 = Instantiate(frogPrefab, spot2.transform.position, spot2.transform.rotation, player.transform);
        var frog3 = Instantiate(frogPrefab, spot3.transform.position, spot3.transform.rotation, player.transform);
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
        var bridge = Instantiate(bridgePrefab, new Vector3(transform.position.x, transform.position.y + 2.5f, -5), Quaternion.Euler(0, 0, 0));
        bridge.transform.SetParent(gameObject.transform);
    }
}
