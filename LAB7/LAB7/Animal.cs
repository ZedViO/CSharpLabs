using System.Xml.Serialization;

namespace LAB7
{
    [Comment("Реализация абстрактного класса \"Animal\"")]
    [XmlInclude(typeof(Cow))]
    [XmlInclude(typeof(Pig))]
    [XmlInclude(typeof(Lion))]
    public abstract class Animal
    {
        //Animal info definition
        public string Country { get; set; }
        public bool HideFromOtherAnimals { get; set; }
        public string Name { get; set; }
        public string WhatAnimal { get; set; }

        //Animal methods definition
        public Animal(string country, bool hidefromanimals, string name, string whatanimal) //Constructor
        {
            Country = country;
            HideFromOtherAnimals = hidefromanimals;
            Name = name;
            WhatAnimal = whatanimal;
        }
        public abstract eClassificationAnimal GetClassificationAnimal();
        public abstract eFavoriteFood GetFavoriteFood();
        public abstract void SayHello();
    }
}
