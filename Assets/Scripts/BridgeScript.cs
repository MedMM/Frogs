using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private bool isActive = true;

    public Color activeStateColor;
    public Color disableStateColor;

    public Sprite[] sprites;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public void SetActiveState(bool state)
    {
        isActive = state;
    }

    public void SetActiveState()
    {
        isActive = !isActive;
    }

    public bool GetActiveState()
    {
        return isActive;
    }

    void Update()
    {
        spriteRenderer.color = isActive ? activeStateColor : disableStateColor;
    }
}
