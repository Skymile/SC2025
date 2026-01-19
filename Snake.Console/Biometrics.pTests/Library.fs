#nowarn "9"
open Expecto
open FsCheck
open Biometrics.Core

let run (algo: IAlgorithm) (input: byte array) width height =
    let output = Array.zeroCreate<byte> input.Length
    use inputPinned = fixed input
    use outputPinned = fixed output
    algo.Apply(
        inputPinned,
        outputPinned,
        input.Length,
        width * 3,
        width,
        height
    )
    output

let grayscale = run(Grayscale())
let binarize = run(ThresholdBinarization())

let genPixels length = 
    Gen.arrayOfLength length Arb.generate<byte>

let rgbGen =
    gen {
        let! pixels = 
            Gen.choose(1, 1000)
            |> Gen.map genPixels
        return! pixels
    }
        
type RgbGen() = 
    static member RGB(): Arbitrary<byte array> =
        Arb.fromGen rgbGen

let cfg =
    { FsCheckConfig.defaultConfig with 
        maxTest = 100
        arbitrary = [ typeof<RgbGen> ] }

let tests = 
    testList "Biometric Tests" [
        testProperty "Grayscale input unchanged" <| fun (input: byte array) ->
            let copy = input |> Array.copy
            grayscale input 3 3 |> ignore
            
            copy
            |> Array.zip input
            |> Array.forall (fun (a, b) -> a = b)

        testProperty "Grayscale idempotent" <| fun (input: byte array) ->
            let once = grayscale input 3 3
            let twice = grayscale once 3 3

            once
            |> Array.zip twice
            |> Array.forall (fun (a, b) -> a = b)

        testProperty "Binarize idempotent" <| fun (input: byte array) ->
            let once = binarize input 3 3
            let twice = binarize once 3 3

            once
            |> Array.zip twice
            |> Array.forall (fun (a, b) -> a = b)

        testProperty "Grayscale to uniform pixels is unchanging" <| fun (input: byte) (count: byte) ->
            let array = Array.init ((int)count * 3) (fun _ -> input)
            let output = grayscale array 3 3

            output |> Array.forall (fun a -> a = input)
    ]

runTestsWithCLIArgs [] [||] tests |> ignore
