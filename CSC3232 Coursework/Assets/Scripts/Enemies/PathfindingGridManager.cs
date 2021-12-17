using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class PathfindingGridManager : MonoBehaviour
{

    [SerializeField]
    private SpinningBladeMovement obstactleMovement;
    [SerializeField]
    private LayerMask obstacleMask;

    [SerializeField]
    private Tilemap tilemap;
    private BoundsInt tilemapBounds;
    private Node[,] nodeGrid;

    public Node[,] GetGrid()
    {
        return nodeGrid;
    }

    // Start is called before the first frame update
    void Start()
    {
        //bound the tilemap to only be where there are tiles.
        tilemap.CompressBounds();
        //get the size of the tilemap
        tilemapBounds = tilemap.cellBounds;
        //create a grid of nodes for pathfinding
        CreateGrid();
    }

    //checks whether a position is within bounds.
    //if position is in bounds, return true.
    public bool CheckBounds(Vector3 position)
    {
        //if position is out of bounds
        if (position.x < tilemapBounds.xMin || position.x > tilemapBounds.xMax || position.y < tilemapBounds.yMin || position.y > tilemapBounds.yMax)
        {
            return false;
        }
        return true;
    }

    private void FixedUpdate()
    {
        //if any obstacles are moving
        if (obstactleMovement.GetBladeMoving())
        {
            //update the grid 
            CreateGrid();
        }
    }

    //creates a grid of nodes for pathfinding
    private void CreateGrid()
    {
        //sets the size of the node grid to be equal to the tilemap size.
        nodeGrid = new Node[tilemapBounds.size.x, tilemapBounds.size.y];

        //loops through the tilemap and initialises the nodes.
        for (int x = tilemapBounds.xMin, i = 0; i < tilemapBounds.size.x; x++, i++)
        {
            for (int y = tilemapBounds.yMin, j = 0; j < tilemapBounds.size.y; y++, j++)
            {
                //if node or node below has a tile, OR spinning blade is overlapping with node, node is impassable
                if (tilemap.HasTile(new Vector3Int(x, y, 0)) || CheckTileBelow(x, y) || CheckOverlappingWithObstactle(x, y))
                {
                    nodeGrid[i, j] = new Node(false, x, y);
                }
                else
                {
                    nodeGrid[i, j] = new Node(true, x, y);
                }
            }
        }
    }

    //checks if the tilemap square is overlapping with an obstactle
    private bool CheckOverlappingWithObstactle(int x, int y)
    {
        //get the position of the tile
        Vector2 boxPosition = new Vector2(x + 0.5f, y + 0.5f);
        //check if it overlaps with the obstacle
        Collider2D obstacle = Physics2D.OverlapBox(boxPosition, new Vector2(0.5f, 0.5f), 0, obstacleMask);
        if (obstacle != null)
        {
            return true;
        }
        return false;
    }

    //check node below the tile
    private bool CheckTileBelow(int x, int y)
    {
        //ensure it is within bounds
        if (y - 1 >= tilemapBounds.yMin)
        {
            //if it has a tile, original node is impassable too.
            if (tilemap.HasTile(new Vector3Int(x, y - 1, 0)))
            {
                return true;
            }
        }
        return false;
    }

    //returns a list of all the neighbours of a node
    public List<Node> GetNeighbours(Node node)
    {
        //initialise a list of nodes
        List<Node> neighbours = new List<Node>();

        //gets the position of the node
        int[] positions = node.GetPosition();
        //converts the coordinate into an array positions
        int[] arrayPositions = new int[] { positions[0] - tilemapBounds.xMin, positions[1] - tilemapBounds.yMin };

        //gets all nodes adjectent (Note: Not diagonal) to the current node.

        //if tile to the right in bounds
        if (arrayPositions[0] + 1 <= nodeGrid.GetUpperBound(0))
        {
            //if the node is passable, add it
            if (nodeGrid[arrayPositions[0] + 1, arrayPositions[1]].GetPassable())
            {
                neighbours.Add(nodeGrid[arrayPositions[0] + 1, arrayPositions[1]]);
            }
        }
        //if tile to the left in bounds
        if (arrayPositions[0] - 1 >= 0)
        {
            //if the node is passable, add it
            if (nodeGrid[arrayPositions[0] - 1, arrayPositions[1]].GetPassable())
            {
                neighbours.Add(nodeGrid[arrayPositions[0] - 1, arrayPositions[1]]);
            }
        }
        //if tile above in bounds
        if (arrayPositions[1] + 1 <= nodeGrid.GetUpperBound(1))
        {
            //if the node is passable, add it
            if (nodeGrid[arrayPositions[0], arrayPositions[1] + 1].GetPassable())
            {
                neighbours.Add(nodeGrid[arrayPositions[0], arrayPositions[1] + 1]);
            }
        }
        //if tile below in bounds
        if (arrayPositions[1] - 1 >= 0)
        {
            //if the node is passable, add it
            if (nodeGrid[arrayPositions[0], arrayPositions[1] - 1].GetPassable())
            {
                neighbours.Add(nodeGrid[arrayPositions[0], arrayPositions[1] - 1]);
            }
        }

        return neighbours;
    }

    //finds the node that a position vector is currently in
    public Node GetNodeFromPosition(Vector3 position)
    {
        Node currentNode;

        //convert vector position into tilemap cell
        Vector3Int gridPosition = tilemap.WorldToCell(position);

        //uses tilemap cell to setup the node
        currentNode = nodeGrid[gridPosition.x - tilemapBounds.xMin, gridPosition.y - tilemapBounds.yMin];

        return currentNode;
    }

}
