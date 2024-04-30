

using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public interface IMovingDirectionCalculator
    {

        /// <summary>
        /// YAxis is towards direction, XAxis is side direction.
        /// </summary>
        /// <param name="inputMovDir"></param>
        /// <returns></returns>
        Vector3 GetDirection(Vector2 inputMovDir);
    }
}
