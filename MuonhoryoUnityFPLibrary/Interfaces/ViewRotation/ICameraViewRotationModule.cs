using System;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public interface ICameraViewRotationModule
    {
        /// <summary>
        /// First argument - rotation's value, second - rotation around y
        /// </summary>
        event Action<Vector3, float> ChangeViewRotationEvent;
        /// <summary>
        /// First argument - perspective direction, second - horizontal direction
        /// </summary>
        event Action<Vector3, Vector2> ChangeViewDirectionEvent;
        Vector3 CurrentViewDirection_ { get; }
        Vector2 CurrentHorizontalViewDirection_ { get; }
        void Rotate(Vector3 rotation);
        void SetRotation(Vector3 rotation);
        void SetXRotation(float xRotation);
        void SetYRotation(float yRotation);
        void SetZRotation(float zRotation);
    }
    public static class CameraViewRotatorModule
    {
        public static void Rotate(this ICameraViewRotationModule module, Vector2 rotation) =>
            module.Rotate(new Vector3(rotation.x, rotation.y, 0));

        public static void SetRotation(this ICameraViewRotationModule module, Vector2 rotation) =>
            module.SetRotation(new Vector3(rotation.x, rotation.y, 0));
    }
}
