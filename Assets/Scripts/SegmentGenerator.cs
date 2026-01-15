using System.Collections.Generic;
using UnityEngine;

public class SegmentGenerator : MonoBehaviour
{
    public Transform player;

    public int segmentLength = 127;
    public int segmentsOnScreen = 3;

    private int zPos = 0;
    private Queue<GameObject> activeSegments = new Queue<GameObject>();
    public List<CharacterData> characters;


    void Start()
    {
        InitPlayer();
        player = GamePlayManager.Instance.GetPlayer().transform;
        for (int i = 0; i < segmentsOnScreen; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        GameObject firstSegment = activeSegments.Peek();

        if (player.position.z > firstSegment.transform.position.z + segmentLength)
        {
            SpawnSegment();
            RemoveOldSegment();
        }
    }

    void SpawnSegment()
    {
        GameObject seg = SegmentPool.Instance.GetSegment();
        seg.transform.position = new Vector3(0, 0, zPos);

        activeSegments.Enqueue(seg);
        zPos += segmentLength;
    }

    void RemoveOldSegment()
    {
        GameObject oldSeg = activeSegments.Dequeue();
        oldSeg.SetActive(false);
    }

    public void InitPlayer()
    {
        int characterId = PlayerPrefs.GetInt("CHARACTER_ID", 1);
        GameObject prefab = characters.Find(x => x.id == characterId.ToString()).prefab;
        Instantiate(prefab);
    }
}
