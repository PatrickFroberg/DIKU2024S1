using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public int Damage = -1;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<RubyController>(out var rubyController))
            rubyController.ChangeHealth(Damage);
    }
}
