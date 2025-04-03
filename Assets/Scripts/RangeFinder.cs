using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeFinder
{
    public List<OverlayTile> GetTilesInRange(OverlayTile startingTile, int range, int jumpDistance)
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
                surroundingTile.AddRange(MapManager.Instance.GetNeighbourTiles(item, new List<OverlayTile>(), jumpDistance));
            }

            inRangeTiles.AddRange(surroundingTile);
            tileForPreviousStep = surroundingTile.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }
}
