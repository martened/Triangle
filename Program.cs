using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// using System.Text.Json;
// using System.Text.Json.Serialization;

namespace mySpase
{
    class Programm
    {
        
        static void Main()
        {
            bool useRandom;  
            //int count = 0; 
            int nm = 0;
            int nb = 0;  
            //int em = 0;  //element of list
            float x = 0;
            float y = 0;
            Point p;
            Triangle t1;
            Triangle t2;
            List<Point> points = new List<Point>();
            Random random = new();

            Console.WriteLine("Самі обираєте координати вершин трикутників? T/N/J  (J = Jsonfile)");
            string answer = Console.ReadLine();
            //Console.WriteLine(answer);
            if (answer.Equals("j", StringComparison.OrdinalIgnoreCase))
            {
                string json_tr1_ = @"/home/marina/Desktop/kpi/prog/Json_files/lab3_2_1.json";
                string jsonString = File.ReadAllText(json_tr1_);
                t1 = JsonConvert.DeserializeObject<Triangle>(jsonString);
                t1.Init();
                string json_tr2_ = @"/home/marina/Desktop/kpi/prog/Json_files/lab3_2_2.json";
                string jsonString_ = File.ReadAllText(json_tr2_);
                t2 = JsonConvert.DeserializeObject<Triangle>(jsonString_);
                t2.Init();
            }
            else
            {    
                if (string.IsNullOrEmpty(answer) || answer.Equals("y", StringComparison.OrdinalIgnoreCase))  
                {
                    useRandom = false;
                    //Console.WriteLine("false");
                }
                else
                {
                    if(answer.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        useRandom = true;
                    }
                    else
                    {
                        return;
                        Console.WriteLine("такого варіанту не існує ");
                    }
                    //Console.WriteLine("true");
                }
                // useRandom = !(string.IsNullOrEmpty(answer) || answer.Equals("y", StringComparison.OrdinalIgnoreCase))

                if (useRandom)
                {
                    try
                    {
                        Console.WriteLine("введить найменьше число діапазону: ");
                        nm = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("введить найбільше число діапазону: ");
                        nb = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("error");
                        return;
                    }
                }
                //Console.WriteLine(nm + nb);

                if(!useRandom)
                {
                    // while(true)
                    for(int i = 0; i < 6; i++)
                    {
                        try
                        {
                            Console.WriteLine("Введіть x: ");
                            x = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Введіть y: ");
                            y = Convert.ToInt32(Console.ReadLine());
                            p = new Point (x,y);
                            points.Add(p); 
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                else
                {
                    for(int i = 0; i < 6; i++)
                    {
                        x = random.Next(nm, nb);
                        y = random.Next(nm, nb);
                        p = new Point (x,y);
                        //Console.WriteLine(p);
                        points.Add(p);
                    }
                }

                t1 = new Triangle(points.GetRange(0,3));
                t2 = new Triangle(points.GetRange(3,3));
            }//end else

            if(t1.Validate() != true && t2.Validate() != true)
            {
                Console.WriteLine("error");
                return;
            }
            Console.WriteLine("Що вам треба порівняти? \n1.площі \n2.периметри \n3.висоти \n4.медіани \n5.бісекртиси \n6.радіуси вписаних кіл \n7.радіуси описаних кіл");
            string answer2 = Console.ReadLine();   
            //Console.WriteLine(answer2);

            for(int i = 0; i < answer2.Length; i++)
            {
                try
                {
                    switch(answer2[i])
                    {
                        case '1':
                            float S1 = t1.Area();
                            float S2 = t2.Area(); 
                            Console.WriteLine("S1 = " + S1);
                            Console.WriteLine("S2 = " + S2 + "\n");
                            if(S1 == S2)
                            {
                                Console.WriteLine("площі рівні \n");
                            }
                            else
                            {
                                if(S1 < S2)
                                {
                                    Console.WriteLine("площа першого трикутника більша \n");
                                    
                                }
                                else
                                {
                                    Console.WriteLine("площа другого трикутника більша \n");
                                }
                            }
                            break;
                        case '2':
                            float P1 = t1.Perimetr();
                            float P2 = t2.Perimetr();
                            Console.WriteLine("P1 = " + P1);
                            Console.WriteLine("P2 = " + P2);
                            if(P1 == P2)
                            {
                                Console.WriteLine("периметри рівні \n");
                            }
                            else
                            {
                                if(P1 > P2)
                                {
                                    Console.WriteLine("периметр першого трикутника більший \n");
                                    
                                }
                                else
                                {
                                    Console.WriteLine("периметр другого трикутника більший \n");
                                }
                            }

                            break;
                        case '3':
                            float[] h1 = t1.Height();
                            float[] h2 = t2.Height();
                            Print_array("h1", h1);
                            Print_array("h2", h2);
                            Array.Sort(h1, 0, 3);
                            Array.Sort(h2, 0, 3);
                            if(h1.SequenceEqual(h2))
                            {
                                Console.WriteLine("всі висоти рівні між собою \n");
                            }
                            else
                            {
                                Console.WriteLine("не всі висоти рівні між собою \n");
                            }
                            break;
                        case '4':
                            float[] M1 = t1.Medians();
                            float[] M2 = t2.Medians();
                            Print_array("M1", M1);
                            Print_array("M2", M2);
                            Array.Sort(M1, 0, 3);
                            Array.Sort(M2, 0, 3);
                            if(M1.SequenceEqual(M2))
                            {
                                Console.WriteLine("всі медіани рівні між собою \n");
                            }
                            else
                            {
                                Console.WriteLine("не всі медіани рівні між собою \n");
                            }
                            break;
                        case '5':
                            float[] l1 = t1.Bisectors();
                            float[] l2 = t2.Bisectors();
                            Print_array("l1", l1);
                            Print_array("l2", l2);
                            Array.Sort(l1, 0, 3);
                            Array.Sort(l2, 0, 3);
                            if(l1.SequenceEqual(l2))
                            {
                                Console.WriteLine("всі бісектриси рівні між собою \n");
                            }
                            else
                            {
                                Console.WriteLine("не всі бісектриси рівні між собою \n");
                            }
                            break;
                        case '6':
                            float r1 = t1.Radius_s();
                            float r2 = t2.Radius_s();
                            Console.WriteLine("r1 = " + r1);
                            Console.WriteLine("r2 = " + r2);
                            if(r1 == r2)
                            {
                                Console.WriteLine("вписані радіуси рівні \n");
                            }
                            else
                            {
                                if(r1 > r2)
                                {
                                    Console.WriteLine("вписаний радіус першого трикутника більший \n");
                                    
                                }
                                else
                                {
                                    Console.WriteLine("вписаний радіус другого трикутника більший \n");
                                }
                            }
                            break;
                        case '7':
                            float R1 = t1.Radius_b();
                            float R2 = t2.Radius_b();
                            Console.WriteLine("R1 = " + R1);
                            Console.WriteLine("R2 = " + R2);
                            if(R1 == R2)
                            {
                                Console.WriteLine("описані радіуси рівні \n");
                            }
                            else
                            {
                                if(R1 > R2)
                                {
                                    Console.WriteLine("описаний радіус першого трикутника більший \n");
                                    
                                }
                                else
                                {
                                    Console.WriteLine("описаний радіус другого трикутника більший \n");
                                }
                            }
                            break;
                        
                    }//end switch
                }  
                catch
                {
                    Console.WriteLine("error");
                    break;

                }  
            }//end for
            string trng1 = t1.Type_triangle();
            Console.WriteLine("перший трикутник " + trng1);
            string trng2 = t2.Type_triangle();
            Console.WriteLine("другий трикутник " + trng2);

            Console.WriteLine("Бажаєте повернути трикутники відносно однієї з вершин(1), чи відносно центру описаного кола(2)");
            int answ = Convert.ToInt32(Console.ReadLine());
            if(answ == 1)
            {
                t1.Print_triangle();
                t2.Print_triangle();
                Point O;
                Console.WriteLine("Оберіть вершину за точку обертання: \n1\n2\n3");
                int num = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("На скільки треба повернути трикутник?\n");
                float w = Convert.ToInt32(Console.ReadLine());
                float w_ = ((float)Math.PI * w) / 180;
                O = t1.vertexes[num-1]; 
                Triangle tn1 = t1.RotateTriangle(O, w_);
                Triangle tn2 = t2.RotateTriangle(O, w_);
                tn1.Print_triangle();
                tn2.Print_triangle();
            }
            if(answ == 2)
            {
                t1.Print_triangle();
                t2.Print_triangle();
                Point O;
                Console.WriteLine("На скільки треба повернути трикутник?\n");
                float w = Convert.ToInt32(Console.ReadLine());
                float w_ = ((float)Math.PI * w) / 180;
                O = t1.CircumscribedCircle();
                Triangle tn1 = t1.RotateTriangle(O, w_);
                Triangle tn2 = t2.RotateTriangle(O, w_);
                tn1.Print_triangle();
                tn2.Print_triangle();
            }
            string json_tr1 = JsonConvert.SerializeObject(t1);
            string filePath1 = @"/home/marina/Desktop/kpi/prog/Json_files/lab3_2_1.json";
            File.WriteAllText(filePath1, json_tr1);
            string json_tr2 = JsonConvert.SerializeObject(t2);
            string filePath2 = @"/home/marina/Desktop/kpi/prog/Json_files/lab3_2_2.json";
            File.WriteAllText(filePath2, json_tr2);

        }//end Main
        static void Print_array(string str, float[] arr)
        {
            Console.Write(str + " = ");
            foreach (var el in arr)
            {
                Console.Write(el);
                Console.Write("  ");
            }
            Console.Write("\n");
        }
    }
    
    class Point   
    {
        public float x, y;
        public Point()
        {
            x = 0;
            y = 0;
        }

        public Point(float x_ , float y_)
        {
            x = x_;
            y = y_;
        }
        public bool Compare (Point P2)   //перевірка чи є дві точки трикутника повністю рівними між собою
        {
            if (x == P2.x && y == P2.y)
            {
                return(true);
            }   
            return(false);
        }
        public void Print_point()
        {
            Console.WriteLine("x = " + x + "\t" + "y = " + y);
        }
    }

    class Vector : Point
    {

        public Vector(float x_ , float y_) : base (x_, y_)
        {
            
        }
        public Vector(Point P) : base (P.x, P.y)  //робить вектор y точці (0,0) => P(x,y)
        {

        }
        // public void Substract(Vector V)     //віднімання векторів
        // {
        //     x -= V.x;
        //     y -= V.y;
        // }
        public Vector(Point P1, Point P2)      //рахуємо вектор із двох точок
        {
            x = P2.x - P1.x;
            y = P2.y - P1.y;
        }
        public bool IsColinear(Vector V2)
        {
            if (Math.Abs(x * V2.y) == Math.Abs(y * V2.x))
            {
                Console.WriteLine("деякі вектори колінеарні");
                return(true);
            }
            return(false);
        }

        public float Lenght ()  //длина вектора
        {
            return((float)Math.Sqrt(x * x + y * y));
        }
        public float cosAlfa(Vector V2)  //cos альфа для розрахунків
        {
            return((x * V2.y + y * V2.x) / (Lenght() * V2.Lenght()));   
        }
        public float sinAlfa(Vector V2)  //sin альфа для розрахунків
        {
            float cos_alfa = cosAlfa(V2);
            return((float)Math.Sqrt(1 - (cos_alfa * cos_alfa)));
        }
        public void Rotate(float w)   //розрахунок нових координат вершин повернутих на кут w відносно одної з вершин
        {
            float x_ = x * (float)Math.Cos(w) - y * (float)Math.Sin(w);
            float y_ = x * (float)Math.Sin(w) - y * (float)Math.Cos(w);
            x = x_;
            y = y_;
        }
        public Point RelativeEndPoint(Point P1)  
        {
            Point P2 = new Point();
            P2.x = x + P1.x;
            P2.y = y + P1.y;
            return(P2);
        }
    }

    class Triangle 
    {
        public Point[] vertexes; //вершини трикутника
        float[] sin_angles;   //кути трикутника
        Vector[] vectors; //вектори трикутника
        float[] sides;    //сторони трикутника

        public Triangle()
        {
            vertexes = new Point[3];
        }
        public Triangle(List<Point> vertex)
        {
            vertexes = vertex.ToArray();
            Init();   
        }

        public bool Init()
        {
            if (!Validate())
            {
                return(false);
            }
            vectors = new Vector[3];
            vectors[0] = new Vector(vertexes[0], vertexes[1]);
            vectors[1] = new Vector(vertexes[1], vertexes[2]);
            vectors[2] = new Vector(vertexes[2], vertexes[0]);
            sides = new float[3];
            sin_angles = new float[3];
            for (int i = 0; i < 3; i++)
            {
                sides[i] = vectors[i].Lenght();
                sin_angles[i] = vectors[i].sinAlfa(vectors[(i + 1) % 3]);
            }
            return(true);
        }
        public bool Validate()  //
        {
            if (vertexes[0].Compare(vertexes[1]) || 
                vertexes[1].Compare(vertexes[2]) || 
                vertexes[2].Compare(vertexes[0]))
            {
                return(false);
            }

            Vector vec1 = new Vector(vertexes[0], vertexes[1]);
            Vector vec2 = new Vector(vertexes[0], vertexes[2]);
            if (vec1.IsColinear(vec2))
            {
                return(false);
            }
            return(true);
        }

        public void Print_triangle()
        {
            for(int i = 0; i < 3; i++)
            {
                vertexes[i].Print_point();
            }
        }


        public float Area()  //метод для розрахунку площі
        {
            float S = (((sides[0]) * (sides[1]) * (vectors[0].sinAlfa(vectors[1]))) / 2.0F);
            return((float)Math.Round(S, 5));
        }

        public float Perimetr()   //періметр
        {
            float P = (sides[0] + sides[1] + sides[2]);
            return((float)Math.Round(P, 5));
        }

        public float[] Height()    //висота
        {
            float[] h = new float[3];
            for(int i = 0; i < 3; i++)
            {
                h[i] = (float)Math.Round((sides[(i + 1) % 3] * (sin_angles[i])), 5);

                //h[i] = (sides[i] * (vectors[(i + 1) % 3].sinAlfa(vectors[(i + 2) % 3])));
            }
            return(h);
        }

        public float[] Medians()    //медіани
        {
            float[] m = new float[3];
            for(int i = 0; i < 3; i++)
            {
                m[i] = (float)Math.Round(((float)(Math.Sqrt(sides[(i + 1) % 3] * 2*sides[(i + 1) % 3] +
                                                            sides[(i + 2) % 3] * 2*sides[(i + 2) % 3] - 
                                                            sides[i] * sides[i]))/2.0F), 5);
            }

            return(m);
        }

        public float[] Bisectors()      //бісектриси
        {
            float[] l = new float[3];
            for(int i = 0; i < 3; i++)
            {
                l[i] = (float)Math.Round((2 * sides[(i + 2) % 3] * sides[(i + 1) % 3] * (vectors[(i + 1) % 3].sinAlfa(vectors[(i + 2) % 3]))), 5);
            }
            return(l);
        }

        public float Radius_s()        //small radius 
        {
            float r = ((2 * Area()) / (sides[0] + sides[1] + sides[2]));
            return((float)Math.Round(r, 5));
        }  

        public float Radius_b()       //big radius 
        {
            float R = ((sides[0]) / (2 * vectors[1].sinAlfa(vectors[2])));
            return((float)Math.Round(R, 5));
        }  

        public string Type_triangle()
        {
            string trng = "";
            if (sides[0] == sides[1] || sides[1] == sides[2] || sides[0] == sides[2])
            {
                //Console.WriteLine("трикутник рівнобедрений ");
                trng += "рівнобедрений ";
            }
            for(int i = 0; i < 3; i++)
            {
                
                float cos_angle = vectors[i].cosAlfa(vectors[(i + 1) % 3]);
                if(cos_angle < 0)
                {
                    trng += "тупокутний ";
                }
                if(cos_angle == 0)
                {
                    trng += "прямокутний ";
                }
            }
            if (sides[0] == sides[1] && sides[1] == sides[2] && sides[0] == sides[2])
            {
                trng += "рівносторонній ";
            }
            return(trng);
        }
        
        public Point CircumscribedCircle()
        {
            float x;
            float y;
            x =((vertexes[0].y - vertexes[2].y) * ((float)Math.Pow(vertexes[1].x, 2) + (float)Math.Pow(vertexes[1].y, 2)) +
                (vertexes[1].y - vertexes[0].y) * ((float)Math.Pow(vertexes[2].x, 2) + (float)Math.Pow(vertexes[2].y, 2)) +
                (vertexes[2].y - vertexes[1].y) * ((float)Math.Pow(vertexes[0].x, 2) + (float)Math.Pow(vertexes[0].x, 2))) 
                /
                (2 * (vertexes[0].y * (vertexes[2].x - vertexes[1].x) + vertexes[1].y *
                (vertexes[0].x - vertexes[2].x) + vertexes[2].y * (vertexes[1].x - vertexes[0].x)));

            y =((vertexes[2].x - vertexes[0].x) * ((float)Math.Pow(vertexes[1].x, 2) + (float)Math.Pow(vertexes[1].y, 2)) +
                (vertexes[0].x - vertexes[1].x) * ((float)Math.Pow(vertexes[2].x, 2) + (float)Math.Pow(vertexes[2].y, 2)) +
                (vertexes[1].x - vertexes[2].x) * ((float)Math.Pow(vertexes[0].x, 2) + (float)Math.Pow(vertexes[0].x, 2))) 
                /
                (2 * (vertexes[0].y * (vertexes[2].x - vertexes[1].x) + vertexes[1].y *
                (vertexes[0].x - vertexes[2].x) + vertexes[2].y * (vertexes[1].x - vertexes[0].x)));

            Point P = new Point(x,y);
            return(P);
        }
        public Triangle RotateTriangle(Point O, float w)  //O - точка обертання; w - кут повороту
        {
            var newVertexes = new List<Point>{};
            Triangle tn;
            for(int i = 0; i < 3; i++)
            {
                Vector Vn = new Vector(O, vertexes[i]);
                Vn.Rotate(w);
                Point Pn = Vn.RelativeEndPoint(O);
                newVertexes.Add(Pn);
            }
            tn = new Triangle(newVertexes);
            return(tn);
        }
    }
}