using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [Header("Magnet Settings")]
    public float magnetRadius = 5f;
    public float pullSpeed = 8f;
    public LayerMask coinLayer;

    private bool magnetActive = false;
    private List<CointCollect> attractedCoins = new List<CointCollect>();

    public ParticleSystem magnetCoinEff;

    void Update()
    {
        if (!magnetActive) return;

        Collider[] hits = Physics.OverlapSphere(transform.position, magnetRadius, coinLayer);

        foreach (Collider hit in hits)
        {
            CointCollect coin = hit.GetComponent<CointCollect>();

            if (coin != null && !attractedCoins.Contains(coin))
            {
                attractedCoins.Add(coin);
            }
        }

        // 2. Kéo coin về player
        for (int i = attractedCoins.Count - 1; i >= 0; i--)
        {
            CointCollect coin = attractedCoins[i];

            if (coin == null)
            {
                attractedCoins.RemoveAt(i);
                continue;
            }

            coin.transform.position = Vector3.MoveTowards(
                coin.transform.position,
                transform.position,
                pullSpeed * Time.deltaTime
            );

            // 3. Khi coin chạm player
            if (Vector3.Distance(coin.transform.position, transform.position) < 0.5f)
            {

                attractedCoins.RemoveAt(i);
            }
        }
    }

    public void ActivateMagnet(float duration)
    {
        magnetActive = true;
        magnetCoinEff.Play();
        CancelInvoke();
        Invoke(nameof(DeactivateMagnet), duration);
    }

    void DeactivateMagnet()
    {
        magnetActive = false;
        attractedCoins.Clear();
        magnetCoinEff.Stop();
    }

    void CollectCoin(Coin coin)
    {
        // TODO: + score / + coin
        Destroy(coin.gameObject);
    }
}
