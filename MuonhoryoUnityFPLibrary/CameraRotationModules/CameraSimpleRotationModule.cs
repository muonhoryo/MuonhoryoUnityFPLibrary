
using System;
using UnityEngine;

namespace MuonhoryoLibrary.Unity.COM
{
    public sealed class CameraSimpleRotationModule : MonoBehaviour, ICameraViewRotationModule, IActiveModule
    {
        public event Action<Vector3, float> ChangeViewRotationEvent = delegate { };
        public event Action<Vector3, Vector2> ChangeViewDirectionEvent = delegate { };
        public event Action ActivateModuleEvent = delegate { };
        public event Action DeactivateModuleEvent = delegate { };

        [SerializeField] private MonoBehaviour RotationLimiter;
        [SerializeField] private MonoBehaviour RotationOffsetProvider;

        private IRotationLimiter ParsedRotationLimiter;
        private IConstProvider<Vector3> ParsedRotationOffsetProvider;

        private bool IsActive = false;
        private Vector3 CurrentRotation;
        [SerializeField] private Transform ViewObject;
        [SerializeField] private bool IsActiveOnAwake = false;

        public Vector3 RotationOffset_ => ParsedRotationOffsetProvider.GetValue();
        public IRotationLimiter RotationLimiter_ => ParsedRotationLimiter;
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
        public Vector3 CurrentRotation_
        {
            get => CurrentRotation;
            private set
            {
                CurrentRotation = value;
                ChangeViewRotationEvent(CurrentRotation_, CurrentRotation_.y);
                ChangeViewDirectionEvent(CurrentViewDirection_, CurrentHorizontalViewDirection_);
            }
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
            if (ViewObject == null)
                throw new ArgumentNullException("Missing ViewObject.");

            ParsedRotationLimiter = RotationLimiter as IRotationLimiter;
            if (ParsedRotationLimiter == null)
                throw new ArgumentNullException("Missing RotationLimiter.");

            ParsedRotationOffsetProvider = RotationOffsetProvider as IConstProvider<Vector3>;
            if (ParsedRotationOffsetProvider == null)
                throw new ArgumentNullException("Missing RotationOffsetProvider.");

            if (!IsActiveOnAwake)
                enabled = false;
            CurrentRotation_ = ViewObject.eulerAngles;
        }
        private void Start()
        {
            if (IsActive != enabled)
                IsActive_ = true;
        }
        private void LateUpdate()
        {
            ViewObject.eulerAngles = CurrentRotation_ + RotationOffset_;
        }

        //

        private void RotateViewObject(Vector3 rotation)
        {
            CurrentRotation_ = ClampRotation(CurrentRotation_ + rotation);
            SetViewObjectRotation(CurrentRotation_ + rotation);
        }
        private Vector3 ClampRotation(Vector3 input)
        {
            float x = input.x, y = input.y, z = input.z;

            x %= 360;
            if (x < 0) x += 360;
            y %= 360;
            if (y < 0) y += 360;
            z %= 360;
            if (z < 0) z += 360;

            return new Vector3(x, y, z);
        }
        private void SetViewObjectRotation(Vector3 rotation)
        {
            CurrentRotation_ = ParsedRotationLimiter.GetLimitedRotation(rotation);
            LateUpdate();
        }

        public void Rotate(Vector3 rotation)
        {
            RotateViewObject(rotation);
        }
        public void SetRotation(Vector3 rotation)
        {
            SetViewObjectRotation(rotation);
        }
        public void SetXRotation(float rotation)
        {
            SetRotation(new Vector3(rotation, ViewObject.eulerAngles.y, ViewObject.eulerAngles.z));
        }
        public void SetYRotation(float rotation)
        {
            SetRotation(new Vector3(ViewObject.eulerAngles.x, rotation, ViewObject.eulerAngles.z));
        }
        public void SetZRotation(float rotation)
        {
            SetRotation(new Vector3(ViewObject.eulerAngles.x, ViewObject.eulerAngles.y, rotation));
        }

        public Vector3 CurrentViewDirection_ => CurrentRotation_.DirectionFromStereoRotation();
        public Vector2 CurrentHorizontalViewDirection_ => (-CurrentRotation_.y + 90).DirectionOfAngle();
        bool IActiveModule.IsActive { get => IsActive_; set => IsActive_ = value; }
    }
}
