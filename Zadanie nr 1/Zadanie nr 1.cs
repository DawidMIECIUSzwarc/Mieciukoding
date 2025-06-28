class Osoba
{
    private string imię;
    public string Nazwisko;
    public DateTime? DataUrodzenia = null;
    public DateTime? DataŚmierci = null;
    public Osoba(string imięNazwisko)
    {
        ImięNazwisko = imięNazwisko;
    }
    public string Imię
    {
        get { return imię; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Imię nie może być puste.");
            imię = value;
        }
    }
    public string ImięNazwisko
    {
        get
        {
            return (Imię + " " + Nazwisko).Trim();
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Imię = "";
                Nazwisko = "";
            }
            else
            {
                string[] części = value.Trim().Split(' ');
                Imię = części[0];
                if (części.Length > 1)
                    Nazwisko = części[części.Length - 1];
                else
                    Nazwisko = "";
            }
        }
    }
    public TimeSpan? Wiek
    {
        get
        {
            if (DataUrodzenia == null)
                return null;
            DateTime koniec = DataŚmierci ?? DateTime.Now;
            return koniec - DataUrodzenia;
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.Write("Podaj imię i nazwisko: ");
        string imięNazwisko = Console.ReadLine();
        Osoba osoba = new Osoba(imięNazwisko);
        Console.Write("Podaj datę urodzenia (rrrr-mm-dd): ");
        string tekstData = Console.ReadLine();
        DateTime dataUrodzenia;
        while (!DateTime.TryParse(tekstData, out dataUrodzenia))
        {
            Console.Write("Spróbuj jeszcze raz");
            tekstData = Console.ReadLine();
        }
        osoba.DataUrodzenia = dataUrodzenia;
        Console.WriteLine("\n--- Dane osoby ---");
        Console.WriteLine("Imię: " + osoba.Imię);
        Console.WriteLine("Nazwisko: " + osoba.Nazwisko);
        Console.WriteLine("Imię i nazwisko: " + osoba.ImięNazwisko);
        if (osoba.Wiek != null)
        {
            int lata = osoba.Wiek.Value.Days / 365;
            Console.WriteLine("Wiek (około): " + lata + " lat");
        }
        else
        {
            Console.WriteLine("Wiek: nieznany");
        }
    }
}
