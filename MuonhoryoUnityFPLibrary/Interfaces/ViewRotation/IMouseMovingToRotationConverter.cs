

using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public interface IMouseMovingToRotationConverter
    {
        Vector2 GetRotation(Vector2 mouseMoving);
    }
}
