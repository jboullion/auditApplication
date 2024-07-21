using System;
using InterviewApplication;

class Program
{
    static void Main(string[] args)
    {
        var audit = new Audit();

        // Test the Audit class
        audit.EnterAddition(1, 8);
        audit.EnterAddition(1, 8);
        audit.EnterValue(2, 16);
        audit.EnterAddition(1, 8);

        Console.WriteLine($"Employee 1 vacation hours: {audit.CalculateCurrentVacationHours(1)}");
        Console.WriteLine($"Employee 2 vacation hours: {audit.CalculateCurrentVacationHours(2)}");

        audit.RevokeLastAction(1);

        Console.WriteLine($"After revoke - Employee 1 vacation hours: {audit.CalculateCurrentVacationHours(1)}");
    }
}