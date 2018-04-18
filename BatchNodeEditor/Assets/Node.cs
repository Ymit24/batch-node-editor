using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Node {
    public Rect rect;
    public string title;
    public bool isDragged;
    public bool isSelected;

    public List<ConnectionPoint> inPoints, outPoints;

    public GUIStyle style, defaultNodeStyle, selectedNodeStyle;

    public Action<Node> OnRemoveNode;

    public Node(Vector2 position, string title, string[] inputNames, string[] outputNames, float width, float height, int inPoints, int outPoints, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
    {
        this.title = title;
        rect = new Rect(position.x, position.y, width, height + 20 * (1+Math.Max(inPoints, outPoints)));
        style = nodeStyle;
        this.inPoints = new List<ConnectionPoint>();
        this.outPoints = new List<ConnectionPoint>();

        for (int i = 0; i < inPoints; i++)
        {
            this.inPoints.Add(new ConnectionPoint(this, inputNames[i], i, ConnectionPointType.In, inPointStyle, OnClickInPoint));
        }

        for (int i = 0; i < outPoints; i++)
        {
            this.outPoints.Add(new ConnectionPoint(this, outputNames[i], i, ConnectionPointType.Out, outPointStyle, OnClickOutPoint));
        }

        defaultNodeStyle = nodeStyle;
        selectedNodeStyle = selectedStyle;
        OnRemoveNode = OnClickRemoveNode;
    }

    public void Drag(Vector2 delta)
    {
        rect.position += delta;
    }

    public virtual void Draw()
    {
        GUI.Box(rect, title, style);
        foreach (ConnectionPoint cp in inPoints)
        {
            cp.Draw();
        }
        foreach (ConnectionPoint cp in outPoints)
        {
            cp.Draw();
        }
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        isSelected = true;
                        style = selectedNodeStyle;
                    }
                    else
                    {
                        isSelected = false;
                        style = defaultNodeStyle;
                    }
                    GUI.changed = true;
                }
                if (e.button == 1 && isSelected && rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;
            case EventType.MouseUp:
                isDragged = false;
                break;
            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }
        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        menu.ShowAsContext();
    }
    private void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }
}
