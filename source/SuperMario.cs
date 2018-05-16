﻿using System;
using System.Collections.Generic;

namespace SuperMarioRefactoring
{
  public class SuperMario
  {
    private readonly Dictionary<Status, Func<SuperMario>> _representationen =
      new Dictionary<Status, Func<SuperMario>>();

    //API stabil halten
    public SuperMario() : this(3, Status.Klein)
    {
    }

    //Nicht öffentlich, um falsche Nutzung zu vermeiden
    //Vorteil durch öffentliche Factory-Methode: Keine Anpassung am Client notwendig
    private SuperMario(int anzahlLeben, Status status)
    {
      Status = status;
      AnzahlLeben = anzahlLeben;

      ErzeugeStatusübergängeBeiTreffern();
    }

    private void ErzeugeStatusübergängeBeiTreffern()
    {
      _representationen.Add(Status.Tot, 
        () => new SuperMario(AnzahlLeben, Status.Tot));

      _representationen.Add(Status.MitFeuerblume, 
        () => new SuperMario(AnzahlLeben, Status.MitPilz));

      _representationen.Add(Status.MitPilz,
        () => new SuperMario(AnzahlLeben, Status.Klein));

      _representationen.Add(Status.Klein,
        () =>
        {
          VermindereLeben();
          return new SuperMario(AnzahlLeben, Status);
        });
    }

    internal Status Status { get; private set; }
    internal int AnzahlLeben { get; private set; }
    internal bool BesitztYoshi { get; private set; }

    //Factory-Pattern
    public static SuperMario StarteMitLeben(int anzahlLeben)
    {
      return new SuperMario(anzahlLeben, Status.Klein);
    }

    public SuperMario WirdVonGegnerGetroffen()
    {
      if (BesitztYoshi)
      {
        BesitztYoshi = false;
        return this;
      }


      if (_representationen.ContainsKey(Status))
        return _representationen[Status]();


      throw new InvalidOperationException();
    }

    private void VermindereLeben()
    {
      if (AnzahlLeben == 0) Status = Status.Tot;

      AnzahlLeben -= 1;
    }

    public SuperMario FindetLeben()
    {
      if (Status != Status.Tot)
      {
        AnzahlLeben += 1;
        return this;
      }

      Status = Status.Klein;
      AnzahlLeben = 0;

      return this;
    }

    public SuperMario FindetPilz()
    {
      if (Status == Status.Tot)
        return this;

      if (Status == Status.MitFeuerblume)
        return this;

      Status = Status.MitPilz;

      return this;
    }

    public SuperMario FindetFeuerblume()
    {
      if (Status == Status.Tot)
        return this;

      Status = Status.MitFeuerblume;

      return this;
    }

    public SuperMario FindetYoshi()
    {
      BesitztYoshi = true;
      return this;
    }

    public SuperMario FälltInLoch()
    {
      BesitztYoshi = false;
      Status = Status.Klein;

      VermindereLeben();
      return this;
    }
  }

  internal enum Status
  {
    Tot,
    Klein,
    MitPilz,
    MitFeuerblume
  }
}