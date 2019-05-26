using UnityEngine;

public interface IUsable
{
    Transform GetPivot();
    Transform GetPoint();
    int GetLayer();
    void Perform();
}