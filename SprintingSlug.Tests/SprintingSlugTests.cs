using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SprintingSlug.Tests
{
    [TestClass]
    public class SprintingSlugTests
    {
        [TestMethod]
        public void GetAdjectives()
        {
            // Returns a list of adjectives
            // All trailing chars match given parameter
        }

        [TestMethod]
        public void GetAnimals()
        {
            // Returns a list of animals
            // All trailing chars match given parameter
        }

        // All trailing chars are uppercase
        // Different chars  (a, h, n, w and z)
        // Unallowed input throws (' ', %, -, .)
        // Weird but allowed (Ó, ô, Ô, ö, Ö, ŏ, ō, Ō, ő, ø)
        // Uppercase and lowercase parameters
    }
}