using UnityEngine;

public abstract class EnviromentalHazard : MonoBehaviour, IEnviromentalHazard
{
    public abstract void Activate();
    public abstract void Deactivate();
}
