using UIKit;
using GlobalToast.Extensions;
namespace GlobalToast.ToastViews
{
    public class ProgressMessageToastView : BaseToastView
    {
        public UIActivityIndicatorView ActivityIndicator { get; set; }
        public UILabel MessageLabel { get; set; }

        public ProgressMessageToastView(Toast toast) : base(toast)
        {
        }

        protected override void Initialize()
        {
            base.Initialize();

            ActivityIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            ActivityIndicator.StartAnimating();
            ActivityIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(ActivityIndicator);

            if (!string.IsNullOrEmpty(Toast.Message))
            {
                ActivityIndicator.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.White;

                MessageLabel = new UILabel();
                MessageLabel.Text = Toast.Message;
                MessageLabel.Font = Toast.Appearance.MessageFont;
                MessageLabel.TextColor = Toast.Appearance.MessageColor;
                MessageLabel.Lines = 0;
                MessageLabel.AdjustsFontSizeToFitWidth = true;
                MessageLabel.MinimumFontSize = 10f;
                MessageLabel.TextAlignment = Toast.Appearance.MessageTextAlignment;
                MessageLabel.TranslatesAutoresizingMaskIntoConstraints = false;
                AddSubview(MessageLabel);
            }
        }

        protected override void ConstrainChildren()
        {
            base.ConstrainChildren();

            ActivityIndicator.SafeLeadingAnchor().ConstraintEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            ActivityIndicator.SafeCenterYAnchor().ConstraintEqualTo(this.SafeCenterYAnchor()).Active = true;

            if (MessageLabel != null)
            {
                ActivityIndicator.SafeBottomAnchor().ConstraintLessThanOrEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;
                ActivityIndicator.SafeTopAnchor().ConstraintGreaterThanOrEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;

                MessageLabel.SafeLeadingAnchor().ConstraintEqualTo(ActivityIndicator.SafeTrailingAnchor(), Toast.Layout.PaddingLeading).Active = true;
                MessageLabel.SafeTrailingAnchor().ConstraintEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
                MessageLabel.SafeBottomAnchor().ConstraintEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;
                MessageLabel.SafeTopAnchor().ConstraintEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;
            }
            else
            {
                ActivityIndicator.SafeBottomAnchor().ConstraintEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;
                ActivityIndicator.SafeTopAnchor().ConstraintEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;
                ActivityIndicator.SafeTrailingAnchor().ConstraintEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
            }
        }
    }
}
