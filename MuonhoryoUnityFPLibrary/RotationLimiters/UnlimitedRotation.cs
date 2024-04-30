using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    /// <summary>
    /// Return input rotation.
    /// </summary>
    public sealed class UnlimitedRotation :MonoBehaviour,IRotationLimiter
    {
        public Vector3 GetLimitedRotation(Vector3 rotation) => rotation;
    }
}
