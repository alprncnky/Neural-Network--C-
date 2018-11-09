using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace N_Network_2
{

    class Perceptron
    {
        public float[] weights;
        public double c = 0.01;         // learning rate

        public Perceptron(int n)
        {
            weights = new float[n];
            for (int i = 0; i < weights.Length; i++)
            {
                Random rnd = new Random();
                weights[i] = Random();
            }
        }


        public int feedforward(float[] inputs)
        {
            float sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }
            return activate(sum);       // TODO: activate fonksiyonu yap
        }

        int activate(float sum)
        {
            if (sum > 0) return 1;
            else return -1;
        }

        int Random()
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int x = rndNum.Next(1,3);
            if (x == 2)
                x = 1;
            else
                x = -1;
            return x;
        }


        public void train(float[] inputs, int desired)
        {
            int guess = feedforward(inputs);
            float error = desired - guess;
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] += (float)c * error * inputs[i];
            }
        }
    }



    class Trainer           // Noktolar olustur inputs ve answer olsun sonra Trainer.inputs olarak Perception constructirina yollicaz
    {
        public float[] inputs;
        public int answer;
        Random rnd = new Random();

        public Trainer()
        {
            inputs = new float[3];
            inputs[0] = (float)RandomValue();
            inputs[1] = (float)RandomValue();
            inputs[2] = 1;
            if (f(inputs[0]) > inputs[1])
            {
                answer = 1;
            }
            else
                answer = -1;
        }

        float f(float x)
        {
            return 2 * x + 1;
        }

        int RandomValue()
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));
            int rnd = rndNum.Next(2, 596);
            return rnd;
        }
    }


class Dugum
    {
        public int number = 0;
        public int[] x;
        public int[] y;
        public bool[] usttemi;

        public Dugum()
        {
            x = new int[50];
            y = new int[50];
            usttemi = new bool[50];
        }
    }


class Drawer
{
        Dugum z;

        public Drawer()
        {
            z = new Dugum();
        }

        public void DrawP(int a,int b,bool c)
        {
            z.x[z.number] = a;
            z.y[z.number] = b;
            z.usttemi[z.number] = c;
            z.number = z.number + 1;
        }



        public void EkranaYaz()
        {
            Color renk;
            Bitmap bm2 = new Bitmap(@"graph.png", true);
            Bitmap bm = bm2;
            bm.Save(@"graphresult.png");

            for(int i=2;i<600;i++)
            {
                int lineY = 2 * i + 1;
                if(lineY<596)
                {
                    bm.SetPixel(i, lineY-3, Color.Black);
                    bm.SetPixel(i, lineY-2, Color.Black);
                    bm.SetPixel(i, lineY-1, Color.Black);
                    bm.SetPixel(i, lineY, Color.Black);
                    bm.SetPixel(i, lineY+1, Color.Black);
                    bm.SetPixel(i, lineY+2, Color.Black);
                    bm.SetPixel(i, lineY+3, Color.Black);
                }
            }
            
            for (int i = 0; i < z.number; i++)
            {
                //bm = new Bitmap(@"graphresult.png", true);
                int _X = z.x[i];
                int _Y = z.y[i];
                if (z.usttemi[i] == true)
                {
                    renk = Color.Red;
                }
                else
                {
                    renk = Color.RoyalBlue;
                }
                bm.SetPixel(_X - 2, _Y - 2, renk);
                bm.SetPixel(_X - 1, _Y - 2, renk);
                bm.SetPixel(_X , _Y - 2, renk);
                bm.SetPixel(_X +1, _Y - 2, renk);
                bm.SetPixel(_X +2, _Y - 2, renk);
                bm.SetPixel(_X - 2, _Y - 1, renk);
                bm.SetPixel(_X + 2, _Y - 1, renk);
                bm.SetPixel(_X - 2, _Y, renk);
                bm.SetPixel(_X + 2, _Y, renk);
                bm.SetPixel(_X - 2, _Y +1, renk);
                bm.SetPixel(_X + 2, _Y +1, renk);
                bm.SetPixel(_X - 2, _Y + 2, renk);
                bm.SetPixel(_X + 2, _Y + 2, renk);
                bm.SetPixel(_X - 1, _Y + 2, renk);
                bm.SetPixel(_X, _Y + 2, renk);
                bm.SetPixel(_X+1, _Y + 2, renk);
                bm.SetPixel(_X - 1, _Y - 1, renk);
                bm.SetPixel(_X, _Y - 1, renk);
                bm.SetPixel(_X + 1, _Y-1, renk);
                bm.SetPixel(_X - 1, _Y, renk);
                bm.SetPixel(_X, _Y, renk);
                bm.SetPixel(_X + 1, _Y, renk);
                bm.SetPixel(_X - 1, _Y + 1, renk);
                bm.SetPixel(_X, _Y + 1, renk);
                bm.SetPixel(_X + 1, _Y + 1, renk);

            }
            bm.Save(@"graphresult.png");
            Process.Start(@"graphresult.png");
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            Perceptron p = new Perceptron(3);
            Perceptron p2 = new Perceptron(3);
            Trainer[] training = new Trainer[10];         // <---  Egitim icin yollanan nokta sayisi

            while (!exit)
            {
                Console.WriteLine("Yapay Sinir Noronu Egitimi Programi");
                Console.WriteLine("--- Menu ---");
                Console.WriteLine("1.Norona Ogret");
                Console.WriteLine("2.Ilk Hali ile dene");
                Console.WriteLine("3.Ogretilmis Hali ile dene");
                Console.WriteLine("4.Exit");
                string secim = Console.ReadLine();
                if (secim == "1")
                {
                    for (int i = 0; i < 10; i++)          // <---  Egitim icin yollanan nokta sayisi
                    {
                        training[i] = new Trainer();
                        float[] point = { training[i].inputs[0], training[i].inputs[1], 1 };
                        p.train(point, training[i].answer);
                    }
                    Console.WriteLine("ISLEM BASARIYLA GERCEKLESTIRILDI...");
                }
                else if (secim == "2")
                {
                    Drawer d = new Drawer();
                    bool ustmu;
                    for (int i = 0; i < 50; i++)            // <--- ekran cizilecek nokta sayisi
                    {
                        p2 = new Perceptron(3);
                        training[i] = new Trainer();
                        float[] point = { training[i].inputs[0], training[i].inputs[1], 1 };
                        int result = p2.feedforward(point);
                        if (result == 1)
                            ustmu = true;
                        else
                            ustmu = false;
                        d.DrawP((int)training[i].inputs[0], (int)training[i].inputs[1], ustmu);
                    }
                    d.EkranaYaz();
                }
                else if (secim == "3")
                {
                    Drawer d = new Drawer();
                    bool ustmu;
                    for (int i = 0; i < 50; i++)            // <--- ekran cizilecek nokta sayisi
                    {
                        training[i] = new Trainer();
                        float[] point = { training[i].inputs[0], training[i].inputs[1], 1 };
                        int result = p.feedforward(point);
                        if (result == 1)
                            ustmu = true;
                        else
                            ustmu = false;
                        d.DrawP((int)training[i].inputs[0], (int)training[i].inputs[1], ustmu);
                    }
                    d.EkranaYaz();
                }
                else if(secim=="4")
                {
                    Console.Clear();
                    Console.WriteLine("CIKIS YAPIYORSUNUZ");
                }
                Console.ReadKey();
                Console.Clear();
            }

        }
    }
}
