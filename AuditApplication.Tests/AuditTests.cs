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
    }
}