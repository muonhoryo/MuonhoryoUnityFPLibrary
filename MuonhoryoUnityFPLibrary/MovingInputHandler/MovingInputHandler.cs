

using UnityEngine;
using MuonhoryoLibrary.Unity.COM;
using System;

namespace MuonhoryoLibrary.Unity
{
    public sealed class MovingInputHandler : MonoBehaviour, IActiveModule
    {
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private MonoBehaviour MovingModule;

        private IMovingModule ParsedMovingModule;

        [SerializeField] private string InputName_TowardsAxis;
        [SerializeField] private string InputName_SideAxis;

        [SerializeField] private bool IsActive = false;
        public bool IsActive_
        {
            get => IsActive;
            set
            {
                if (IsActive != value)
                {
                    IsActive = value;
                    enabled = value;
                    if (IsActive)
                        ActivateModuleEvent.Invoke();
                    else
                        DeactivateModuleEvent.Invoke();
                }
            }
        }

        private void OnEnable()
        {
            if (!IsActive_)
                enabled = false;
        }
        private void OnDisable()
        {
            if (IsActive_)
                enabled = true;
        }
        private void Awake()
        {
            if (string.IsNullOrEmpty(InputName_TowardsAxis))
                throw new NullReferenceException("Missing Input name - Towards axis.");
            if (string.IsNullOrEmpty(InputName_SideAxis))
                throw new NullReferenceException("Missing Input name - Side axis.");

            ParsedMovingModule = MovingModule as IMovingModule;
            if (ParsedMovingModule == null)
                throw new NullReferenceException("Missing MovingModule.");
        }
        private void LateUpdate()
        {
            Vector2 input = new Vector2(
                Input.GetAxisRaw(InputName_SideAxis),
                Input.GetAxisRaw(InputName_TowardsAxis));
            if (input == Vector2.zero)
            {
                ParsedMovingModule.StopMoving();
            }
            else
            {
                ParsedMovingModule.SetMovingDirection(input);
            }
        }

        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}
