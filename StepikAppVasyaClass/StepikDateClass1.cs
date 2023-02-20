using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepikAppVasyaClass
{
    internal class StepikDateClass1
    {
        //Напишите здесь необходимый класс
        public class Date
        {
            int excessDays; //число дней сверх месяца, например 32.01, 68.02
            public int Day
            {
                get { return _day; }
                set
                {
                    if (value > 0 && value <= this.GetNumberOfDays(this.Month)) _day = value; //дата попадает в разрешенный диапазон дат
                    else if (value > this.GetNumberOfDays(this.Month)) // дата больше, чем количество дней в месяце
                    {
                        excessDays = value - this.GetNumberOfDays(this.Month); //порядок важен, смотрим сколько дней уходят в следующий месяц
                        this.Month++; // переходим в следующий месяц
                        this.Day = excessDays; //назначаем новую дату в новом месяце
                    }
                    else if (value == 0 && this.Year > 1) // переходим в предыдущий месяц
                    {
                        this.Month--;
                        this.Day = this.GetNumberOfDays(this.Month);
                    }
                    else this.Month--;      // отрицательные значения даты и переход к 0 году  
                }
            }
            public int Month
            {
                get { return _month; }
                set
                {
                    if (value > 0 && value < 13) _month = value;//месяц попадает в разрешенный диапазон месяцев
                    else if (value > 12) // месяц больше, чем количество месяцев в году
                    {
                        this.Year += value / 12; //важно поменять сначала год, иначе год может уйти в ноль на следующем шагеб если месяц == 0
                        this.Month = value % 12; //????
                    }
                    else if (value == 0) //месяц ушел в ноль, переходим в предыдущий год
                    {
                        this.Month = 12; // порядок важен, если год уходит в ноль - он сбрасывает все на 1.1.1.
                        this.Year--;
                    }
                    else this.Month = 1;

                }
            }
            public int Year
            {
                get { return _year; }
                set
                {
                    if (value > 0) _year = value;
                    else
                    {
                        this.Year = 1;
                        this.Month = 1;
                        this.Day = 1;
                    }
                }
            }

            private int _day;
            private int _month;
            private int _year;

            public Date(int day, int month, int year)
            {
                this.Year = year; //порядок важен для работы сеттеров
                this.Month = month;
                this.Day = day;
            }

            private int GetNumberOfDays(int month)
            {
                switch (month)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12: return 31;
                    case 4:
                    case 6:
                    case 9:
                    case 11: return 30;
                    case 2:
                        {
                            if (this.Year % 400 == 0) return 29;
                            else if (this.Year % 100 == 0) return 28;
                            else if (this.Year % 4 == 0) return 29;
                            else return 28;
                        }
                    default: return 1;
                }
            }

            private string[] nameOfMonths = { "default", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            public Date Next()
            {
                return new Date(this.Day + 1, this.Month, this.Year);
            }
            public Date Previous()
            {
                return new Date(this.Day - 1, this.Month, this.Year);
            }
            public void Print()
            {
                Console.WriteLine($"The {this.Day} of {nameOfMonths[this.Month]} {this.Year}");
            }
            public void PrintForward(int n)
            {
                this.PrintForward(n - 1, this.Next()); //вызов рекурсивного метода (перегрузка) для предыдущих дней
            }

            private void PrintForward(int n, Date previousDate)
            {
                previousDate.Print();
                if (n > 0) this.PrintForward(n - 1, previousDate.Next());
            }

            public void PrintBackward(int n)
            {
                PrintBackward(n - 1, this.Previous()); //вызов рекурсивного метода (перегрузка) для предыдущих дней
            }
            protected void PrintBackward(int n, Date NextDate)
            {
                NextDate.Print();
                if (n > 0) PrintBackward(n - 1, NextDate.Previous());
            }
        }


    }
}
