namespace SuperMarioImWorkshop.Status

open SuperMarioImWorkshop.Kontrakte

type MarioMitPilz(leben : IchBinLebendig) =
    interface IchBinSuperMario with
        member s.WirdVonGegnerGetroffen () =
            KleinerMario leben :> IchBinSuperMario
        member s.FindetFeuerblume () =
            MarioMitFeuerblume leben :> IchBinSuperMario
        member s.Schiessen _ =
            s :> IchBinSuperMario

and MarioMitFeuerblume(leben : IchBinLebendig) =
     interface IchBinSuperMario with
        member s.WirdVonGegnerGetroffen () =
            MarioMitPilz leben :> IchBinSuperMario
        member s.FindetFeuerblume () =
            s :> IchBinSuperMario
        member s.Schiessen mit =
            mit "Feuer"
            s :> IchBinSuperMario

and KleinerMario(leben : IchBinLebendig) =
    interface IchBinSuperMario with
        member s.WirdVonGegnerGetroffen () =
            leben.Vermindern()
        member s.FindetFeuerblume () =
            MarioMitFeuerblume leben :> IchBinSuperMario
        member s.Schiessen _ =
            s :> IchBinSuperMario

type ToterMario(leben : IchBinLebendig) =
    interface IchBinSuperMario with
        member s.WirdVonGegnerGetroffen () =
            s :> IchBinSuperMario
        member s.FindetFeuerblume () =
            s :> IchBinSuperMario
        member s.Schiessen _ =
            s :> IchBinSuperMario