using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ConnectionPointType { In, Out }

public class ConnectionPoint {
    public Rect rect;
    public ConnectionPointType type;
    public Node node;
    public GUIStyle style;
    public Action<ConnectionPoint> OnClickConnectionPoint;
    public int index;

    public ConnectionPoint(Node node, int index, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint)
    {
        this.node = node;
        this.index = index;
        this.type = type;
        this.style = style;
        this.OnClickConnectionPoint = OnClickConnectionPoint;
        rect = new Rect(0, 0, 10, 20);
    }

    public void Draw()
    {
        rect.y = node.rect.y /*+ (node.rect.height * 0.5f)*/ + (rect.height * 0.5f) + rect.height * index;

        switch (type)
        {
            case ConnectionPointType.In:
                rect.x = node.rect.x - rect.width + 8f;
                break;
            case ConnectionPointType.Out:
                rect.x = node.rect.x + node.rect.width - 8;
                break;
        }

        if (GUI.Button(rect,"",style))
        {
            if (OnClickConnectionPoint != null)
            {
                OnClickConnectionPoint(this);
            }
        }
    }
}
