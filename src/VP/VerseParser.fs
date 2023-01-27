module VP.VerseParser

open System
open System.Text
open Spin.Parser
open Spin

type Book = Book of string

type LineRange = { From: int; Through: int }

let lineCount range =
    range.Through - range.From

type LineSelection =
    | Single of int
    | Range of {| From: int; Through: int |}


type Verse = 
    { Book: Book
      Chapter: int
      Lines: LineSelection}

let regBook : Parser<Book> =
    fun input -> 
        (atLeast1 letter 
        |> map (String.Concat >> Book)) input


let private oneOrTwo = character '1' |> orElse (character '2')


let numberedBook : Parser<Book> =
    fun input ->
        (succeed (fun num name -> Book $"{num} {name}")
        |> apply (token oneOrTwo)
        |> apply (word |> map String.Concat)) input


let book : Parser<Book> =
    fun input ->
        (numberedBook |> orElse regBook) input


let lines : Parser<LineSelection> =
    parser {
        let! from = (token numeric) |> map Convert.ToInt32
        let! _ = token (character '-')
        let! through = numeric |> map Convert.ToInt32

        return Range {| From = from; Through = through |}
    } |> orElse (numeric |> map (Convert.ToInt32 >> Single))


let verse : Parser<Verse> =
    parser {
        let! book = token book
        let! chapter = (token numeric) |> map Convert.ToInt32
        let! _ = token (character ':')
        let! lines = lines
        return { Book = book; Chapter = chapter; Lines = lines }
    }
