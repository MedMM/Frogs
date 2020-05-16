using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public bool isActive = true;

    public Color activeStateColor;
    public Color disableStateColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetActiveState(bool state)
    {
        isActive = state;
    }

    public void SetActiveState()
    {
        isActive = !isActive;
    }

    void Update()
    {
        spriteRenderer.color = isActive ? activeStateColor : disableStateColor;
    }
}
