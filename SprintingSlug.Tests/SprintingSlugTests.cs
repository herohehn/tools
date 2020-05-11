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
        public void GetAdjective_NotFound()
        {
            // Empty the wordlist to be sure that nothing is found
            sprintingSlug.adjectives.Clear();

            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('a'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('X'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('x'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('Z'));

            // Weird but allowed
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('ä'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('Ü'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('ö'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('Ó'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('ô'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('Ô'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('ŏ'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('ő'));
            Assert.AreEqual(sprintingSlug.nothingFound, sprintingSlug.GetAdjective('ø'));
        }

        [TestMethod]
        public void GetAdjective_NoLetter()
        {
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('_'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective(' '));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('-'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('%'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('.'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('×'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('¿'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('/'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('|'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('1'));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('='));
            Assert.AreEqual(sprintingSlug.unallowedInput, sprintingSlug.GetAdjective('#'));
        }

        [TestMethod]
        public void GetAnimal_NonEmptyString()
        {
            // Returns a string that has more than one character
            var result = sprintingSlug.GetAnimal('o');
            Assert.IsTrue(result is string);
            Assert.IsFalse(string.IsNullOrEmpty(result));
            Assert.IsTrue(result.Length > 1);
        }

        [TestMethod]
        public void GetAnimal_LeadingCharMatches()
        {
            // Leading char matches given parameter (in uppercase)
            Assert.AreEqual('A', sprintingSlug.GetAnimal('a').ToCharArray()[0]);
            Assert.AreEqual('A', sprintingSlug.GetAnimal('A').ToCharArray()[0]);

            Assert.AreEqual('H', sprintingSlug.GetAnimal('H').ToCharArray()[0]);
            Assert.AreEqual('N', sprintingSlug.GetAnimal('n').ToCharArray()[0]);

            Assert.AreEqual('Z', sprintingSlug.GetAnimal('z').ToCharArray()[0]);
            Assert.AreEqual('Z', sprintingSlug.GetAnimal('Z').ToCharArray()[0]);
        }
    }
}