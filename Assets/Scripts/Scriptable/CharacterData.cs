using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Data")]
public class CharacterData : ShopItemData
{
    public CharacterType type;


    public override ItemCategory Category => ItemCategory.Character;
}
