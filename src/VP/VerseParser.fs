module VP.VerseParser

open System
open Spin.Parser
open Verse


let regBook : Parser<Book> =
    atLeast1 letter 
    |> map (String.Concat >> Book)


let private oneOrTwo = 
    character '1' 
    |> orElse (character '2')


let numberedBook : Parser<Book> =
    token oneOrTwo
    |> map (fun num name -> Book $"{num} {name}")
    |> apply (word |> map String.Concat)


let book : Parser<Book> =
    numberedBook |> orElse regBook 


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

let parse (input: string) : Verse =
    match verse input with
    | Ok struct (verse, _) -> verse
    | Error e -> raise (Exception(e.Message))
