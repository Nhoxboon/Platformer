using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : NhoxBehaviour
{
    [SerializeField] protected Transform holder;

    [SerializeField] protected int spawnerCount = 0;
    public int SpawnerCount { get => this.spawnerCount; }

    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPrefabs();
        this.LoadHolder();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null)
        {
            return;
        }
        this.holder = transform.Find("Holder");
        Debug.Log(transform.name + ": Load Holder", gameObject);
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0)
        {
            return;
        }

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            prefabs.Add(prefab);
        }
        this.HidePrefabs();

        Debug.Log(transform.name + ": Load Prefabs", gameObject);
    }

    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform Spawn(string prefabName, Vector3 spawnPos)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab " + prefabName + " not found");
            return null;
        }

        return this.Spawn(prefab, spawnPos);
    }

    public virtual Transform Spawn(Transform prefab, Vector3 spawnPos)
    {
        Transform newPrefab = this.GetObjectFromPool(prefab);
        newPrefab.gameObject.SetActive(true);
        newPrefab.SetParent(this.holder);

        newPrefab.position = spawnPos;
        newPrefab.localScale = prefab.localScale;

        this.spawnerCount++;
        return newPrefab;
    }

    protected virtual Transform GetObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in this.poolObjs)
        {
            if (poolObj.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        if (this.poolObjs.Contains(obj)) return;

        obj.position = Vector3.zero;
        obj.localScale = Vector3.one;

        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        this.spawnerCount--;
    }

    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in prefabs)
        {
            if (prefab.name == prefabName)
            {
                return prefab;
            }
        }

        return null;
    }

    public virtual Transform RandomPrefab()
    {
        int rand = Random.Range(0, this.prefabs.Count);
        return this.prefabs[rand];
    }

    public virtual void Hold(Transform obj)
    {
        obj.parent = this.holder;
    }
}
