using UIKit;
using GlobalToast.Extensions;
namespace GlobalToast.ToastViews
{
    public class MessageToastView : BaseToastView
    {
        public UILabel MessageLabel { get; set; }

        public MessageToastView(Toast toast) : base(toast)
        {
        }
        
        protected override void Initialize()
        {
            base.Initialize();

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

        protected override void ConstrainChildren()
        {
            base.ConstrainChildren();

            MessageLabel.SafeLeadingAnchor().ConstraintEqualTo(this.SafeLeadingAnchor(), Toast.Layout.PaddingLeading).Active = true;
            MessageLabel.SafeTrailingAnchor().ConstraintEqualTo(this.SafeTrailingAnchor(), -Toast.Layout.PaddingTrailing).Active = true;
            MessageLabel.SafeBottomAnchor().ConstraintEqualTo(this.SafeBottomAnchor(), -Toast.Layout.PaddingBottom).Active = true;
            MessageLabel.SafeTopAnchor().ConstraintEqualTo(this.SafeTopAnchor(), Toast.Layout.PaddingTop).Active = true;
        }
    }
}
