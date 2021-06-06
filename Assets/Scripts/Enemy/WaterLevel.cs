using UnityEngine;

public sealed class WaterLevel : EnviromentalHazard
{
    [SerializeField]
    private Transform _water;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _acceleration;

    private bool _isActive;
    private float _time;

    public override void Activate()
    {
        Activate(true);
    }

    public override void Deactivate()
    {
        Activate(false);
    }

    private void Activate(bool isActive)
    {
        _isActive = isActive;
        if (_isActive)
        {
            _time = 0;
        }
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        _time += Time.deltaTime;
        _water.transform.localPosition = _time * (_speed + _acceleration * _time) * Vector3.up;
    }
}