using System;
using Xunit;
using InterviewApplication;

namespace AuditApplication.Tests
{
    public class AuditTests
    {
        [Fact]
        public void CalculateCurrentVacationHours_ShouldReturnCorrectHours()
        {
            var audit = new Audit();
            audit.EnterAddition(1, 8);
            audit.EnterAddition(1, 8);
            
            Assert.Equal(16, audit.CalculateCurrentVacationHours(1));
        }

        [Fact]
        public void RevokeLastAction_ShouldRemoveLastAction()
        {
            var audit = new Audit();
            audit.EnterAddition(1, 8);
            audit.EnterAddition(1, 8);
            audit.RevokeLastAction(1);
            
            Assert.Equal(8, audit.CalculateCurrentVacationHours(1));
        }

        [Fact]
        public void RequestVacationTime_ShouldCreateNewRequest()
        {
            var audit = new Audit();
            var requestId = audit.RequestVacationTime(1, new DateTime(2024, 7, 1), new DateTime(2024, 7, 5));
            Assert.True(requestId > 0);
        }

        [Fact]
        public void ApproveVacationRequest_ShouldUpdateVacationHours()
        {
            var audit = new Audit();
            audit.EnterValue(1, 80); // Set initial vacation balance to 80 hours
            var requestId = audit.RequestVacationTime(1, new DateTime(2024, 7, 1), new DateTime(2024, 7, 5));
            
            Assert.True(audit.ApproveVacationRequest(requestId));
            Assert.Equal(40, audit.CalculateCurrentVacationHours(1)); // 80 - (5 days * 8 hours)
        }

        [Fact]
        public void GetUpcomingVacations_ShouldReturnCorrectVacations()
        {
            var audit = new Audit();
            audit.RequestVacationTime(1, new DateTime(2024, 7, 1), new DateTime(2024, 7, 5));
            var requestId = audit.RequestVacationTime(2, new DateTime(2024, 8, 1), new DateTime(2024, 8, 5));
            audit.ApproveVacationRequest(requestId);

            var upcomingVacations = audit.GetUpcomingVacations(new DateTime(2024, 7, 15), new DateTime(2024, 8, 15));
            Assert.Single(upcomingVacations);
            Assert.Equal(2, upcomingVacations[0].EmployeeNumber);
        }

        [Fact]
        public void ApproveVacationRequest_ShouldDeductCorrectHours()
        {
            var audit = new Audit();
            audit.EnterValue(1, 100); // Set initial balance
            var requestId = audit.RequestVacationTime(1, new DateTime(2024, 7, 1), new DateTime(2024, 7, 5));
            
            audit.ApproveVacationRequest(requestId);
            
            Assert.Equal(76, audit.CalculateCurrentVacationHours(1)); // 100 - 24 hours
        }
    }
}