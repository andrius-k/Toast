# Toast messages for Xamarin.iOS

Toast is Xamarin iOS toast message library for presenting short automatically disappearing messages. You can choose to add a toast to current KeyWindow of your application which means that toast will not belong to any view controller and will not disappear when navigated away from it. You can also choose to add toast to a specific view controller in which case top and bottom layout guides of parent view controller will be taken into account when positioning the toast.

Toas is very customizable so you can match the style of your application as well as fulfill functional requirements that you have.

This library currently allow you to:

* Style labels and frame of the toast;
* Configure layout;
* Configure position on the screen
* Choose between automatically and manually dismissing toast;
* Pick between two types of views;
* Pick between two types of show/hide animations;
* Choose where to add toast (view controller or key window of application)
* Provide your own view for presentation;

## Screenshots

![Alt text](etc/images/Sample.png?raw=true "Toast Sample")

## Code samples

```cs
// Present a simple toast
Toast.ShowToast("This is a message");

// Present a simple toast with title
Toast.ShowToast("This is a message", "Title");

// More configurations
Toast.MakeToast("This is a message")
     .SetTitle("Title")
     .SetPosition(ToastPosition.Top) // Default is Bottom
     .SetDuration(ToastDuration.Long) // Default is Regular
     .SetShowShadow(false) // Default is true
     .SetAnimator(new ScaleAnimator()) // Default is FadeAnimator
     .SetParentController(this) // Default is null. This means that toast is added to KeyWindow
     .SetBlockTouches(true) // Default is false. If false touches that occur on the toast will be sent down to parent view
     .SetAutoDismiss(false) // Default is true. If set to false Dismiss button will be shown
     .Show();
```

## Global Properties

You can set static global properties for *Appearance*, *Layout* and *Animation*. Global properties will be used for every instance of toast if those properties are not provided for that particular instance. This means that you can set those properties in your initialization logic and no longer worry about them.

```cs
Toast.GlobalAnimator = new ScaleAnimator();
Toast.GlobalLayout.MarginBottom = 16f;
Toast.GlobalAppearance.MessageColor = UIColor.Red;
Toast.GlobalAppearance.TitleFont = UIFont.SystemFontOfSize(16, UIFontWeight.Light);

// Or you can replace entire objects
Toast.GlobalAppearance = new ToastAppearance
{
    Color = UIColor.Blue,
    CornerRadius = 4
};
```

## Auto Layout

Toasts and views inside them are all using Auto Layout. on iOS 11 and above views are constrained to `SafeAreaLayoutGuide`. On pre iOS 11 `SafeAreaLayoutGuide` is not used to keep backwards compatibility. On pre iOS 11 when parent view controller is provided top and bottom layout guides of view controller are used to make sure it appears above/bellow (based on `ToastPosition`) bars.

## Other Things

Manually dismissing toasts can be dismissed not only by user clicking the button, but also from your code.

```cs
var toast = Toast.MakeToast("This is a message")
                 .SetAutoDismiss(false)
                 .Show();
toast.Dismiss();
```

If toast is auto dismissing calling `Dismiss();` method will have no effect.

## Backwards compatibility 

Tested on iOS 10 and 11. Should work on previous versions as well.
