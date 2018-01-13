namespace Xamarin.Forms.FSharp

open System
open Xamarin.Forms

module Utils =
    let beginInvokeOnMainThread f =
        Device.BeginInvokeOnMainThread (Action f)