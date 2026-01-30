namespace TraitForge.MVC.Trait
{
    public class Trait
    {
        public int Id { get; set; }
        public String Name {  get; set; }
        public int Raw { get; set; }
        public bool Active { get; set; }
        public int TraitTypeId { get; set; }

        public Trait()
        {
            
        }

        public Trait(int id, String name, int raw, bool active, int traitTypeId)
        {
            Id = id;
            Name = name;
            Raw = raw;
            Active = active;
            TraitTypeId = traitTypeId;
        }

    }
}
