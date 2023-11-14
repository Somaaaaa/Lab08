using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Policy;

namespace ZH_Gyakorlas3
{
    internal class Program
    {
        class ZH
        {
            string title;
            string genre;
            string publisher;
            DateTime originalRelease;
            DateTime stadiaRelease;
            public ZH(string input, string genre)
            {
                string[] a = input.Split(';');
                title = a[0];
                this.genre = genre;
                publisher = a[2];
                originalRelease = DateTime.Parse(a[3]);
                stadiaRelease = DateTime.Parse(a[4]);
            }
            public bool publisherTrue(string publisher)
            {
                return this.publisher == publisher;
            }
            public bool sameYear()
            {
                return originalRelease.Year == stadiaRelease.Year;
            }
            public void gamesOut()
            {
                Console.WriteLine($"{title} {genre} {originalRelease}");
            }
            public bool genreTrue(string genre)
            {
                return this.genre == genre;
            }
        }
        static void Main(string[] args)
        {
            string[] genres = File.ReadAllText("genre.txt").Split(',');
            for (int i = 0; i < genres.Length; i++) genres[i] = genres[i].Split('=')[0];

            string[] allLines = File.ReadAllLines("stadia_dataset.csv");
            ZH[] jatekok = new ZH[allLines.Length - 1];

            for (int i = 1; i < allLines.Length; i++)
            {
                jatekok[i - 1] = new ZH(allLines[i], genres[int.Parse(allLines[i].Split(';')[1]) - 1]);
            }

            string answer = "";
            while (answer != "0")
            {
                Console.WriteLine("Adj meg egy számot 1-3 között, 0 ha ki akarsz lépni");
                answer = Console.ReadLine();
                if (answer == "1")
                {
                    string publisher = Console.ReadLine();
                    int count = 0;
                    for (int i = 0; i < jatekok.Length; i++)
                    {
                        if (jatekok[i].publisherTrue(publisher)) count++;
                    }
                    Console.WriteLine(count);
                }
                if (answer == "2")
                {
                    for (int i = 0; i < jatekok.Length; i++)
                    {
                        if (jatekok[i].sameYear()) jatekok[i].gamesOut();
                    }
                }
                if (answer == "3")
                {
                    int count = 0;
                    for (int i = 0; i < genres.Length; i++)
                    {
                        Console.Write($"{genres[i]} ");
                        for (int j = 0; j < jatekok.Length; j++)
                        {
                            if (jatekok[j].genreTrue(genres[i])) count++;
                        }
                        Console.WriteLine(count);
                        count = 0;
                    }
                }
            }
        }
    }
}
