using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SprintingSlug.Tests
{
    [TestClass]
    public class SprintingSlugTests
    {
        MainWindow sprintingSlug = new MainWindow();

        [TestMethod]
        public void GetAdjective_NonEmptyString()
        {
            // Returns a string that has more than one character
            var result = sprintingSlug.GetAdjective('o');
            Assert.IsTrue(result is string);
            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Length > 1);
        }

        [TestMethod]
        public void GetAdjective_LeadingCharMatches()
        {
            // Leading char matches given parameter (in uppercase)
            Assert.AreEqual('A', sprintingSlug.GetAdjective('a').ToCharArray()[0]);
            Assert.AreEqual('A', sprintingSlug.GetAdjective('A').ToCharArray()[0]);

            Assert.AreEqual('H', sprintingSlug.GetAdjective('H').ToCharArray()[0]);
            Assert.AreEqual('N', sprintingSlug.GetAdjective('n').ToCharArray()[0]);

            Assert.AreEqual('Z', sprintingSlug.GetAdjective('z').ToCharArray()[0]);
            Assert.AreEqual('Z', sprintingSlug.GetAdjective('Z').ToCharArray()[0]);
        }

        [TestMethod]
        public void GetAnimal()
        {
            // Returns an animal
            // Leading char matches given parameter (uppercase)
        }

        // Unallowed input throws (' ', %, -, .)
        // Weird but allowed (Ó, ô, Ô, ö, Ö, ŏ, ō, Ō, ő, ø)
        // Nothing found
    }
}