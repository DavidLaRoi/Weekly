using Weekly.API.Test.Fakers;

namespace Weekly.API.Test
{
    public static class Faking
    {
        public readonly static GroupFaker GroupFaker = new GroupFaker();

        //public static T One<T>(this Bogus.Faker<T> faker) => faker.Generate(0)
    }
}
