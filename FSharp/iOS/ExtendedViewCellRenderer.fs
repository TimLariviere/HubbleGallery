namespace HubbleGalleryFSharp.iOS

open Xamarin.Forms
open Xamarin.Forms.Platform.iOS
open UIKit

// F# version of https://blog.wislon.io/posts/2017/04/11/xamforms-listview-selected-colour
type ExtendedViewCellRenderer() =
    inherit ViewCellRenderer()

    override this.GetCell(item: Cell, reusableCell: UITableViewCell, tv: UITableView) =
        let cell = base.GetCell(item, reusableCell, tv)
        let backgroundView = new UIView()
        backgroundView.BackgroundColor <- Color.Transparent.ToUIColor()
        cell.SelectedBackgroundView <- backgroundView
        cell

module Dummy_ExtendedViewCellRenderer =
    [<assembly: Xamarin.Forms.ExportRenderer(typeof<Xamarin.Forms.ViewCell>, typeof<ExtendedViewCellRenderer>)>]
    do ()

