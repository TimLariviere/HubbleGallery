namespace HubbleGalleryFSharp

open HubbleGalleryFSharp.Models
open Xamarin.Forms
open Xamarin.Forms.Xaml

type DetailPage(item: HubblePicture) as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<DetailPage>)

    // Controls
    let _pictureImage = this.FindByName "PictureImage" :> Image
    let _titleLabel = this.FindByName "TitleLabel" :> Label

    // Lifecycle
    override this.OnAppearing() =
        _pictureImage.Source <- ImageSource.FromUri item.ThumbnailLink
        _titleLabel.Text <- item.Title