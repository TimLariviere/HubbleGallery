namespace HubbleGalleryFSharp

open HubbleGalleryFSharp.Models
open HubbleGalleryFSharp.HubbleDataSource
open System
open Xamarin.Forms
open Xamarin.Forms.FSharp
open Xamarin.Forms.Xaml

type MainPage() as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<MainPage>)

    let mutable isInitialized = false

    // Controls
    //let _feedPicker = this.FindByName "FeedPicker" :> Picker
    let _picturesListView = this.FindByName "PicturesListView" :> ListView

    // Methods
    let loadAsync feed = async {
        let! pictures = HubbleDataSource.loadAsync feed
        Utils.beginInvokeOnMainThread (fun () ->
            _picturesListView.ItemsSource <- pictures
            this.IsBusy <- false
        )
    }

    // Event handlers
    //let onFeedSelected = EventHandler(fun _ args ->
    //    let feed = _feedPicker.SelectedItem :?> string |> getFeedFromName
    //    this.IsBusy <- true
    //    loadAsync feed |> Async.Start
    //)

    let onItemSelected = EventHandler<ItemTappedEventArgs>(fun _ args ->
        let item = args.Item :?> HubblePicture
        this.Navigation.PushAsync(new DetailPage(item)) |> Async.AwaitTask |> Async.Start
    )

    // Lifecycle
    override this.OnAppearing() =
        // Load feed picker
        //_feedPicker.Items.Clear()
        //feeds |> Map.iter (fun k v -> _feedPicker.Items.Add v.Name)
        //_feedPicker.SelectedIndex <- 0

        // Events
        //_feedPicker.SelectedIndexChanged.AddHandler onFeedSelected
        _picturesListView.ItemTapped.AddHandler onItemSelected

        if isInitialized = false then
            isInitialized <- true
            this.IsBusy <- true
            loadAsync Feeds.Top100 |> Async.Start
        else
            ()

    override this.OnDisappearing() =
        //_feedPicker.SelectedIndexChanged.RemoveHandler onFeedSelected
        _picturesListView.ItemTapped.RemoveHandler onItemSelected