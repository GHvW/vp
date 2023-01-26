namespace VP.Test

open FSharp.Core

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


// [<Fact>]
// let ``And the book does not start with a number`` () =
