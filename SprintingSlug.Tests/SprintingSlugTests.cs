using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SprintingSlug.Tests
{
    [TestClass]
    public class SprintingSlugTests
    {
        MainWindow sprintingSlug = new MainWindow();

        [TestMethod]
        public void GetAdjective()
        {
            // Returns an adjective
            // Trailing char matches given parameter
            Assert.AreEqual('a', sprintingSlug.GetAdjective('a').ToCharArray()[0]);
        }

        [TestMethod]
        public void GetAnimal()
        {
            // Returns an animal
            // Trailing char matches given parameter
        }

        // All trailing chars are uppercase
        // Different chars  (a, h, n, w and z)
        // Unallowed input throws (' ', %, -, .)
        // Weird but allowed (Ó, ô, Ô, ö, Ö, ŏ, ō, Ō, ő, ø)
        // Uppercase and lowercase parameters
    }
}