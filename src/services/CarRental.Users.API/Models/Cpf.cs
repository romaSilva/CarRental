using CarRental.Core.DomainObjects;
using System;

namespace CarRental.Users.API.Models
{
    public class Cpf
    {
        public const int CpfLength = 11;
        public string Number { get; private set; }

        protected Cpf() { }

        public Cpf(string number)
        {
            if (!IsValid(number))
            {
                throw new DomainException("Invalid CPF");
            }

            Number = number;
        }

        public static bool IsValid(string cpf)
        {
            //Validate CPF
            return cpf.Length == CpfLength;
        }
    }
}