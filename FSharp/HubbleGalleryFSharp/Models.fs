namespace HubbleGalleryFSharp.Models

open System

type Category = Category of string
type HtmlString = Html of string

type HubblePicture =
    {
        Title: string
        Link: Uri
        Description: HtmlString
        Author: string
        ImageLink: Uri
        Categories: Category array
        Date: DateTime
    }