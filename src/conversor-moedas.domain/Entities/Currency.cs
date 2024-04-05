using conversor_moedas.domain.Shared;

namespace conversor_moedas.domain.Entities
{
    public class Currency : Entity 
    {
        public Currency(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
