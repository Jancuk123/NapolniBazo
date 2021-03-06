﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapolniBazo
{
    class Program
    {
        static void Main(string[] args)
        {
            Entities db = new Entities();
            Random r = new Random();
            
            for (int k = 0; k< 10000; k++)
            {
               var zadnji = (from x in db.Podatkis
                             orderby x.Id descending
                             select x).FirstOrDefault();
                for (int j = 1; j < 13; j++)
                {
                    Podatkis nov = new Podatkis();
                    nov.IdPostaje = j;
                    nov.Cas = zadnji.Cas.AddMinutes(15);
                    int x = r.Next(0, 2);
                    nov.Temp = (decimal)(r.Next(-10, 40) + r.Next(0, 9) / 10.0);
                    nov.Vlaga = (decimal)(r.Next(0, 100) + r.Next(0, 9) / 10.0);
                    if (x == 1)
                        nov.Padavine = r.Next(0, 50);
                    else
                        nov.Padavine = 0;
                    db.Podatkis.Add(nov);
                }
                db.SaveChanges();
                if(k%100==0)
                Console.WriteLine("K je: "+k+", Do konca še: "+(10000-k));
            }
            Console.WriteLine("konec");
            Console.ReadLine();
        }
    }
}
