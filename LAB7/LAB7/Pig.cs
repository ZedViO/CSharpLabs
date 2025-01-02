namespace LAB7
{
    [Comment("Реализация производного класса \"Pig\"")]
    public class Pig : Animal
    {
        public Pig() : base("", false, "", "") { }
        public Pig(string country, bool hidefromanimals, string name, string whatanimal) : base(country, hidefromanimals, name, whatanimal) { }

        public override eFavoriteFood GetFavoriteFood()
        {
            return eFavoriteFood.Everything;
        }
        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Omnivores;
        }
        public override void SayHello()
        {
            Console.WriteLine("Oui");
        }

    }
}
