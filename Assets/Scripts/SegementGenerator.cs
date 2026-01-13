using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegementGenerator : MonoBehaviour
{
    public GameObject[] segment;
    [SerializeField] int zPos = 380;
    [SerializeField] bool creatingSegment = false;
    [SerializeField] int segmentNum = 0;

    // Unity Message | 0 references
    void Update()
    {
        if (creatingSegment == false)
        {
            creatingSegment = true;
            StartCoroutine(SegmentGen());
        }
    }

    // 1 reference
    IEnumerator SegmentGen()
    {
        Instantiate(segment[segmentNum], new Vector3(0, 0, zPos), Quaternion.identity);
        segmentNum++;
        if (segmentNum == segment.Length)
        {
            segmentNum = 0;
        }
        zPos += 107;
        yield return new WaitForSeconds(3);
        creatingSegment = false;
    }
}
