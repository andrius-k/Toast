# Toast messages for Xamarin.iOS

Toast is Xamarin iOS toast message library for presenting short automatically disappearing messages. You can choose to add a toast to current KeyWindow of your application which means that toast will not belong to any view controller and will not disappear when navigated away from it. You can also choose to add toast to a specific view controller in which case top and bottom layout guides of parent view controller will be taken into account when positioning the toast.

Toast is very customizable so you can match the style of your application as well as fulfill functional requirements that you have.

This library currently allows you to:

* Style labels and frame of the toast;
* Configure layout;
* Configure position on the screen
* Choose between automatically and manually dismissing toast;
* Pick between two types of views;
* Pick between two types of show/hide animations;
* Choose where to add toast (view controller or key window of application)
* Provide your own view for presentation;
* Show progress indicator inside the toast;

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
     .SetProgressIndicator(true) // Default is false
     .Show();
```

## Progress Indicator

Toast could contain a progress indicator (`UIActivityIndicatorView`). If both `Message` and `Title` are set to `null`, toast will only contain progress indicator.

```cs
Toast.MakeToast(null).SetProgressIndicator(true).Show();
```
Or just:
```cs
Toast.ShowProgressIndicator();
```

If `Message` and/or `Title` is set together with `SetProgressIndicator(true)`, spinner will appear on the left side of the toast.

## Appearance, Layout and Animation

### Appearance

Visual appearance of the toast could be adjusted by setting `ToastAppearance` object like so:

```cs
Toast.MakeToast("This is a message")
     .SetAppearance(new ToastAppearance
{
   Color = UIColor.Blue,
   CornerRadius = 8,
   // Other properties removed for brevity
}).Show();
```

### Layout

Layout properties (margin, padding and spacing) of the toast could be adjusted by setting `ToastLayout` object like so:

```cs
Toast.MakeToast("This is a message")
     .SetLayout(new ToastLayout
{
   PaddingLeading = 16,
   PaddingTrailing = 16,
   Spacing = 6,
   // Other properties removed for brevity
}).Show();
```

### Animation

Show and hide animations could be controlled using `ToastAnimator`. There are two animators provided out of the box: `FadeAnimator` (default) and `ScaleAnimator`. Animator could be changed like so:

```cs
Toast.MakeToast("This is a message")
     .SetAnimator(new ScaleAnimator())
     .Show();
```

In order to provide custom animation you need to subclass `ToastAnimator` and override `AnimateShow` and `AnimateHide` methods.

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

// Every toast will use those global properties from now on, 
// unless they are overridden for a specific instance
Toast.ShowToast("This is a message");
```

## Auto Layout

Toasts and views inside them are all using Auto Layout. on iOS 11 and above views are constrained to `SafeAreaLayoutGuide`. On pre iOS 11 `SafeAreaLayoutGuide` is not used to keep backwards compatibility. On pre iOS 11 when parent view controller is provided, top and bottom layout guides of view controller are used to make sure it appears above/bellow (based on `ToastPosition`) bars.

## Auto Dismiss

Toasts can be dismissed not only by user clicking the button, but also from your code.

```cs
var toast = Toast.MakeToast("This is a message")
                 .SetAutoDismiss(false)
                 .Show();
toast.Dismiss();
```

Both auto-dismissible and non-auto-dismissible toasts can be dismissed by calling `Dismiss();` method.

## Writing Custom Views

When creating custom toast views make sure to subclass `BaseToastView`. There are 2 main methods that you need to override: `Initialize()` and `ConstrainChildren()`.

### Creating Sub Views

In `Initialize()` method initialize, add and populate UI components needed for the view. `Toast` instance will come as a constructor parameter.

### Positioning Sub Views

In `ConstrainChildren()` add Auto Layout constrains for views initialized in `Initialize()`. Make sure to use `UIView` extension methods provided in `UIViewExtensions` to get backwards compatible anchors (`NSLayout*AxisAnchor` and `NSLayoutDimension`). Look in one of the provided views for simple examples (`ToastViews` folder).

### Presenting Custom Views

To use custom built view, call `SetToastViewFactory` and provide a `Func` that will return your newly built view.

```cs
Toast.MakeToast("This is a message")
     .SetToastViewFactory(t => new MyNewToastView(t))
     .Show();
```

### Adding Additional Parameters

If you want to add additional parameters to your custom toast view (like images,  etc.) you should add another constructor with all values that you need and pass parameters when constructing the view in `SetToastViewFactory` method.

## Backwards compatibility 

Tested on iOS 10, 11 and 12. Should work on previous versions as well.

## Setup

Available on NuGet:
* [Toast](https://www.nuget.org/packages/Toast.iOS/)
