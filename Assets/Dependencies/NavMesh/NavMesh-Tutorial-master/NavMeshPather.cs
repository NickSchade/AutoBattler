using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshPather
{
    public static int IndexFromMask(int mask)
    {
        for (int i = 0; i < 32; ++i)
        {
            if ((1 << i & mask) != 0)
            {
                return i;
            }
        }
        return -1;
    }

    public static float Cost(NavMeshPath path)
    {
        if (path.corners.Length < 2) return 0;

        float cost = 0;
        NavMeshHit hit;
        NavMesh.SamplePosition(path.corners[0], out hit, 0.1f, NavMesh.AllAreas);
        Vector3 rayStart = path.corners[0];
        int mask = hit.mask;
        int index = IndexFromMask(mask);

        for (int i = 1; i < path.corners.Length; ++i)
        {

            while (true)
            {
                NavMesh.Raycast(rayStart, path.corners[i], out hit, mask);

                cost += NavMesh.GetAreaCost(index) * hit.distance;

                if (hit.mask != 0) mask = hit.mask;

                index = IndexFromMask(mask);
                rayStart = hit.position;

                if (hit.mask == 0)
                { //hit boundary; move startPoint of ray a bit closer to endpoint
                    rayStart += (path.corners[i] - rayStart).normalized * 0.01f;
                }

                if (!hit.hit) break;
            }
        }

        return cost;
    }
}
