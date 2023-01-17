module VP.VerseParser

open System
open System.Text
open Spin.Parsers
open Spin

type Book = Book of string

let book () : Parser<Book> =
    parser {
        let! it = many letter
        return Book(String(List.toArray it))
    }