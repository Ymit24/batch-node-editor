using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ValueNode : Node {
    string value;
    public ValueNode(Vector2 position, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
        : base(position, "Value", new string[] { "exec", }, new string[] { "exec" }, 100, 0, 1, 1, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
    }

    public override void Draw()
    {
        base.Draw();

        this.value = GUI.TextField(new Rect(rect.position + new Vector2(10, 10), new Vector2(rect.size.x, 20)), value);
    }
}
