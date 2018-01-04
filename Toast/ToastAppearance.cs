using System;
using UIKit;
namespace GlobalToast
{
    public class ToastAppearance
    {
        public UIColor Color { get; set; } = UIColor.FromRGBA(0f, 0f, 0f, 0.85f);
        public UIColor MessageColor { get; set; } = UIColor.White;
        public UIColor TitleColor { get; set; } = UIColor.White;
        public UIColor DismissButtonColor { get; set; } = UIColor.FromRGB(255, 75, 75);
        public UIFont MessageFont { get; set; } = UIFont.SystemFontOfSize(16f, UIFontWeight.Regular);
        public UIFont TitleFont { get; set; } = UIFont.SystemFontOfSize(16f, UIFontWeight.Medium);
        public UIFont DismissButtonFont { get; set; } = UIFont.SystemFontOfSize(16f, UIFontWeight.Regular);
        public UILineBreakMode DismissButtonLineBreakMode { get; set; } = UILineBreakMode.MiddleTruncation;
        public UITextAlignment MessageTextAlignment { get; set; } = UITextAlignment.Left;
        public nfloat CornerRadius { get; set; } = 5;
    }
}
