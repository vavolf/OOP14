using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    [Serializable]
    abstract public class Organization
    {
        [NonSerialized] public string nameOfOrganization;
        public Organization() { }
        public Organization(string orgName)
        {
            nameOfOrganization = orgName;
        }
        public string NameOfOrganization { get => nameOfOrganization; set => nameOfOrganization = value; }
        public abstract void Burning();
        public override string ToString() => $"Тип объекта – {this.GetType()}, название организации – {this.nameOfOrganization}";
    }
    [Serializable]
    public class Date
    {
        public int year;
        public int month;
        public int day;
        public int Year { get => year; set => year = value; }
        public int Month
        {
            get => month;
            set
            {
                if (value < 1)
                    month = 1;
                else if (value > 12)
                    month = 12;
                else
                    month = value;
            }
        }
        public int Day
        {
            get => day;
            set
            {
                if (value < 1)
                    day = 1;
                else if (value > 31)
                    day = 31;
                else
                    day = value;
            }
        }
        public Date() { }
        public Date(int dateDay, int dateMonth, int dateYear)
        {
            day = dateDay;
            month = dateMonth;
            year = dateYear;
        }
        public override string ToString() => $"Тип объекта – {this.GetType()}, дата создания: {day}.{month}.{year}";
    }
    [Serializable]
    public class Document : Organization
    {
        public bool stamp;
        public Date date;
        public bool IsStamped { get => stamp; set => stamp = value; }
        public Document() { }
        public Document(string organizationName, int dateDay, int dateMonth, int dateYear, bool isStamped)
            : base(organizationName)
        {
            date = new Date(dateDay, dateMonth, dateYear);
            stamp = isStamped;
        }
        public int Day
        {
            get => date.Day;
        }
        public int Month
        {
            get => date.Month;
        }
        public int Year
        {
            get => date.Year;
        }

        // виртуальные методы, подлежащие переопределению

        virtual public void Store()
        {
            Console.WriteLine("Документ хранится в сейфе");
        }

        // реализация методов интерфейса

        public void Change()
        {
            Console.WriteLine("Документ изменен");
        }
        public void Find()
        {
            Console.WriteLine("Документ найден");
        }
        public void Lose()
        {
            Console.WriteLine("Документ утерян");
        }

        public override void Burning()
        {
            Console.WriteLine("Oh no! The document is burning!");
        }

        // Переопределенные методы Object

        /*public override string ToString() => $"Тип объекта – {this.GetType()}, организация – {this.NameOfOrganization}, " +
            $"дата создания – {date.Day}.{date.Month}.{date.Year}, есть ли печать – " + (stamp ? "есть" : "нет");
        public override int GetHashCode() => HashCode.Combine(this.NameOfOrganization, date.Day, date.Month, date.Year);
        public override bool Equals(object obj)
        {
            if (obj is Document objectType)
            {
                if (this.date.Day == objectType.date.Day
                        && this.date.Month == objectType.date.Month
                            && this.date.Year == objectType.date.Year
                                && this.NameOfOrganization == objectType.NameOfOrganization)
                {
                    return true;
                }
            }
            return false;
        }*/
    }
}