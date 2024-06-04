using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Практическая_работа26_1_
{
    internal class Program
    {
        static void Main(string[] args)
        {
        c:
            Console.Write("\nВведите номер задания:");
            int N = int.Parse(Console.ReadLine());
            switch (N)
            {
                case 1:
                    {
                        Console.Write("Введите какую задачу вывести 1 или 2: ");
                        int n = int.Parse(Console.ReadLine());
                        switch (n)
                        {
                            case 1:
                                {
                                    Console.Write("Введите первую дату (ДДММГГГГ):");
                                    string перваяДатаВвод = Console.ReadLine();
                                    Console.Write("Введите вторую дату (ДДММГГГГ):");
                                    string втораяДатаВвод = Console.ReadLine();
                                    // Парсинг дат
                                    DateTime перваяДата = DateTime.ParseExact(перваяДатаВвод, "ddMMyyyy", null);
                                    DateTime втораяДата = DateTime.ParseExact(втораяДатаВвод, "ddMMyyyy", null);

                                    int разницаМесяцев = ВычислениеМесяцев(перваяДата, втораяДата);
                                    Console.WriteLine($"Количество месяцев между двумя датами: {разницаМесяцев}");
                                }
                                break;
                            case 2:
                                {
                                    Console.Write("Введите текущую дату (ДДММГГГГ): ");
                                    string текущаяДата = Console.ReadLine();
                                    Console.Write("Введите текущее время (ЧЧ:ММ:СС): ");
                                    string текущееВремя = Console.ReadLine();
                                    Console.Write("Введите длительность стирки (в минутах): ");
                                    int продолжВМин = int.Parse(Console.ReadLine());
                                    // Преобразование строки в DateTime
                                    string datetimeстрока = $"{текущаяДата} {текущееВремя}";
                                    DateTime началоDateTime = DateTime.ParseExact(datetimeстрока, "ddMMyyyy HH:mm:ss", CultureInfo.InvariantCulture);

                                    DateTime конецDateTime = началоDateTime.AddMinutes(продолжВМин);

                                    Console.WriteLine("Дата и время окончания стирки: " + конецDateTime.ToString("ddMMyyyy HH:mm:ss"));
                                }
                                break;
                        }
                    }
                    goto c;
                case 2:
                    {
                        List<Заказ> заказы = new List<Заказ>();
                        заказы.Add(new Заказ
                        {
                            номерЗаказа = 1,
                            датаЗаказа = DateTime.ParseExact("01122023", "ddMMyyyy", CultureInfo.InvariantCulture),
                            имяКлиента = "Иванов Иван Иванович",
                            адресКлиента = "ул. Ленина 10",
                            деньВыволнения = 10,
                            стоимость = 15000.50m
                        });
                        заказы.Add(new Заказ
                        {
                            номерЗаказа = 2,
                            датаЗаказа = DateTime.ParseExact("15052023", "ddMMyyyy", CultureInfo.InvariantCulture),
                            имяКлиента = "Петров Петр Петрович",
                            адресКлиента = "ул. Энгельса 5",
                            деньВыволнения = 5,
                            стоимость = 8000.75m
                        });
                        Console.WriteLine("Список всех заказов с датой выполнения:");
                        foreach (var заказ in заказы)
                        {
                            Console.WriteLine(заказ);
                        }
                        Console.WriteLine("\nВведите номер месяца (1-12):");
                        int номерМесяца = int.Parse(Console.ReadLine());
                        var заказыВМесяце = заказы.Where(o => o.датаЗаказа.Month == номерМесяца).ToList();
                        Console.WriteLine($"\nЗаказы, сделанные в {номерМесяца} месяце:");
                        if (заказыВМесяце.Count > 0)
                        {
                            foreach (var заказ in заказыВМесяце)
                            {
                                Console.WriteLine(заказ);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Нет заказов в указанном месяце.");
                        }
                    }
                    goto c;
                case 3:
                    {
                        string fileName = "orders.txt";
                        List<Order> orders = new List<Order>();
                        ЗаписьДанных(fileName);
                        Console.WriteLine("Информация о заказах из файла:");
                        Вывод(fileName);
                        ЧтениеДанных(fileName, orders);
                        ПодсчетКоличестваЗаказов(orders);
                        ДорогойЗаказВМесяце(orders);
                        УпорядПоДате(orders);
                    }
                    goto c;
                default:
                    {
                        Console.WriteLine("Такой задачи нет.");
                    }
                    goto c;
            }
        }
        //1111111111
        static int ВычислениеМесяцев(DateTime перваяДата, DateTime втораяДата)
        {
            int рахницаГодов = втораяДата.Year - перваяДата.Year;
            int разницаМесяцев = втораяДата.Month - перваяДата.Month;
            return (рахницаГодов * 12) + разницаМесяцев;
        }



        //22222
        struct Заказ
        {
            public int номерЗаказа;
            public DateTime датаЗаказа;
            public string имяКлиента;
            public string адресКлиента;
            public int деньВыволнения;
            public decimal стоимость;
            public DateTime ДатаЗавершения()
            {
                return датаЗаказа.AddDays(деньВыволнения);
            }
            public override string ToString()
            {
                return $"Заказ {номерЗаказа}. \nДата заказа: {датаЗаказа:ddMMyyyy}, ФИО заказчика: {имяКлиента}, Адрес заказчика: {адресКлиента}, Срок выполнения в днях: {деньВыволнения}, Стоимость заказа: {стоимость:C}, Дата выполнения: {ДатаЗавершения():ddMMyyyy}";
            }
        }



        //333333333
        struct Order
        {
            public int номерЗаказа2;
            public DateTime датаЗаказа2;
            public string имяКлиента2;
            public string адресКлиента2;
            public int деньВыволнения2;
            public double стоимость2;
            public override string ToString()
            {
                return $"Заказ{номерЗаказа2}. {датаЗаказа2.ToString("ddMMyyyy")} {датаЗаказа2.ToString("HH:mm:ss")}; {имяКлиента2}; {адресКлиента2}; {деньВыволнения2} дней; {стоимость2} руб.";
            }
        }
        static void Вывод(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        static void ЗаписьДанных(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("1;01012024 10:30:00;Иванов Иван;ул. Пушкина, д. 10;7;15000");
                writer.WriteLine("2;29052024 14:45:00;Петров Петр;ул. Лермонтова, д. 5;5;20000");
                writer.WriteLine("3;15042024 09:00:00;Сидоров Сидор;ул. Гоголя, д. 15;10;30000");
            }
        }
        static void ЧтениеДанных(string fileName, List<Order> orders)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    Order order = new Order();
                    order.номерЗаказа2 = int.Parse(parts[0]);
                    order.датаЗаказа2 = DateTime.ParseExact(parts[1], "ddMMyyyy HH:mm:ss", null);
                    order.имяКлиента2 = parts[2];
                    order.адресКлиента2 = parts[3];
                    order.деньВыволнения2 = int.Parse(parts[4]);
                    order.стоимость2 = double.Parse(parts[5]);
                    orders.Add(order);
                }
            }
        }
        static void ПодсчетКоличестваЗаказов(List<Order> orders)
        {
            DateTime threeYearsAgo = DateTime.Now.AddYears(-3);
            var recentOrders = orders.Where(order => order.датаЗаказа2 >= threeYearsAgo);
            int orderCount = recentOrders.Count();
            double totalCost = recentOrders.Sum(order => order.стоимость2);
            Console.WriteLine($"Количество заказов за последние три года: {orderCount}");
            Console.WriteLine($"Общая стоимость заказов за последние три года: {totalCost} руб.");
        }
        static void ДорогойЗаказВМесяце(List<Order> orders)
        {
            DateTime currentDate = DateTime.Now;
            var currentMonthOrders = orders.Where(order => order.датаЗаказа2.Year == currentDate.Year && order.датаЗаказа2.Month == currentDate.Month);
            Order mostExpensiveOrder = currentMonthOrders.OrderByDescending(order => order.стоимость2).FirstOrDefault();
            Console.WriteLine("Самый дорогой заказ текущего месяца:");
            Console.WriteLine(mostExpensiveOrder);
        }
        static void УпорядПоДате(List<Order> orders)
        {
            var orderedOrders = orders.OrderBy(order => order.датаЗаказа2);
            foreach (var order in orderedOrders)
            {
                string fileName = $"{order.датаЗаказа2.ToString("ddMMyyyy")}.txt";
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine(order);
                }
            }
            Console.WriteLine("Информация упорядочена по дате заказа.");
        }
    }


}




