using System;
using System.Collections.Generic;
using System.Linq;
using RoadRepair.Repairs;

namespace RoadRepair
{

    public class Planner
    {
        
        // private readonly Patching _patching = null;
        //private readonly Filling _filling = null;
        // private readonly Resurfacing _resurfacing = null;
        // public Planner(Patching patching,Resurfacing resurfacing,Filling filling)
        // {
        //     _patching = patching;
        //     _resurfacing = resurfacing;
        //     _filling = filling;
        // }
        /// <summary>
        /// The total number of hours needed to complete the work.
        /// </summary>
        public int HoursOfWork { get; set; }

        /// <summary>
        /// The number of people available to do the work
        /// </summary>
        public int Workers { get; set; }

        /// <summary>
        /// The time to complete the work, using all the workers.
        /// </summary>
        /// <returns>The number of hours to complete the work.</returns>
        public double GetTime()
        {
            var time = HoursOfWork / Workers;
            return time;
        }
        Dictionary<string, double> costMap = new Dictionary<string, double>();

        /// <summary>
        /// Return the correct type of repair (either a filling, a patch or a resurface)
        /// depending on the density of potholes.
        /// </summary>
        /// <param name="road">A road needing repair</param>
        /// <returns>Either a Filling, a Patching or a Resurfacing</returns>
        public string SelectRepairType(Road road)
        {
            string roadRepareType = string.Empty;
            // Use the road.Width, road.Length and road.Potholes properties to calculate the density of potholes. 
            var totalLength=100; //Here I'm assuming that total length will be 100 unit because total length not provided
            var density = (road.Width * road.Length * road.Potholes)/totalLength * 100;
            // If the density of potholes is 40% or more the road should be resurfaced.
            // If the density of potholes is 20% or more, but less than 40%, the road should be patched.
            // Otherwise it should be filled.
            if (density >= 40)
            {
                //return resurfaced
                return roadRepareType = "Resurfacing";
            }
            else if (density >= 20 && density < 40)
            {
                //return patched
                return roadRepareType = "Patching";

            }
            else
            {
                //return filled
                return roadRepareType = "Filling";


            }
            //return roadRepareType;
            // throw new NotImplementedException("TODO");
        }

        /// <summary>
        /// Calculate the total cost of all the repairs for a list of roads that need repairs.
        /// </summary>
        /// <param name="roads">A list of roads needing repairs</param>
        /// <returns>The total cost of all the repairs</returns>
        public double GetCostOfRepairs(List<Road> roads)
        {
            string roadRepareType = String.Empty;
            double cost = 0;
            double totalCost = 0;
            
            foreach (var road in roads)
            {
                roadRepareType = SelectRepairType(road);
                if (roadRepareType == "Resurfacing")
                {
                    Resurfacing resurfacing = new Resurfacing(road);
                    cost += resurfacing.GetCost();
                    costMap.Add(roadRepareType, cost);
                    totalCost += cost;
                }
                else if (roadRepareType == "Patching")
                {
                    Patching patching = new Patching(road);
                    cost += patching.GetCost();
                    costMap.Add(roadRepareType, cost);
                    totalCost += cost;

                }
                else if (roadRepareType == "Filling")
                {
                    Filling filling = new Filling(road);
                    cost += filling.GetCost();
                    costMap.Add(roadRepareType, cost);
                    totalCost += cost;

                }
                cost = 0;
            }
            return totalCost;

        }

        /// <summary>
        /// When there is not enough money available to repair all the roads,
        /// select a subset of the roads so that the cost of repairs is less than or equal to the money available.
        /// </summary>
        /// <param name="roads">A list of roads needing repairs</param>
        /// <param name="availableMoney">The money available for repairs</param>
        /// <returns>A subset of roads that can be repaired with the available money</returns>
        //public List<Road> SelectRoadsToRepair(List<Road> roads, double availableMoney)
        public Dictionary<string, double> SelectRoadsToRepair(List<Road> roads, double availableMoney)
        {
            var repairCost = GetCostOfRepairs(roads);
            Dictionary<string, double> RoadsToRepairs =  new Dictionary<string, double>();

            if (repairCost > availableMoney)
            {
                foreach (KeyValuePair<string, double> kvp in costMap)
                {
                    Console.WriteLine("Key = {0}, Value = {1}",
                              kvp.Key, kvp.Value);
                    if (kvp.Key == "Resurfacing" && kvp.Value < availableMoney)
                    {
                        RoadsToRepairs.Add(kvp.Key, kvp.Value);
                    }
                    if (kvp.Key == "Patching" && kvp.Value < availableMoney)
                    {
                        RoadsToRepairs.Add(kvp.Key, kvp.Value);
                    }
                    if (kvp.Key == "Filling" && kvp.Value < availableMoney)
                    {
                        RoadsToRepairs.Add(kvp.Key, kvp.Value);
                    }
                }
            }
            return RoadsToRepairs;
           
        }
    }
}
