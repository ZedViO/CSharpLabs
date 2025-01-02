namespace LAB7
{
    [Comment("Реализация производного класса \"Cow\"")]
    public class Cow : Animal
    {
        public Cow() : base("", false, "", "") { }
        public Cow(string country, bool hidefromanimals, string name, string whatanimal) : base(country, hidefromanimals, name, whatanimal) { }
        public override eFavoriteFood GetFavoriteFood()
        {
            return eFavoriteFood.Plants;
        }

        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Herbivores;
        }
        public override void SayHello()
        {
            Console.WriteLine("Moo");
        }
    }
}
