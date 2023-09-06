using System;
using UIKit;
using GlobalToast.Extensions;
namespace GlobalToast.ToastViews
{
    public class BaseToastView : UIView
    {
        public NSLayoutConstraint TopContraint { get; protected set; }
        public NSLayoutConstraint BottomContraint { get; protected set; }
        public NSLayoutConstraint LeadingContraint { get; protected set; }
        public NSLayoutConstraint TrailingContraint { get; protected set; }
        public NSLayoutConstraint CenterXContraint { get; protected set; }
        public NSLayoutConstraint CenterYContraint { get; protected set; }

        protected Toast Toast { get;  }

        public virtual UIView ParentView
        {
            get => Toast.ParentController != null ?
                        Toast.ParentController.View :
                        UIApplication.SharedApplication.KeyWindow;
        }

        public BaseToastView(Toast toast)
        {
            Toast = toast;
        }

        /// <summary>
        /// Sets up the views of the toast and adds autolayout constraints.
        /// </summary>
        public virtual void Setup()
        {
            Initialize();
            ConstrainInParent();
            ConstrainChildren();
        }

        /// <summary>
        /// Initialize appearance, layout and set values of UI elements.
        /// </summary>
        protected virtual void Initialize()
        {
            BackgroundColor = Toast.Appearance.Color;
            Layer.CornerRadius = Toast.Appearance.CornerRadius;
            UserInteractionEnabled = Toast.BlockTouches;
            TranslatesAutoresizingMaskIntoConstraints = false;

            if(Toast.ShowShadow)
                DropShadowOnFrame();
        }

        /// <summary>
        /// Constrains the view inside parent.
        /// </summary>
        protected virtual void ConstrainInParent()
        {
            if(Toast.Position == ToastPosition.Bottom)
            {
                BottomContraint = this.SafeBottomAnchor().ConstraintEqualTo(GetBottomAnchor(), -Toast.Layout.MarginBottom);
                BottomContraint.Active = true;

                TopContraint = this.SafeTopAnchor().ConstraintGreaterThanOrEqualTo(GetTopAnchor(), Toast.Layout.MarginTop);
                TopContraint.Active = true;
            }
            else if(Toast.Position == ToastPosition.Top)
            {
                TopContraint = this.SafeTopAnchor().ConstraintEqualTo(GetTopAnchor(), Toast.Layout.MarginTop);
                TopContraint.Active = true;

                BottomContraint = this.SafeBottomAnchor().ConstraintLessThanOrEqualTo(GetBottomAnchor(), -Toast.Layout.MarginBottom);
                BottomContraint.Active = true;
            }
            else if (Toast.Position == ToastPosition.Center)
            {
                CenterYContraint = this.SafeCenterYAnchor().ConstraintEqualTo(GetCenterYAnchor(), Toast.Layout.MarginCenter);
                CenterYContraint.Active = true;

                TopContraint = this.SafeTopAnchor().ConstraintGreaterThanOrEqualTo(GetTopAnchor(), Toast.Layout.MarginTop);
                TopContraint.Active = true;

                BottomContraint = this.SafeBottomAnchor().ConstraintLessThanOrEqualTo(GetBottomAnchor(), -Toast.Layout.MarginBottom);
                BottomContraint.Active = true;
            }

            LeadingContraint = this.SafeLeadingAnchor().ConstraintGreaterThanOrEqualTo(ParentView.SafeLeadingAnchor(), Toast.Layout.MarginLeading);
            TrailingContraint = this.SafeTrailingAnchor().ConstraintLessThanOrEqualTo(ParentView.SafeTrailingAnchor(), -Toast.Layout.MarginTrailing);
            CenterXContraint = this.SafeCenterXAnchor().ConstraintEqualTo(ParentView.SafeCenterXAnchor());

            NSLayoutConstraint.ActivateConstraints(
                new []{LeadingContraint, TrailingContraint, CenterXContraint});
        }

        /// <summary>
        /// Use this method to add constraints to the child views of the toast.
        /// </summary>
        protected virtual void ConstrainChildren()
        {

        }

        /// <summary>
        /// Adds the shadow on the frame.
        /// </summary>
        protected virtual void DropShadowOnFrame()
        {
            Layer.ShadowColor = UIColor.Black.CGColor;
            Layer.ShadowOffset = new CoreGraphics.CGSize(1, 1);
            Layer.ShadowOpacity = 0.5f;
        }

        /// <summary>
        /// If running on pre-iOS 11 and ParentController is set, returns TopLayoutGuide of ParentController.
        /// Otherwise returns SafeTopAnchor of ParentView.
        /// </summary>
        protected virtual NSLayoutYAxisAnchor GetTopAnchor()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0) || Toast.ParentController == null)
            {
                return ParentView.SafeTopAnchor();
            }
            else
            {
                return Toast.ParentController.TopLayoutGuide.BottomAnchor;
            }
        }

        /// <summary>
        /// If running on pre-iOS 11 and ParentController is set, returns BottomLayoutGuide of ParentController.
        /// Otherwise returns SafeBottomAnchor of ParentView.
        /// </summary>
        protected virtual NSLayoutYAxisAnchor GetBottomAnchor()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0) || Toast.ParentController == null)
            {
                return ParentView.SafeBottomAnchor();
            }
            else
            {
                return Toast.ParentController.BottomLayoutGuide.TopAnchor;
            }
        }

        /// <summary>
        /// If running on pre-iOS 11 and ParentController is set, returns CenterYAnchor of ParentController.
        /// Otherwise returns SafeCenterYAnchor of ParentView.
        /// </summary>
        protected virtual NSLayoutYAxisAnchor GetCenterYAnchor()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0) || Toast.ParentController == null)
            {
                return ParentView.SafeCenterYAnchor();
            }
            else
            {
                return Toast.ParentController.View.CenterYAnchor;
            }
        }

        /// <summary>
        /// Start show animation
        /// </summary>
        public virtual void AnimateShow()
        {
            Toast.Animator.AnimateShow(this);
        }

        /// <summary>
        /// Start hide animation.
        /// <paramref name="completion"/> will be invoked when animation is finished.
        /// </summary>
        public virtual void AnimateHide(Action completion)
        {
            Toast.Animator.AnimateHide(this, completion);
        }

        public virtual void Dismiss()
        {
            InvokeOnMainThread(() =>
            {
                AnimateHide(() =>
                {
                    RemoveFromSuperview();
                });

                Toast.DismissCallback?.Invoke();
            });
        }
    }
}
