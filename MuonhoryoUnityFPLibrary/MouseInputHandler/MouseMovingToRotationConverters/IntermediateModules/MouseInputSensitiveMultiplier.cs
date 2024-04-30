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
        [SerializeField] private MonoBehaviour Converter;
        [SerializeField] private MonoBehaviour SensitiveProvider;

        private IMouseMovingToRotationConverter ParsedConverter;
        private IConstProvider<Vector2> ParsedSensitiveProvider;

        public Vector2 Sensitive_ => ParsedSensitiveProvider.GetValue();

        private void Awake()
        {
            ParsedConverter = Converter as IMouseMovingToRotationConverter;
            if (ParsedConverter == null)
                throw new NullReferenceException("Missing mouseinput-to-rotation converter.");

            ParsedSensitiveProvider = SensitiveProvider as IConstProvider<Vector2>;
            if (ParsedSensitiveProvider == null)
                throw new NullReferenceException("Missing SensitiveProvider.");
        }

        public Vector2 GetRotation(Vector2 mouseInput)
        {
            Vector2 input = new Vector2(mouseInput.x * Sensitive_.x, mouseInput.y * Sensitive_.y);
            return ParsedConverter.GetRotation(input);
        }
    }
}
