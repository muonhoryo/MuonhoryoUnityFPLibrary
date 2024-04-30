using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public sealed class FlatGroundMovDirModule : MonoBehaviour, IMovingDirectionCalculator
    {
        [SerializeField] private MonoBehaviour ViewModule;

        private ICameraViewRotationModule ParsedViewModule;

        private void Awake()
        {
            ParsedViewModule = ViewModule as ICameraViewRotationModule;
            if (ParsedViewModule == null)
                throw new NullReferenceException("Missing ViewModule.");
        }
        public Vector3 GetDirection(Vector2 Input)
        {
            if (ViewModule == null)
                throw new NullReferenceException("Missing ViewModule.");
            Input.y = Mathf.Floor(Input.y); //Toward axis
            Input.x = -Mathf.Floor(Input.x); //Side axis
            Vector2 horViewDir = ParsedViewModule.CurrentHorizontalViewDirection_;
            Vector2 perpHorViewDir = Vector2.Perpendicular(horViewDir) * Input.x;
            horViewDir *= Input.y;
            Vector3 viewDir = horViewDir.ConvertHorDirToGlobalDir();
            Vector3 perpDir = perpHorViewDir.ConvertHorDirToGlobalDir();

            return (viewDir + perpDir).normalized;
        }
    }
}
