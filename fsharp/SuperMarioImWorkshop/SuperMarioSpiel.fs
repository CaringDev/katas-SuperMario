namespace SuperMarioImWorkshop

module SuperMarioSpiel =
    
    open SuperMarioImWorkshop.Kontrakte
    open SuperMarioImWorkshop.Modi
    open SuperMarioImWorkshop.Status
    
    type Spiel =
        private { Leben : IchBinLebendig } with
            member s.StarteAlsKleinerMario () =
                KleinerMario s.Leben :> IchBinSuperMario

    let private toterMario l = ToterMario l :> IchBinSuperMario
    let private kleinerMario l = KleinerMario l :> IchBinSuperMario

    let private unendlich was =
        Seq.initInfinite (fun _ -> was)
    
    let StarteMitVorgegebenerAnzahlLeben anzahl =
        let extraLeben = Seq.replicate 3 kleinerMario
        let begrenztesLeben =
            unendlich toterMario            
            |> Seq.append (Seq.replicate anzahl kleinerMario)
        { Leben = KonfigurierbaresLeben(begrenztesLeben, extraLeben) }
    
    let StarteMitDreiLeben () =
        StarteMitVorgegebenerAnzahlLeben 3