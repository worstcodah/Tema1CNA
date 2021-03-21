using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tema1CNAServer.Exceptions;

namespace PersonServer.Services
{
    public class PersonService : GetPersonDataService.GetPersonDataServiceBase
    {
        public string GetPersonGender(string cnp)
        {
            switch (cnp.ElementAt(0))
            {
                case '1':
                case '3':
                case '5':
                case '7':
                    return "sex masculin";

                case '2':
                case '4':
                case '6':
                case '8':
                    return "sex feminin";


            }
            return String.Empty;

        }

        public int GetPersonAge(string cnp)
        {

            var currentYear = 2021;
            System.Text.StringBuilder yearOfBirth = new System.Text.StringBuilder();
            switch (cnp.ElementAt(0))
            {
                case '1':
                case '2':
                case '7':
                case '8':
                    yearOfBirth.Append("19");
                    break;
                case '3':
                case '4':
                    yearOfBirth.Append("18");
                    break;
                case '5':
                case '6':
                    yearOfBirth.Append("20");
                    break;


            }
            yearOfBirth.Append(cnp.Substring(1, 2));






            return currentYear - int.Parse(yearOfBirth.ToString());



        }

        public override Task<PersonData> GetPersonData(Person person, ServerCallContext context)
        {

            try
            {

                Console.WriteLine("Nume: " + person.Name + " CNP:" + person.Cnp);


                ValidatePersonName(person.Name);
                ValidatePersonCnp(person);

                return Task.FromResult(new PersonData
                {
                    Age = GetPersonAge(person.Cnp),
                    Gender = GetPersonGender(person.Cnp)
                });
            }

            catch (InvalidPersonNameException exception)
            {
                Console.WriteLine(exception.Message);
                try
                {
                    ValidatePersonCnp(person);
                }
                catch (InvalidPersonCnpException secondException)
                {
                    Console.WriteLine(secondException.Message);
                }
            }
            catch (InvalidPersonCnpException exception)
            {
                Console.WriteLine(exception.Message);
            }

            return Task.FromResult(new PersonData());




        }

        public void ValidatePersonName(string name)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");

            if (!regex.IsMatch(name))
            {
                throw new InvalidPersonNameException(name);
            }

        }

        public void ValidatePersonCnp(Person person)
        {
            var currentYear = 2021;
            Regex regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(person.Cnp))
            {
                throw new InvalidPersonCnpException("CNP-ul trebuie sa fie alcatuit doar din cifre !");
            }

            else if (person.Cnp.StartsWith('9'))
            {
                throw new InvalidPersonCnpException("CNP-ul nu poate incepe cu cifra 9 !");

            }
            else if (PersonNotBornYet(person))
            {
                throw new InvalidPersonCnpException("Conform CNP-ului introdus, persoana s-a nascut dupa 2021 (in " + (currentYear - GetPersonAge(person.Cnp)) + ", nu corespunde realitatii) !");
            }
            else if (person.Cnp.Length != 13)
            {
                throw new InvalidPersonCnpException("CNP-ul trebuie sa aiba lungimea de fix 13 caractere !");
            }
        }
        public bool PersonNotBornYet(Person person)
        {
            return ((person.Cnp.StartsWith('5') || person.Cnp.StartsWith('6')) && person.Cnp.Substring(1, 2).CompareTo("21") > 0);
        }
    }
}
