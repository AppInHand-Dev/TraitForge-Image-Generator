namespace TraitForge.MVC.Collection
{
    public class Collection
    {
        public int Id { get; set; }
        public String Name {  get; set; }
        public String Description { get; set; }

        public Collection(String name, String description)
        {
            Name = name;
            Description = description;
        }

        public Collection(int id, String name, String description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

    }
}
