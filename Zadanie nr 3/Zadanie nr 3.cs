using System;
using System.Text;

abstract class Figura
{
    private (double X, double Y) wspolrzedne;

    public (double X, double Y) Wspolrzedne
    {
        get => wspolrzedne;
        set
        {
            if (double.IsNaN(value.X) || double.IsNaN(value.Y))
                throw new ArgumentException("Nieprawidłowe współrzędne.");
            wspolrzedne = value;
        }
    }

    public abstract double Pole { get; }
    public abstract double Obwod { get; }
    public abstract double Srednica { get; }
}

class Kolo : Figura
{
    private double promien;

    public double Promien
    {
        get => promien;
        set
        {
            if (value <= 0) throw new ArgumentException("Promień musi być dodatni.");
            promien = value;
        }
    }

    public override double Pole => Math.PI * promien * promien;
    public override double Obwod => 2 * Math.PI * promien;
    public override double Srednica => 2 * promien;
}

class Prostokat : Figura
{
    public double BokA { get; set; }
    public double BokB { get; set; }
    public double Orientacja { get; set; }

    public double Kat => 90.0;

    public override double Pole => BokA * BokB;
    public override double Obwod => 2 * (BokA + BokB);
    public override double Srednica => Math.Sqrt(BokA * BokA + BokB * BokB);

    public override string ToString() => $"Prostokąt: A={BokA}, B={BokB}, Orientacja={Orientacja}, Pole={Pole}, Obwod={Obwod}, Średnica={Srednica}";
}

class Deltoid : Figura
{
    public double BokA { get; set; }
    public double BokB { get; set; }
    public double Orientacja { get; set; }

    public double KatAlfa => 60.0;
    public double KatBeta => 120.0;

    public double PrzekatnaDluga => Math.Sqrt(2 * BokA * BokA * (1 - Math.Cos(KatAlfa * Math.PI / 180)));
    public double PrzekatnaKrotka => Math.Sqrt(2 * BokB * BokB * (1 - Math.Cos(KatBeta * Math.PI / 180)));

    public override double Pole => (PrzekatnaDluga * PrzekatnaKrotka) / 2;
    public override double Obwod => 2 * (BokA + BokB);
    public override double Srednica => Math.Max(PrzekatnaDluga, PrzekatnaKrotka);

    public override string ToString() => $"Deltoid: A={BokA}, B={BokB}, KatAlfa={KatAlfa}, KatBeta={KatBeta}, Orientacja={Orientacja}, Przekątne=({PrzekatnaDluga},{PrzekatnaKrotka}), Pole={Pole}, Obwod={Obwod}, Średnica={Srednica}";
}

class Program
{
    static double WczytajLiczbe(string komunikat)
    {
        double wynik;
        while (true)
        {
            Console.Write(komunikat);
            if (double.TryParse(Console.ReadLine(), out wynik) && wynik > 0)
                return wynik;
            Console.WriteLine("Liczba musi być kurcze ten");
        }
    }

    static double WczytajOrientacje(string komunikat)
    {
        double wynik;
        while (true)
        {
            Console.Write(komunikat);
            if (double.TryParse(Console.ReadLine(), out wynik) && wynik >= 0 && wynik < 360)
                return wynik;
            Console.WriteLine("Orientacja musi być liczbą z przedziału [0, 360).");
        }
    }

    static void Main()
    {
        Console.WriteLine("--- Wprowadź dane prostokąta ---");
        var prostokat = new Prostokat();
        prostokat.BokA = WczytajLiczbe("Podaj bok A: ");
        prostokat.BokB = WczytajLiczbe("Podaj bok B: ");
        prostokat.Orientacja = WczytajOrientacje("Podaj orientację: ");
        Console.WriteLine(prostokat);

        Console.WriteLine("\n--- Wprowadź dane deltoidu ---");
        var deltoid = new Deltoid();
        deltoid.BokA = WczytajLiczbe("Podaj bok A: ");
        deltoid.BokB = WczytajLiczbe("Podaj bok B: ");
        deltoid.Orientacja = WczytajOrientacje("Podaj orientację: ");
        Console.WriteLine(deltoid);

        Console.WriteLine("\n--- Wprowadź dane koła ---");
        var kolo = new Kolo();
        kolo.Promien = WczytajLiczbe("Podaj promień: ");
        Console.WriteLine($"Koło: Promień={kolo.Promien}, Pole={kolo.Pole}, Obwód={kolo.Obwod}, Średnica={kolo.Srednica}");
    }
}
