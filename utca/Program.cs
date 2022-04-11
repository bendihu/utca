namespace utca;

public class Utca
{
    public bool Paros { get; set; }
    public int Szelesseg { get; set; }
    public string Tipus { get; set; }
    public int Hazszam { get; set; }
}
public class Program
{
    static List<Utca> list = new List<Utca>();
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Feladat1();
        Feladat2();
        Feladat3();
        Feladat4();
        Feladat5();
        Feladat6();

        Console.ReadKey();
    }
    private static void Feladat1()
    {
        StreamReader sr = new StreamReader(@"kerites.txt");
        int paros = 2, paratlan = 1;

        while (!sr.EndOfStream)
        {
            string[] line = sr.ReadLine().Split(' ');
            Utca uj = new Utca();

            uj.Paros = line[0] == "0" ? true : false;
            uj.Szelesseg = int.Parse(line[1]);
            uj.Tipus = line[2];

            if (uj.Paros)
            {
                uj.Hazszam = paros;
                paros += 2;
            }
            else if(!uj.Paros)
            {
                uj.Hazszam = paratlan;
                paratlan += 2;
            }

            list.Add(uj);
        }

        sr.Close();
    }
    private static void Feladat2()
    {
        Console.WriteLine("2. feladat");
        Console.WriteLine($"Az eladott telkek száma: {list.Count}\n");
    }
    private static void Feladat3()
    {
        Console.WriteLine("3. feladat");

        var utolso = list.Last();

        if (utolso.Paros) Console.WriteLine("A páros oldalon adták el az utolsó telket.");
        else Console.WriteLine("A páratlan oldalon adták el az utolsó telket.");

        Console.WriteLine($"Az utolsó telek házszáma: {utolso.Hazszam}");
        Console.WriteLine();
    }
    private static void Feladat4()
    {
        Console.WriteLine("4. feladat");

        var paratlan = list.Where(x => !x.Paros).ToList();

        for (int i = 1; i < paratlan.Count - 1; i++)
        {
            if (paratlan[i].Tipus != ":" && paratlan[i].Tipus != "#")
            {
                if (paratlan[i - 1].Tipus == paratlan[i].Tipus || paratlan[i + 1].Tipus == paratlan[i].Tipus)
                {
                    Console.WriteLine($"A szomszédossal egyezik a kerítés színe: {paratlan[i].Hazszam}\n");
                    break;
                }
            }
        }
    }
    private static void Feladat5()
    {
        Console.WriteLine("5. feladat");

        Console.Write("Adjon meg egy házszámot! ");
        int bSzam = int.Parse(Console.ReadLine());

        char[] szinek = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        char[] hasznalt = new char[3];
        bool paros = bSzam % 2 == 0 ? true : false;
        var szures = list.Where(x => x.Paros == paros).ToList();

        for (int i = 0; i < szures.Count; i++)
        {
            if (szures[i].Hazszam == bSzam)
            {
                if (bSzam == szures.First().Hazszam)
                {
                    hasznalt[1] = szures[i].Tipus[0];
                    hasznalt[2] = szures[i + 1].Tipus[0];
                }
                else if (bSzam == szures.Last().Hazszam)
                {
                    hasznalt[0] = szures[i - 1].Tipus[0];
                    hasznalt[1] = szures[i].Tipus[0];
                }
                else
                {
                    hasznalt[0] = szures[i - 1].Tipus[0];
                    hasznalt[1] = szures[i].Tipus[0];
                    hasznalt[2] = szures[i + 1].Tipus[0];
                }

                Console.WriteLine($"A kerítés színe / állapota: {szures[i].Tipus}");
                break;
            }
        }

        Random rand = new Random();
        int szam = 0;

        do
        {
            szam = rand.Next(0, szinek.Length);
        }
        while (szinek[szam] == hasznalt[0] && szinek[szam] == hasznalt[1] && szinek[szam] == hasznalt[2]);

        Console.WriteLine($"Egy lehetséges festési szín: {szinek[szam]}\n");
    }
    private static void Feladat6()
    {
        StreamWriter sw = new StreamWriter(@"utcakep.txt");
        var paratlan = list.Where(x => !x.Paros).ToList();

        foreach (var item in paratlan)
        {
            for (int i = 0; i < item.Szelesseg; i++)
            {
                sw.Write(item.Tipus);
            }
        }
        
        sw.WriteLine();

        foreach (var item in paratlan)
        {
            sw.Write(item.Hazszam);
            int offset = item.Hazszam.ToString().Length;

            for (int i = 0; i < item.Szelesseg - offset; i++)
            {
                sw.Write(" ");
            }
        }

        sw.Close();
    }
}