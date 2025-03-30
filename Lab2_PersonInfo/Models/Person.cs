﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1_BirthDateWindow.Models;

namespace Lab2_PersonInfo.Models
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public bool IsAdult { get; }
        public string SunSign { get; }
        public string ChineseSign { get; }
        public bool IsBirthday { get; }

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            Thread.Sleep(2000);
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;

            if (BirthDate > DateTime.Today)
                throw new ArgumentException("The selected date is in the future.");
            if (CalculateAge(BirthDate) > 135)
                throw new ArgumentException("The age entered is too high.");

            IsAdult = CalculateAge(BirthDate) >= 18;
            SunSign = GetWesternZodiacSign(BirthDate);
            ChineseSign = ((ChineseZodiac)(BirthDate.Year % 12)).ToString();
            IsBirthday = BirthDate.Month == DateTime.Today.Month && BirthDate.Day == DateTime.Today.Day;
        }

        public Person(string firstName, string lastName, string email) :
            this(firstName, lastName, email, DateTime.Now)
        { }

        public Person(string firstName, string lastName, DateTime birthDate) :
            this(firstName, lastName, "", birthDate) 
        { }

        private static string GetWesternZodiacSign(DateTime birthDate)
        {
            int month = birthDate.Month, day = birthDate.Day;

            if ((month == 3 && day >= 21) || (month == 4 && day <= 19)) return "Aries";
            if ((month == 4 && day >= 20) || (month == 5 && day <= 20)) return "Taurus";
            if ((month == 5 && day >= 21) || (month == 6 && day <= 21)) return "Gemini";
            if ((month == 6 && day >= 22) || (month == 7 && day <= 22)) return "Cancer";
            if ((month == 7 && day >= 23) || (month == 8 && day <= 22)) return "Leo";
            if ((month == 8 && day >= 23) || (month == 9 && day <= 22)) return "Virgo";
            if ((month == 9 && day >= 23) || (month == 10 && day <= 23)) return "Libra";
            if ((month == 10 && day >= 24) || (month == 11 && day <= 21)) return "Scorpio";
            if ((month == 11 && day >= 22) || (month == 12 && day <= 21)) return "Sagittarius";
            if ((month == 12 && day >= 22) || (month == 1 && day <= 19)) return "Capricorn";
            if ((month == 1 && day >= 20) || (month == 2 && day <= 18)) return "Aquarius";
            return "Pisces";
        }

        private static int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate > DateTime.Today.AddYears(-age)) age--;
            return age;
        }

    }
}
