namespace HubbleGalleryFSharp.iOS

open Xamarin.Forms
open Xamarin.Forms.Platform.iOS
open UIKit
open CoreGraphics
open System

// F# version of https://gist.github.com/sthewissen/30e9b9a620157177fed880617def4a41#file-blurrednavigation-cs
type BlurredNavigationRenderer() =
    inherit NavigationRenderer()

    let mutable _viewAdded = false

    override this.ViewDidLoad() =
        base.ViewDidLoad()

        let titleTextAttributes = UIStringAttributes()
        titleTextAttributes.ForegroundColor <- UIColor.White
        this.NavigationBar.TitleTextAttributes <- titleTextAttributes

        // Remove background colors and such to form a completely transparent bar
        this.NavigationBar.ShadowImage <- new UIImage()
        this.NavigationBar.TintColor <- UIColor.White
        this.NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default)
        this.NavigationBar.Translucent <- true

        if _viewAdded = false then
            let effect = UIBlurEffect.FromStyle UIBlurEffectStyle.Dark
            let visualEffectView = new UIVisualEffectView(effect)

            let bounds = this.NavigationBar.Bounds.Inset(nfloat 0., nfloat -40.)
            bounds.Offset(nfloat 0., nfloat -40.)
            visualEffectView.Frame <- bounds
            visualEffectView.Tag <- nint 74619

            this.NavigationBar.AddSubview visualEffectView
            this.NavigationBar.SendSubviewToBack visualEffectView

            _viewAdded <- true
        else
            ()

module Dummy_NoLineNavigationRenderer =
    [<assembly: Xamarin.Forms.ExportRenderer(typeof<Xamarin.Forms.NavigationPage>, typeof<BlurredNavigationRenderer>)>]
    do ()

