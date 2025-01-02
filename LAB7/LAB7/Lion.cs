namespace LAB7
{
    [Comment("Реализация производного класса \"Lion\"")]
    public class Lion : Animal
    {
        public Lion() : base("", false, "", "") { }
        public Lion(string country, bool hidefromanimals, string name, string whatanimal) : base(country, hidefromanimals, name, whatanimal) { }
        public override eFavoriteFood GetFavoriteFood()
        {
            return eFavoriteFood.Meat;
        }
        public override eClassificationAnimal GetClassificationAnimal()
        {
            return eClassificationAnimal.Carnivores;
        }
        public override void SayHello()
        {
            Console.WriteLine("Arrrr");
        }
    }
}
