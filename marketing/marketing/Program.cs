using System;
using System.Collections.Generic;
using System.Linq;

namespace marketing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person> ();
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"people.in");

            // 1 READ FILE
            while ((line = file.ReadLine()) != null)
            {
                // System.Console.WriteLine(line);
                string[] words = line.Split('|');
                Person person = new Person(words[0], words[1], words[2], words[3], words[4], words[5], words[6], words[7]);
                people.Add(person);
            }
            file.Close();

            // SORT ITEMS
            people.Sort((x, y) => y.Score.CompareTo(x.Score));

            // BUILD OUTPUT
            var targetPeople = people.Take(10);
            List<string> targetPeopleIds = targetPeople.Select(p => p.Id).ToList();
            foreach (var person in targetPeople)
            {
                Console.WriteLine(person.Id + " " + person.Name + " " + person.Score);
            }
            System.IO.File.WriteAllLines(@"people.out", targetPeopleIds);
        }
    }
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CurrentRole { get; set; }
        public string Country { get; set; }
        public string Industry { get; set; }
        public int RecommendationsQty { get; set; }
        public int ConnectionsQty { get; set; }
        public double Score { get; set; }

        public Person(string id, string name, string lastName, string currentRole, string country, string industry, string recommendationsQty, string connectionsQty)
        {
            Id = id.Trim();
            Name = name.Trim();
            LastName = lastName.Trim();
            CurrentRole = currentRole.Trim();
            Country = country.Trim();
            Industry = industry.Trim();
            RecommendationsQty = String.IsNullOrEmpty(recommendationsQty.Trim()) ? 0 : Convert.ToInt32(recommendationsQty.Trim());
            ConnectionsQty = String.IsNullOrEmpty(connectionsQty.Trim()) ? 0 : Convert.ToInt32(connectionsQty.Trim()); ;
            Score = ((double)(RecommendationsQty - 5) / 2 * 7 + (double)(ConnectionsQty - 300) / 50 * 3) / 10.00;
        }
    }
}
