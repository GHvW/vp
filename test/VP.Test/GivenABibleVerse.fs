namespace VP.Test

open System

open Xunit
open FsUnit.Xunit

open Spin
open VP

module ``Given a Bible verse`` =

    module ``And the book starts with a number`` =

        let verse = Location.init "1 Corinthians 10:13"

        [<Fact>]
        let ``When parsing the book`` () =
            let result =
                VerseParser.book verse


            let it = result |> Result.toOption |> Option.get


            it.Item |> should equal (Verse.Book "1 Corinthians")
            it.CharsConsumed |> should equal 13

    module ``And the book isn't numbered`` =
        let verse = Location.init "Hebrews 11:1"

        [<Fact>]
        let ``When parsing the book`` () =
            let it =
                VerseParser.book verse
                |> Result.toOption
                |> Option.get

            it.Item |> should equal (Verse.Book "Hebrews")
            it.CharsConsumed |> should equal 7

    module ``And the verse includes a range of lines`` =

        let verse = Location.init "1 Corinthians 10:1-13"

        [<Fact>]
        let ``When parsing the full verse`` () =
            let result =
                VerseParser.verse verse

            let it =
                result
                |> Result.toOption
                |> Option.get

            it.Item.Book |> should equal (Verse.Book "1 Corinthians")
            it.Item.Chapter |> should equal 10
            it.CharsConsumed |> should equal (verse.Input.Length)

            let lines = it.Item.Lines

            lines.From |> should equal 1 
            lines.Through |> should equal 13

    module ``And the verse has extra spacing between parts`` =
        let verse =  Location.init "1  Corinthians   10 : 1 - 13"
        
        [<Fact>]
        let ``When parsing the full verse`` () =
            let result =
                VerseParser.verse verse

            let it =
                result
                |> Result.toOption
                |> Option.get

            it.Item.Book |> should equal (Verse.Book "1 Corinthians")
            it.Item.Chapter |> should equal 10
            it.CharsConsumed |> should equal (verse.Input.Length)

            let lines = it.Item.Lines

            lines.From |> should equal 1 
            lines.Through |> should equal 13

