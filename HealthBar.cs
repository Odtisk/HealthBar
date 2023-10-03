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

    private void Update()
    {
        _realHealthView.value = _player.NormalizedHealth;

        if (_player.NormalizedHealth < _previousHealthNormalized && _previousHealthJob == null)
        {
            _previousHealthJob = StartCoroutine(ShowDamage());
        }
        else if (_player.NormalizedHealth > _previousHealthNormalized)
        {
            _previousHealthNormalized = _player.NormalizedHealth;
            _previousHealthView.value = _previousHealthNormalized;
        }
        else if (_player.NormalizedHealth == _previousHealthNormalized && _previousHealthJob != null)
        {
            StopCoroutine(_previousHealthJob);
            _previousHealthJob = null;
        }
    }

    private IEnumerator ShowDamage()
    {
        var delay = new WaitForSeconds(0.7f);
        var timeStep = new WaitForSeconds(0.01f);
        yield return delay;

        for (float i = 0; i <= 1; i += 0.01f)
        {
            _previousHealthView.value = Mathf.MoveTowards(_previousHealthNormalized, _player.NormalizedHealth, i);
            yield return timeStep;
        }

        _previousHealthNormalized = _player.NormalizedHealth;
    }
}
