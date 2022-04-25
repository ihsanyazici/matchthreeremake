using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private List<Drop> pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = new List<Drop>();
    }

    public void AddToPool(Drop drop)
    {
        pool.Add(drop);
        drop.transform.parent = transform;
    }
    public void RemoveFromPool(Drop drop)
    {
        pool.Remove(drop);
    }
}
