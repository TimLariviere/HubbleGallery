namespace HubbleGalleryFSharp

open System
open FSharp.Data
open HubbleGalleryFSharp.Models

type HubbleDataSource = XmlProvider<"HubbleImageGallerySample.xml">

module HubbleDataSource =
    let getThumbnail (item: HubbleDataSource.Item) =
        let imgMatch = System.Text.RegularExpressions.Regex.Match(item.Description, "img src=\"(.*?)\"")
        match imgMatch.Success with
        | true -> imgMatch.Groups.[1].Value
        | false -> ""

    let createHubblePictures (item: HubbleDataSource.Item) =
        let parseDate (str: string) =
            str.Remove(str.Length - 4)
            |> DateTime.Parse
            |> (fun d -> d.AddHours -5.0)

        {
            Title = item.Title.Trim()
            Link = item.Link |> Uri
            Description = Html item.Description
            Author = item.Author
            ImageLink = item |> getThumbnail |> Uri
            Categories = item.Categories |> Array.map (fun x -> Category x)
            Date = parseDate item.PubDate
        }

    let loadAsync () = async {
        let! sample = HubbleDataSource.AsyncLoad "http://hubblesite.org/feed/images/gallery?format=rss"
        return (sample.Channel.Items |> Array.map createHubblePictures)
    }