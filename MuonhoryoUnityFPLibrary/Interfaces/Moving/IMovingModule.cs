


using System;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public interface IMovingModule
    {
        event Action<Vector3> StartMovingEvent;
        event Action<Vector2> ChangeMovingDirectionEvent;
        event Action StopMovingEvent;

        bool IsMoving_ { get; }
        Vector2 InputMovingDirection_ { get; }
        Vector3 RealMovingDirection_ { get; }
        Vector3 CurrentObjectPosition_ { get; }

        /// <summary>
        /// Y of input vector - moving towards.
        /// X of input vector - moving to the sides.
        /// </summary>
        /// <param name="inputDirection"></param>
        void SetMovingDirection(Vector2 inputDirection);
        void StopMoving();
    }
    public static class MovingModule
    {
        public static void SetMovingDirection(IMovingModule module, Vector2 inputDir,
            Action<Vector2> startMovingAction, Action<Vector2> setMovingDirAction)
        {
            if (inputDir != Vector2.zero)
            {
                if (module.IsMoving_)
                {
                    setMovingDirAction(inputDir);
                }
                else
                {
                    startMovingAction(inputDir);
                }
            }
        }
        public static void StopMoving(IMovingModule module, Action stopMovingAction)
        {
            if (module.IsMoving_)
            {
                stopMovingAction();
            }
        }
    }
}
