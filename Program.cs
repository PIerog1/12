using System;

public class Produkt
{
    private string nazwa;
    private decimal cena;
    private int iloscNaMagazynie;

    public string Nazwa
    {
        get => nazwa;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nazwa produktu nie może być pusta");
            nazwa = value;
        }
    }

    public decimal Cena
    {
        get => cena;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("Cena musi być większa niż 0.");
            cena = value;
        }
    }

    public int IloscNaMagazynie
    {
        get => iloscNaMagazynie;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Ilość na magazynie nie może być ujemna.");
            iloscNaMagazynie = value;
        }
    }

    public Produkt(string nazwa, decimal cena, int iloscNaMagazynie)
    {
        Nazwa = nazwa;
        Cena = cena;
        IloscNaMagazynie = iloscNaMagazynie;
    }

    public void WyswietlInformacje()
    {
        Console.WriteLine($"Produkt: {Nazwa}");
        Console.WriteLine($"Cena: {Cena} zł");
        Console.WriteLine($"Ilość na magazynie: {IloscNaMagazynie}");
    }

    public decimal ObliczCenePoRabacie(decimal procentRabatu)
    {
        if (procentRabatu < 0 || procentRabatu > 100)
            throw new ArgumentException("Rabat musi być w przedziale 0–100%.");

        decimal rabat = Cena * (procentRabatu / 100);
        return Cena - rabat;
    }

    public bool CzyDostepny()
    {
        return IloscNaMagazynie > 0;
    }

    public void SprzedajSztuki(int ilosc)
    {
        if (ilosc <= 0)
        {
            Console.WriteLine("Nieprawidłowa liczba sztuk do sprzedaży.");
            return;
        }

        if (IloscNaMagazynie >= ilosc)
        {
            IloscNaMagazynie -= ilosc;
            Console.WriteLine($"Sprzedano {ilosc} sztuk produktu {Nazwa}. Pozostało: {IloscNaMagazynie}");
        }
        else
        {
            Console.WriteLine($"Błąd: dostępnych jest tylko {IloscNaMagazynie} sztuk produktu {Nazwa}.");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Produkt produkt = new Produkt("Laptop", 3499.99m, 15);

        produkt.WyswietlInformacje();

        Console.WriteLine("Cena po rabacie 10%: " + produkt.ObliczCenePoRabacie(10));
        Console.WriteLine("Czy dostępny? " + produkt.CzyDostepny());

        produkt.SprzedajSztuki(5);
        produkt.SprzedajSztuki(20);

        Console.WriteLine("Czy dostępny po sprzedaży? " + produkt.CzyDostepny());

        Console.ReadKey();
    }
}