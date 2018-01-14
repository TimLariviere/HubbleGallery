namespace HubbleGalleryFSharp

open System
open FSharp.Data
open HubbleGalleryFSharp.Models


module HubbleDataSource =
    let baseUrl = "https://www.spacetelescope.org/images/feed/"

    type HubbleDataSource = XmlProvider<"HubbleImageGallerySample.xml">

    type CategoryFeeds =
        | All
        | Anniversary
        | Cosmology
        | Exoplanets
        | Galaxies
        | Illustrations
        | JamesWebbSpaceTelescope
        | Mission
        | Miscellaneous
        | Nebulae
        | QuasarsBlackHoles
        | SolarSystem
        | Spacecraft
        | StarClusters
        | Stars

    type Feeds =
        | Top100
        | Category of CategoryFeeds

    type Feed = { Id: string; Name: string }

    let feeds = 
        [ 
            Top100, { Id = "top100"; Name = "Top 100" }
            Category All, { Id = ""; Name = "All" }
            Category Anniversary, { Id = "anniversary"; Name = "Anniversary" }
            Category Cosmology, { Id = "cosmology"; Name = "Cosmology" }
            Category Exoplanets, { Id = "exoplanets"; Name = "Exoplanets" }
            Category Galaxies, { Id = "galaxies"; Name = "Galaxies" }
            Category Illustrations, { Id = "illustrations"; Name = "Illustrations" }
            Category JamesWebbSpaceTelescope, { Id = "jwst"; Name = "James Webb Space Telescope" }
            Category Mission, { Id = "mission"; Name = "Mission" }
            Category Miscellaneous, { Id = "misc"; Name = "Miscellaneous" }
            Category Nebulae, { Id = "nebulae"; Name = "Nebulae" }
            Category QuasarsBlackHoles, { Id = "blackholes"; Name = "Black Holes" }
            Category SolarSystem, { Id = "solarsystem"; Name = "Solar System" }
            Category Spacecraft, { Id = "spacecraft"; Name = "Spacecraft" }
            Category StarClusters, { Id = "starclusters"; Name = "Star Clusters" }
            Category Stars, { Id = "stars"; Name = "Stars" }
        ] |> Map

    let getFeedName feed =
        let feedName = feeds.Item feed
        match feed with
        | Top100 -> feedName.Id
        | Category All -> ""
        | Category c -> "category/" + feedName.Id

    let getFeedFromName name =
        feeds |> Map.findKey (fun k v -> v.Name = name)

    let composeUrl feed =
        baseUrl + (getFeedName feed)

    let createHubblePictures (item: HubbleDataSource.Item) =
        {
            Title = item.Title.Trim()
            Link = item.Link |> Uri
            Description = Html item.Description
            ThumbnailLink = item.Enclosure.Url |> Uri
            ImageLink = item.Enclosure.Url |> Uri
            Date = item.PubDate
        }

    let loadAsync feed = async {
        let url = composeUrl feed
        let! sample = HubbleDataSource.AsyncLoad url
        return (sample.Channel.Items |> Array.map createHubblePictures)
    }