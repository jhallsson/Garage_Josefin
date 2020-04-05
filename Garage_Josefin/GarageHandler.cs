﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Garage_Josefin
{
    public class GarageHandler
    {
        Garage<Vehicle> garage;
        UI console = new UI();
        

        public GarageHandler()
        {
            CreateGarage();
            
            //FullGarage();
        }
        private void CreateGarage()
        {
            string input = console.GetInput("Capacity: ", "Int"); //kommer inte tillbaka förrän rätt
            int.TryParse(input, out int capacity); //checkar ändå

            garage = new Garage<Vehicle>(capacity);
            garage.GarageCapacity = capacity;
        }
        
        public Vehicle CreateVehicle(/*string type, string regNr, string color, int wheels*/) //vilken typ??
        {                                                //skicka typ(string), reg, col, whe, subegen?? fråga i case?
            
                //ToDo: Switch - för mycket upprepning?
                string type = console.GetInput("Type of Vehicle: ", "Type").ToLower(); //ToDo: säg till om invalid input inte bara för null!
                string regNr = console.GetInput("Reg. Number: ", "RegNr");
            
            //ToDo: regnr exists already
            
                string color = console.GetInput("Color: ");
                int.TryParse(console.GetInput("Number of wheels: "), out int wheels); //else?
                //värden kommer inte tillbaka förrän de är okej

                switch (type)
                {
                    case "airplane":
                        int.TryParse(console.GetInput("Seats: "), out int seats);
                        Vehicle airplane = new Airplane(seats, regNr, color, wheels);
                        return airplane;
                    case "boat":
                        double.TryParse(console.GetInput("Draft: "), out double draft);
                        Vehicle boat = new Boat(draft, regNr, color, wheels);
                        return boat;
                    case "bus":
                        double.TryParse(console.GetInput("Length: "), out double length);
                        Vehicle bus = new Bus(length, regNr, color, wheels);
                        return bus;
                    case "car":
                        string brand = console.GetInput("Which Brand?");
                        Vehicle car = new Car(brand, regNr, color, wheels);
                        return car;
                    case "motorcycle":
                        double.TryParse(console.GetInput("Top Speed: "), out double topSpeed);
                        Vehicle motorcycle = new Motorcycle(topSpeed, regNr, color, wheels);
                        return motorcycle;
                default:
                    console.Print("Something Went Wrong. Try Again.");
                    //om värdena är checkade borde den inte nå default
                    Vehicle v = new Vehicle("---123","---",0);
                    return v;
                    //ToDo: bättre lösning!
            }
                //extra: color måste vara någon av förbestämd?
                //Vehicle vehicle = new Vehicle(regNumb, color, wheelCount);//ToDo: kunna skapa subklasser också

                //return newVehicle;     //ToDo: blir det fel att sätta newvehicle = car/bus/boat? 
                //eller sätta newVehicle i varje Case
                //ToDo: inte kunna skapa utan ifyllda värden
            
            
        }
        
        
        public bool RegNumberExists(string regNumber)
        {
            return garage.Vehicles.Any(v => v.RegNumb == regNumber);
        }
        public void TryInput(string input)
        {
            

        }

        internal bool GarageIsFull()
        {
            return CountVehicles() >= garage.GarageCapacity;
        }

        public bool Park(Vehicle vehicle)   //ToDo: Gör om!!
        {
            int countBefore = CountVehicles();
            Vehicle emptyValue = garage.Vehicles.Where(v => v == null).FirstOrDefault();
            int emptyIndex = Array.IndexOf(garage.Vehicles, emptyValue);
           
            garage.Vehicles[emptyIndex] = vehicle;

            //int count = garage.Vehicles.Count(v => v is Vehicle); //ToDo:move
            
            int countAfter = CountVehicles();
            return countAfter > countBefore; //statement: true/false smart!!!
        }

        public bool Leave(string regNr)
        {
            int countBefore = CountVehicles();
            string search = regNr.ToUpper();
                                                            //input => method body
            Vehicle vehicleLeaving = garage.Vehicles?.Where(v => v.RegNumb == search).FirstOrDefault();
            int index = Array.IndexOf(garage.Vehicles, vehicleLeaving);
            garage.Vehicles[index] = null;
            int countAfter = CountVehicles();
            return countBefore > countAfter; //ToDo: bool eller vehicle?
             
        }

        public void ListVehicles() 
        {
            //ToDo:funkar inte för null
            foreach (var vehicle in garage.Vehicles)
            {
                if (vehicle is Vehicle)
                {
                    string info = StringifyOutput(vehicle);
                    console.Print(info);
                }
            }
        }

        internal void ListVehicleTypes(string type)
        {
            //ToDo: kod
            //lista över typer - gå igenom - hitta rätt - lista
            //if type=list[n] -> 
            //if type= key lista alla i subklass

            foreach (var vehicle in garage.Vehicles)
            {
                string info="";
                //switch? så länge
                type = type.ToLower();
                switch (type)
                {
                    case "vehicle":
                        ListVehicles();
                        break;
                    case "airplane":
                        if(vehicle is Airplane)
                        info = StringifyOutput(vehicle);
                        break;
                    case "boat":
                        if (vehicle is Boat)
                        info = StringifyOutput(vehicle);
                        break;
                    case "bus":
                        if (vehicle is Bus)
                        info = StringifyOutput(vehicle);
                        break;
                    case "car":
                        if (vehicle is Car)
                        info = StringifyOutput(vehicle);
                        break;
                    case "motorcycle":
                        if (vehicle is Motorcycle)
                        info = StringifyOutput(vehicle);
                        break;
                    default: 
                        info = "The type doesn't exist";
                        break;
                }
                
                console.Print(info);
            }
        }

        public string Search(string regNr) //ToDo: understand ?-nullcheck / gör om
        {
            regNr = regNr.ToUpper();            //ToDo: .toUpper fult/fel plats?
            /*if (RegNumberExists(regNr))
            {*/
                Vehicle vehicle = garage.Vehicles?.Where(v => v?.RegNumb == regNr).FirstOrDefault();
                int index = Array.IndexOf(garage.Vehicles, vehicle);
                return StringifyOutput(vehicle);
            
            
        }
        public void SearchProperty()
        {
            string type = console.GetInput("Type: ").ToLower();
            string color = console.GetInput("Color: ");
            string wheelCount= console.GetInput("Wheels: ");
            List<Vehicle> searchList = new List<Vehicle>();

            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "airplane":
                        garage.Vehicles.Where(v => v is Airplane).ToList(); //ToDo: rätt att välja tolist?
                        break;
                    case "boat":
                        searchList = garage.Vehicles.Where(v => v is Boat).ToList(); //ToDo: hitta hur man kan skicka typ
                        break;
                    case "bus":
                        searchList = garage.Vehicles.Where(v => v is Bus).ToList();
                        break;
                    default:
                        break;
                }
            }else if (!string.IsNullOrEmpty(color))
            {
                searchList = searchList.Where(v => v.Color == color).ToList();
            }else if (int.TryParse(wheelCount, out int wheels))
            {
            //ToDo: hur gör jag med int?
            searchList = searchList.Where(v => v.WheelCount == wheels).ToList();
            }



        }
        public string StringifyOutput(Vehicle vehicle) //ToDo: använd .tostring istället?
        {

            string vehicleInfo = $"{vehicle.RegNumb}, {vehicle.Color}, {vehicle.WheelCount} wheels.";

            return vehicleInfo;
        }
        public int CountVehicles()
        {
            int count = garage.Vehicles.Count(v => v is Vehicle);
            return count;
        }
        public void FullGarage()
        {
            //Garage<Vehicle> fullGarage = new Garage<Vehicle>(6);
            Vehicle car = new Car("Volvo", "ABC123", "red", 4);     //ToDo: rätt deklarerat med Vehicle?
            Vehicle car2 = new Car("Ford", "DEF456", "blue", 4);
            Vehicle boat = new Boat(3.52, "GHI789", "red", 4);
            Vehicle motorcycle = new Motorcycle(320.7, "JKL012", "red", 4);
            Vehicle airplane = new Airplane(500, "MNO345", "white", 4);
            Vehicle bus = new Bus(12.41, "PQR678", "red", 4);

            Park(car);
            Park(car2);
            Park(boat);
            Park(motorcycle);
            Park(airplane);
            Park(bus);
           

        }
    }
}
