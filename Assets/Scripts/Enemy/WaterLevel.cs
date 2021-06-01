using UnityEngine;

public sealed class WaterLevel : EnviromentalHazard
{
    [SerializeField]
    private Transform _water;
    [SerializeField]
    private float _speed;

    private bool _isActive;

    public override void Activate()
    {
        _isActive = true;
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        _water.transform.position += _speed * Time.deltaTime * Vector3.up;
    }
}