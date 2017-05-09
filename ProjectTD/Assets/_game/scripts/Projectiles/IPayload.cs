using UnityEngine;
using System.Collections;

public interface IPayload
{
    void Fire(Transform target);
    void Update();
}
