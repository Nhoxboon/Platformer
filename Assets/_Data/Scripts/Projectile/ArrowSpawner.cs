using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : Spawner
{
    private static ArrowSpawner instance;
    public static ArrowSpawner Instance => instance;

    public static string arrow = "Arrow";

    protected override void Awake()
    {
        base.Awake();
        if(ArrowSpawner.instance != null)
        {
            Debug.LogError("ONLY 1 instances of ArrowSpawner exist");
        }
        ArrowSpawner.instance = this;
    }

}
