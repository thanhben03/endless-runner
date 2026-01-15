using System.Collections.Generic;
using UnityEngine;

public class SegmentPool : MonoBehaviour
{
    public static SegmentPool Instance;

    public GameObject[] segmentPrefabs;
    public int poolSize = 6;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        InitPool();
    }

    void InitPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject seg = Instantiate(
                segmentPrefabs[i % segmentPrefabs.Length],
                transform
            );

            seg.SetActive(false);
            pool.Enqueue(seg);
        }
    }

    public GameObject GetSegment()
    {
        GameObject seg = pool.Dequeue();
        seg.SetActive(true);
        pool.Enqueue(seg);
        return seg;
    }
}
