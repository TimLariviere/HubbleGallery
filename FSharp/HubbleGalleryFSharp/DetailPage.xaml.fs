namespace HubbleGalleryFSharp

open HubbleGalleryFSharp.Models
open Xamarin.Forms
open Xamarin.Forms.Xaml
open System.Text.RegularExpressions

type DetailPage(item: HubblePicture) as this =
    inherit ContentPage()
    let _ = base.LoadFromXaml(typeof<DetailPage>)

    // Controls
    let _pictureImage = this.FindByName "PictureImage" :> Image
    let _titleLabel = this.FindByName "TitleLabel" :> Label
    let _dateLabel = this.FindByName "DateLabel" :> Label
    let _descriptionLabel = this.FindByName "DescriptionLabel" :> Label

    // Methods
    let getTextFromDescription (Html description) =
        Regex.Replace(description, "<.*?>", "").Trim().Replace(System.Environment.NewLine, System.Environment.NewLine + System.Environment.NewLine)

    // Lifecycle
    override this.OnAppearing() =
        _pictureImage.Source <- ImageSource.FromUri item.ThumbnailLink
        _titleLabel.Text <- item.Title
        _dateLabel.Text <- item.Date.ToString "dd MMMM yyyy, HH:mm"
        _descriptionLabel.Text <- getTextFromDescription item.Description