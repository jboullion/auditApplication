using System;
using InterviewApplication;

class Program
{
    static void Main(string[] args)
    {
        Audit audit = new Audit();

        // Initialize some vacation balances
        audit.EnterValue(1, 80); // Employee 1 starts with 80 hours
        audit.EnterValue(2, 120); // Employee 2 starts with 120 hours

        Console.WriteLine("Initial vacation balances:");
        Console.WriteLine($"Employee 1: {audit.CalculateCurrentVacationHours(1)} hours");
        Console.WriteLine($"Employee 2: {audit.CalculateCurrentVacationHours(2)} hours");

        // Request vacation time
        int request1 = audit.RequestVacationTime(1, new DateTime(2023, 7, 1), new DateTime(2023, 7, 5));
        int request2 = audit.RequestVacationTime(2, new DateTime(2023, 8, 1), new DateTime(2023, 8, 10));

        Console.WriteLine("\nVacation requests made:");
        Console.WriteLine($"Employee 1 request ID: {request1}, Hours: {audit.CalculateRequestHours(request1)}");
        Console.WriteLine($"Employee 2 request ID: {request2}, Hours: {audit.CalculateRequestHours(request2)}");

        // Approve vacation requests
        audit.ApproveVacationRequest(request1);
        audit.ApproveVacationRequest(request2);

        Console.WriteLine("\nVacation requests approved. Updated balances:");
        Console.WriteLine($"Employee 1: {audit.CalculateCurrentVacationHours(1)} hours");
        Console.WriteLine($"Employee 2: {audit.CalculateCurrentVacationHours(2)} hours");

        // Get upcoming vacations
        var upcomingVacations = audit.GetUpcomingVacations(new DateTime(2023, 6, 1), new DateTime(2023, 12, 31));
        Console.WriteLine("\nUpcoming approved vacations:");
        foreach (var vacation in upcomingVacations)
        {
            Console.WriteLine($"Employee {vacation.EmployeeNumber}: {vacation.StartDate:d} to {vacation.EndDate:d}, Hours: {audit.CalculateRequestHours(vacation.RequestId)}");
        }

        // Try to request vacation with insufficient balance
        try
        {
            int request3 = audit.RequestVacationTime(1, new DateTime(2023, 9, 1), new DateTime(2023, 9, 30));
            Console.WriteLine($"\nAttempting to request {audit.CalculateRequestHours(request3)} hours of vacation for Employee 1");
            audit.ApproveVacationRequest(request3);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }
    }
}