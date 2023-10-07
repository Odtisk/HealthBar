using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    
    public event UnityAction HealthChanged;

    public float Health { get; private set; }
    public float MaxHealth => _maxHealth;
    public float NormalizedHealth => Health / _maxHealth;

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void Heal(float receivedHealth)
    {
        Health = Mathf.Clamp(Health + receivedHealth, 0, _maxHealth);
        HealthChanged?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        Heal(damage * -1);
    }
}
