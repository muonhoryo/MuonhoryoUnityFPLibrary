
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

        [SerializeField] private MonoBehaviour Converter;
        [SerializeField] private MonoBehaviour InversionModeProvider;

        private IMouseMovingToRotationConverter ParsedConverter;
        private IConstProvider<InversionMode> ParsedInversionModeProvider;

        public InversionMode InversionMode_ => ParsedInversionModeProvider.GetValue();
        private void Awake()
        {
            ParsedConverter = Converter as IMouseMovingToRotationConverter;
            if (ParsedConverter == null)
                throw new NullReferenceException("Missing mousemoving-to-rotation converter.");

            ParsedInversionModeProvider = InversionModeProvider as IConstProvider<InversionMode>;
            if (ParsedInversionModeProvider == null)
                throw new NullReferenceException("Missing InversionModeProvider.");
        }

        public Vector2 GetRotation(Vector2 input)
        {
            Vector2 newInput = new Vector2(
                (InversionMode_ & InversionMode.Horizontal) != 0 ? -input.x : input.x,
                (InversionMode_ & InversionMode.Vertical) != 0 ? -input.y : input.y);
            return ParsedConverter.GetRotation(newInput);
        }

    }
}
