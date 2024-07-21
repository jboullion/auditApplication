using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewApplication {
    // An auditable history of vacation time calculations
    public class Audit {
        // Since these all seem to be related let's create a List of a class that represents an audit entry
        // public ArrayList ActionTypes;
        // public ArrayList ActionAmount;
        // public ArrayList EmployeeNumbers;

        // We set this private unless we have a specific reason to expose it
        // We are using readonly to ensure that this list is not reassigned
        private readonly List<AuditEntry> AuditEntries = new List<AuditEntry>();

        // We no longer need to initialize these lists
        // public Audit() {
        //     ActionTypes = new ArrayList();
        //     ActionAmount = new ArrayList();
        //     EmployeeNumbers = new ArrayList();
        // }

        // employee earned or took some vacation time
        public void EnterAddition(int employeeNumber, int amount) {
            // ActionTypes.Add(1);
            // ActionAmount.Add(amount);
            // EmployeeNumbers.Add(employeeNumber);

            // Now we can add an AuditEntry to the list instead of tracking each field separately
            AuditEntries.Add(new AuditEntry(employeeNumber, ActionType.Addition, amount));
        }

        // employee's vacation time was manually corrected to a particular amount
        public void EnterValue(int employeeNumber, int amount) {
            // ActionTypes.Add(2);
            // ActionAmount.Add(amount);
            // EmployeeNumbers.Add(employeeNumber);

            // Now we can add an AuditEntry to the list instead of tracking each field separately
            AuditEntries.Add(new AuditEntry(employeeNumber, ActionType.SetValue, amount));
        }

        // mistakes happen - undo the last action recorded for an employee
        public void RevokeLastAction(int employeeNumber) {
            // int lastAction = -1;
            // for ( var x = 1; x < ActionTypes.Count; x++ ) {
            //     if ( (int)EmployeeNumbers[x] == employeeNumber ) {
            //         lastAction = x;
            //     }
            // }

            // // remove it from the audit history
            // ArrayList newActionTypes = new ArrayList();
            // ArrayList newActionAmount = new ArrayList();
            // ArrayList newEmployeeNumbers = new ArrayList();
            // for ( var x = 1; x < ActionTypes.Count; x++ ) {
            //     if ( x != lastAction ) {
            //         newActionTypes.Add(ActionTypes[x]);
            //         newActionAmount.Add(ActionTypes[x]);
            //         newEmployeeNumbers.Add(EmployeeNumbers[x]);
            //     }
            // }
            // ActionTypes = newActionTypes;
            // ActionAmount = newActionAmount;
            // EmployeeNumbers = newEmployeeNumbers;
            
            // Find the last action for this employee
            var lastAction = AuditEntries.FindLastIndex(e => e.EmployeeNumber == employeeNumber);

            // If we found an action, remove it from the list
            if (lastAction != -1)
            {
                AuditEntries.RemoveAt(lastAction);
            }
        }

        // get the vacation hours to date for this employee
        public decimal CalculateCurrentVacationHours(int employeeNumber) {
            // int startingValue = 0;

            // for ( var x = 1; x < ActionTypes.Count; x++ ) {
            //     if ( (int)EmployeeNumbers[x] == employeeNumber ) {
            //         switch ( (int)ActionTypes[x] ) {
            //             case 1:
            //                 startingValue += (int)ActionAmount[x];
            //                 break;
            //             case 2:
            //                 startingValue = (int)ActionAmount[x];
            //                 break;
            //         }
            //     }
            // }

            // return startingValue;

            // Instead of iterating over the lists, we can use LINQ to filter and aggregate the entries
            return AuditEntries
                .Where(e => e.EmployeeNumber == employeeNumber)
                .Aggregate(0m, (current, entry) => entry.ActionType == ActionType.Addition 
                    ? current + entry.Amount 
                    : entry.Amount);
        }


        // We can add a method to get the full audit history
        public List<AuditEntry> GetAuditHistory()
        {
            return AuditEntries;
        }

        // We can add a method to clear the audit history
        public void ClearAuditHistory()
        {
            AuditEntries.Clear();
        }

        // We can add a method to get the total vacation hours for all employees
        public decimal CalculateTotalVacationHours()
        {
            return AuditEntries
                .Aggregate(0m, (current, entry) => entry.ActionType == ActionType.Addition 
                    ? current + entry.Amount 
                    : entry.Amount);
        }

    }

        
    public enum ActionType
    {
        Addition,
        SetValue
    }

    public class AuditEntry
    {
        public int EmployeeNumber { get; }
        public ActionType ActionType { get; }
        public decimal Amount { get; }

        // We are using a constructor to ensure that all required fields are set when creating an AuditEntry
        public AuditEntry(int employeeNumber, ActionType actionType, decimal amount)
        {
            EmployeeNumber = employeeNumber;
            ActionType = actionType;
            Amount = amount;
        }
    }

    public class VacationRequest
    {
        public int RequestId { get; set; }
        public int EmployeeNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public VacationRequestStatus Status { get; set; }
    }

    public enum VacationRequestStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
