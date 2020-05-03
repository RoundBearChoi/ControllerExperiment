using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public static class CostlyFunction
{
    public static void Costly()
    {
        float value = 0f;

        for (int i = 0; i < 5000; i++)
        {
            math.exp10(math.sqrt(value));
        }
    }
}