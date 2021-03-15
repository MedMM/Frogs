using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isActive = true;

    [SerializeField] private Color activeStateColor;
    [SerializeField] private Color disableStateColor;
    [SerializeField] private Sprite[] sprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    private void SetColor()
    {
        spriteRenderer.color = isActive ? activeStateColor : disableStateColor;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Players.Instance.CurrentPlayer.ClickOnBridge(this);
        }
    }

    public void SetActiveState(bool state)
    {
        isActive = state;
        SetColor();
    }

    public void SetActiveState()
    {
        SetActiveState(isActive);
    }

    public bool GetActiveState()
    {
        return isActive;
    }
}
