using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneBuilder))]
[CanEditMultipleObjects]
public class SceneBuilder : Editor
{
    [SerializeField] private Vector2 lilyPosition;


}
