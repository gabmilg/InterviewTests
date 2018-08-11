using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GraduationTracker.Tests.Unit
{
    public class TestHelperExtensions
    {
        public static void Throw<T>(Action action, string expectedMsg) where T : Exception
        {
            try
            {
                action.Invoke();
                Assert.Fail($"{typeof(T)} exception should be thrown.");
            }
            catch (Exception ex)
            {
                if (ex.GetType() != typeof(T))
                    Assert.Fail($"{typeof(T)} exception should be thrown.");

                Assert.AreEqual(expectedMsg, ex.Message);
            }
        }
    }
}
