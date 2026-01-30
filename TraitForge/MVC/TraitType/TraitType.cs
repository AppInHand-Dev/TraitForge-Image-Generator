using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraitForge.MVC.TraitType
{
    public class TraitType
    {
        public int Id { get; set; }
        public String Name {  get; set; }
        public int Order { get; set; }
        public bool Active { get; set; }
        public int CollectionId { get; set; }
        public List<TraitForge.MVC.Trait.Trait> Traits { get; set; }

        public TraitType()
        {
            
        }

        public TraitType(int id, String name, int order, bool active, int collectionId)
        {
            Id = id;
            Name = name;
            Order = order;
            Active = active;
            CollectionId = collectionId;
        }

    }
}
