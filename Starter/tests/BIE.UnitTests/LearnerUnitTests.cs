using BIE.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BIE.UnitTests;

[TestClass]
public class LearnerUnitTests
{
    [TestMethod]
    public void TouristAt30DaysIsApproved()
    {
        var entry = new Entry("Sam Test", "Orion", "Tourismus", 30, false, false, false, true);
        var decision = EntryPolicy.Evaluate(entry);
        Assert.AreEqual("approved", decision.Status);
    }

    [TestMethod]
    public void TouristAt29DaysIsApproved()
    {
        var entry = new Entry("Sam Test", "Orion", "Tourismus", 29, false, false, false, true);
        Assert.AreEqual("approved", EntryPolicy.Evaluate(entry).Status);
    }

    [TestMethod]
    public void TouristAt31DaysIsRejected()
    {
        var entry = new Entry("Sam Test", "Orion", "Tourismus", 31, false, false, false, true);
        Assert.AreEqual("rejected", EntryPolicy.Evaluate(entry).Status);
    }

    [TestMethod]
    public void KiraCanWorkFor90Days()
    {
        var entry = new Entry("Kira Lichtleiter", "Kepler-186f", "Arbeit", 90, false, false, false, true);
        Assert.AreEqual("approved", EntryPolicy.Evaluate(entry).Status);
    }

    [TestMethod]
    public void BorinCanWorkFor180Days()
    {
        var entry = new Entry("Borin Schaltkreis", "Orion", "Arbeit", 180, false, false, false, true);
        Assert.AreEqual("approved", EntryPolicy.Evaluate(entry).Status);
    }

    [TestMethod]
    public void RejectionTakesPrecedenceOverQuarantine()
    {
        var entry = new Entry("Nox", "Orion", "Tourismus", 5, true, true, false, true);
        Assert.AreEqual("rejected", EntryPolicy.Evaluate(entry).Status);
    }
}
