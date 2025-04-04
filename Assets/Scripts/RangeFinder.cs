using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class RangeFinder
{
    public List<OverlayTile> GetTilesInRange(OverlayTile startingTile, int range, int jumpHeight)
    {
        var inRangeTiles = new List<OverlayTile>();

        int stepCount = 0;

        inRangeTiles.Add(startingTile);

        var tileForPreviousStep = new List<OverlayTile>();
        tileForPreviousStep.Add(startingTile);

        while (stepCount < range)
        {
            var surroundingTile = new List<OverlayTile>();

            foreach (var item in tileForPreviousStep)
            {
                surroundingTile.AddRange(MapManager.Instance.GetNeighbourTiles(item, new List<OverlayTile>(), jumpHeight));
            }

            inRangeTiles.AddRange(surroundingTile);
            tileForPreviousStep = surroundingTile.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }

    public List<OverlayTile> GetTilesInAtkRange(OverlayTile startingTile, int atkRange, int atkHeight)
    {
        var inAtkRangeTiles = new List<OverlayTile>();

        inAtkRangeTiles.AddRange(MapManager.Instance.GetNeighbourAtkTiles(startingTile, new List<OverlayTile>(), atkHeight, atkRange));
        inAtkRangeTiles.Remove(startingTile);

        return inAtkRangeTiles;
    }
}
