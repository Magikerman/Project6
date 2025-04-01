using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder
{
    public List<OverlayTile> FindPath(OverlayTile start, OverlayTile end)
    { 
        List<OverlayTile> openList = new List<OverlayTile>();
        List<OverlayTile> closedList = new List<OverlayTile>();

        openList.Add(start);

        while (openList.Count > 0)
        {
            OverlayTile currentOverlayTile = openList.OrderBy(x => x.F).First();

            openList.Remove(currentOverlayTile);
            closedList.Add(currentOverlayTile);

            if (currentOverlayTile == end)
            {
                return GetFinishedList(start, end);
            }

            var neighbourTiles = GetNeighbourTiles(currentOverlayTile);

            foreach (var neighbour in neighbourTiles)
            {
                //cambiar 1 por "CharacterInfo.JumpHeight"
                if(neighbour.IsBlocked || closedList.Contains(neighbour) || Mathf.Abs(currentOverlayTile.GridPos.z - neighbour.GridPos.z) > 1)
                {
                    continue;
                }

                neighbour.G = GetManhattenDistance(start, neighbour);
                neighbour.H = GetManhattenDistance(end, neighbour);

                neighbour._previousTile = currentOverlayTile;

                if(!openList.Contains(neighbour))
                {
                    openList.Add(neighbour);
                }
            }
        }
        return new List<OverlayTile>();
    }

    private List<OverlayTile> GetFinishedList(OverlayTile start, OverlayTile end)
    {
        List<OverlayTile> finishedList = new List<OverlayTile>();

        OverlayTile currentTile = end;
        while (currentTile != start)
        {
            finishedList.Add(currentTile);
            currentTile = currentTile._previousTile;
        }

        finishedList.Reverse();

        return finishedList;
    }

    private int GetManhattenDistance(OverlayTile start, OverlayTile neighbour)
    {
        return Mathf.Abs(start.GridPos.x - neighbour.GridPos.x) + Mathf.Abs(start.GridPos.y - neighbour.GridPos.y);
    }

    private List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile)
    {
        var map = MapManager.Instance.map;

        List<OverlayTile> neighbours = new List<OverlayTile>();

        //arriba
        Vector2Int locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x, currentOverlayTile.GridPos.y + 1);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //abajo
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x, currentOverlayTile.GridPos.y - 1);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //derecha
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x + 1, currentOverlayTile.GridPos.y);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        //izquierda
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x - 1, currentOverlayTile.GridPos.y);

        if (map.ContainsKey(locationToCheck))
        {
            neighbours.Add(map[locationToCheck]);
        }

        return neighbours;
    }
}
