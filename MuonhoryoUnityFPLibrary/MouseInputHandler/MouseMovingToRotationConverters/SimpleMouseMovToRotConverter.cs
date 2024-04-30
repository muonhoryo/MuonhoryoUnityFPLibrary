

using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public sealed class SimpleMouseMovToRotConverter :MonoBehaviour, IMouseMovingToRotationConverter
    {
        public Vector2 GetRotation(Vector2 mouseMoving)
        {
            return new Vector2
            (-mouseMoving.y, //Rotate around XAxis, vertical rotation
            mouseMoving.x); //Rotate around YAxis, horizontal rotation
        }
    }
}
