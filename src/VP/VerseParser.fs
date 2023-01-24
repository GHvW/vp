module VP.VerseParser

open System
open System.Text
open Spin.Parser
open Spin

type Book = Book of string

let regBook : Parser<Book> =
    fun input -> 
        input
        |> (atLeast1 letter 
            |> Parser.map (fun letters -> 
                Book (String (List.toArray letters))))


let numberedBook : Parser<Book> =
    parser {
        let! num = character '1' |> Parser.orElse (character '2')
        and! _ = character ' '
        and! book = atLeast1 letter
        return Book $"{num} {book}"
    }


let book : Parser<Book> =
    fun input ->
        input
        |> (numberedBook |> Parser.orElse regBook)
