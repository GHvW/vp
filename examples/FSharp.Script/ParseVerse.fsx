#r "../../src/VP/bin/Debug/net6.0/Spin.dll"
#r "../../src/VP/bin/Debug/net6.0/VP.dll"

open VP
open Spin

let parsed = Parser.run VerseParser.verse "1 Corinthians 10:1-13"

let (Verse.Book book) = parsed.Book

let chapter = parsed.Chapter

let (verses, count) =
    let it = parsed.Lines
    ($"{it.From} - {it.Through}", it.Through - it.From + 1)

printfn "Book: %s, Chapter: %d, Verse: %s, line total: %d" book chapter verses count
