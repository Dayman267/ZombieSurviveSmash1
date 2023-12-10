using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSo : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        PlayerHealthDD health = character.GetComponent<PlayerHealthDD>();
        if(health != null)
            health.RestoreHealth(val);
    }
}
