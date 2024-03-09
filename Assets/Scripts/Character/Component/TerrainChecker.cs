using UnityEngine;

public class TerrainChecker
{

    private static float[] GetTextureMix(Vector3 characharacterPosition, Terrain terrain)
    {
        var terrainPosition = terrain.transform.position;
        TerrainData terrainData = terrain.terrainData;

        int mapX = Mathf.RoundToInt((characharacterPosition.x - terrainPosition.x) / terrainData.size.x * terrainData.alphamapWidth);
        int mapZ = Mathf.RoundToInt((characharacterPosition.z - terrainPosition.z) / terrainData.size.z * terrainData.alphamapHeight);
        float[,,] splatMapData = terrainData.GetAlphamaps(mapX, mapZ, 1, 1);

        float[] cellmix = new float[splatMapData.GetUpperBound(2) + 1];
        for (int i = 0; i < cellmix.Length; i++)
        {
            cellmix[i] = splatMapData[0, 0, i];
        }
        return cellmix;
    }

    public static string GetLayerName(Vector3 characharacterPosition, Terrain terrain)
    {
        float[] cellmix = GetTextureMix(characharacterPosition, terrain);
        float strongest = 0;
        int maxIndex = 0;

        for (int i = 0; i < cellmix.Length; i++)
        {
            if (cellmix[i] > strongest)
            {
                maxIndex = i;
                strongest = cellmix[i];
            }
        }
        return terrain.terrainData.terrainLayers[maxIndex].name;
    }
}
