namespace SuperMarioImWorkshop.Modi

open SuperMarioImWorkshop.Kontrakte 

type KonfigurierbaresLeben(verbleibendeLeben, wennExtraLeben) =
    interface IchBinLebendig with
        member __.Vermindern () =
            let nächstesLeben = verbleibendeLeben |> Seq.head
            let verbleibendesLeben = verbleibendeLeben |> Seq.tail
            KonfigurierbaresLeben(verbleibendeLeben, wennExtraLeben)
            |> nächstesLeben