namespace SuperMarioImWorkshop.Kontrakte

type IchBinSuperMario =
    abstract member WirdVonGegnerGetroffen : unit -> IchBinSuperMario
    abstract member FindetFeuerblume : unit -> IchBinSuperMario
    abstract member Schiessen : (string -> unit) -> IchBinSuperMario