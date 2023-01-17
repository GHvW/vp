module VP.VerseParser

open System
open System.Text
open Spin.Parser
open Spin

type Book = Book of string

let book () : Parser<Book> =
    parser {
        let! num = Parser.numeric
        let! it = many letter
        return Book(String(List.toArray it))
    }
