using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satellite.Classes
{
    public class Post
    {
        public string ID { get; set; }
        public string Header { get; set; }
        public string Author { get; set; }
        public string Point { get; set; }
        public DateTime Time { get; set; }
        public int Number { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
    public class Publisher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
    