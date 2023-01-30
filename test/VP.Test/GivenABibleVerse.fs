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
            let struct (Verse.Book (result), rest) =
                VerseParser.book verse
                |> Result.toOption
                |> Option.get

            result |> should equal "1 Corinthians"
            (String.Concat rest) |> should equal " 10:13"

    module ``And the book isn't numbered`` =
        let verse = "Hebrews 11:1"

        [<Fact>]
        let ``When parsing the book`` () =
            let struct (Verse.Book (result), rest) =
                VerseParser.book verse
                |> Result.toOption
                |> Option.get

            result |> should equal "Hebrews"
            (String.Concat rest) |> should equal " 11:1"

    module ``And the verse includes a range of lines`` =

        let it = "1 Corinthians 10:1-13"

        [<Fact>]
        let ``When parsing the full verse`` () =
            let struct (result, rest) =
                VerseParser.verse it
                |> Result.toOption
                |> Option.get

            result.Book |> should equal (Verse.Book "1 Corinthians")
            result.Chapter |> should equal 10
            (Seq.isEmpty rest) |> should be True

            let lines = result.Lines

            lines.From |> should equal 1 
            lines.Through |> should equal 13

    module ``And the verse has extra spacing between parts`` =
        let verse = "1  Corinthians   10 : 1 - 13"
        
        [<Fact>]
        let ``When parsing the full verse`` () =
            let struct (result, rest) =
                VerseParser.verse verse
                |> Result.toOption
                |> Option.get

            result.Book |> should equal (Verse.Book "1 Corinthians")
            result.Chapter |> should equal 10
            (Seq.isEmpty rest) |> should be True

            let lines = result.Lines

            lines.From |> should equal 1 
            lines.Through |> should equal 13

