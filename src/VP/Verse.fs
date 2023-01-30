module VP.Verse


type Book = Book of string


type LineRange = { From: int; Through: int }


let count (lineRange: LineRange) : int =
    lineRange.Through - lineRange.From + 1


type Verse = 
    { Book: Book
      Chapter: int
      Lines: LineRange}
