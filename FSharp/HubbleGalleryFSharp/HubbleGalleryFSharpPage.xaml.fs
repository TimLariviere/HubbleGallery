namespace HubbleGalleryFSharp

open Xamarin.Forms
open Xamarin.Forms.FSharp
open Xamarin.Forms.Xaml

type HubbleGalleryFSharpPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<HubbleGalleryFSharpPage>)

    // Controls
    let _picturesListView = this.FindByName "PicturesListView" :> ListView

    // Methods
    let onAppearingAsync = async {
        let! pictures = HubbleDataSource.loadAsync()
        Utils.beginInvokeOnMainThread (fun () ->
            _picturesListView.ItemsSource <- pictures
        )
    }

    // Lifecycle
    override this.OnAppearing() = onAppearingAsync |> Async.Start