namespace HubbleGalleryFSharp

open Xamarin.Forms

type App() =
    inherit Application(MainPage = NavigationPage(MainPage()))

    override this.OnStart() =
        let navigationPage = this.MainPage :?> NavigationPage
        navigationPage.BackgroundColor <- Color.FromHex("#182D40")
        navigationPage.BarBackgroundColor <- Color.Transparent
        navigationPage.BarTextColor <- Color.White