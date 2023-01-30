module VP.VerseParser

open System
open Spin.Parser
open Verse


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


let lines : Parser<LineRange> =
    parser {
        let! from = token natural
        let! _ = token (character '-')
        let! through = natural

        return { From = from; Through = through }
    } |> orElse (natural |> map (fun it -> { From = it; Through = it }))


let verse : Parser<Verse> =
    parser {
        let! book = token book
        let! chapter = token natural
        let! _ = token (character ':')
        let! lines = lines
        return { Book = book; Chapter = chapter; Lines = lines }
    }
