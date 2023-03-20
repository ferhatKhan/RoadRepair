using RoadRepair;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var roads = new List<Road>();
        var road1 = new Road() { Length = 6, Width = 2, Potholes = 1 };
        var road2 = new Road() { Length = 3, Width = 2, Potholes = 5 };
        var road3 = new Road() { Length = 8, Width = 2, Potholes = 6 };
        roads.Add(road1);
        roads.Add(road2);
        roads.Add(road3);
        double availableMoney = 100;
        Planner planner = new Planner();
        //  planner.SelectRepairType(road1);
        // planner.GetCostOfRepairs(roads);
        var result = planner.SelectRoadsToRepair(roads, availableMoney);
        var r = result;
        var rl = result.ToList();
        Console.WriteLine("result ={0}", result.ToList());
    }
}