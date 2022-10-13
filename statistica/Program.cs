using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace statistica
{
    internal class Program
    {

        /// <summary>
        /// Symulacja rzutów kostką, rzucamy kostką 100 razy i sumujemy tylko pary wylowanych liczb 
        /// </summary>
        
        static int[] zbiorSumyOczekZDwochKostek (int liczbaRzutowKostka = 100)
        {
            Random r = new Random();
            int[] wyniki = new int[liczbaRzutowKostka];
            for ( int i = 0; i < liczbaRzutowKostka; i++)
            {
                int pierwszaKostka = r.Next(1, 7);                  ///losujemy liczbę oczek dla pierwszej kostki określając jej zakres
                int drugaKostka = r.Next(1, 7);                     ///losujemy liczbę oczek dla drugiej kostki określając jej zakres
                wyniki[i] = pierwszaKostka + drugaKostka;
            }
            return wyniki;                                          //zwracamy sumę dwóch kostek
        }

        /// <summary>
        /// Agregacja danych - zsumujemy zawartość przesłanej tablicy
        /// </summary>
        /// <param name="args"></param>

        static double suma ( double[] wartosci )
        {
            double suma = 0;                                        ///inicjacja zmeinnej suma o wartosci 0
            foreach (double wartosc in wartosci) suma += wartosc;   ///sumowanie wartosci z przekazanej talbicy
            return suma; 
        }

        /// <summary>
        /// Mając wylosowany zbiór i ich sumę, możemy obliczyć średnią elementów zbioru
        /// </summary>
        /// <param name="args"></param>

        static double srednia (double[] wartosci)
        {
            if (wartosci == null) throw new ArgumentNullException("Przesłano objekt pusty");
            if (wartosci.Length == 0) throw new ArgumentNullException("W tablicy nie ma elementów");
            return suma(wartosci) / wartosci.Length;
        }


        /// <summary>
        /// majac średnią, sumę i zbiór danych mozemy obliczyć rozrzut wartości wokół niej czyli wariancję 
        /// </summary>
        /// <param name="args"></param>

        static double wariancja (double[] wartosci )
        {
            double srednia = Program.srednia(wartosci);
            double wariancia = 0;
            foreach( double wartosc in wartosci )
            {
                double odchylenie = wartosc - srednia;
                wariancia += odchylenie + odchylenie;
            }
            return wariancia / wartosci.Length;
        }

        static double odchyleneiStnd ( double[] wartosci  )
        {
            return Math.Sqrt(wariancja(wartosci));
        }

        /// <summary>
        /// Przeszukiwanie zbioru. Funkcja która okresli jaka jest minimalna i maksymalna wartość zbioru
        /// </summary>
        /// <param name="args"></param>

        static void ekstrema(double[] wartosci, out int indexMaximum, out int indexMinimum )
        {
            if (wartosci == null) throw new ArgumentNullException("Przesłano objekt pusty");
            if (wartosci.Length == 0) throw new ArgumentNullException("W tablicy nie ma elementów");

            indexMinimum = 0;
            indexMaximum = 0;
            int i;
            double minimum = wartosci[0], maximum = wartosci[0];
            for ( i = 1; i < wartosci.Length; i++);
            {
                if (wartosci[i] < minimum)
                {
                    indexMinimum = i;
                    minimum = wartosci[i];
                }
                if (wartosci[i] > maximum)
                {
                    indexMaximum = i;
                    maximum = wartosci[i];
                }
            }
        }
        /// <summary>
        /// metoda ekstrema nie zwraca jednak wartnosci a jedynie indeksy tablicy, co potem łatwo można wyświetlić 
        /// </summary>
        /// <param name="wartosci"></param>
        /// <returns></returns>

        static double zakres (double[] wartosci)
        {
            int indexMinimum, indexMaximum;
            ekstrema(wartosci, out indexMinimum, out indexMaximum);
            return wartosci[indexMaximum] - wartosci[indexMinimum];
        }


        static void Main(string[] args)
        {
            ///zwracamy do konsli wyniki losownia 
            ///wykorzystujemy do tego pętlę foreach

            int[] wartosci = zbiorSumyOczekZDwochKostek(100);
            foreach (int wartosc in wartosci) Console.WriteLine("" + wartosc + "; ");
            Console.ReadLine();

            ///przed wyświelteniem zawartosci zminnej suma musimy dokonać rzutowania z int na double 
            ///w pierwszej kolejnosci generujemy tablicę 100 losowych watosci, następnie dokonujemy konwersji i wyświetlamy sumę. 

            foreach (int wartosc in wartosci) Console.WriteLine(" " + wartosc + "; ");
            double[] tablica = Array.ConvertAll<int, double>(wartosci, i => (double)i);             ///wyrażenie Lambda - użycie operatora konwersji  na każdym elemeńcie
            Console.WriteLine("Liczba elementów: " + tablica.Length);
            Console.WriteLine("Suma = " + suma(tablica));

            Console.ReadLine();

            ///wyswietlamy średnią

            Console.WriteLine("Średnia:  " + srednia(tablica));

            ///wyświetlamy wariancje 

            Console.WriteLine("Wariancja: " + wariancja(tablica));
            Console.WriteLine("Odchylenie Standardowe: " + odchyleneiStnd(tablica));
            Console.ReadLine();

            ///wyświetlanie minimum i maksimum z funkcji ekstrema i zakres
            ///
            int indexMinimum, indexMaximum;
            ekstrema(tablica, out indexMinimum, out indexMaximum);
            Console.WriteLine("Wartosci od " + tablica[indexMinimum] + " do " + tablica[indexMaximum]);
            Console.WriteLine("Zakres: "
                + zakres(tablica));

            Console.ReadLine();
            

        }
    }
}
