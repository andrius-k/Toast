using System;
using UIKit;
namespace GlobalToast.Extensions
{
    /// <summary>
    /// Provides pre iOS 11 compatible helper properties for for SafeAreaLayoutGuide anchors.
    /// Helper properties has prefix 'Safe' in their name. 
    /// If app is running on pre iOS 11, Anchor properties from the view will be used.
    /// If app is running on iOS 11 or grater, SafeAreaLayoutGuide properties will be used.
    /// </summary>
    public static class UIViewExtensions
    {
        public static NSLayoutXAxisAnchor SafeLeadingAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.LeadingAnchor;
            else
                return view.LeadingAnchor;
        }

        public static NSLayoutXAxisAnchor SafeTrailingAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.TrailingAnchor;
            else
                return view.TrailingAnchor;
        }

        public static NSLayoutXAxisAnchor SafeLeftAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.LeftAnchor;
            else
                return view.LeftAnchor;
        }

        public static NSLayoutXAxisAnchor SafeRightAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.RightAnchor;
            else
                return view.RightAnchor;
        }

        public static NSLayoutYAxisAnchor SafeTopAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.TopAnchor;
            else
                return view.TopAnchor;
        }

        public static NSLayoutYAxisAnchor SafeBottomAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.BottomAnchor;
            else
                return view.BottomAnchor;
        }

        public static NSLayoutXAxisAnchor SafeCenterXAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.CenterXAnchor;
            else
                return view.CenterXAnchor;
        }

        public static NSLayoutYAxisAnchor SafeCenterYAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.CenterYAnchor;
            else
                return view.CenterYAnchor;
        }

        public static NSLayoutDimension SafeHeightAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.HeightAnchor;
            else
                return view.HeightAnchor;
        }

        public static NSLayoutDimension SafeWidthAnchor(this UIView view)
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                return view.SafeAreaLayoutGuide.WidthAnchor;
            else
                return view.WidthAnchor;
        }
    }
}
