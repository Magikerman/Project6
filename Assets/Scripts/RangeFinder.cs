using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeFinder : MonoBehaviour
{
    public List<OverlayTile> GetTilesInRange(OverlayTile startingTile, int range)
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
                surroundingTile.AddRange(MapManager.Instance.GetNeighbourTiles(item, new List<OverlayTile>()));
            }

            inRangeTiles.AddRange(surroundingTile);
            tileForPreviousStep = surroundingTile.Distinct().ToList();
            stepCount++;
        }

        return inRangeTiles.Distinct().ToList();
    }
}
