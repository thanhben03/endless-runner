using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "PowerUp",
    menuName = "Shop/PowerUp"
)]
public class PUData : ShopItemData
{
    public PowerUpType type;

    public override ItemCategory Category => ItemCategory.PowerUp;
}
