namespace HubbleGalleryFSharp.Models

open System

type HtmlString = Html of string

type HubblePicture =
    {
        Title: string
        Link: Uri
        Description: HtmlString
        ThumbnailLink: Uri
        ImageLink: Uri
        Date: DateTime
    }