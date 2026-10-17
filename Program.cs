
using System;
using System.Collections.Generic;

// Steg 1: Basklass som innehåller det alla typer av media har gemensamt.
class Media
{
    public string Titel { get; set; }
    public int Ar { get; set; }

    public Media(string titel, int ar)
    {
        Titel = titel;
        Ar = ar;
    }

    // virtual gör att underklasser kan skriva över metoden.
    public virtual void VisaInfo()
    {
        Console.WriteLine($"{Titel} ({Ar})");
    }
}

// Steg 2: Bok ärver från Media och lägger till en författare.
class Bok : Media
{
    public string Forfattare { get; set; }

    public Bok(string titel, int ar, string forfattare)
        : base(titel, ar)
    {
        Forfattare = forfattare;
    }

    public override void VisaInfo()
    {
        Console.WriteLine($"Bok: {Titel}, år {Ar}, författare: {Forfattare}");
    }
}

// Steg 2: Film ärver från Media och har en speltid.
class Film : Media
{
    public int Speltid { get; set; }

    // Steg 5: En film kan ha flera ljudspår.
    private List<Ljudspar> ljudspar = new List<Ljudspar>();

    public Film(string titel, int ar, int speltid)
        : base(titel, ar)
    {
        Speltid = speltid;
    }

    public void LaggTillLjudspar(Ljudspar spar)
    {
        ljudspar.Add(spar);
    }

    public override void VisaInfo()
    {
        Console.WriteLine($"Film: {Titel}, år {Ar}, speltid: {Speltid} minuter");

        if (ljudspar.Count > 0)
        {
            Console.WriteLine("  Ljudspår i filmen:");

            foreach (Ljudspar spar in ljudspar)
            {
                Console.WriteLine($"  - {spar.Titel} av {spar.Artist}");
            }
        }
    }
}

// Steg 2: Ljudspar är en egen underklass av Media.
class Ljudspar : Media
{
    public string Artist { get; set; }
    public int Langd { get; set; }

    public Ljudspar(string titel, int ar, string artist, int langd)
        : base(titel, ar)
    {
        Artist = artist;
        Langd = langd;
    }

    public override void VisaInfo()
    {
        Console.WriteLine(
            $"Ljudspår: {Titel}, artist: {Artist}, år {Ar}, längd: {Langd} minuter");
    }
}

// Steg 3: Spellista innehåller flera ljudspår.
class Spellista
{
    private List<Ljudspar> spar = new List<Ljudspar>();

    public string Namn { get; set; }

    public Spellista(string namn)
    {
        Namn = namn;
    }

    public void LaggTill(Ljudspar ljudspar)
    {
        spar.Add(ljudspar);
    }

    // Visar alla objekt som finns i spellistan.
    public void VisaAlla()
    {
        Console.WriteLine($"\nSpellista: {Namn}");

        foreach (Ljudspar ljudspar in spar)
        {
            ljudspar.VisaInfo();
        }
    }

    // Steg 5: Räknar ihop den totala speltiden.
    public int TotalSpeltid()
    {
        int total = 0;

        foreach (Ljudspar ljudspar in spar)
        {
            total += ljudspar.Langd;
        }

        return total;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Bok bok = new Bok("Harry Potter och de vises sten", 1997, "J.K. Rowling");

        Film film = new Film("Interstellar", 2014, 169);

        Ljudspar lat1 = new Ljudspar("Blinding Lights", 2019, "The Weeknd", 4);
        Ljudspar lat2 = new Ljudspar("Save Your Tears", 2020, "The Weeknd", 4);
        Ljudspar lat3 = new Ljudspar("Starboy", 2016, "The Weeknd", 4);

        // Steg 5: Filmen får flera ljudspår.
        film.LaggTillLjudspar(lat1);
        film.LaggTillLjudspar(lat2);

        // Steg 4: Samma lista kan innehålla olika typer av Media.
        List<Media> bibliotek = new List<Media>();

        bibliotek.Add(bok);
        bibliotek.Add(film);
        bibliotek.Add(lat1);
        bibliotek.Add(lat2);
        bibliotek.Add(lat3);

        Console.WriteLine("MEDIEBIBLIOTEK");
        Console.WriteLine("==============");

        // Polymorfism: rätt VisaInfo-metod väljs automatiskt
        // beroende på vilken underklass objektet tillhör.
        foreach (Media media in bibliotek)
        {
            media.VisaInfo();
            Console.WriteLine();
        }

        // Steg 3: Skapar en spellista och lägger till ljudspår.
        Spellista minSpellista = new Spellista("Mina favoriter");

        minSpellista.LaggTill(lat1);
        minSpellista.LaggTill(lat2);
        minSpellista.LaggTill(lat3);

        minSpellista.VisaAlla();

        // Steg 5: Skriver ut spellistans totala längd.
        Console.WriteLine(
            $"\nTotal speltid: {minSpellista.TotalSpeltid()} minuter");

        Console.WriteLine("\nTryck på en tangent för att avsluta.");
        Console.ReadKey();
    }
}
