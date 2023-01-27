namespace VP.Test

open System

open Xunit
open FsUnit.Xunit

open VP

module ``Given a Bible verse`` =

    module ``And the book starts with a number`` =

        let verse = "1 Corinthians 10:13"

        [<Fact>]
        let ``When parsing the book`` () =
            let struct (VerseParser.Book (result), rest) =
                VerseParser.book verse
                |> Result.toOption
                |> Option.get

            result |> should equal "1 Corinthians"
            (String.Concat rest) |> should equal " 10:13"

    module ``And the book isn't numbered`` =
        let verse = "Hebrews 11:1"
        
        [<Fact>]
        let ``When parsing the book`` () =
            let struct (VerseParser.Book (result), rest) =
                VerseParser.book verse
                |> Result.toOption
                |> Option.get

            result |> should equal "Hebrews"
            (String.Concat rest) |> should equal " 11:1"

// [<Fact>]
// let ``And the book does not start with a number`` () =
