using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
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

    private void SetColor()
    {
        spriteRenderer.color = isActive ? activeStateColor : disableStateColor;
    }
}
