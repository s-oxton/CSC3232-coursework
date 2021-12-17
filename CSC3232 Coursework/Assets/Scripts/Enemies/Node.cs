using UnityEngine;

public class Node
{
    //world position of the node
    private int xPos;
    private int yPos;

    //node details
    private int h;
    private int g;
    private Node parentNode;
    private bool passable;

    #region Getters and Setters
    public Node GetParent()
    {
        return parentNode;
    }

    public void SetParent(Node newParent)
    {
        parentNode = newParent;
    }

    public bool GetPassable()
    {
        return passable;
    }

    public int[] GetPosition()
    {
        return new[] { xPos, yPos };
    }

    public int GetFCost()
    {
        return h + g;
    }

    public int GetHCost()
    {
        return h;
    }

    public int GetGCost()
    {
        return g;
    }


    public void SetGCost(int hCost)
    {
        h = hCost;
    }

    public void SetHCost(int gCost)
    {
        g = gCost;
    }
    #endregion

    public Node(bool tempPassable, int x, int y)
    {
        passable = tempPassable;
        xPos = x;
        yPos = y;
        h = 0;
        g = 0;
    }

}
