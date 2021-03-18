using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToFollow = null;

    private void Update()
    {
        if (gameObjectToFollow != null)
        {
            transform.position = gameObjectToFollow.transform.position;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetGameObjectToFollow(GameObject newGameObject)
    {
        gameObjectToFollow = newGameObject;
    }

}
