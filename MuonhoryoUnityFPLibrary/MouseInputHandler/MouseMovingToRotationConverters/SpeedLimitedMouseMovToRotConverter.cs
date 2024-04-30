

using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{

    /// <summary>
    /// If SpeedLimit of any axis is 0, then by this axis rotation haven't any speed limits.
    /// </summary>
    public sealed class SpeedLimitedMouseMovToRotConverter:MonoBehaviour,IMouseMovingToRotationConverter
    {
        //private SpeedLimitedMouseMovToRotConverter() { }
        //public SpeedLimitedMouseMovToRotConverter(Vector2 SpeedLimits)
        //{
        //    if (SpeedLimits.x < 0 ||
        //        SpeedLimits.y < 0)
        //        throw new System.ArgumentException("SpeedLimits must be more or equal 0.");

        //    this.SpeedLimits = SpeedLimits;
        //}

        [SerializeField] private Vector2 SpeedLimits;

        public Vector2 SpeedLimits_ => SpeedLimits;

        public Vector2 GetRotation(Vector2 mouseMoving)
        {
            return new Vector2(
                Mathf.Clamp(-mouseMoving.y,-SpeedLimits.y,SpeedLimits.y),
                Mathf.Clamp(mouseMoving.x,-SpeedLimits.x,SpeedLimits.x));
        }
    }
}
