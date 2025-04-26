using System;
using System.Collections.Generic;

namespace EmergencyResponseSimulation
{
    public class Incident
    {
        public string Type { get; }
        public string Location { get; }
        public string Difficulty { get; }

        public Incident(string type, string location, string difficulty)
        {
            Type = type;
            Location = location;
            Difficulty = difficulty;
        }
    }

    public abstract class EmergencyUnit
    {
        public string Name { get; protected set; }
        public int Speed { get; protected set; }

        public abstract bool CanHandle(string incidentType);
        public abstract void RespondToIncident(Incident incident);
    }

    public class Police : EmergencyUnit
    {
        public Police(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Crime" || incidentType == "Riot" || incidentType == "Hostage";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is responding to a {incident.Type.ToLower()} at {incident.Location}.");
        }
    }

    public class Firefighter : EmergencyUnit
    {
        public Firefighter(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Fire" || incidentType == "Explosion" || incidentType == "Hazardous Spill";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is putting out a {incident.Type.ToLower()} at {incident.Location}.");
        }
    }

    public class Ambulance : EmergencyUnit
    {
        public Ambulance(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Medical" || incidentType == "Accident" || incidentType == "Poisoning";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is treating patients at {incident.Location}.");
        }
    }

    public class CoastGuard : EmergencyUnit
    {
        public CoastGuard(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Drowning" || incidentType == "Boat Accident" || incidentType == "Water Rescue";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is responding to a {incident.Type.ToLower()} at {incident.Location}.");
        }
    }

    public class K9Unit : EmergencyUnit
    {
        public string DogBreed { get; }

        public K9Unit(string name, int speed, string dogBreed)
        {
            Name = name;
            Speed = speed;
            DogBreed = dogBreed;
        }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Drug Bust" || incidentType == "Search and Rescue" || incidentType == "Bomb Threat";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is handling a {incident.Type.ToLower()} with {DogBreed} at {incident.Location}.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<EmergencyUnit> units = new List<EmergencyUnit>
            {
                new Police("Police Unit 1", 60),
                new Firefighter("Firefighter Unit 1", 50),
                new Ambulance("Ambulance Unit 1", 70),
                new Police("Police Unit 2", 65),
                new Ambulance("Ambulance Unit 2", 75),
                new CoastGuard("Coast Guard Unit 1", 40),
                new K9Unit("K9 Unit 1", 45, "German Shepherd")
            };

            string[] incidentTypes = {
                "Crime", "Fire", "Medical", "Riot", "Accident",
                "Explosion", "Hostage", "Poisoning", "Drowning",
                "Boat Accident", "Drug Bust", "Search and Rescue",
                "Bomb Threat", "Hazardous Spill", "Water Rescue"
            };

            string[] locations = {
                "Downtown", "City Hall", "Suburbs", "Shopping Mall",
                "Residential Area", "Industrial Zone", "Harbor",
                "Beach", "Park", "Highway"
            };

            string[] difficulties = { "Easy", "Medium", "Hard" };

            Random random = new Random();
            int score = 0;

            for (int round = 1; round <= 5; round++)
            {
                Console.WriteLine($"\n    -Turn {round} ---");

                string randomType = incidentTypes[random.Next(incidentTypes.Length)];
                string randomLocation = locations[random.Next(locations.Length)];
                string randomDifficulty = difficulties[random.Next(difficulties.Length)];
                Incident incident = new Incident(randomType, randomLocation, randomDifficulty);

                Console.WriteLine($"    Incident: {incident.Type} at {incident.Location} (Difficulty: {incident.Difficulty})");

                EmergencyUnit respondingUnit = null;
                foreach (var unit in units)
                {
                    if (unit.CanHandle(incident.Type))
                    {
                        respondingUnit = unit;
                        break;
                    }
                }

                if (respondingUnit != null)
                {
                    respondingUnit.RespondToIncident(incident);
                    score += 10;
                    Console.WriteLine($"    +10 points");
                }
                else
                {
                    Console.WriteLine($"    No available unit to handle {incident.Type} incident!");
                    score -= 5;
                    Console.WriteLine($"    -5 points");
                }

                Console.WriteLine($"    Current Score: {score}");
            }

            Console.WriteLine($"\n--- Simulation Complete ---");
            Console.WriteLine($"Final Score: {score}");
        }
    }
}