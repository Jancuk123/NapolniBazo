using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace NapolniBazo
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        static void Main(string[] args)
        {
            aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;
            
            Console.ReadLine();
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Entities db = new Entities();
            Random r = new Random();

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
                db.SaveChanges();
                Console.WriteLine("Dodan: "+nov.IdPostaje+" - "+nov.Cas+" - "+nov.Vlaga + "% - " + nov.Temp + "°C - " + nov.Padavine+"mm");
            }
            
            
        }
    }
}
