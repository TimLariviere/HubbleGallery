namespace HubbleGalleryFSharp

open Xamarin.Forms
open Xamarin.Forms.Xaml

type HubbleGalleryFSharpPage() =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<HubbleGalleryFSharpPage>)
