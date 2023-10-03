using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;

    public float Health { get; private set; }
    public float MaxHealth => _maxHealth;
    public float NormalizedHealth => Health / _maxHealth;

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void Heal(float receivedHealth)
    {
        if (Health + receivedHealth < _maxHealth)
            Health += receivedHealth;
        else
            Health = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (Health > damage)
            Health -= damage;
        else
            Health = 0;
    }
}
