using System;
using UIKit;
using GlobalToast.Extensions;
namespace GlobalToast.ToastViews
{
    public class DismissibleTitleMessageToastView : TitleMessageToastView
    {
        public UIButton DismissButton { get; set; }

        public DismissibleTitleMessageToastView(Toast toast) : base(toast)
        {
        }

        /// <summary>
        /// Gets the maximum width of the dismiss button.
        /// Possible values 0 to 1.
        /// </summary>
        protected virtual nfloat DismissButtonMaxWidth
        {
            get => 0.5f;
        }

        protected override void Initialize()
        {
            base.Initialize();

            DismissButton = new UIButton(UIButtonType.System);
            DismissButton.TitleLabel.Font = Toast.Appearance.DismissButtonFont;
            DismissButton.SetTitleColor(Toast.Appearance.DismissButtonColor, UIControlState.Normal);
            DismissButton.SetTitle(Toast.DismissButtonTitle, UIControlState.Normal);
            DismissButton.TranslatesAutoresizingMaskIntoConstraints = false;
            DismissButton.TitleLabel.LineBreakMode = Toast.Appearance.DismissButtonLineBreakMode;
            DismissButton.TouchUpInside += DismissButton_TouchUpInside;
            AddSubview(DismissButton);
        }

        protected override void ConstrainChildren()
        {
            TitleLabel.SafeTrailingAnchor().ConstraintEqualTo(DismissButton.SafeLeadingAnchor(), -Toast.Layout.Spacing).Active = true;
            TitleLabel.SafeLeadingAnchor().ConstraintEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            TitleLabel.SafeTopAnchor().ConstraintEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;
            TitleLabel.SafeBottomAnchor().ConstraintEqualTo(MessageLabel.SafeTopAnchor(), -Toast.Layout.Spacing).Active = true;

            MessageLabel.SafeLeadingAnchor().ConstraintEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            MessageLabel.SafeTrailingAnchor().ConstraintEqualTo(DismissButton.SafeLeadingAnchor(), -Toast.Layout.Spacing).Active = true;
            MessageLabel.SafeBottomAnchor().ConstraintEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;

            DismissButton.SafeTrailingAnchor().ConstraintEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
            DismissButton.SafeCenterYAnchor().ConstraintEqualTo(this.SafeCenterYAnchor()).Active = true;
            // The following constraint makes sure that button is not wider than specified amount of available width
            DismissButton.SafeWidthAnchor().ConstraintLessThanOrEqualTo(this.SafeWidthAnchor(), DismissButtonMaxWidth, 0f).Active = true;

            DismissButton.SetContentCompressionResistancePriority(
                MessageLabel.ContentCompressionResistancePriority(UILayoutConstraintAxis.Horizontal) + 10,
                UILayoutConstraintAxis.Horizontal);
        }

        private void DismissButton_TouchUpInside(object sender, EventArgs e)
        {
            Dismiss();
        }

        public override void RemoveFromSuperview()
        {
            base.RemoveFromSuperview();

            if (DismissButton != null)
                DismissButton.TouchUpInside -= DismissButton_TouchUpInside;
        }
    }
}
