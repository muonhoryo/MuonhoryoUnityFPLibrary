
using System;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public class GroundMovingModule : MonoBehaviour, IActiveModule, IMovingModule
    {
        public event Action<Vector3> StartMovingEvent = delegate { };
        public event Action<Vector2> ChangeMovingDirectionEvent = delegate { };
        public event Action StopMovingEvent = delegate { };
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private MonoBehaviour MovingDirectionCalculator;

        private IMovingDirectionCalculator ParsedMovDirCalculator;

        [SerializeField] private Rigidbody TargetRigidbody;

        private bool IsActive = false;
        private Vector2 MovingDirection = Vector3.zero;

        [SerializeField] private float AddingForceModifier = 1;
        [SerializeField] private float DefaultSpeed;

        public CompositeFloat Speed_ { get; private set; }

        public bool IsMoving_ => IsActive_;

        public Vector3 RealMovingDirection_ =>
            ParsedMovDirCalculator != null ?
                ParsedMovDirCalculator.GetDirection(MovingDirection) : Vector3.zero;
        public Vector2 InputMovingDirection_ => MovingDirection;
        public bool IsActive_
        {
            get => IsActive;
            set
            {
                IsActive = value;
                enabled = value;
                if (IsActive)
                    ActivateModuleEvent.Invoke();
                else
                    DeactivateModuleEvent.Invoke();
            }
        }
        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
        public Vector3 CurrentObjectPosition_ => transform.position;
        protected Rigidbody Rigidbody_ => TargetRigidbody;

        public void SetMovingDirection(Vector2 inputDir)
        {
            MovingModule.SetMovingDirection(this, inputDir, StartMovingAction, SetMovingDirectionAction);
        }
        private void StartMovingAction(Vector2 inputDir)
        {
            SetMovingDirectionAction(inputDir);
            IsActive_ = true;
            if (IsActive_)
                StartMovingEvent(InputMovingDirection_);
        }
        private void SetMovingDirectionAction(Vector2 inputDir)
        {
            MovingDirection = inputDir;
            ChangeMovingDirectionEvent(MovingDirection);
        }
        public void StopMoving()
        {
            MovingModule.StopMoving(this, StopMovingAction);
        }
        private void StopMovingAction()
        {
            IsActive_ = false;
            Rigidbody_.velocity = new Vector3(0, Rigidbody_.velocity.y, 0);
            StopMovingEvent();
        }

        //Unity API

        private void OnDisable()
        {
            if (IsActive)
                enabled = true;
        }
        private void OnEnable()
        {
            if (!IsActive)
                enabled = false;
        }
        private void Awake()
        {
            if (TargetRigidbody == null)
                throw new NullReferenceException("Missing TargetRigidbody.");

            if (MovingDirectionCalculator != null)
            {
                ParsedMovDirCalculator = MovingDirectionCalculator as IMovingDirectionCalculator;
                if (ParsedMovDirCalculator == null)
                    throw new NullReferenceException
                        ("Cant't parse MovingDirectionCalculator to IMovingDirectionCalculator.");
            }

            Speed_ = new CompositeFloat(DefaultSpeed);

            if (!IsActive)
                enabled = false;
        }
        private void FixedUpdate()
        {
            TargetRigidbody.AddForce(RealMovingDirection_ * (float)Speed_ * AddingForceModifier, ForceMode.Force);
            if (TargetRigidbody.velocity.magnitude > (float)Speed_)
                TargetRigidbody.velocity = TargetRigidbody.velocity.normalized * (float)Speed_;
        }
    }
}
