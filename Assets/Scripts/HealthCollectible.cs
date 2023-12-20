using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int HealthBonus = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<RubyController>(out var rubyController))
            return;
        
        if (rubyController.Health < rubyController.MaxHealth)
        {
            rubyController.ChangeHealth(HealthBonus);
            Destroy(gameObject);
        }
    }
}
