using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _realHealthView;
    [SerializeField] private Slider _previousHealthView;
    [SerializeField] private Player _player;

    private float _previousHealthNormalized;
    private Coroutine _previousHealthJob;

    private void Start()
    {
        _previousHealthNormalized = _player.NormalizedHealth;
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHelathChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHelathChanged;
    }

    private void OnHelathChanged()
    {
        _realHealthView.value = _player.NormalizedHealth;

        if (_player.NormalizedHealth > _previousHealthNormalized)
        {
            _previousHealthNormalized = _player.NormalizedHealth;
            _previousHealthView.value = _previousHealthNormalized;
        }
        else if (_player.NormalizedHealth < _previousHealthNormalized)
        {
            if (_previousHealthJob != null)
            {
                StopCoroutine(_previousHealthJob);
                _previousHealthJob = null;
            }

            _previousHealthJob = StartCoroutine(ShowDamage());
        }
    }

    private IEnumerator ShowDamage()
    {
        var delay = new WaitForSeconds(0.7f);
        var timeStep = new WaitForSeconds(0.01f);
        float startValue = _previousHealthNormalized;
        yield return delay;

        for (float i = 0; i <= 1; i += 0.01f)
        {
            _previousHealthNormalized = Mathf.MoveTowards(startValue, _player.NormalizedHealth, i);
            _previousHealthView.value = _previousHealthNormalized;
            yield return timeStep;
        }
    }
}
