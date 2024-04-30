
using System;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public sealed class RotationInverter : MonoBehaviour, IMouseMovingToRotationConverter
    {
        public enum InversionMode
        {
            None = 0,
            Vertical = 1,
            Horizontal = 2,
            BothInverse = 3
        }

        public event Action<InversionMode> ChangeInversionModeEvent = delegate { };

        [SerializeField] private MonoBehaviour Converter;
        [SerializeField] private InversionMode inversionMode;

        private IMouseMovingToRotationConverter ParsedConverter;
        public InversionMode inversionMode_
        {
            get => inversionMode;
            set
            {
                inversionMode = value;
                ChangeInversionModeEvent(inversionMode);
            }
        }
        private void Awake()
        {
            ParsedConverter = Converter as IMouseMovingToRotationConverter;
            if (ParsedConverter == null)
                throw new NullReferenceException("Missing mousemoving-to-rotation converter.");
        }

        public Vector2 GetRotation(Vector2 input)
        {
            Vector2 newInput = new Vector2(
                (inversionMode & InversionMode.Horizontal) != 0 ? -input.x : input.x,
                (inversionMode & InversionMode.Vertical) != 0 ? -input.y : input.y);
            return ParsedConverter.GetRotation(newInput);
        }

    }
}
