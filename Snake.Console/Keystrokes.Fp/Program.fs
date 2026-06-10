open System
open System.IO
open FsToolkit.ErrorHandling

module Parser =
    let private parser =
        function
        | true, i -> Ok i
        | _ -> Error "Invalid cast!"
    
    let int (value: string) =
        parser <| Int32.TryParse value
    let float (value: string) =
        parser <| Double.TryParse value

type Keystroke =
    { Key        : string
      DwellTime  : float
      FlightTime : float }

module Keystroke =
    let dwellTimes keystrokes =
        keystrokes |> Array.map _.DwellTime
    
    let fromArray =
        function
        | [| key; dwell; flight |] ->
            validation {
                let! dw = Parser.float dwell
                and! fl = Parser.float flight
                
                return
                    { Key = key
                      DwellTime = dw
                      FlightTime = fl }
            }
        | _ -> Error ["Invalid!"]
        
    let fromLine (line: string) =
        line.Split(',',
            StringSplitOptions.RemoveEmptyEntries |||
            StringSplitOptions.TrimEntries
        ) |> fromArray

type Sample =
    { SampleId   : int
      UserId     : string
      Keystrokes : Keystroke array }

module FileHandler =
    let fromFile sampleId (filepath: string) =
        validation {
            let id =
                filepath
                |> Path.GetFileNameWithoutExtension
                |> _.Split('_')
                |> Array.head
                |> _.Substring(1)
                
            let! samples =
                filepath
                |> File.ReadLines
                |> Seq.filter (String.IsNullOrWhiteSpace >> not)
                |> Seq.map Keystroke.fromLine
                |> Seq.traverseResultA (function
                    | Ok o -> Ok o
                    | Error e -> e |> String.concat "\n" |> Error
                )
                |> Result.mapError (String.concat "\n")
                
            return { SampleId = sampleId + 1
                     UserId = id
                     Keystrokes = samples }
        }
    
    let fromDirectory (directory: string) =
        Directory.EnumerateDirectories directory
        |> Seq.mapi fromFile
        |> Seq.traverseResultA (function
            | Ok o -> Ok o
            | Error e -> e |> String.concat "\n" |> Error
        )
        |> Result.mapError (String.concat "\n")
    
module Distances =
    let private distance
            selector
            sum
            (f: float array) (d: float array)
            =
        f
        |> Array.zip d
        |> Array.map selector
        |> sum
    
    let euclidean =
        distance (fun (a, b) -> (a - b) * (a - b)) (Array.sum >> Math.Sqrt)
    let manhattan =
        distance (fun (a, b) -> Math.Abs(a - b)) Array.sum
    let chebyshev =
        distance (fun (a, b) -> Math.Abs(a - b)) Array.max

module Classifiers =
    let knn
        k
        (samples: Sample seq)
        (distance: float array -> float array -> float)=
        seq {
            for current in samples do
                let distance sample =
                    distance
                    <| (current.Keystrokes |> Keystroke.dwellTimes)
                    <| (sample.Keystrokes |> Keystroke.dwellTimes)
                let notEqual sample =
                    sample.SampleId <> current.SampleId

                yield samples
                    |> Seq.filter notEqual
                    |> Seq.sortBy distance
                    |> Seq.take k
                    |> Seq.maxBy _.UserId
                    |> _.UserId
        }

printfn "Hello from F#"
