using MuonhoryoLibrary.Unity.COM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    /// <summary>
    /// Doesn't allow to camera rotate too higher and too lower by vertical axis.
    /// </summary>
    public class HumanViewRotationLimiter :MonoBehaviour, IRotationLimiter
    {
        //private HumanViewRotationLimiter() { }
        //public HumanViewRotationLimiter(float BottomLimit, float TopLimit)
        //{
        //    if (BottomLimit < 0 || BottomLimit > 360)
        //        throw new ArgumentOutOfRangeException("BottomLimit must be in degrees in [0;360] diapasone.");
        //    if (TopLimit < 0 || TopLimit > 360)
        //        throw new ArgumentOutOfRangeException("TopLimit must be in degrees in [0;360] diapasone.");
        //    if (TopLimit <= BottomLimit)
        //        throw new ArgumentException("TopLimit must be more than BottomLimit.");

        //    this.BottomLimit = BottomLimit;
        //    this.TopLimit = TopLimit;
        //}

        [SerializeField] private MonoBehaviour BottomLimitProvider;
        [SerializeField] private MonoBehaviour TopLimitProvider;

        private IConstProvider<float> ParsedBottomLimitProvider;
        private IConstProvider<float> ParsedTopLimitProvider;
        public float BottomLimit_ => ParsedBottomLimitProvider.GetValue();
        public float TopLimit_ => ParsedTopLimitProvider.GetValue();

        public Vector3 GetLimitedRotation(Vector3 rotation)
        {
            float newX = rotation.x;
            if (newX > BottomLimit_ && newX < TopLimit_)
            {
                newX = newX < 180 ? BottomLimit_ : TopLimit_;
            }
            return new Vector3(newX, rotation.y, rotation.z);
        }

        private void Awake()
        {
            ParsedBottomLimitProvider = BottomLimitProvider as IConstProvider<float>;
            if (ParsedBottomLimitProvider == null)
                throw new NullReferenceException("Missing BottomLimitProvider.");

            ParsedTopLimitProvider = TopLimitProvider as IConstProvider<float>;
            if (ParsedTopLimitProvider == null)
                throw new NullReferenceException("Missing TopLimitProvider.");
        }
    }
}
