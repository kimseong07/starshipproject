using UnityEngine;

public interface IDamageable
{
    void OnDamage(float damamge, Vector3 hitPosition, Vector3 hitNormal);
}
