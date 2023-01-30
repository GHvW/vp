module VP.Verse


type Book = Book of string


type LineRange = { From: int; Through: int }


let count (lineRange: LineRange) : int =
    lineRange.Through - lineRange.From + 1
    

let lineCount range =
    range.Through - range.From


type LineSelection =
    | Single of int
    | Range of LineRange


type Verse = 
    { Book: Book
      Chapter: int
      Lines: LineSelection}
