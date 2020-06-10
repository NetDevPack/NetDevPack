using NetDevPack.Domain;

namespace NetDevPack.Tests.Specs
{
    public class Director : Entity
    {
        public string Name { get; }

        public Director(string name)
        {
            Name = name;
        }
    }
}