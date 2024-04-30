

using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public interface IRotationLimiter
    {
        Vector3 GetLimitedRotation(Vector3 input);
    }
}
