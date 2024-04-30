using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public sealed class MouseInputSensitiveMultiplier : MonoBehaviour, IMouseMovingToRotationConverter
    {
        public event Action<Vector2> ChangeSensitiveEvent = delegate { };

        [SerializeField] private MonoBehaviour Converter;

        private IMouseMovingToRotationConverter ParsedConverter;

        private const float MaxSensitive = 1000;
        [SerializeField][Range(0, MaxSensitive)] private float Sensitive_X;
        [SerializeField][Range(0, MaxSensitive)] private float Sensitive_Y;

        private void Awake()
        {
            ParsedConverter = Converter as IMouseMovingToRotationConverter;
            if (ParsedConverter == null)
                throw new NullReferenceException("Missing mouseinput-to-rotation converter.");
        }

        public Vector2 GetRotation(Vector2 mouseInput)
        {
            Vector2 input = new Vector2(mouseInput.x * Sensitive_X, mouseInput.y * Sensitive_Y);
            return ParsedConverter.GetRotation(input);
        }

        public void SetSensitive(Vector2 Sensitive)
        {
            Vector2 sens = new Vector2(
                Mathf.Clamp(Sensitive.x, 0, MaxSensitive),
                Mathf.Clamp(Sensitive.y, 0, MaxSensitive));
            Sensitive_X = sens.x;
            Sensitive_Y = sens.y;
            ChangeSensitiveEvent(sens);
        }
    }
}
