using System;

class Wektor
{
    private readonly double[] _wartosci;
    public int Rozmiar => _wartosci.Length;
    public double[] Wartosci => _wartosci;
    public double Dlugosc => Math.Sqrt(ObliczIloczynSkalarny(this, this));
    public Wektor(int rozmiar)
    {
        if (rozmiar <= 0)
            throw new ArgumentException("Rozmiar wektora musi być dodatni.");
        _wartosci = new double[rozmiar];
    }
    public Wektor(params double[] dane)
    {
        if (dane == null || dane.Length == 0)
            throw new ArgumentException("Wektor nie może być pusty.");
        _wartosci = new double[dane.Length];
        Array.Copy(dane, _wartosci, dane.Length);
    }
    public double this[int i]
    {
        get => _wartosci[i];
        set => _wartosci[i] = value;
    }
    public static double ObliczIloczynSkalarny(Wektor a, Wektor b)
    {
        if (a.Rozmiar != b.Rozmiar)
            return double.NaN;
        double suma = 0;
        for (int i = 0; i < a.Rozmiar; i++)
        {
            suma += a[i] * b[i];
        }
        return suma;
    }
    public static Wektor DodajWektory(params Wektor[] wektory)
    {
        if (wektory.Length == 0)
            throw new ArgumentException("Nie podano żadnych wektorów.");
        int rozmiar = wektory[0].Rozmiar;
        foreach (var w in wektory)
        {
            if (w.Rozmiar != rozmiar)
                throw new ArgumentException("Wektory muszą mieć taki sam rozmiar.");
        }
        double[] suma = new double[rozmiar];
        foreach (var w in wektory)
        {
            for (int i = 0; i < rozmiar; i++)
            {
                suma[i] += w[i];
            }
        }
        return new Wektor(suma);
    }
    public static Wektor operator +(Wektor a, Wektor b)
    {
        if (a.Rozmiar != b.Rozmiar)
            throw new ArgumentException("Wektory muszą mieć taki sam rozmiar.");
        double[] wynik = new double[a.Rozmiar];
        for (int i = 0; i < a.Rozmiar; i++)
        {
            wynik[i] = a[i] + b[i];
        }
        return new Wektor(wynik);
    }
    public static Wektor operator -(Wektor a, Wektor b)
    {
        if (a.Rozmiar != b.Rozmiar)
            throw new ArgumentException("Wektory muszą mieć taki sam rozmiar.");
        double[] wynik = new double[a.Rozmiar];
        for (int i = 0; i < a.Rozmiar; i++)
        {
            wynik[i] = a[i] - b[i];
        }
        return new Wektor(wynik);
    }
    public static Wektor operator *(Wektor v, double s)
    {
        double[] wynik = new double[v.Rozmiar];
        for (int i = 0; i < v.Rozmiar; i++)
        {
            wynik[i] = v[i] * s;
        }
        return new Wektor(wynik);
    }
    public static Wektor operator *(double s, Wektor v) => v * s;
    public static Wektor operator /(Wektor v, double s)
    {
        if (s == 0)
            throw new DivideByZeroException("Nie wolno dzielić przez zero.");
        double[] wynik = new double[v.Rozmiar];
        for (int i = 0; i < v.Rozmiar; i++)
        {
            wynik[i] = v[i] / s;
        }
        return new Wektor(wynik);
    }
    public override string ToString()
    {
        return $"[{string.Join(", ", _wartosci)}]";
    }
    // Główna część programu
    public static void Main()
    {
        int rozmiar = PobierzRozmiar();
        Console.WriteLine("Podaj współrzędne pierwszego wektora:");
        double[] dane1 = WczytajDane(rozmiar);
        Console.WriteLine("Podaj współrzędne drugiego wektora:");
        double[] dane2 = WczytajDane(rozmiar);
        Wektor v1 = new Wektor(dane1);
        Wektor v2 = new Wektor(dane2);
        Console.WriteLine("\n--- Wyniki ---");
        Console.WriteLine($"Wektor 1: {v1}");
        Console.WriteLine($"Wektor 2: {v2}");
        Console.WriteLine($"Suma: {v1 + v2}");
        Console.WriteLine($"Różnica: {v1 - v2}");
        Console.WriteLine($"Iloczyn skalarny: {Wektor.ObliczIloczynSkalarny(v1, v2)}");
        Console.WriteLine($"Długość W1: {v1.Dlugosc:F2}");
        Console.WriteLine($"Wektor 1 pomnożony przez 2: {v1 * 2}");
        Console.WriteLine($"Wektor 2 podzielony przez 2: {v2 / 2}");
        Console.WriteLine($"Suma wielu: {Wektor.DodajWektory(v1, v2)}");
    }
    static int PobierzRozmiar()
    {
        while (true)
        {
            Console.Write("Ile wymiarów mają wektory? ");
            if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
                return n;

            Console.WriteLine("Błędna wartość. Wprowadź liczbę większą od 0.");
        }
    }
    static double[] WczytajDane(int rozmiar)
    {
        while (true)
        {
            Console.WriteLine($"Wprowadź {rozmiar} liczb oddzielonych spacją:");
            string linia = Console.ReadLine();
            string[] czesci = linia.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (czesci.Length != rozmiar)
            {
                Console.WriteLine("Nieprawidłowa liczba współrzędnych.");
                continue;
            }
            double[] wynik = new double[rozmiar];
            bool poprawne = true;
            for (int i = 0; i < rozmiar; i++)
            {
                if (!double.TryParse(czesci[i], out wynik[i]))
                {
                    Console.WriteLine("Błąd w danych liczbowych.");
                    poprawne = false;
                    break;
                }
            }
            if (poprawne)
                return wynik;
        }
    }
}
